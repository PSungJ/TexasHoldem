using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager G_Instance;
    public enum round { PRE, FLOP, TURN, RIVER }    //Pre-Flop > Flop > Turn > River ������ ���� ����
    public enum role { BTN, SB, BB }    // ������ư, ��������ε�, �����ε� ���� ����

    void Start()
    {
        
    }

    public void NewGame()
    {

    }

    public void PreRound()
    {

    }

    public void FlopRound()
    {

    }

    public void TurnRound()
    {

    }

    public void RiverRound()
    {

    }

    public void FinishGame()
    {

    }
}
