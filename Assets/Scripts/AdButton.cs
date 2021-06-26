
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AdButton : MonoBehaviour
{
	private void Start()
	{
		this.Load.transform.DORotate(new Vector3(0f, 0f, -360f), 2f, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
	}

	private void Update()
    {
        //	if (GameController.AdsController.IsReady())
        //	{
        //		this.Load.gameObject.SetActive(false);
        //		this.Icon.gameObject.SetActive(true);
        //		this.Button.enabled = true;
        //	}
        //	else
        //	{
        //		this.Load.gameObject.SetActive(true);
        //		this.Icon.gameObject.SetActive(false);
        //		this.Button.enabled = false;
        //	}
    }

	public Button Button;

	public Image Load;

	public Image Icon;
}
