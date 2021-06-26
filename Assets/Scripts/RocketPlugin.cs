
using System;
using UnityEngine;

public class RocketPlugin : CanonPlugin
{
	private void Start()
	{
	}

	public override void SetVisible(bool visible)
	{
		if (visible)
		{
			this._time = 0.84f;
		}
		base.SetVisible(visible);
	}

	private void Update()
	{
		if (base.gameObject.activeInHierarchy)
		{
			this._time += Time.deltaTime;
			if (this._time >= this._timeSpawn)
			{
				this._time = 0f;
				this.SpawnRocket(this.RocketLEmitter.position);
				this.SpawnRocket(this.RocketREmitter.position);
			}
		}
	}

	public void SpawnRocket(Vector2 position)
	{
		RocketBullet component = TuNDPool.Spawn(this.RocketBullet, this.Canon.PlayController.FrameGame.transform).GetComponent<RocketBullet>();
		component.transform.localScale = Vector3.one * this.Canon.transform.localScale.x;
		component.transform.position = position;
		component.SetInfo(position, this.Canon.PlayController.FrameGame.VertExtent, this.Canon.FirePower * this.Canon.MaxSpeed);
	}

	public Canon Canon;

	public Transform RocketLEmitter;

	public Transform RocketREmitter;

	public GameObject RocketBullet;

	private float _timeSpawn = 1.67f;

	private float _time = 0.84f;
}
