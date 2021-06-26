
using System;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogOffer : Popup
{
	private void Start()
	{
		this.ButtonClose.onClick.AddListener(new UnityAction(this.Hide));
		this.ButtonBuy.onClick.AddListener(delegate()
		{
			GameController.PurchaseController.BuyProductID(PurchaseController.PackageOffer1);
		});
	}

	private void Update()
	{
	}

	public override void Show()
	{
		base.Show();
		string price = GameController.PurchaseController.GetPrice(PurchaseController.PackageOffer1);
		if (!price.Equals(string.Empty))
		{
			this.ButtonBuy.GetComponentInChildren<Text>().text = price;
		}
		price = GameController.PurchaseController.GetPrice(PurchaseController.PackageOffer1Normal);
		if (!price.Equals(string.Empty))
		{
			this.OldPrice.text = price;
		}
	}

	public Button ButtonClose;

	public Button ButtonBuy;

	public Text OldPrice;
}
