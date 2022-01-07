using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public enum StateGame
    {
        S1,
        S2,
        S3,
        S4,
        S5,
    }

    public StateGame stateGame;


    private void Awake()
    {
        instance = this;
    }
}
