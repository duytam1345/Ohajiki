using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;

public class HapticsManager : MonoBehaviour
{
    public static HapticsManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void Haptic(HapticTypes types)
    {
        MMVibrationManager.Haptic(types);
    }
}
