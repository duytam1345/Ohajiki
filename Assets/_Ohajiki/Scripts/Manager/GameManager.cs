using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int totalLevel;

    public int touchCount;

    public bool delayCheck = true;//after point up .5f active call checkwinlose


    public int indexLevel;

    public enum StateGame
    {
        Ready,//Show Start Btn
        Draw,//Draw 
        DragToMove,//Wait to drag end draging
        Moving,//draged. wait until all marbles stop move
        Win,//win game
        Lose //lose game
    }

    public StateGame stateGame;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        LoadLevel();
        SetState(StateGame.Ready);
    }

    public void SetState(StateGame newStateGame)
    {
        stateGame = newStateGame;
        switch (stateGame) {
            case StateGame.Ready:
            UIManager.instance.ShowTapToStart();
            break;
            case StateGame.Draw:
            UIManager.instance.ShowDrawLine();
            break;
            case StateGame.DragToMove:
            UIManager.instance.ShowDragToMove();
            break;
            case StateGame.Moving:
            Delay(.1f, () => {
                delayCheck = false;
            });
            break;
            case StateGame.Win:
            UIManager.instance.ShowWin();
            break;
            case StateGame.Lose:
            UIManager.instance.ShowLose();
            break;
        }
    }

    public void AddTouchCount()
    {
        touchCount++;
    }

    public void CheckWinLose()
    {
        if (!delayCheck) {
            if(!DrawLineManager.instance.b) {
                SetState(StateGame.Lose);
                return;
            }

            if (touchCount != 2) {
                SetState(StateGame.Lose);
            } else {
                foreach (var item in MarbleManager.instance.marbles.ToArray()) {
                    if (item.name != "Player") {
                        if (item.hit) {
                            Destroy(item.gameObject);
                            MarbleManager.instance.marbles.Remove(item);
                        }
                    }
                }

                if (MarbleManager.instance.marbles.Count == 1) {
                    SetState(StateGame.Win);
                } else {
                    DrawLineManager.instance.ResetLine();
                    SetState(StateGame.Draw);
                }
            }

            touchCount = 0;
            delayCheck = true;
        }
    }

    public void Delay(float f, System.Action action)
    {
        StartCoroutine(DelayCo(f, action));
    }

    IEnumerator DelayCo(float f, System.Action action)
    {
        yield return new WaitForSeconds(f);
        action();
    }

    public void LoadLevel()
    {
        if (GameObject.FindGameObjectWithTag("Level")) {
            Destroy(GameObject.FindGameObjectWithTag("Level"));
        }

        GameObject g = Instantiate(Resources.Load("Level/Level" + indexLevel) as GameObject, Vector3.zero, Quaternion.identity);

        MarbleManager.instance.marbles.Clear();
        foreach (Transform item in g.transform) {
            MarbleManager.instance.marbles.Add(item.GetComponent<Marble>());
        }
    }

    public void ResetLevel()
    {
        if (indexLevel > totalLevel) {
            indexLevel = 1;
        }

        if (indexLevel <=0) {
            indexLevel = 1;
        }

        UIManager.instance.textStage.text = "STAGE " + indexLevel;
        DrawLineManager.instance.ResetLine();
        SetState(StateGame.Ready);
        touchCount = 0;
        delayCheck = true;
        LoadLevel();
    }

    public void NextLevel()
    {
        indexLevel++;
        
        ResetLevel();
    }
}

//anim ui
//hit effect 