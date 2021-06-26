
using System;
using UnityEngine;

public class SpriteSheetEff : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		this._time += Time.deltaTime;
		if (this._time >= 0.05f)
		{
			this._time = 0f;
			this._currentSprite++;
			if (this._currentSprite >= this.Sprites.Length)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
			else
			{
				this.SpriteRenderer.sprite = this.Sprites[this._currentSprite];
			}
		}
	}

	public SpriteRenderer SpriteRenderer;

	public Sprite[] Sprites;

	private int _currentSprite;

	private float _time;
}
