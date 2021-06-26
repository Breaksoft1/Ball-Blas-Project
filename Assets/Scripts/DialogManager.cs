
using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
	public Toast Toast
	{
		get
		{
			return (!this._toast) ? (this._toast = (Toast)this.createDialog(this.ToastPrefabs)) : this._toast;
		}
	}

	public PopupPurchaseResult PopupPurchaseResult
	{
		get
		{
			return (!this._popupPurchaseResult) ? (this._popupPurchaseResult = (PopupPurchaseResult)this.createDialog(this.PurchaseResultPrefab)) : this._popupPurchaseResult;
		}
	}

	public DialogLevelClear DialogLevelClear
	{
		get
		{
			return (!this._levelClearPrefabs) ? (this._levelClearPrefabs = (DialogLevelClear)this.createDialog(this.LevelClearPrefabs)) : this._levelClearPrefabs;
		}
	}

	public DialogSecondChance DialogSecondChance
	{
		get
		{
			return (!this._dialogSecondChance) ? (this._dialogSecondChance = (DialogSecondChance)this.createDialog(this.SecondChancePrefab)) : this._dialogSecondChance;
		}
	}

	public DialogGameOver DialogGameOver
	{
		get
		{
			return (!this._dialogGameOver) ? (this._dialogGameOver = (DialogGameOver)this.createDialog(this.GameOverPrefab)) : this._dialogGameOver;
		}
	}

	public DialogShopCanon DialogShopCanon
	{
		get
		{
			return (!this._dialogShopCanon) ? (this._dialogShopCanon = (DialogShopCanon)this.createDialog(this.ShopCanonPrefab)) : this._dialogShopCanon;
		}
	}

	public DialogIdleEarn DialogIdleEarn
	{
		get
		{
			return (!this._dialogIdleEarn) ? (this._dialogIdleEarn = (DialogIdleEarn)this.createDialog(this.OfflineEarnPrefab)) : this._dialogIdleEarn;
		}
	}

	public DialogDailyReward DialogDailyReward
	{
		get
		{
			return (!this._dialogDailyReward) ? (this._dialogDailyReward = (DialogDailyReward)this.createDialog(this.DailyReward)) : this._dialogDailyReward;
		}
	}

	public DialogShop DialogShop
	{
		get
		{
			return (!this._dialogShop) ? (this._dialogShop = (DialogShop)this.createDialog(this.ShopPrefab)) : this._dialogShop;
		}
	}

	public DialogOffer DialogOffer
	{
		get
		{
			return (!this._dialogOffer) ? (this._dialogOffer = (DialogOffer)this.createDialog(this.OfferPrefab)) : this._dialogOffer;
		}
	}

	public DialogSetting DialogSetting
	{
		get
		{
			return (!this._dialogSetting) ? (this._dialogSetting = (DialogSetting)this.createDialog(this.SettingPrefab)) : this._dialogSetting;
		}
	}

	public DialogRateUs DialogRateUs
	{
		get
		{
			return (!this._dialogRateUs) ? (this._dialogRateUs = (DialogRateUs)this.createDialog(this.RateUsPrefab)) : this._dialogRateUs;
		}
	}

	public DialogDailyMission DialogDailyMission
	{
		get
		{
			return (!this._dialogDailyMission) ? (this._dialogDailyMission = (DialogDailyMission)this.createDialog(this.DailyMission)) : this._dialogDailyMission;
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void HideAllDialog()
	{
		foreach (Popup popup in this.ListDialogs)
		{
			if (popup.gameObject.activeSelf)
			{
				popup.Hide();
			}
		}
	}

	private Popup createDialog(GameObject prefab)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(prefab);
		gameObject.SetActive(false);
		gameObject.transform.SetParent(base.transform, false);
		Popup component = gameObject.GetComponent<Popup>();
		this.ListDialogs.Add(component);
		return component;
	}

	public int GetNumberActiveDialog()
	{
		int num = 0;
		foreach (Popup popup in this.ListDialogs)
		{
			if (popup.gameObject.activeSelf)
			{
				num++;
			}
		}
		return num;
	}

	[HideInInspector]
	public List<Popup> ListDialogs = new List<Popup>();

	public GameObject ToastPrefabs;

	public GameObject LevelClearPrefabs;

	public GameObject PurchaseResultPrefab;

	public GameObject SecondChancePrefab;

	public GameObject GameOverPrefab;

	public GameObject ShopCanonPrefab;

	public GameObject OfflineEarnPrefab;

	public GameObject DailyReward;

	public GameObject ShopPrefab;

	public GameObject OfferPrefab;

	public GameObject SettingPrefab;

	public GameObject RateUsPrefab;

	public GameObject DailyMission;

	private Toast _toast;

	private PopupPurchaseResult _popupPurchaseResult;

	private DialogLevelClear _levelClearPrefabs;

	private DialogSecondChance _dialogSecondChance;

	private DialogGameOver _dialogGameOver;

	private DialogShopCanon _dialogShopCanon;

	private DialogIdleEarn _dialogIdleEarn;

	private DialogDailyReward _dialogDailyReward;

	private DialogShop _dialogShop;

	private DialogOffer _dialogOffer;

	private DialogSetting _dialogSetting;

	private DialogRateUs _dialogRateUs;

	private DialogDailyMission _dialogDailyMission;
}
