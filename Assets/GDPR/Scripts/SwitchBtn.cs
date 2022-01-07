using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchBtn : MonoBehaviour
{
    //Public
    public Image Graphic;
    public Sprite SpriteSwitchOn, SpriteSwitchOff;
    public bool isOn = false;
    public System.Action<bool> callback;

    private void Start()
    {
        SetSwitch(isOn);
    }

    public void ChangleSwitch()
    {
        isOn = !isOn;
        SetSwitch(isOn);
    }

    public void SetSwitch(bool isOn)
    {
        Graphic.sprite = isOn ? SpriteSwitchOn : SpriteSwitchOff;
        if (callback != null) callback(isOn);
    }
}
