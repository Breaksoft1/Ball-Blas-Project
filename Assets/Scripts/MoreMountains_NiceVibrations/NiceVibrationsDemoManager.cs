
using System;
using UnityEngine;
using UnityEngine.UI;

namespace MoreMountains.NiceVibrations
{
	public class NiceVibrationsDemoManager : MonoBehaviour
	{
		protected virtual void Awake()
		{
			MMVibrationManager.iOSInitializeHaptics();
		}

		protected virtual void Start()
		{
			this.DisplayInformation();
		}

		protected virtual void DisplayInformation()
		{
			if (MMVibrationManager.Android())
			{
				this._platformString = "API version " + MMVibrationManager.AndroidSDKVersion().ToString();
			}
			else if (MMVibrationManager.iOS())
			{
				this._platformString = "iOS " + MMVibrationManager.iOSSDKVersion();
			}
			else
			{
				this._platformString = Application.platform + ", not supported by Nice Vibrations for now.";
			}
			this.DebugTextBox.text = "Platform : " + this._platformString + "\n Nice Vibrations v1.3";
		}

		protected virtual void OnDisable()
		{
			MMVibrationManager.iOSReleaseHaptics();
		}

		public virtual void TriggerDefault()
		{
			Handheld.Vibrate();
		}

		public virtual void TriggerVibrate()
		{
			//MMVibrationManager.Vibrate();
		}

		public virtual void TriggerSelection()
		{
			MMVibrationManager.Haptic(HapticTypes.Selection);
		}

		public virtual void TriggerSuccess()
		{
			MMVibrationManager.Haptic(HapticTypes.Success);
		}

		public virtual void TriggerWarning()
		{
			MMVibrationManager.Haptic(HapticTypes.Warning);
		}

		public virtual void TriggerFailure()
		{
			MMVibrationManager.Haptic(HapticTypes.Failure);
		}

		public virtual void TriggerLightImpact()
		{
			MMVibrationManager.Haptic(HapticTypes.LightImpact);
		}

		public virtual void TriggerMediumImpact()
		{
			MMVibrationManager.Haptic(HapticTypes.MediumImpact);
		}

		public virtual void TriggerHeavyImpact()
		{
			MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
		}

		public Text DebugTextBox;

		protected string _debugString;

		protected string _platformString;

		protected const string _CURRENTVERSION = "1.3";
	}
}
