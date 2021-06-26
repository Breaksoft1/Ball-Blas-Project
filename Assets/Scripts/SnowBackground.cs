
using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SnowBackground : MonoBehaviour
{
	private void Start()
	{
		TuNDPool.Preload(this.ImageSnow.gameObject, base.transform, 50);
		base.StartCoroutine(this.StartSnow());
	}

	private IEnumerator StartSnow()
	{
		yield return new WaitForEndOfFrame();
		for (int i = 0; i < 50; i++)
		{
			this.CreateSnow(this.RectTransform.rect.y + UnityEngine.Random.Range(0f, this.RectTransform.rect.height));
		}
		yield break;
	}

	private void CreateSnow(float posY)
	{
		Image image = TuNDPool.Spawn(this.ImageSnow.gameObject, base.transform).GetComponent<Image>();
		image.transform.localPosition = new Vector2(this.RectTransform.rect.x + UnityEngine.Random.Range(0f, this.RectTransform.rect.width), posY);
		image.transform.localScale = UnityEngine.Random.Range(0.2f, 1f) * Vector2.one;
		image.color = new Color(1f, 1f, 1f, UnityEngine.Random.Range(0.5f, 1f));
		float duration = (image.transform.localPosition.y - this.RectTransform.rect.y) / (float)UnityEngine.Random.Range(100, 150);
		image.transform.DOLocalMoveY(this.RectTransform.rect.y, duration, false).OnComplete(delegate
		{
			TuNDPool.Despawn(image.gameObject);
			this.CreateSnow(this.RectTransform.rect.y + this.RectTransform.rect.height);
		});
	}

	private void Update()
	{
	}

	public Image ImageSnow;

	public RectTransform RectTransform;
}
