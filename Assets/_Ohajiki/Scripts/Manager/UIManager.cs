using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Text textStage;

    public GameObject TapToStart;
    public GameObject DrawLine;
    public GameObject DragToMove;
    public GameObject Win;
    public GameObject Lose;

    private void Awake()
    {
        instance = this;
    }

    public void NextBtn()
    {
        GameManager.instance.NextLevel();
    }

    public void TryBtn()
    {
        GameManager.instance.ResetLevel();
    }

    public void ShowTapToStart()
    {
        TapToStart.SetActive(true);
        DrawLine.SetActive(false);
        DragToMove.SetActive(false);
        Win.SetActive(false);
        Lose.SetActive(false);
    }

    public void ShowDrawLine()
    {
        TapToStart.SetActive(false);
        DrawLine.SetActive(true);
        DragToMove.SetActive(false);
        Win.SetActive(false);
        Lose.SetActive(false);
    }
    public void ShowDragToMove()
    {
        TapToStart.SetActive(false);
        DrawLine.SetActive(false);
        DragToMove.SetActive(true);
        Win.SetActive(false);
        Lose.SetActive(false);
    }

    public void ShowWin()
    {
        TapToStart.SetActive(false);
        DrawLine.SetActive(false);
        DragToMove.SetActive(false);
        Win.SetActive(true);
        Lose.SetActive(false);
    }

    public void ShowLose()
    {
        TapToStart.SetActive(false);
        DrawLine.SetActive(false);
        DragToMove.SetActive(false);
        Win.SetActive(false);
        Lose.SetActive(true);
    }
}
