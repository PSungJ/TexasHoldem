using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager G_Instance;
    public enum round { PRE, FLOP, TURN, RIVER }    //Pre-Flop > Flop > Turn > River 순으로 라운드 진행
    public enum role { BTN, SB, BB }    // 딜러버튼, 스몰블라인드, 빅블라인드 역할 지정

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
