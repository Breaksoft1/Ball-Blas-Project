
using System;
using DG.Tweening;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	private void Start()
	{
	}

	private void FixedUpdate()
	{
		this.BulletBehavior(Time.fixedDeltaTime);
	}

	public void BulletBehavior(float deltime)
	{
		base.transform.Translate(Vector3.up * this._velocity.y * deltime);
		if (base.transform.position.y > this._yLimit + this.BoxCollider2D.size.y)
		{
			this.Despawn();
		}
	}

	public void SetInfo(Canon canon, float yLimit)
	{
		this._velocity = canon.BulletVelocity;
		this._yLimit = yLimit;
		this._damage = canon.FirePower;
		float x = base.transform.position.x;
		base.transform.position = new Vector2(canon.BulletEmiter.position.x, base.transform.position.y);
		this._tween = base.transform.DOMoveX(x, 0.1f, false);
	}

	public float Width
	{
		get
		{
			return this.BoxCollider2D.size.x * base.transform.localScale.x * 5f / 4f;
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag.Equals("ball") && base.gameObject.activeInHierarchy)
		{
			Ball component = other.gameObject.GetComponent<Ball>();
			if (component.OnFrameGame)
			{
				this.Despawn();
				component.Bleed(this._damage);
			}
		}
		else if (other.gameObject.tag.Equals("bosshit") && base.gameObject.activeInHierarchy)
		{
			Boss boss = other.gameObject.GetComponent<BossHit>().Boss;
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

	private float _damage;

	private Vector3 _velocity;

	private float _yLimit;

	public BoxCollider2D BoxCollider2D;

	public SpriteRenderer SpriteRenderer;

	private Tween _tween;
}
