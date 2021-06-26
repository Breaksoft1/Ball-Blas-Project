
using System;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogSetting : Popup
{
	private void Start()
	{
		this.ButtonClose.onClick.AddListener(new UnityAction(this.Hide));
		this.ButtonVibrate.onClick.AddListener(delegate()
		{
			Preference.Instance.DataGame.IsVibrate = !Preference.Instance.DataGame.IsVibrate;
			this.ImageXVibrate.GetComponentInChildren<Image>().gameObject.SetActive(!Preference.Instance.DataGame.IsVibrate);
		});
		this.ButtonMusic.onClick.AddListener(delegate()
		{
			Preference.Instance.DataGame.IsMusic = !Preference.Instance.DataGame.IsMusic;
			this.ImageXMusic.GetComponentInChildren<Image>().gameObject.SetActive(!Preference.Instance.DataGame.IsMusic);
		});
		this.ButtonSound.onClick.AddListener(delegate()
		{
			Preference.Instance.DataGame.IsSound = !Preference.Instance.DataGame.IsSound;
			this.ImageXSound.GetComponentInChildren<Image>().gameObject.SetActive(!Preference.Instance.DataGame.IsSound);
		});
		this.ButtonRestorePurchase.onClick.AddListener(new UnityAction(GameController.PurchaseController.RestorePurchases));
	//	this.ButtonRestorePurchase.gameObject.SetActive(false);
	}

	private void Update()
	{
	}

	public override void Show()
	{
		base.Show();
		this.ImageXVibrate.GetComponentInChildren<Image>().gameObject.SetActive(!Preference.Instance.DataGame.IsVibrate);
		this.ImageXMusic.GetComponentInChildren<Image>().gameObject.SetActive(!Preference.Instance.DataGame.IsMusic);
		this.ImageXSound.GetComponentInChildren<Image>().gameObject.SetActive(!Preference.Instance.DataGame.IsSound);
	}

	public Button ButtonClose;

	public Button ButtonVibrate;

	public Button ButtonMusic;

	public Button ButtonSound;

	public Image ImageXVibrate;

	public Image ImageXMusic;

	public Image ImageXSound;

	public Button ButtonRestorePurchase;
}
