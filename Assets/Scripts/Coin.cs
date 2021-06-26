
using System;
using UnityEngine;

public class Coin : BaseController
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	public void InitCoin(int value, Vector3 position, float size, FrameGame frameGame)
	{
		base.transform.position = position;
		this._coinValue = value;
		this._size = size;
		base.transform.localScale = size * Vector3.one / this.CircleCollider2D.radius;
		this.Rigidbody2D.AddForce(new Vector2((float)UnityEngine.Random.Range(-200, 200), (float)UnityEngine.Random.Range(0, 200)));
		this._frameGame = frameGame;
		this.SpriteRenderer.sprite = Resources.Load<Sprite>("Images/GamePlay/" + this._sprite[UnityEngine.Random.Range(0, this._sprite.Length)]);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag.Equals("canon"))
		{
			Preference.Instance.DataGame.Coin += this._coinValue;
			this._frameGame.PlayController.CurrentCoin += this._coinValue;
			TextCoin component = BaseController.InstantiatePrefab("Prefabs/Effect/TextCoin").GetComponent<TextCoin>();
			component.transform.SetParent(base.transform.parent);
			component.StartEffect(this._coinValue, base.transform.position, this._size, base.transform.localScale.x * 1f);
			UnityEngine.Object.Destroy(base.gameObject);
			this._frameGame.Coins.Remove(this);
			GameController.AudioController.PlayOneShot("Audios/Effect/coin_collect");
		}
	}

	public SpriteRenderer SpriteRenderer;

	public CircleCollider2D CircleCollider2D;

	public Rigidbody2D Rigidbody2D;

	private int _coinValue;

	private float _size;

	private FrameGame _frameGame;

	private string[] _sprite = new string[]
	{
		"coin1",
		"coin2",
		"coin3",
		"coin4"
	};
}
