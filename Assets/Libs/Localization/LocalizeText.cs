using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace HyperCasualTemplate
{

    public class LocalizeText : MonoBehaviour
    {
        public static LocalizeText instance;

        [FormerlySerializedAs("currUIData")] [HideInInspector] public LocalizeTextData currTextData;

        [SerializeField] private List<LocalizeTextData> data = new List<LocalizeTextData>();

        [SerializeField] private SystemLanguage defaultLanguage = SystemLanguage.English;

        private void Awake()
        {
            instance = this;
            currTextData = GetLocalizeData(ConfigData.UserLanguage);
        }

        public LocalizeTextData GetLocalizeData(SystemLanguage language)
        {
            var result = data.Find(x => x.Language == language);
            if (result == null) return data.Find(x => x.Language == defaultLanguage);
            return result;
        }

        public void UpdateCurrentData()
        {
            currTextData = GetLocalizeData(ConfigData.UserLanguage);
        }
        
        
        
        public string GetNotificationText()
        {
            if (currTextData == null)
            {
                UpdateCurrentData();
            }

            var getData = currTextData.Notification;
            var mssId = new System.Random().Next(0, getData.Length);
            return getData[mssId];
        }

    }
}
