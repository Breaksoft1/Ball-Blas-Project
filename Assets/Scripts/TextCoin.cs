
using System;
using DG.Tweening;
using UnityEngine;

public class TextCoin : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	public void StartEffect(int value, Vector3 position, float size, float scale)
	{
		this.TextMesh.text = "$" + value;
		float duration = UnityEngine.Random.Range(0.4f, 0.6f);
		base.transform.position = new Vector2(position.x + size, position.y);
		base.transform.DOMoveX(position.x - size, duration, false).SetLoops(-1, LoopType.Yoyo).OnStepComplete(delegate
		{
			this._red = !this._red;
			this.TextMesh.color = ((!this._red) ? Color.white : Color.red);
		});
		base.transform.localScale = base.transform.localScale * scale;
		base.transform.DOMoveY(position.y + size * 8f, UnityEngine.Random.Range(1.5f, 2f), false).OnComplete(delegate
		{
			UnityEngine.Object.Destroy(base.gameObject);
		});
	}

	public TextMesh TextMesh;

	private bool _red;
}
