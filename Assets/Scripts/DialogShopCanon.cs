
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class DialogShopCanon : Popup
{
	private void Start()
	{
		this.HorizontalScrollSnap.OnSelectionChangeStartEvent.AddListener(delegate()
		{
			this.SetFocus(null);
		});
		this.HorizontalScrollSnap.OnSelectionChangeEndEvent.AddListener(delegate(int A_1)
		{
			this.SetFocus(this.HorizontalScrollSnap.CurrentPageObject().gameObject.GetComponent<CanonItem>());
		});
		this.HorizontalScrollSnap._scroll_rect.onValueChanged.AddListener(delegate(Vector2 p)
		{
			this.UpdateScale();
		});
		this.ButtonUse.onClick.AddListener(delegate()
		{
			this.Hide();
			GameController.ScreenManager.PlayController.SetCanon(this._currentCanonItem.CanonId);
			GameController.AnalyticsController.LogEvent("use_cannon", "id", (float)this._currentCanonItem.CanonId);
		});
		this.ButtonBack.onClick.AddListener(delegate()
		{
			this.Hide();
		});
		this.ButtonUse.gameObject.SetActive(false);
		this.ButtonBuy.gameObject.SetActive(false);
		this.ButtonBuy.onClick.AddListener(delegate()
		{
			if (this._currentCanonItem.PriceMoney <= 0f)
			{
				this.BuyCannonByCoin(this._currentCanonItem.CanonId);
			}
			else
			{
				switch (this._currentCanonItem.CanonId)
				{
				case 3:
					GameController.PurchaseController.BuyProductID(PurchaseController.PackageCannon3);
					break;
				case 4:
					GameController.PurchaseController.BuyProductID(PurchaseController.PackageCannon4);
					break;
				case 5:
					GameController.PurchaseController.BuyProductID(PurchaseController.PackageCannon5);
					break;
				case 6:
					GameController.PurchaseController.BuyProductID(PurchaseController.PackageCannon6);
					break;
				}
			}
			GameController.AnalyticsController.LogEvent("click_buy_cannon", "id", (float)this._currentCanonItem.CanonId);
		});
		this.ButtonNext.onClick.AddListener(delegate()
		{
			this.HorizontalScrollSnap.NextScreen();
		});
		this.ButtonPre.onClick.AddListener(delegate()
		{
			this.HorizontalScrollSnap.PreviousScreen();
		});
	}

	private void BuyCannonByCoin(int id)
	{
		if (Preference.Instance.DataGame.Coin >= this.GetCanonItem(id).PriceCoin)
		{
			Preference.Instance.DataGame.Coin -= this.GetCanonItem(id).PriceCoin;
			Preference.Instance.DataGame.CannonStatuses[id].IsOpen = true;
			this.Hide();
			GameController.ScreenManager.PlayController.SetCanon(id);
			GameController.AudioController.PlayOneShot("Audios/Effect/success");
			GameController.ScreenManager.PlayController.MenuUI.UpgradePrice();
		}
		else
		{
			this.Hide();
			GameController.DialogManager.DialogShop.Show();
		}
	}

	private void Update()
	{
		this.TextCoin.text = FormatUtil.FormatMoney((long)Preference.Instance.DataGame.Coin);
	}

	private void UpdateScale()
	{
		foreach (GameObject gameObject in this.HorizontalScrollSnap.ChildObjects)
		{
			float num = Mathf.Abs(this.HorizontalScrollSnap._scroll_rect.content.localPosition.x + gameObject.transform.localPosition.x + this.HorizontalScrollSnap._childSize / 2f) / (this.HorizontalScrollSnap._childSize * 2f);
			float num2 = 1.2f * (1f - num);
			num2 = Mathf.Clamp(num2, 0.9f, 1.2f);
			gameObject.GetComponent<CanonItem>().SkeletonGraphic.transform.localScale = Vector3.one * num2;
		}
	}

	private void SetFocus(CanonItem canonItem)
	{
		this._currentCanonItem = canonItem;
		if (this._currentCanonItem)
		{
			this.TextName.text = this._currentCanonItem.CanonName;
			if (this._currentCanonItem.PriceMoney > 0f)
			{
				this.ImageCoin.gameObject.SetActive(false);
				this.TextPrice.text = this._currentCanonItem.PriceMoney + " USD";
				string price = GameController.PurchaseController.GetPrice("ballblast.package.cannon." + this._currentCanonItem.CanonId);
				if (!price.Equals(string.Empty))
				{
					this.TextPrice.text = price;
				}
			}
			else
			{
				this.ImageCoin.gameObject.SetActive(true);
				this.TextPrice.text = FormatUtil.FormatMoneyDetail((long)this._currentCanonItem.PriceCoin);
			}
			if (Preference.Instance.DataGame.CannonStatuses[this._currentCanonItem.CanonId].IsOpen || (this._currentCanonItem.PriceCoin == 0 && this._currentCanonItem.PriceMoney <= 0f))
			{
				this.ButtonUse.gameObject.SetActive(true);
				this.ButtonUse.GetComponentInChildren<Text>().text = "USE";
			}
			else if (Preference.Instance.DataGame.CannonStatuses[this._currentCanonItem.CanonId].NumTry > 0)
			{
				this.ButtonUse.gameObject.SetActive(true);
				this.ButtonUse.GetComponentInChildren<Text>().text = string.Format("USE\n({0} time)", Preference.Instance.DataGame.CannonStatuses[this._currentCanonItem.CanonId].NumTry);
			}
			else
			{
				this.ButtonUse.gameObject.SetActive(false);
			}
			this.ButtonBuy.gameObject.SetActive(!this.ButtonUse.gameObject.activeSelf);
			if (this._currentCanonItem.CanonId == 6)
			{
				this.ButtonNext.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.1f);
			}
			else
			{
				this.ButtonNext.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.5f);
			}
			if (this._currentCanonItem.CanonId == 0)
			{
				this.ButtonPre.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.1f);
			}
			else
			{
				this.ButtonPre.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.5f);
			}
			if (this._currentCanonItem.CanonId == 9 && Preference.Instance.DataGame.CurrentLevel < 50 && !Preference.Instance.DataGame.CannonStatuses[this._currentCanonItem.CanonId].IsOpen)
			{
				this.TextUnlock.gameObject.SetActive(true);
				this.ButtonUse.gameObject.SetActive(false);
				this.ButtonBuy.gameObject.SetActive(false);
				this.TextUnlock.text = "Clear level <color=yellow>50</color> to unlock";
			}
			else if (this._currentCanonItem.CanonId == 8 && Preference.Instance.DataGame.CurrentLevel < 20 && !Preference.Instance.DataGame.CannonStatuses[this._currentCanonItem.CanonId].IsOpen)
			{
				this.TextUnlock.gameObject.SetActive(true);
				this.ButtonUse.gameObject.SetActive(false);
				this.ButtonBuy.gameObject.SetActive(false);
				this.TextUnlock.text = "Clear level <color=yellow>20</color> to unlock";
			}
			else
			{
				this.TextUnlock.gameObject.SetActive(false);
			}
		}
		foreach (GameObject gameObject in this.HorizontalScrollSnap.ChildObjects)
		{
			if (canonItem != gameObject.GetComponent<CanonItem>())
			{
				gameObject.GetComponent<CanonItem>().SetFocus(false);
			}
			else
			{
				gameObject.GetComponent<CanonItem>().SetFocus(true);
			}
		}
	}

	public override void Show()
	{
		base.Show();
		if (Preference.Instance.DataGame.CannonStatuses[3].IsOpen)
		{
			this.GetCanonItem(4).PriceMoney = 0f;
		}
		if (Preference.Instance.DataGame.CannonStatuses[4].IsOpen)
		{
			this.GetCanonItem(3).PriceMoney = 0f;
		}
		if (Preference.Instance.DataGame.CannonStatuses[5].IsOpen)
		{
			this.GetCanonItem(6).PriceMoney = 0f;
		}
		if (Preference.Instance.DataGame.CannonStatuses[6].IsOpen)
		{
			this.GetCanonItem(5).PriceMoney = 0f;
		}
		base.StartCoroutine(this._Show(Preference.Instance.DataGame.CurrenCanon));
	}

	private CanonItem GetCanonItem(int cannonID)
	{
		for (int i = 0; i < this.CanonItems.Length; i++)
		{
			if (this.CanonItems[i].CanonId == cannonID)
			{
				return this.CanonItems[i];
			}
		}
		return null;
	}

	public void Show(int id)
	{
		base.Show();
		if (Preference.Instance.DataGame.CannonStatuses[3].IsOpen)
		{
			this.GetCanonItem(4).PriceMoney = 0f;
		}
		if (Preference.Instance.DataGame.CannonStatuses[4].IsOpen)
		{
			this.GetCanonItem(3).PriceMoney = 0f;
		}
		if (Preference.Instance.DataGame.CannonStatuses[5].IsOpen)
		{
			this.GetCanonItem(6).PriceMoney = 0f;
		}
		if (Preference.Instance.DataGame.CannonStatuses[6].IsOpen)
		{
			this.GetCanonItem(5).PriceMoney = 0f;
		}
		base.StartCoroutine(this._Show(id));
	}

	private IEnumerator _Show(int id)
	{
		yield return new WaitForEndOfFrame();
		for (int i = 0; i < this.CanonItems.Length; i++)
		{
			if (this.CanonItems[i].CanonId == id)
			{
				this.HorizontalScrollSnap._currentPage = i;
			}
		}
		this.HorizontalScrollSnap.UpdateLayout();
		this.UpdateScale();
		yield return new WaitForEndOfFrame();
		this.SetFocus(this.HorizontalScrollSnap.CurrentPageObject().gameObject.GetComponent<CanonItem>());
		yield break;
	}

	public override void OnShowComplete()
	{
		base.OnShowComplete();
	}

	public HorizontalScrollSnap HorizontalScrollSnap;

	public Button ButtonUse;

	public Button ButtonBuy;

	public Button ButtonNext;

	public Button ButtonPre;

	public Text TextPrice;

	public Image ImageCoin;

	public Text TextName;

	public Text TextCoin;

	public Text TextUnlock;

	public Button ButtonBack;

	private CanonItem _currentCanonItem;

	public CanonItem[] CanonItems;
}
