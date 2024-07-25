using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm { get; private set; }


    private void Awake()
    {
        if (gm == null)
            gm = this;
        else
            Destroy(gm);
    }
}

public enum Tags
{
    Player = 0,
    Enemy = 1,
    Top = 2,
    Mid = 3,
    Bottom = 4,
    Attack = 5,
    Paladin = 6,
}

public enum AnimStates
{
    Idle,
    Move,
    Attack,
    Dead,
}