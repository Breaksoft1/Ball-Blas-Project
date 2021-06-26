
using System;
using Spine.Unity;
using UnityEngine;

public class LazeBullet : CanonPlugin
{
	private void Start()
	{
		this.LeftLaze.TweenLaze(-1);
		this.RightLaze.TweenLaze(1);
	}

	private void Update()
	{
		this.HandLeft.gameObject.transform.position = this.Canon.LeftArm.bone.GetWorldPosition(base.transform);
		this.HandRight.gameObject.transform.position = this.Canon.RightArm.bone.GetWorldPosition(base.transform);
	}

	public Canon Canon;

	public LazeLine LeftLaze;

	public LazeLine RightLaze;

	public SpriteRenderer HandLeft;

	public SpriteRenderer HandRight;
}
