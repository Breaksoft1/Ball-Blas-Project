
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SlideToShoot : MonoBehaviour
{
	private void Start()
	{
		this._handPos = this.ImageHand.rectTransform.localPosition;
		this._sw1Pos = this.ImageSwoosh1.rectTransform.localPosition;
		this._sw2Pos = this.ImageSwoosh2.rectTransform.localPosition;
		this._sw3Pos = this.ImageSwoosh3.rectTransform.localPosition;
		this.SwipeHand();
	}

	private void Update()
	{
	}

	private void SwipeHand()
	{
		float w = 300f;
		this.ImageHand.rectTransform.localPosition = new Vector2(this._handPos.x - w, this._handPos.y);
		this.ImageHand.color = new Color(1f, 1f, 1f, 0f);
		DOTween.Sequence().AppendCallback(delegate
		{
			this.ImageHand.rectTransform.localPosition = new Vector2(this._handPos.x - w, this._handPos.y);
			this.ImageHand.color = new Color(1f, 1f, 1f, 0f);
			this.ImageHand.DOFade(1f, 0.1f);
			this.ImageSwoosh1.rectTransform.localPosition = new Vector2(this._sw1Pos.x - w, this._sw1Pos.y);
			this.ImageSwoosh1.color = new Color(1f, 1f, 1f, 0f);
			this.ImageSwoosh1.DOFade(0.8f, 0.1f);
			this.ImageSwoosh2.rectTransform.localPosition = new Vector2(this._sw2Pos.x - w, this._sw2Pos.y);
			this.ImageSwoosh2.color = new Color(1f, 1f, 1f, 0f);
			this.ImageSwoosh2.DOFade(0.8f, 0.1f);
			this.ImageSwoosh3.rectTransform.localPosition = new Vector2(this._sw3Pos.x - w, this._sw3Pos.y);
			this.ImageSwoosh3.color = new Color(1f, 1f, 1f, 0f);
			this.ImageSwoosh3.DOFade(0.8f, 0.1f);
		}).AppendInterval(0.1f).AppendCallback(delegate
		{
			this.ImageHand.rectTransform.DOLocalMoveX(this._handPos.x, 1f, false).SetEase(Ease.OutQuad).OnComplete(delegate
			{
				this.ImageHand.DOFade(0f, 0.2f).SetDelay(0.3f);
			});
			this.ImageSwoosh1.rectTransform.DOLocalMoveX(this._sw1Pos.x, 1f, false).SetDelay(0.1f).SetEase(Ease.OutQuad).OnComplete(delegate
			{
				this.ImageSwoosh1.DOFade(0f, 0.2f).SetDelay(0.2f);
			});
			this.ImageSwoosh2.rectTransform.DOLocalMoveX(this._sw2Pos.x, 1f, false).SetDelay(0.2f).SetEase(Ease.OutQuad).OnComplete(delegate
			{
				this.ImageSwoosh2.DOFade(0f, 0.2f).SetDelay(0.1f);
			});
			this.ImageSwoosh3.rectTransform.DOLocalMoveX(this._sw3Pos.x, 1f, false).SetDelay(0.3f).SetEase(Ease.OutQuad).OnComplete(delegate
			{
				this.ImageSwoosh3.DOFade(0f, 0.2f).SetDelay(0f);
			});
		}).AppendInterval(2f).OnComplete(delegate
		{
			this.SwipeHand();
		});
	}

	public Image ImageBar;

	public Image ImageHand;

	public Image ImageSwoosh1;

	public Image ImageSwoosh2;

	public Image ImageSwoosh3;

	private Vector3 _handPos;

	private Vector3 _sw1Pos;

	private Vector3 _sw2Pos;

	private Vector3 _sw3Pos;
}
