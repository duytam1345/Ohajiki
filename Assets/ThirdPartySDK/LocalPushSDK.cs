using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HyperCasualTemplate
{
	/// <summary>
	/// 常駐する(DontDestroyOnLoadを設定する)GameObjectにアタッチすること
	/// </summary>
	public class LocalPushSDK : MonoBehaviour
	{
		
		
        [SerializeField] private string title = "Hyper Casual Template";
        [SerializeField] private string cannelId = "HyperCasualTemplate";
        
       void OnApplicationFocus(bool hasFocus)
        {
            //if (!hasFocus) return;
            LocalPushNotification.RemoveAllDeliveredNotifications();
            LocalPushNotification.SetApplicationBadge(0);
        }
		private void Start()
		{
		    AddNotificationCalendar();
			ConfigData.FirebaseSDKInstance.OnFetchRemoteConfigComplete += AddNotificationCalendar;
		}

		public string GetNotificationString() {
			LocalPushNotification.RegisterChannel(cannelId, $"{title} Channel", "Generic notifications");
			if (LocalizeText.instance != null)
			{
				return LocalizeText.instance.GetNotificationText();
			}
			return "";
		}

		public void AddNotificationCalendar() {
			var mss = GetNotificationString();
			var hour = 19;
			var minute = 00;

			string[] timeArr = ConfigData.GetValue<string>(Config.PushNotification_Time).Split(':');
			System.Int32.TryParse(timeArr[0], out hour);
			if (timeArr.Length >= 2) System.Int32.TryParse(timeArr[1], out minute);
			hour = hour < 0 ? 0 : (hour > 23 ? 23 : hour);
			minute = minute < 0 ? 0 : (minute > 59 ? 59 : minute);
			LocalPushNotification.AddCalendarTrigger(title, mss, 1, hour, minute);
			Debug.Log("===========AddNotificationCalendar: " + mss + ", time: "+hour+":"+minute);
		}

		public void AddNotificationSchedule(int seconds) {
			var mss = GetNotificationString();
			LocalPushNotification.AddSchedule(title, mss, 1, seconds, cannelId);
			Debug.Log("===========AddNotificationSchedule: " + mss + ", seconds: "+seconds);
		}
	}
}
