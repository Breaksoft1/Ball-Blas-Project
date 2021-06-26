
using System;
using DG.Tweening;
using UnityEngine;

public class PowerUpEffect : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	public void StartEffect(Vector3 position)
	{
		DOTween.Sequence().Append(this.SpriteRenderer.DOFade(0.5f, 0.1f)).AppendInterval(0.3f).Append(this.SpriteRenderer.DOFade(0f, 0.1f)).OnComplete(delegate
		{
			UnityEngine.Object.Destroy(base.gameObject);
		});
		base.gameObject.transform.position = new Vector2(position.x, position.y + 0.5f);
		base.gameObject.transform.DOMoveY(base.gameObject.transform.position.y + 0.5f, 0.5f, false);
	}

	public SpriteRenderer SpriteRenderer;
}
