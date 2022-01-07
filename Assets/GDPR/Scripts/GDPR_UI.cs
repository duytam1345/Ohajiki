using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_IOS
using UnityEngine.iOS;
#endif


public class GDPR_UI : MonoBehaviour
{
    //Public
    public SwitchBtn SwitchAnalyticsSupport;
    public SwitchBtn SwitchAdvertising;
    public Text[] TextsNeedChangleProductName;
    public GameObject[] Pages;

    //Private
    private int curIndexOfPage;

    private bool AnalyticsSupport
    {
        get => PlayerPrefs.GetInt("GDPR/AnalyticsSupport", 0) != 0;
        set
        {
            PlayerPrefs.SetInt("GDPR/AnalyticsSupport", value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    private bool Advertising
    {
        get => PlayerPrefs.GetInt("GDPR/Advertising", 0) != 0;
        set
        {
            PlayerPrefs.SetInt("GDPR/Advertising", value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    private int gdpr_check_state
    {
        get => PlayerPrefs.GetInt("GDPR/gdpr_check_state", 0);
        set
        {
            PlayerPrefs.SetInt("GDPR/gdpr_check_state", value);
            PlayerPrefs.Save();
        }
    }

    private void Awake()
    {
        if (HyperCasualTemplate.ConfigData.Instance == null) return;
#if UNITY_IOS && !UNITY_EDITOR
        var iosVer = new System.Version(UnityEngine.iOS.Device.systemVersion);
		if (iosVer >= new System.Version("14.5"))
		{
            Destroy(gameObject);
        }
#endif
        if (gdpr_check_state == 3 || !HyperCasualTemplate.ConfigData.GetValue<bool>(HyperCasualTemplate.Config.is_gdpr_countries))
        {
            Destroy(gameObject);
        }
        else
        {
            gdpr_check_state = 1;
            transform.GetChild(0).gameObject.SetActive(true);
        }
        
    }

    private void OnDestroy()
    {
#if !NO_UIDEBUG
        Debug.LogWarning("GDPR_UI: Destroy()");
#endif
    }

    private void Start()
    {
        foreach(Text text in TextsNeedChangleProductName)
        {
            text.text = string.Format(text.text, Application.productName);
        }

        SwitchAnalyticsSupport.isOn = AnalyticsSupport;
        SwitchAdvertising.isOn = Advertising;

        SwitchAnalyticsSupport.callback += (bool isOn) =>
        {
            AnalyticsSupport = isOn;
        };

        SwitchAdvertising.callback += (bool isOn) =>
        {
            Advertising = isOn;
        };

        Pages[0].SetActive(true);
    }

    public void ChangleCurActivePage(int index)
    {
        for (int i = 0; i < Pages.Length; i++)
        {
            Pages[i].SetActive(index == i);
        }
        curIndexOfPage = index;
    }

    public void ISupportThat()
    {
        if ((!AnalyticsSupport || !Advertising) && curIndexOfPage == 2)
        {
            ChangleCurActivePage(3);
            return;
        }
        gdpr_check_state = 3;
        AnalyticsSupport = Advertising = true;
        Destroy(gameObject);
    }

    public void PrivacyPolicy()
    {
        Application.OpenURL("https://site.nicovideo.jp/app/privacy/index.html");
    }
}

#if UNITY_EDITOR
public class DGPR_UI_Editor
{
    [MenuItem("Tools/GDPR/ShowPrefs")]
    public static void ShowGdprPrefs()
    {
        Debug.Log($"gdpr_check_state:{PlayerPrefs.GetInt("GDPR/gdpr_check_state")}");
        Debug.Log($"Advertising:{PlayerPrefs.GetInt("GDPR/Advertising")}");
        Debug.Log($"AnalyticsSupport:{PlayerPrefs.GetInt("GDPR/AnalyticsSupport")}");
    }

    [MenuItem("Tools/GDPR/ClearPrefs")]
    public static void ClearGdprPrefs()
    {
        PlayerPrefs.SetInt("GDPR/gdpr_check_state", 0);
        PlayerPrefs.SetInt("GDPR/Advertising", 0);
        PlayerPrefs.SetInt("GDPR/AnalyticsSupport", 0);
        PlayerPrefs.Save();
    }
    
}
#endif
