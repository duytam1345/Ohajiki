using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "HyperCasualTemplate/Localization/LocalizeTextData")]
public class LocalizeTextData : ScriptableObject
{
    [SerializeField]
    private SystemLanguage language = SystemLanguage.English;
    public SystemLanguage Language => language;

    [Header("Notification:")]
    [SerializeField]
    private string[] notification;
    public string[] Notification => notification;

    
    
    [Header("Sample Today:")]
    [SerializeField, TextArea]
    protected string today = string.Empty;
    public string Today => today;
    
    [Header("Sample Button:")]
    [SerializeField]
    private string textButtonReset = string.Empty;
    public string TextButtonReset => textButtonReset;

    [SerializeField]
    private string textButtonCancel = string.Empty;
    public string TextButtonCancel => textButtonCancel;



}
