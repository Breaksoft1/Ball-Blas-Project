
using System;
using DG.Tweening;
using UnityEngine;

public class DustEffect : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	public void StartEffect(Ball ball)
	{
		base.gameObject.transform.localScale = Vector3.one * Mathf.Max(ball.transform.localScale.x * 1.2f, 0.4f);
		base.gameObject.transform.position = new Vector2(ball.transform.position.x, ball.transform.position.y - ball.CircleCollider2D.bounds.size.y / 2f);
		float num = 0.6f;
		for (int i = 0; i < UnityEngine.Random.Range(5, 10); i++)
		{
			SpriteRenderer component = UnityEngine.Object.Instantiate<GameObject>(this.BallBlur, base.transform).GetComponent<SpriteRenderer>();
			component.gameObject.SetActive(true);
			float duration = UnityEngine.Random.Range(num - 0.2f, num);
			float num2 = UnityEngine.Random.Range(ball.CircleCollider2D.bounds.size.x / 4f, ball.CircleCollider2D.bounds.size.x / 3f);
			float f = (float)UnityEngine.Random.Range(0, 360) * 0.0174532924f;
			component.transform.localScale = Vector2.one * UnityEngine.Random.Range(0.2f, 0.4f);
			component.transform.DOLocalMove(new Vector2(num2 * Mathf.Cos(f), num2 * Mathf.Sin(f)), duration, false).SetEase(Ease.OutQuad);
			component.DOFade(0f, duration).SetEase(Ease.InQuad);
		}
		DOTween.Sequence().AppendInterval(num).AppendCallback(delegate
		{
			UnityEngine.Object.Destroy(base.gameObject);
		});
	}

	public GameObject BallBlur;
}
