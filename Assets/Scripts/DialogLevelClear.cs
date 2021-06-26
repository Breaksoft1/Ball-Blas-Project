
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DialogLevelClear : Popup
{
	private void Start()
	{
		this.ButtonContinue.onClick.AddListener(delegate()
		{
			this.Hide();
			GameController.ScreenManager.PlayController.SetBackground(UnityEngine.Random.Range(0, GameController.ScreenManager.PlayController.BackgroundPrefabs.Length));
			GameController.ScreenManager.PlayController.SetGameStatus(PlayController.Game_Status.START);
			if (!Preference.Instance.DataGame.RateUs && Preference.Instance.DataGame.CurrentLevel == 7)
			{
				Preference.Instance.DataGame.RateUs = true;
				GameController.DialogManager.DialogRateUs.Show();
			}
			else if (Preference.Instance.DataGame.CurrentLevel > 2)
			{
                //GameController.AdsController.ShowInterstitial();

            }
		});
	}

	public override void Show()
	{
		this._coinBonus = (int)(Preference.Instance.DataGame.FirePower * 50f);
		base.Show();
		this.TextLevel.text = Preference.Instance.DataGame.CurrentLevel + string.Empty;
		this.TextCoinBonus.text = this._coinBonus + string.Empty;
		RectTransform component = base.transform.parent.GetComponent<RectTransform>();
		for (int i = 0; i < 60; i++)
		{
			Image image = base.CreateImage(this.Firework, component);
			image.color = GameColor.GAME_COLOR[UnityEngine.Random.Range(0, GameColor.GAME_COLOR.Length)];
			image.transform.eulerAngles = new Vector3(0f, 0f, (float)UnityEngine.Random.Range(0, 360));
			image.transform.localScale = new Vector2(UnityEngine.Random.Range(0.5f, 1f), UnityEngine.Random.Range(0.3f, 1f));
			image.rectTransform.localPosition = new Vector2(UnityEngine.Random.Range(-component.rect.width / 2f, component.rect.width / 2f), component.rect.y + component.rect.height + 20f);
			image.gameObject.SetActive(false);
			float duration = UnityEngine.Random.Range(0.8f, 1.6f);
			float delay = UnityEngine.Random.Range(0f, 1f);
			image.transform.DORotate(new Vector3(0f, 0f, (float)UnityEngine.Random.Range(0, 360)), duration, RotateMode.Fast).SetEase(Ease.Linear).SetDelay(delay).OnStart(delegate
			{
				image.gameObject.SetActive(true);
			});
			image.transform.DOLocalMoveY(component.rect.y - 20f, duration, false).SetDelay(delay).SetEase(Ease.Linear).OnComplete(delegate
			{
				UnityEngine.Object.Destroy(image.gameObject);
			});
		}
	}

	public override void OnShowComplete()
	{
		base.OnShowComplete();
		if (this._sequence != null)
		{
			this._sequence.Kill(false);
		}
		this._sequence = DOTween.Sequence().AppendInterval(0.2f).Append(this.TextLevel.transform.DOLocalMoveX(-60f, 0.2f, false).SetEase(Ease.Linear)).AppendCallback(delegate
		{
			this.TextLevel.transform.localPosition = new Vector2(60f, this.TextLevel.transform.localPosition.y);
			this.TextLevel.text = Preference.Instance.DataGame.CurrentLevel + 1 + string.Empty;
		}).AppendInterval(0.1f).Append(this.TextLevel.transform.DOLocalMoveX(0f, 0.2f, false).SetEase(Ease.Linear));
		Preference.Instance.DataGame.Coin += this._coinBonus;
		GameController.AudioController.PlayOneShot("Audios/Effect/coin_fly");
	}

	private void Update()
	{
	}

	public Text TextLevel;

	public Sprite Firework;

	public Button ButtonContinue;

	public Text TextCoinBonus;

	public RectTransform RectTransform;

	private int _coinBonus;

	private Sequence _sequence;
}
