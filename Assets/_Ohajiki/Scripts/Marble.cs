using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marble : MonoBehaviour
{
    public bool touch;

    public bool hit;

    private void OnCollisionEnter(Collision collision)
    {
        if(GameManager.instance.stateGame == GameManager.StateGame.Moving) {
            if(collision.gameObject.layer == 6) {
                hit = true;
                GameManager.instance.AddTouchCount();
                HapticsManager.instance.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
            }
        }
    }
}
