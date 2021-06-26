
using System;
using UnityEngine;

public class CanonPlugin : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	public virtual void SetVisible(bool visible)
	{
		base.gameObject.SetActive(visible);
	}
}
