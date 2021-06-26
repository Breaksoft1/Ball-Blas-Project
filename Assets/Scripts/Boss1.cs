
using System;
using System.Collections;
using DG.Tweening;
using Spine;
using Spine.Unity;
using UnityEngine;

public class Boss1 : Boss
{
	public override void Start()
	{
		base.Start();
		for (int i = 0; i < this.SkeletonAnimation.skeleton.slots.Count; i++)
		{
			if (this.SkeletonAnimation.skeleton.slots.Items[i].ToString().Equals("m2_eye"))
			{
				this._slotEye = this.SkeletonAnimation.skeleton.slots.Items[i];
				break;
			}
		}
	}

	public override void Update()
	{
		base.Update();
		this.EyeGlow.transform.position = this._slotEye.bone.GetWorldPosition(this.SkeletonAnimation.transform);
		this.EyeGlow.transform.localScale = new Vector2(this._slotEye.bone.scaleX, this._slotEye.bone.scaleY);
	}

	public override void Fire()
	{
		base.Fire();
		base.StartCoroutine(this._Fire());
	}

	private IEnumerator _Fire()
	{
		yield return new WaitForSeconds(UnityEngine.Random.Range(0.3f, 0.7f));
		BombBoss bomb = TuNDPool.Spawn(this.BoomBossPrefab, GameController.ScreenManager.PlayController.FrameGame.transform).GetComponent<BombBoss>();
		bomb.transform.position = this.BombEmitter.position;
		bomb.transform.localScale = Vector3.zero;
		bomb.transform.DOScale(Vector3.one * GameController.ScreenManager.PlayController.Canon._canonScale, 0.3f);
		GameController.ScreenManager.PlayController.FrameGame.BombBosss.Add(bomb);
		yield break;
	}

	public override void StartMove()
	{
		base.StartMove();
		this.Rigidbody2D.AddForce(new Vector2(130f, 0f));
	}

	public override void HitEffect()
	{
		base.HitEffect();
		if (this._tween != null)
		{
			this._tween.Kill(false);
		}
		this.EyeGlow.DOFade(1f, 0.1f).OnComplete(delegate
		{
			this.EyeGlow.DOFade(0f, 0.1f);
		});
	}

	public GameObject BoomBossPrefab;

	public Transform BombEmitter;

	public SpriteRenderer EyeGlow;

	private Slot _slotEye;

	private Tween _tween;
}
