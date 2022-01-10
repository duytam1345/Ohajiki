using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class DebugOption : MonoBehaviour
{
}
partial class SROptions
{
    [Category(GeneralCategory)]
    [DisplayName("Select Level")]
    [Sort(951)]
    public int SelectLevel {
        get { return GameManager.instance.indexLevel; }
        set {
            GameManager.instance.indexLevel = value;
            GameManager.instance.ResetLevel();
        }
    }

    [Category(GeneralCategory)]
    [DisplayName("Max Count Draw")]
    [Sort(952)]
    public int MaxCountDraw {
        get { return DrawLineManager.instance.maxCountPoint; }
        set {
            DrawLineManager.instance.maxCountPoint = value;
        }
    }
}
