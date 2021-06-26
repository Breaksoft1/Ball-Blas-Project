
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogShop : Popup
{
	private void Start()
	{
		this.ButtonClose.onClick.AddListener(new UnityAction(this.Hide));
		this.ButtonsPurchase[0].onClick.AddListener(delegate()
		{
			GameController.PurchaseController.BuyProductID(PurchaseController.PackageCoin1);
		});
		this.ButtonsPurchase[1].onClick.AddListener(delegate()
		{
			GameController.PurchaseController.BuyProductID(PurchaseController.PackageCoin2);
		});
		this.ButtonsPurchase[2].onClick.AddListener(delegate()
		{
			GameController.PurchaseController.BuyProductID(PurchaseController.PackageCoin3);
		});
		this.ButtonsPurchase[3].onClick.AddListener(delegate()
		{
			GameController.PurchaseController.BuyProductID(PurchaseController.PackageCoin4);
		});
		this.ButtonsPurchase[4].onClick.AddListener(delegate()
		{
			GameController.PurchaseController.BuyProductID(PurchaseController.PackageCoin5);
		});
		this.ButtonsCombo[0].onClick.AddListener(delegate()
		{
			GameController.PurchaseController.BuyProductID(PurchaseController.PackageCombo1);
		});
		this.ButtonsCombo[1].onClick.AddListener(delegate()
		{
			GameController.PurchaseController.BuyProductID(PurchaseController.PackageCombo2);
		});
		this.ButtonsCombo[2].onClick.AddListener(delegate()
		{
			GameController.PurchaseController.BuyProductID(PurchaseController.PackageCombo3);
		});
		this.ButtonsCombo[3].onClick.AddListener(delegate()
		{
			GameController.PurchaseController.BuyProductID(PurchaseController.PackageCombo4);
		});
		this.ButtonsCombo[4].onClick.AddListener(delegate()
		{
			GameController.PurchaseController.BuyProductID(PurchaseController.PackageCombo5);
		});
		this.ButtonCoin.onClick.AddListener(delegate()
		{
			this.ButtonCoin.GetComponent<Image>().color = new Color(this.ButtonCoin.GetComponent<Image>().color.r, this.ButtonCoin.GetComponent<Image>().color.g, this.ButtonCoin.GetComponent<Image>().color.b, 1f);
			this.ButtonCombo.GetComponent<Image>().color = new Color(this.ButtonCombo.GetComponent<Image>().color.r, this.ButtonCombo.GetComponent<Image>().color.g, this.ButtonCombo.GetComponent<Image>().color.b, 0.5f);
			this.GoCoin.gameObject.SetActive(true);
			this.GoCombo.gameObject.SetActive(false);
		});
		this.ButtonCombo.onClick.AddListener(delegate()
		{
			this.ButtonCoin.GetComponent<Image>().color = new Color(this.ButtonCoin.GetComponent<Image>().color.r, this.ButtonCoin.GetComponent<Image>().color.g, this.ButtonCoin.GetComponent<Image>().color.b, 0.5f);
			this.ButtonCombo.GetComponent<Image>().color = new Color(this.ButtonCombo.GetComponent<Image>().color.r, this.ButtonCombo.GetComponent<Image>().color.g, this.ButtonCombo.GetComponent<Image>().color.b, 1f);
			this.GoCombo.gameObject.SetActive(true);
			this.GoCoin.gameObject.SetActive(false);
		});
	}

	public override void Show()
	{
		base.Show();
		for (int i = 0; i < this.TextPrice.Length; i++)
		{
			string price = GameController.PurchaseController.GetPrice("ballblast.package.coin." + (i + 1));
			if (!price.Equals(string.Empty))
			{
				this.TextPrice[i].text = price;
			}
		}
		for (int j = 0; j < this.TextPriceCombo.Length; j++)
		{
			string price2 = GameController.PurchaseController.GetPrice("ballblast.package.combo." + (j + 1));
			if (!price2.Equals(string.Empty))
			{
				this.TextPriceCombo[j].text = price2;
			}
		}
	}

	private void Update()
	{
	}

	public Button ButtonClose;

	public Button[] ButtonsPurchase;

	public Button[] ButtonsCombo;

	public Text[] TextPrice;

	public Text[] TextPriceCombo;

	public Button ButtonCoin;

	public Button ButtonCombo;

	public GameObject GoCoin;

	public GameObject GoCombo;
}
