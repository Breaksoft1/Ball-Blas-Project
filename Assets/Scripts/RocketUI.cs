
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RocketUI : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		if (base.gameObject.activeInHierarchy)
		{
			base.transform.localPosition += this._velocity * Time.deltaTime;
			if (base.transform.localPosition.y > 300f)
			{
				TuNDPool.Despawn(base.gameObject);
			}
			this._time += Time.deltaTime;
			if (this._time >= 0.05f)
			{
				this._time = 0f;
				this.SpawnDust();
			}
		}
	}

	private void SpawnDust()
	{
		float timeLife = UnityEngine.Random.Range(0.4f, 0.5f);
		Image dust = TuNDPool.Spawn((UnityEngine.Random.Range(0, 2) != 1) ? this.Dust2 : this.Dust1, base.transform.parent).GetComponent<Image>();
		dust.transform.position = base.transform.position;
		dust.transform.localPosition += new Vector3(UnityEngine.Random.Range(-5f, 5f), -20f, 0f);
		dust.transform.localScale = Vector3.one * UnityEngine.Random.Range(0.8f, 1f);
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

	private Vector3 _velocity = new Vector2(0f, 400f);

	public GameObject Dust1;

	public GameObject Dust2;

	private float _time;
}
