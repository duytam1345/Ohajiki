using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace HyperCasualTemplate
{

    [CreateAssetMenu(menuName = "HyperCasualTemplate/Localization/LocalizeUIData")]
    public class LocalizeUIData : ScriptableObject
    {
        [SerializeField] private SystemLanguage defaultLanguage = SystemLanguage.English;

        [FormerlySerializedAs("localizeUIDetails")] [SerializeField] private List<LocalizeUI> localizeUIs = new List<LocalizeUI>();

        public LocalizeUI CurrentLocalizeUIData
        {
            get
            {
                var value = localizeUIs.Find(x => x.SystemLanguage == ConfigData.UserLanguage);
                if (value == null)
                {
                    value = localizeUIs.Find(x => x.SystemLanguage == defaultLanguage);
                }

                return value;
            }
        }

        [Serializable]
        public class LocalizeUI
        {
            [SerializeField] private SystemLanguage systemLanguage = SystemLanguage.English;

            [SerializeField] private Font font = null;

            [SerializeField, TextArea] private string text = string.Empty;

            public SystemLanguage SystemLanguage
            {
                get { return systemLanguage; }
            }

            public Font Font
            {
                get { return font; }
            }

            public string Text
            {
                get { return text; }
            }
        }
    }
}
