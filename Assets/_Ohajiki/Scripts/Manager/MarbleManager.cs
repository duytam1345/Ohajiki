using HyperCasualTemplate;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleManager : MonoBehaviour
{
    public static MarbleManager instance;

    public TouchEvent touch;

    public Marble currentMarble;

    Vector2 oldVTouch;

    public float sensitivityMove;

    public List<Marble> marbles;

    private void Awake()
    {
        instance = this;
    }

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
            switch (GameManager.instance.stateGame) {
                case GameManager.StateGame.Ready:
                break;
                case GameManager.StateGame.Draw:
                DrawLineManager.instance.StartDraw();
                break;
                case GameManager.StateGame.DragToMove:
                RaycastHit hit;
                Ray r = Camera.main.ScreenPointToRay(touch.GetTouchPos());
                if (Physics.Raycast(r, out hit, 1000, 1 << 6)) {
                    if (hit.collider.name == "Player") {
                        currentMarble = hit.collider.gameObject.GetComponent<Marble>();
                        oldVTouch = touch.GetTouchPos();
                    }
                }
                break;
                case GameManager.StateGame.Moving:
                break;
                case GameManager.StateGame.Win:
                break;
                case GameManager.StateGame.Lose:
                break;
            }
            break;
            case Enums.TouchInfo.Moved:
            break;
            case Enums.TouchInfo.Stationary:
            break;
            case Enums.TouchInfo.Ended:
            switch (GameManager.instance.stateGame) {
                case GameManager.StateGame.Ready:
                GameManager.instance.SetState(GameManager.StateGame.Draw);
                break;
                case GameManager.StateGame.Draw:
                DrawLineManager.instance.EndDraw();
                break;
                case GameManager.StateGame.DragToMove:
                if (currentMarble) {
                    Vector3 vForce = touch.GetTouchPos() - oldVTouch;
                    vForce.z = vForce.y;
                    vForce.y = 0;
                    currentMarble.GetComponent<Rigidbody>().AddForce(vForce * sensitivityMove);
                    currentMarble = null;

                    GameManager.instance.SetState(GameManager.StateGame.Moving);
                }
                break;
                case GameManager.StateGame.Moving:
                break;
                case GameManager.StateGame.Win:
                break;
                case GameManager.StateGame.Lose:
                break;
            }
            break;
            case Enums.TouchInfo.Canceled:
            break;
        }
    }

    private void Update()
    {
        if(Input.touchCount==3) {
            SRDebug.Instance.ShowDebugPanel();
        }

        switch (GameManager.instance.stateGame) {
            case GameManager.StateGame.Ready:
            break;
            case GameManager.StateGame.Draw:
            break;
            case GameManager.StateGame.DragToMove:
            break;
            case GameManager.StateGame.Moving:
            foreach (var item in marbles) {
                if (item.GetComponent<Rigidbody>().velocity != Vector3.zero) {
                    return;
                }
            }
            GameManager.instance.CheckWinLose();
            break;
            case GameManager.StateGame.Win:
            break;
            case GameManager.StateGame.Lose:
            break;
        }
    }
}
