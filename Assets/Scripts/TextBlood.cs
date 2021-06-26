
using System;
using DG.Tweening;
using UnityEngine;

public class TextBlood : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	public void StartEffect(int value, Vector3 position, float scale)
	{
		this.TextMesh.text = string.Empty + value;
		float duration = UnityEngine.Random.Range(0.4f, 0.6f);
		base.transform.position = new Vector2(position.x + UnityEngine.Random.Range(-0.2f, 0.2f), position.y);
		base.transform.DOMoveY(position.y + 1.8f, duration, false).OnComplete(delegate
		{
			UnityEngine.Object.Destroy(base.gameObject);
		});
		base.transform.localScale = base.transform.localScale * scale;
	}

	public TextMesh TextMesh;
}
