using HyperCasualTemplate;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public TouchEvent touch;

    void Start()
    {
        touch.OnTouchInfo += Touch_OnTouchInfo;
    }

    private void Touch_OnTouchInfo(Enums.TouchInfo obj)
    {
        switch (obj) {
            case Enums.TouchInfo.None:
            break;
            case Enums.TouchInfo.Began:
            RaycastHit hit;
            Ray r = Camera.main.ScreenPointToRay(touch.GetTouchPos());
            if (Physics.Raycast(r, out hit)) {
                print(hit.collider.name);
            }
            break;
            case Enums.TouchInfo.Moved:
            break;
            case Enums.TouchInfo.Stationary:
            break;
            case Enums.TouchInfo.Ended:
            break;
            case Enums.TouchInfo.Canceled:
            break;
        }
    }
}
