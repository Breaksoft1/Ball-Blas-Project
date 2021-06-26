
using System;
using DG.Tweening;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

public class CanonItem : MonoBehaviour
{
	private void Start()
	{
	}

	public void TweenLaze(RectTransform hand, int t)
	{
		float num = 1f;
		DOTween.Sequence().AppendInterval(num / 2f).Append(hand.DORotate(new Vector3(0f, 0f, (float)(-30 * t)), num / 2f, RotateMode.Fast)).Append(hand.DORotate(new Vector3(0f, 0f, (float)(30 * t)), num, RotateMode.Fast)).Append(hand.DORotate(new Vector3(0f, 0f, 0f), num / 2f, RotateMode.Fast)).SetLoops(-1);
	}

	private void Update()
	{
		if (this._isFocus && base.gameObject.activeInHierarchy)
		{
			this._timeSpawn += Time.fixedDeltaTime;
			if (this._timeSpawn >= 1f / Mathf.Min(this._fireSpeed, this._maxSpeed))
			{
				this._timeSpawn -= 1f / Mathf.Min(this._fireSpeed, this._maxSpeed);
				int num = (int)Mathf.Max((this._oddSpeed + this._fireSpeed) / this._maxSpeed, 1f);
				this._oddSpeed = Mathf.Max(0f, this._oddSpeed + this._fireSpeed - this._maxSpeed * (float)num);
				this.SpawnBullet(this.BulletEmitter.transform.position, num);
			}
		}
	}

	public void SetFocus(bool isFocus)
	{
		this._isFocus = isFocus;
		if (this.CanonId == 9 && Preference.Instance.DataGame.CurrentLevel < 50 && !Preference.Instance.DataGame.CannonStatuses[this.CanonId].IsOpen)
		{
			this._isFocus = false;
			this.SkeletonGraphic.GetComponentInChildren<Text>(true).gameObject.SetActive(true);
			this.SkeletonGraphic.color = Color.black;
		}
		else if (this.CanonId == 9)
		{
			this.SkeletonGraphic.GetComponentInChildren<Text>(true).gameObject.SetActive(false);
			this.SkeletonGraphic.color = Color.white;
		}
		if (this.CanonId == 8 && Preference.Instance.DataGame.CurrentLevel < 20 && !Preference.Instance.DataGame.CannonStatuses[this.CanonId].IsOpen)
		{
			this._isFocus = false;
			this.SkeletonGraphic.GetComponentInChildren<Text>(true).gameObject.SetActive(true);
			this.SkeletonGraphic.color = Color.black;
		}
		else if (this.CanonId == 8)
		{
			this.SkeletonGraphic.GetComponentInChildren<Text>(true).gameObject.SetActive(false);
			this.SkeletonGraphic.color = Color.white;
		}
		string animationName = (!this._isFocus) ? this.IdleName : this.FireName;
		if (this.GoPlugin)
		{
			this.GoPlugin.gameObject.SetActive(this._isFocus);
		}
		try
		{
			this.SkeletonGraphic.AnimationState.SetAnimation(0, animationName, true);
		}
		catch (Exception ex)
		{
			this.SkeletonGraphic.AnimationState.ClearTracks();
		}
	}

	public void SpawnBullet(Vector3 position, int num)
	{
		for (int i = 0; i < num; i++)
		{
			Image component = TuNDPool.Spawn(this.BulletPrefab, base.transform.parent.parent.parent).GetComponent<Image>();
			component.sprite = this.SpriteBullet;
			component.transform.position = position;
			component.transform.DOLocalMoveX(component.transform.localPosition.x - (float)(num - 1) * component.rectTransform.rect.width * 5f / 4f / 2f + (float)i * component.rectTransform.rect.width * 5f / 4f, 0.13f, false);
		}
	}

	public int CanonId;

	public SkeletonGraphic SkeletonGraphic;

	public string IdleName = string.Empty;

	public string FireName = "fire_01";

	public GameObject BulletEmitter;

	public GameObject BulletPrefab;

	private float _fireSpeed = 30f;

	private float _maxSpeed = 10f;

	private float _timeSpawn;

	private float _oddSpeed;

	public GameObject GoPlugin;

	public string CanonName;

	public int PriceCoin;

	public float PriceMoney;

	public Sprite SpriteBullet;

	private bool _isFocus;
}
