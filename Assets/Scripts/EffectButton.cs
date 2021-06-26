
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class EffectButton : MonoBehaviour
{
	private void Awake()
	{
		this.Image = base.GetComponent<Image>();
	}

	public void StartEff(Image parent)
	{
		if (parent == null)
		{
			parent = base.GetComponent<Image>();
		}
		this.Image.color = new Color(1f, 1f, 1f, 0f);
		this.Image.rectTransform.sizeDelta = parent.rectTransform.sizeDelta + Vector2.one * 10f;
		this.Image.rectTransform.DOSizeDelta(this.Image.rectTransform.sizeDelta + Vector2.one * 70f, 0.5f, false).OnComplete(delegate
		{
			UnityEngine.Object.Destroy(base.gameObject);
		});
		Sequence s = DOTween.Sequence();
		s.Append(this.Image.DOFade(1f, 0.1f)).Append(this.Image.DOFade(0f, 0.4f));
	}

	private Image Image;
}
