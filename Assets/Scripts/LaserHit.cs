
using System;
using DG.Tweening;
using UnityEngine;

public class LaserHit : MonoBehaviour
{
	private void Start()
	{
		this.SpriteRenderer.DOFade(0f, 0.1f).SetLoops(-1, LoopType.Yoyo);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag.Equals("ball") && base.gameObject.activeInHierarchy)
		{
			Ball component = other.gameObject.GetComponent<Ball>();
			if (component.OnFrameGame)
			{
				component.Bleed(this.Canon.FirePower * 2f);
			}
		}
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag.Equals("ball") && base.gameObject.activeInHierarchy)
		{
			if (this._timeStay >= 0.4f)
			{
				this._timeStay = 0f;
				Ball component = other.gameObject.GetComponent<Ball>();
				if (component.OnFrameGame)
				{
					component.Bleed(this.Canon.FirePower * 2f);
				}
			}
		}
		else if (other.gameObject.tag.Equals("bosshit") && base.gameObject.activeInHierarchy && this._timeStay >= 0.4f)
		{
			this._timeStay = 0f;
			Boss boss = other.gameObject.GetComponent<BossHit>().Boss;
			boss.Bleed(this.Canon.FirePower * 2f);
		}
	}

	private void Update()
	{
		if (base.gameObject.activeInHierarchy)
		{
			this._timeStay += Time.deltaTime;
		}
	}

	public SpriteRenderer SpriteRenderer;

	private float _timeStay;

	public Canon Canon;
}
