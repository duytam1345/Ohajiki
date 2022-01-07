using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HyperCasualTemplate
{
    public class LocalizeSample_Today : MonoBehaviour
    {
        public TextMesh textMesh;

        // Start is called before the first frame update
        void Start()
        {
            if (textMesh == null)
            {
                textMesh = GetComponent<TextMesh>();
            }

            if (textMesh != null)
            {
                var now = DateTime.Now;
                if (LocalizeText.instance != null)
                {
                    textMesh.text = string.Format(LocalizeText.instance.currTextData.Today, now.Year, now.Month, now.Day);
                }
            }
        }

    }
}
