using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
#if UNITY_IOS
using UnityEngine.iOS;
#endif

namespace HyperCasualTemplate
{
	/// <summary>
	/// 常駐する(DontDestroyOnLoadを設定する)GameObjectにアタッチすること
	/// </summary>
	public class TenjinSDK : MonoBehaviour
	{
		// Tenjin API Key
		private const string TENJIN_API_KEY = "TELEXEDPZA7CMPVDYRRWUDCZMRFQY6QE";
		private const string TenjinSessionExpireDatetime = "TenjinSessionExpireDatetime";
		private const string TenjinSessionCount = "TenjinSessionCount";
		private const int TenjinConversionValueMax = 63;

		// Start is called before the first frame update
		void Start()
		{
			InitializeTenjin();
		}

		void InitializeTenjin()
		{
			BaseTenjin instance = Tenjin.getInstance(TENJIN_API_KEY);

#if UNITY_IOS && !UNITY_EDITOR
			Debug.Log("OS Version: " + Device.systemVersion);
			var iosVer = new Version(Device.systemVersion);
			if (iosVer >= new Version("14.5"))
			{
				// Tenjin wrapper for requestTrackingAuthorization
				instance.RequestTrackingAuthorizationWithCompletionHandler((status) =>
				{
					Debug.Log("===> App Tracking Transparency Authorization Status: " + status);

					// Registers SKAdNetwork app for attribution
					instance.RegisterAppForAdNetworkAttribution();

					// Sends install/open event to Tenjin
					instance.Connect();

					if (DateTime.Now <= _TenjinSessionExpireDatetime)
					{
						Debug.Log("===> Tenjin Session Expire: " + _TenjinSessionExpireDatetime);
						Debug.Log("===> Tenjin Session Count: " + _TenjinSessionCount);
						instance.UpdateConversionValue(_TenjinSessionCount);
						_TenjinSessionCount++;
					}
				});
			}
			else
			{
				// Registers SKAdNetwork app for attribution
				instance.RegisterAppForAdNetworkAttribution();

				// Sends install/open event to Tenjin
				instance.Connect();

				if (DateTime.Now <= _TenjinSessionExpireDatetime)
				{
					Debug.Log("===> Tenjin Session Expire: " + _TenjinSessionExpireDatetime);
					Debug.Log("===> Tenjin Session Count: " + _TenjinSessionCount);
					instance.UpdateConversionValue(_TenjinSessionCount);
					_TenjinSessionCount++;
				}
			}
#else
			// Sends install/open event to Tenjin
			instance.Connect();
#endif
		}

		private DateTime _TenjinSessionExpireDatetime
		{
			get
			{
				DateTime value;
				string stringValue = PlayerPrefs.GetString(TenjinSessionExpireDatetime, string.Empty);
				if (string.IsNullOrEmpty(stringValue))
				{
					value = DateTime.Now.AddDays(1);
					PlayerPrefs.SetString(TenjinSessionExpireDatetime, value.ToString());
					PlayerPrefs.Save();
				}
				else
				{
					DateTime.TryParse(stringValue, out value);
				}

				return value;
			}
		}

		private int _TenjinSessionCount
		{
			get { return PlayerPrefs.GetInt(TenjinSessionCount, 1); }
			set
			{
				if (value >= TenjinConversionValueMax)
				{
					return;
				}

				PlayerPrefs.SetInt(TenjinSessionCount, value);
				PlayerPrefs.Save();
			}
		}
	}
}
