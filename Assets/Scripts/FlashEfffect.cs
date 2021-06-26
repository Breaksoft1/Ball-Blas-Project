
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FlashEfffect : MonoBehaviour
{
	private void Start()
	{
		this.StartFlashEffect(this.Flash);
	}

	private void Update()
	{
	}

	private void StartFlashEffect(Image image)
	{
		image.rectTransform.localPosition = new Vector3(this.Image.rectTransform.sizeDelta.x, 0f, 0f);
		image.rectTransform.DOLocalMoveX(-this.Image.rectTransform.sizeDelta.x, this.time, false).OnComplete(delegate
		{
			this.StartFlashEffect(image);
		}).SetDelay(this.timeDelay).SetEase(Ease.Linear);
	}

	public Image Image;

	public Image Flash;

	public float time = 1.5f;

	public float timeDelay = 1.5f;
}
