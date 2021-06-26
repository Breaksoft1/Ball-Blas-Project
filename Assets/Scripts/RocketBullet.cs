
using System;
using DG.Tweening;
using UnityEngine;

public class RocketBullet : BaseController
{
	private void Start()
	{
	}

	private void Update()
	{
		if (base.gameObject.activeInHierarchy)
		{
			base.transform.position += this._velocity * Time.deltaTime;
			if (base.transform.position.y > this._yExplo + this.BoxCollider2D.size.y)
			{
				this.Despawn();
			}
			this._time += Time.deltaTime;
			if (this._time >= 0.05f)
			{
				this._time = 0f;
				this.SpawnDust();
			}
		}
	}

	public void SetInfo(Vector2 position, float yExplo, float damage)
	{
		this._yExplo = yExplo;
		base.transform.position = position;
		this._damage = damage;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag.Equals("ball") && base.gameObject.activeInHierarchy)
		{
			Ball component = other.gameObject.GetComponent<Ball>();
			if (component.OnFrameGame)
			{
				GameController.ScreenManager.PlayController.EffectController.StartRocketEff(base.transform.position);
				this.Despawn();
				component.Bleed(this._damage);
			}
		}
		else if (other.gameObject.tag.Equals("bosshit") && base.gameObject.activeInHierarchy)
		{
			Boss boss = other.gameObject.GetComponent<BossHit>().Boss;
			GameController.ScreenManager.PlayController.EffectController.StartRocketEff(base.transform.position);
			this.Despawn();
			boss.Bleed(this._damage);
		}
	}

	public void Despawn()
	{
		TuNDPool.Despawn(base.gameObject);
		if (this._tween != null)
		{
			this._tween.Kill(false);
		}
	}

	private void SpawnDust()
	{
		float timeLife = UnityEngine.Random.Range(0.4f, 0.5f);
		SpriteRenderer dust = TuNDPool.Spawn((UnityEngine.Random.Range(0, 2) != 1) ? this.Dust2 : this.Dust1, base.transform.parent).GetComponent<SpriteRenderer>();
		dust.transform.position = base.transform.position + new Vector3(UnityEngine.Random.Range(-0.1f, 0.1f), -0.15f, 0f);
		dust.transform.localScale = Vector3.one * UnityEngine.Random.Range(0.4f, 0.5f);
		dust.transform.eulerAngles = Vector3.zero;
		dust.color = this.Color1;
		dust.transform.DOScale(0f, timeLife).OnComplete(delegate
		{
			TuNDPool.Despawn(dust.gameObject);
		});
		dust.transform.DORotate(new Vector3(0f, 0f, 180f), timeLife, RotateMode.Fast);
		dust.DOColor(this.Color2, timeLife / 2f).OnComplete(delegate
		{
			dust.DOColor(Color.white, timeLife / 2f);
		});
	}

	public Color Color1;

	public Color Color2;

	private float _damage;

	public GameObject Dust1;

	public GameObject Dust2;

	private Vector3 _velocity = new Vector2(0f, 5f);

	private float _yExplo;

	public BoxCollider2D BoxCollider2D;

	private float _time;

	private Tween _tween;
}
