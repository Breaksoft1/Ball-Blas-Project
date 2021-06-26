
using System;
using DG.Tweening;
using UnityEngine;

public class BloodEffect : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	public void StartEffect(Ball ball)
	{
		base.transform.position = new Vector2(ball.transform.position.x, ball.transform.position.y - ball.SpriteRenderer.bounds.size.y / 4f);
		this.SpriteRenderer.color = ball.SpriteRenderer.color;
		base.transform.localScale = Vector3.one * UnityEngine.Random.Range(0.05f, 0.3f);
		float duration = UnityEngine.Random.Range(0.2f, 0.3f);
		base.transform.DOLocalMove(new Vector2(ball.transform.position.x + UnityEngine.Random.Range(-ball.SpriteRenderer.bounds.size.x / 2f, ball.SpriteRenderer.bounds.size.x / 2f), ball.transform.position.y - ball.SpriteRenderer.bounds.size.y / 2f - UnityEngine.Random.Range(this.SpriteRenderer.sprite.bounds.size.y / 2f, this.SpriteRenderer.sprite.bounds.size.y)), duration, false).SetEase(Ease.OutQuad).OnComplete(delegate
		{
			UnityEngine.Object.Destroy(base.gameObject);
		});
	}

	public void StartEffect(BossHit bossHit)
	{
		base.transform.position = new Vector2(bossHit.transform.position.x, bossHit.transform.position.y - bossHit.CircleCollider2D.bounds.size.y / 4f);
		this.SpriteRenderer.color = bossHit.BloodColor;
		base.transform.localScale = Vector3.one * UnityEngine.Random.Range(0.05f, 0.3f);
		float duration = UnityEngine.Random.Range(0.2f, 0.3f);
		base.transform.DOLocalMove(new Vector2(bossHit.transform.position.x + UnityEngine.Random.Range(-bossHit.CircleCollider2D.bounds.size.x / 2f, bossHit.CircleCollider2D.bounds.size.x / 2f), bossHit.transform.position.y - bossHit.CircleCollider2D.bounds.size.y / 2f - UnityEngine.Random.Range(this.SpriteRenderer.sprite.bounds.size.y / 2f, this.SpriteRenderer.sprite.bounds.size.y)), duration, false).SetEase(Ease.OutQuad).OnComplete(delegate
		{
			UnityEngine.Object.Destroy(base.gameObject);
		});
	}

	public SpriteRenderer SpriteRenderer;
}
