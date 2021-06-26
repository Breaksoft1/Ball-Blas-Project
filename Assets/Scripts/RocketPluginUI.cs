
using System;
using UnityEngine;

public class RocketPluginUI : MonoBehaviour
{
	private void Start()
	{
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

	private void OnEnable()
	{
		this._time = 0.84f;
	}

	public void SpawnRocket(Vector2 position)
	{
		RocketUI component = TuNDPool.Spawn(this.RocketBulletUI, this.CanonItem.transform.parent.parent.parent).GetComponent<RocketUI>();
		component.transform.position = position;
	}

	public CanonItem CanonItem;

	public Transform RocketLEmitter;

	public Transform RocketREmitter;

	public GameObject RocketBulletUI;

	private float _timeSpawn = 1.67f;

	private float _time = 0.84f;
}
