
using System;
using DG.Tweening;
using UnityEngine;

public class LazeLine : MonoBehaviour
{
	private void Awake()
	{
	}

	private void Update()
	{
		RaycastHit2D hit = Physics2D.Raycast(this.Hand.transform.position, base.transform.up, 20f, 1 << LayerMask.NameToLayer("Ball"));
		if (hit)
		{
			this.LaserHit.transform.position = hit.point;
		}
		else
		{
			this.LaserHit.transform.localPosition = new Vector3(0f, 20f, 0f);
		}
		this.LineRenderer.positionCount = 2;
		this.LineRenderer.SetPosition(0, this.Hand.transform.position);
		this.LineRenderer.SetPosition(1, this.LaserHit.transform.position);
	}

	public void TweenLaze(int t)
	{
		float num = 1f;
		DOTween.Sequence().AppendInterval(num / 2f).Append(base.transform.parent.DORotate(new Vector3(0f, 0f, (float)(-30 * t)), num / 2f, RotateMode.Fast)).Append(base.transform.parent.DORotate(new Vector3(0f, 0f, (float)(30 * t)), num, RotateMode.Fast)).Append(base.transform.parent.DORotate(new Vector3(0f, 0f, 0f), num / 2f, RotateMode.Fast)).SetLoops(-1);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag.Equals("ball") && base.gameObject.activeInHierarchy)
		{
			Ball component = other.gameObject.GetComponent<Ball>();
			if (component.OnFrameGame)
			{
				component.Bleed(this.Canon.FirePower / 2f);
			}
		}
	}

	public Canon Canon;

	public LineRenderer LineRenderer;

	public GameObject LaserHit;

	public GameObject Hand;
}
