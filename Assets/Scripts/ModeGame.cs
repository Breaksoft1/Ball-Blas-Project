
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ModeGame : MonoBehaviour
{
	private void Start()
	{
		this.Button.onClick.AddListener(delegate()
		{
			if (this.PlayController.SurvivalMode)
			{
				this.PlayController.SurvivalMode = false;
				this.SetModeGame(1);
			}
			else if (Preference.Instance.DataGame.CurrentLevel < 9)
			{
				this.SetModeGame(2);
				if (this._sequence != null)
				{
					this._sequence.Kill(false);
				}
				this._sequence = DOTween.Sequence().AppendInterval(1f).AppendCallback(delegate
				{
					this.SetModeGame(1);
				});
			}
			else
			{
				this.PlayController.SurvivalMode = true;
				Preference.Instance.DataGame.FirstChangeMode = false;
				this.CheckFocus();
				this.SetModeGame(3);
			}
		});
	}

	public void CheckFocus()
	{
		if (this._tweenFade1 != null)
		{
			this._tweenFade1.Kill(false);
		}
		if (this._tweenFade2 != null)
		{
			this._tweenFade2.Kill(false);
		}
		this.NextMode.color = Color.white;
		this.PreMode.color = Color.white;
		if (Preference.Instance.DataGame.FirstChangeMode && Preference.Instance.DataGame.CurrentLevel >= 9)
		{
			this._tweenFade1 = this.NextMode.DOFade(0.1f, 0.5f).SetLoops(-1, LoopType.Yoyo);
			this._tweenFade2 = this.PreMode.DOFade(0.1f, 0.5f).SetLoops(-1, LoopType.Yoyo);
		}
	}

	private void SetModeGame(int type)
	{
		if (this._tweenCha != null)
		{
			this._tweenCha.Kill(false);
		}
		if (this._tweenSur != null)
		{
			this._tweenSur.Kill(false);
		}
		if (this._tweenDis != null)
		{
			this._tweenDis.Kill(false);
		}
		float duration = 0.2f;
		if (type == 1)
		{
			this.GoChalenger.gameObject.SetActive(true);
			this._tweenCha = this.GoChalenger.DOLocalMoveX(0f, duration, false);
			this._tweenSur = this.GoSurvival.DOLocalMoveX(this.RectMask.rect.width / 2f + this.GoSurvival.rect.width / 2f, duration, false).OnComplete(delegate
			{
				this.GoSurvival.gameObject.SetActive(false);
			});
			this._tweenDis = this.GoSurvivalDisable.DOLocalMoveX(this.RectMask.rect.width / 2f + this.GoSurvivalDisable.rect.width / 2f, duration, false).OnComplete(delegate
			{
				this.GoSurvivalDisable.gameObject.SetActive(false);
			});
		}
		else if (type == 2)
		{
			this.GoSurvivalDisable.gameObject.SetActive(true);
			this.GoSurvival.gameObject.SetActive(false);
			this._tweenDis = this.GoSurvivalDisable.DOLocalMoveX(0f, duration, false);
			this._tweenCha = this.GoChalenger.DOLocalMoveX(-this.RectMask.rect.width / 2f - this.GoChalenger.rect.width / 2f, duration, false).OnComplete(delegate
			{
				this.GoChalenger.gameObject.SetActive(false);
			});
		}
		else
		{
			this.GoSurvival.gameObject.SetActive(true);
			this.GoSurvivalDisable.gameObject.SetActive(false);
			this._tweenSur = this.GoSurvival.DOLocalMoveX(0f, duration, false);
			this._tweenCha = this.GoChalenger.DOLocalMoveX(-this.RectMask.rect.width / 2f - this.GoSurvival.rect.width / 2f, duration, false).OnComplete(delegate
			{
				this.GoChalenger.gameObject.SetActive(false);
			});
		}
		this.PlayController.MenuUI.TextLevel.transform.parent.gameObject.SetActive(!this.PlayController.SurvivalMode);
		this.PlayController.MenuUI.TextHighScore.transform.parent.gameObject.SetActive(this.PlayController.SurvivalMode);
	}

	private void Update()
	{
	}

	public PlayController PlayController;

	public Button Button;

	public RectTransform GoChalenger;

	public RectTransform GoSurvival;

	public RectTransform GoSurvivalDisable;

	public RectTransform RectMask;

	public Text TextLevel;

	public Text TextHighScore;

	public Image NextMode;

	public Image PreMode;

	private Sequence _sequence;

	private Tween _tweenCha;

	private Tween _tweenSur;

	private Tween _tweenDis;

	private Tween _tweenFade1;

	private Tween _tweenFade2;
}
