using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager G_Instance;

    public enum Round { PRE, FLOP, TURN, RIVER , SHOWDOWN, END }    //Pre-Flop > Flop > Turn > River 순으로 라운드 진행
    public enum Role { BTN, SB, BB, UTG, MP, CO }    // 딜러버튼, 스몰블라인드, 빅블라인드, 언더더건, 미들포지션, 컷오프 역할 지정
    public Round round;
    public DeckManager deckManager;
    public GameObject DealerBTN;

    // 커뮤니티 카드 위치
    public Transform[] flopPositions = new Transform[3]; // 플롭 카드 3장 위치
    public Transform turnPosition;                      // 턴 카드 1장 위치
    public Transform riverPosition;                     // 리버 카드 1장 위치

    // 딜러버튼 위치
    private int dealerButtonIndex = 0;

    public void SetRoundState(Round newState)
    {
        round = newState;
        Debug.Log($"현재 라운드 상태: {round}");

        switch (round)
        {
            case Round.PRE:
                PreRound();
                break;
            case Round.FLOP:
                FlopRound();
                break;
            case Round.TURN:
                TurnRound();
                break;
            case Round.RIVER:
                RiverRound();
                break;
            case Round.SHOWDOWN:
                ShowDown();
                break;
            case Round.END:
                EndGame();
                break;
        }
    }

    void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        deckManager.ShuffleCard();
        SetRoundState(Round.PRE);
    }

    public void PreRound()
    {
        // 번 카드 한장 버리기
        deckManager.DiscardCard(deckManager.DealCard());
        // 플레이어들에게 2장씩 카드 분배
        // 카드 분배 순서: SB -> BB -> UTG -> MP -> CO -> BTN
    }

    public void FlopRound()
    {
        // 번 카드 한장 버리기
        deckManager.DiscardCard(deckManager.DealCard());
        // 커뮤니티 카드 3장 오픈
    }

    public void TurnRound()
    {
        // 번 카드 한장 버리기
        deckManager.DiscardCard(deckManager.DealCard());
        // 커뮤니티 카드 1장 오픈
    }

    public void RiverRound()
    {
        // 번 카드 한장 버리기
        deckManager.DiscardCard(deckManager.DealCard());
        // 커뮤니티 카드 1장 오픈
    }

    public void ShowDown()
    {

    }

    public void EndGame()
    {

    }
}
