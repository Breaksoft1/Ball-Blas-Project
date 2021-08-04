
using System;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

public class PurchaseController : BaseController//, IStoreListener
{
	/*
	private void Start ()
	{
		if (PurchaseController.m_StoreController == null) {
			this.InitializePurchasing ();
		}
	}

	public string GetPrice (string product)
	{
		string result;
		try {
			result = PurchaseController.m_StoreController.products.WithID (product).metadata.localizedPriceString;
		} catch (Exception ex) {
			result = string.Empty;
		}
		return result;
	}

	public void InitializePurchasing ()
	{
		if (this.IsInitialized ()) {
			return;
		}
		ConfigurationBuilder configurationBuilder = ConfigurationBuilder.Instance (StandardPurchasingModule.Instance (), new IPurchasingModule[0]);
		configurationBuilder.AddProduct (PurchaseController.PackageCoin1, ProductType.Consumable);
		configurationBuilder.AddProduct (PurchaseController.PackageCoin2, ProductType.Consumable);
		configurationBuilder.AddProduct (PurchaseController.PackageCoin3, ProductType.Consumable);
		configurationBuilder.AddProduct (PurchaseController.PackageCoin4, ProductType.Consumable);
		configurationBuilder.AddProduct (PurchaseController.PackageCoin5, ProductType.Consumable);
		configurationBuilder.AddProduct (PurchaseController.PackageCannon3, ProductType.NonConsumable);
		configurationBuilder.AddProduct (PurchaseController.PackageCannon4, ProductType.NonConsumable);
		configurationBuilder.AddProduct (PurchaseController.PackageCannon5, ProductType.NonConsumable);
		configurationBuilder.AddProduct (PurchaseController.PackageCannon6, ProductType.NonConsumable);
		configurationBuilder.AddProduct (PurchaseController.PackageOffer1, ProductType.Consumable);
		configurationBuilder.AddProduct (PurchaseController.PackageOffer1Normal, ProductType.Consumable);
		configurationBuilder.AddProduct (PurchaseController.PackageCombo1, ProductType.Consumable);
		configurationBuilder.AddProduct (PurchaseController.PackageCombo2, ProductType.Consumable);
		configurationBuilder.AddProduct (PurchaseController.PackageCombo3, ProductType.Consumable);
		configurationBuilder.AddProduct (PurchaseController.PackageCombo4, ProductType.Consumable);
		configurationBuilder.AddProduct (PurchaseController.PackageCombo5, ProductType.Consumable);
		configurationBuilder.AddProduct (PurchaseController.ProductRemoveAd, ProductType.NonConsumable);
		UnityPurchasing.Initialize (this, configurationBuilder);
	}

	public void BuyProductID (string productId)
	{
		if (this.IsInitialized ()) {
			Product product = PurchaseController.m_StoreController.products.WithID (productId);
			if (product != null && product.availableToPurchase) {
				UnityEngine.Debug.Log (string.Format ("Purchasing product asychronously: '{0}'", product.definition.id));
				PurchaseController.m_StoreController.InitiatePurchase (product);
			} else {
				UnityEngine.Debug.Log ("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
			}
		} else {
			UnityEngine.Debug.Log ("BuyProductID FAIL. Not initialized.");
		}
	}

	public void RestorePurchases ()
	{
		if (!this.IsInitialized ()) {
			UnityEngine.Debug.Log ("RestorePurchases FAIL. Not initialized.");
			return;
		}
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer) {
			UnityEngine.Debug.Log ("RestorePurchases started ...");
			IAppleExtensions extension = PurchaseController.m_StoreExtensionProvider.GetExtension<IAppleExtensions> ();
			extension.RestoreTransactions (delegate(bool result) {
				UnityEngine.Debug.Log ("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
			});
		} else {
			UnityEngine.Debug.Log ("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
		}
	}

	private bool IsInitialized ()
	{
		return PurchaseController.m_StoreController != null && PurchaseController.m_StoreExtensionProvider != null;
	}

	public void OnInitializeFailed (InitializationFailureReason error)
	{
		UnityEngine.Debug.Log ("OnInitializeFailed InitializationFailureReason:" + error);
	}

	public PurchaseProcessingResult ProcessPurchase (PurchaseEventArgs args)
	{
		if (string.Equals (args.purchasedProduct.definition.id, PurchaseController.PackageCoin1, StringComparison.Ordinal)) {
			GameController.ScreenManager.PlayController.OnPurchaseCoin (1500000);
		} else if (string.Equals (args.purchasedProduct.definition.id, PurchaseController.PackageCoin2, StringComparison.Ordinal)) {
			GameController.ScreenManager.PlayController.OnPurchaseCoin (500000);
		} else if (string.Equals (args.purchasedProduct.definition.id, PurchaseController.PackageCoin3, StringComparison.Ordinal)) {
			GameController.ScreenManager.PlayController.OnPurchaseCoin (60000);
		} else if (string.Equals (args.purchasedProduct.definition.id, PurchaseController.PackageCoin4, StringComparison.Ordinal)) {
			GameController.ScreenManager.PlayController.OnPurchaseCoin (20000);
		} else if (string.Equals (args.purchasedProduct.definition.id, PurchaseController.PackageCoin5, StringComparison.Ordinal)) {
			GameController.ScreenManager.PlayController.OnPurchaseCoin (3000);
		} else if (string.Equals (args.purchasedProduct.definition.id, PurchaseController.ProductRemoveAd, StringComparison.Ordinal)) {
			//GameController.ScreenManager.PlayController.MenuUI.ButtonRemoveAd.gameObject.SetActive (false);
			GameController.DialogManager.PopupPurchaseResult.ShowSuccessRemoveAds ();
		} else if (string.Equals (args.purchasedProduct.definition.id, PurchaseController.PackageCannon3, StringComparison.Ordinal)) {
			GameController.ScreenManager.PlayController.OnPurchaseCanon (3);
		} else if (string.Equals (args.purchasedProduct.definition.id, PurchaseController.PackageCannon4, StringComparison.Ordinal)) {
			GameController.ScreenManager.PlayController.OnPurchaseCanon (4);
		} else if (string.Equals (args.purchasedProduct.definition.id, PurchaseController.PackageCannon5, StringComparison.Ordinal)) {
			GameController.ScreenManager.PlayController.OnPurchaseCanon (5);
		} else if (string.Equals (args.purchasedProduct.definition.id, PurchaseController.PackageCannon6, StringComparison.Ordinal)) {
			GameController.ScreenManager.PlayController.OnPurchaseCanon (6);
		} else if (string.Equals (args.purchasedProduct.definition.id, PurchaseController.PackageOffer1, StringComparison.Ordinal)) {
			GameController.ScreenManager.PlayController.OnPurchaseOffer ();
		} else if (string.Equals (args.purchasedProduct.definition.id, PurchaseController.PackageCombo1, StringComparison.Ordinal)) {
			GameController.ScreenManager.PlayController.OnPurchaseCombo (1);
		} else if (string.Equals (args.purchasedProduct.definition.id, PurchaseController.PackageCombo2, StringComparison.Ordinal)) {
			GameController.ScreenManager.PlayController.OnPurchaseCombo (2);
		} else if (string.Equals (args.purchasedProduct.definition.id, PurchaseController.PackageCombo3, StringComparison.Ordinal)) {
			GameController.ScreenManager.PlayController.OnPurchaseCombo (3);
		} else if (string.Equals (args.purchasedProduct.definition.id, PurchaseController.PackageCombo4, StringComparison.Ordinal)) {
			GameController.ScreenManager.PlayController.OnPurchaseCombo (4);
		} else if (string.Equals (args.purchasedProduct.definition.id, PurchaseController.PackageCombo5, StringComparison.Ordinal)) {
			GameController.ScreenManager.PlayController.OnPurchaseCombo (5);
		}
		return PurchaseProcessingResult.Complete;
	}

	public void OnPurchaseFailed (Product product, PurchaseFailureReason failureReason)
	{
		GameController.DialogManager.PopupPurchaseResult.ShowFail ();
		UnityEngine.Debug.Log (string.Format ("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
	}

	public void OnInitialized (IStoreController controller, IExtensionProvider extensions)
	{
		UnityEngine.Debug.Log ("OnInitialized: PASS");
		PurchaseController.m_StoreController = controller;
		PurchaseController.m_StoreExtensionProvider = extensions;
	}

	[HideInInspector]
	public static string ProductRemoveAd = "ballblast.product.remove.ads";

	[HideInInspector]
	public static string PackageCoin1 = "ballblast.package.coin.1";

	[HideInInspector]
	public static string PackageCoin2 = "ballblast.package.coin.2";

	[HideInInspector]
	public static string PackageCoin3 = "ballblast.package.coin.3";

	[HideInInspector]
	public static string PackageCoin4 = "ballblast.package.coin.4";

	[HideInInspector]
	public static string PackageCoin5 = "ballblast.package.coin.5";

	[HideInInspector]
	public static string PackageOffer1 = "ballblast.package.offer.1";

	[HideInInspector]
	public static string PackageOffer1Normal = "ballblast.package.offer.1.normal";

	[HideInInspector]
	public static string PackageCannon3 = "ballblast.package.cannon.3";

	[HideInInspector]
	public static string PackageCannon4 = "ballblast.package.cannon.4";

	[HideInInspector]
	public static string PackageCannon5 = "ballblast.package.cannon.5";

	[HideInInspector]
	public static string PackageCannon6 = "ballblast.package.cannon.6";

	[HideInInspector]
	public static string PackageCombo1 = "ballblast.package.combo.1";

	[HideInInspector]
	public static string PackageCombo2 = "ballblast.package.combo.2";

	[HideInInspector]
	public static string PackageCombo3 = "ballblast.package.combo.3";

	[HideInInspector]
	public static string PackageCombo4 = "ballblast.package.combo.4";

	[HideInInspector]
	public static string PackageCombo5 = "ballblast.package.combo.5";

	private static IStoreController m_StoreController;

	private static IExtensionProvider m_StoreExtensionProvider;

	private bool m_PurchaseInProgress;*/
}
