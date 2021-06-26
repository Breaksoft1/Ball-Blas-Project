
using System;
using DG.Tweening;
using UnityEngine;

public class ExplosiveEffect : MonoBehaviour
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
		base.gameObject.transform.position = ball.transform.position;
		Color color = GameColor.GAME_COLOR[UnityEngine.Random.Range(0, GameColor.GAME_COLOR.Length)];
		this.Circle1.color = color;
		this.Circle2.color = color;
		this.Circle3.color = color;
		float num = 0.6f;
		this.Circle1.transform.DOScale(0.8f, num).SetEase(Ease.OutQuad);
		this.Circle1.DOFade(0f, num - 0.2f).SetEase(Ease.InQuad).OnComplete(delegate
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}).SetDelay(0.2f);
		this.Circle2.transform.DOScale(1.1f, num - 0.1f).SetEase(Ease.OutQuad);
		this.Circle2.DOFade(0f, num - 0.1f - 0.2f).SetEase(Ease.InQuad).SetDelay(0.2f);
		this.Circle3.transform.DOScale(1.4f, num - 0.2f).SetEase(Ease.OutQuad);
		this.Circle3.DOFade(0f, num - 0.2f - 0.2f).SetEase(Ease.InQuad).SetDelay(0.2f);
		if (ball.Size == 3)
		{
			this.Circle3.gameObject.SetActive(false);
		}
		for (int i = 0; i < 10; i++)
		{
			SpriteRenderer component = UnityEngine.Object.Instantiate<GameObject>(this.Line, base.transform).GetComponent<SpriteRenderer>();
			component.transform.localScale = new Vector2(1f, UnityEngine.Random.Range(0.5f, 1f));
			component.gameObject.SetActive(true);
			component.color = color;
			int num2 = UnityEngine.Random.Range(0, 360);
			component.transform.eulerAngles = new Vector3(0f, 0f, (float)num2);
			float num3 = UnityEngine.Random.Range(this.Circle2.sprite.bounds.size.x / 2f, this.Circle2.sprite.bounds.size.x);
			float duration = UnityEngine.Random.Range(num - 0.2f, num);
			component.transform.DOLocalMove(new Vector2(num3 * Mathf.Cos(0.0174532924f * (float)(num2 - 90)), num3 * Mathf.Sin(0.0174532924f * (float)(num2 - 90))), duration, false).SetEase(Ease.OutQuad);
			component.DOFade(0f, duration).SetEase(Ease.InQuad);
		}
		for (int j = 0; j < 5; j++)
		{
			SpriteRenderer component2 = UnityEngine.Object.Instantiate<GameObject>(this.BallBlur, base.transform).GetComponent<SpriteRenderer>();
			component2.gameObject.SetActive(true);
			float duration2 = UnityEngine.Random.Range(num - 0.2f, num);
			float num4 = UnityEngine.Random.Range(this.Circle2.sprite.bounds.size.x / 5f, this.Circle2.sprite.bounds.size.x / 4f);
			float f = (float)UnityEngine.Random.Range(0, 360) * 0.0174532924f;
			component2.transform.DOLocalMove(new Vector2(num4 * Mathf.Cos(f), num4 * Mathf.Sin(f)), duration2, false).SetEase(Ease.OutQuad);
			component2.DOFade(0f, duration2).SetEase(Ease.InQuad);
		}
	}

	public SpriteRenderer Circle1;

	public SpriteRenderer Circle2;

	public SpriteRenderer Circle3;

	public GameObject Line;

	public GameObject BallBlur;
}
