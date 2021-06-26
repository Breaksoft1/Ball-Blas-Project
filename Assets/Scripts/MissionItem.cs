
using System;
using UnityEngine;
using UnityEngine.UI;

public class MissionItem : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SetValue(int value, bool isDone)
	{
		if (isDone)
		{
			this.GoDone.gameObject.SetActive(false);
			this.ButtonCollect.gameObject.SetActive(false);
		}
		else
		{
			this.GoDone.gameObject.SetActive(false);
			this.ButtonCollect.gameObject.SetActive(true);
		}
		if (value >= this.NeedValue)
		{
			this.Slider.gameObject.SetActive(false);
			this.Complete.gameObject.SetActive(true);
			this.ButtonCollect.GetComponent<CanvasGroup>().alpha = 1f;
			this.ButtonCollect.GetComponent<Image>().color = Color.white;
		}
		else
		{
			this.Slider.gameObject.SetActive(true);
			this.Complete.gameObject.SetActive(false);
			this.ButtonCollect.GetComponent<CanvasGroup>().alpha = 0.6f;
			this.TextValue.text = value + "/" + this.NeedValue;
			this.Slider.value = (float)value / (float)this.NeedValue;
			this.ButtonCollect.GetComponent<Image>().color = Color.gray;
		}
	}

	public Button ButtonCollect;

	public Text TextValue;

	public Slider Slider;

	public GameObject Complete;

	public GameObject GoDone;

	public int NeedValue;
}
