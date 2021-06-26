
using System;
using UnityEngine;

public class EffectController : BaseController
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	public void StartPowerUpEff(Vector3 position)
	{
		UnityEngine.Object.Instantiate<GameObject>(this.PowerUp, base.transform).GetComponent<PowerUpEffect>().StartEffect(position);
	}

	public void StartTextBlood(int value, Boss boss)
	{
		UnityEngine.Object.Instantiate<GameObject>(this.TextBlood, base.transform).GetComponent<TextBlood>().StartEffect(value, boss.BossHit.transform.position, boss.transform.localScale.x);
	}

	public void StartBombEff(Vector3 position, GameObject bombEffect)
	{
		UnityEngine.Object.Instantiate<GameObject>(bombEffect, base.transform).transform.position = position;
	}

	public void StartRocketEff(Vector3 position)
	{
		GameController.AudioController.PlayOneShot("Audios/Effect/rocket");
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.RocketEffect, base.transform);
		gameObject.transform.position = position;
		gameObject.transform.localScale = Vector3.one * UnityEngine.Random.Range(0.5f, 1f);
	}

	public void StartDustEffect(Ball ball)
	{
		DustEffect component = UnityEngine.Object.Instantiate<GameObject>(this.PrefabDust, base.transform).GetComponent<DustEffect>();
		component.StartEffect(ball);
	}

	public void StartExploEffect(Ball ball)
	{
		ExplosiveEffect component = UnityEngine.Object.Instantiate<GameObject>(this.PrefabExplo, base.transform).GetComponent<ExplosiveEffect>();
		component.StartEffect(ball);
	}

	public void StartBleed(Ball ball)
	{
		float num = (float)UnityEngine.Random.Range(4, 6);
		if (this.PlayController.Canon.FireSpeed >= 10f)
		{
			num = 3f;
		}
		if (this.PlayController.Canon.FireSpeed >= 20f)
		{
			num = 2f;
		}
		if (this.PlayController.Canon.FireSpeed >= 50f)
		{
			num = 1f;
		}
		int num2 = 0;
		while ((float)num2 < num)
		{
			BloodEffect component = UnityEngine.Object.Instantiate<GameObject>(this.Blood, base.transform).GetComponent<BloodEffect>();
			component.StartEffect(ball);
			num2++;
		}
	}

	public void StartBleed(BossHit bossHit)
	{
		float num = (float)UnityEngine.Random.Range(4, 6);
		if (this.PlayController.Canon.FireSpeed >= 10f)
		{
			num = 3f;
		}
		if (this.PlayController.Canon.FireSpeed >= 20f)
		{
			num = 2f;
		}
		if (this.PlayController.Canon.FireSpeed >= 50f)
		{
			num = 1f;
		}
		int num2 = 0;
		while ((float)num2 < num)
		{
			BloodEffect component = UnityEngine.Object.Instantiate<GameObject>(this.Blood, base.transform).GetComponent<BloodEffect>();
			component.StartEffect(bossHit);
			num2++;
		}
	}

	public PlayController PlayController;

	public GameObject PrefabExplo;

	public GameObject PrefabDust;

	public GameObject Blood;

	public GameObject RocketEffect;

	public GameObject TextBlood;

	public GameObject PowerUp;
}
