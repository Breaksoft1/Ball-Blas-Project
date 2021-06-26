
using System;
using UnityEngine;

public class BulletUI : MonoBehaviour
{
	private void Start()
	{
	}

	private void FixedUpdate()
	{
		if (base.gameObject.activeInHierarchy)
		{
			base.transform.localPosition += this._bulletVelocity * Time.fixedDeltaTime;
			if (base.transform.localPosition.y > 300f)
			{
				TuNDPool.Despawn(base.gameObject);
			}
		}
	}

	private Vector3 _bulletVelocity = new Vector2(0f, 800f);
}
