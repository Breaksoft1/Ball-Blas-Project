
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Toast : Popup
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	public virtual void Show(string text)
	{
		this.Show();
		this.Text.text = text;
	}

	public override void OnShowComplete()
	{
		base.OnShowComplete();
		base.StartCoroutine(this._hide());
	}

	private IEnumerator _hide()
	{
		yield return new WaitForSeconds(0.5f);
		this.Hide();
		yield break;
	}

	public Text Text;
}
