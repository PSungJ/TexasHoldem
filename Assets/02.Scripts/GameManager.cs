using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager G_Instance;
    public DeckManager deckManager;

    public enum Round { PRE, FLOP, TURN, RIVER , SHOWDOWN, END }    //Pre-Flop > Flop > Turn > River 순으로 라운드 진행
    public enum Role { BTN, SB, BB, UTG, MP, CO }    // 딜러버튼, 스몰블라인드, 빅블라인드, 언더더건, 미들포지션, 컷오프 역할 지정
    public Round round;
    public Role role;
    public List<Transform> playersTr;
    public float angle;

    [Header("Player / AI Components")]
    public List<Transform> playerHand = new List<Transform>();
    public List<AIHandData> AIHands;

    // AI Hand Class
    [System.Serializable]
    public class AIHandData
    {
        private Transform aiMainTransform;
        public List<Transform> handPositons = new List<Transform>();

        public AIHandData(Transform aiTransform)
        {
            aiMainTransform = aiTransform;
        }
    }

    [Header("Community Cards")]
    public Transform[] flopPositions = new Transform[3];    // 플롭 카드 3장 위치
    public Transform turnPosition;                          // 턴 카드 1장 위치
    public Transform riverPosition;                         // 리버 카드 1장 위치

    [Header("Dealer")]
    public Transform dealerTr;
    public GameObject DealerBTN;
    public int dealerButtonIndex = 0;

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

    private void Awake()
    {
        InitPlayerHand();
        InitAiHand();
    }

    void Start()
    {
        StartGame();
    }

    public void InitPlayerHand()
    {
        Transform[] playerHandPos = GameObject.Find("Players").GetComponentsInChildren<Transform>();
        foreach (Transform playerHand in playerHandPos)
        {
            if (playerHand != this.transform && playerHand.name.StartsWith("Player1HandPos"))
                this.playerHand.Add(playerHand);
        }
    }

    public void InitAiHand()
    {
        Transform parentHands = GameObject.Find("Players").GetComponent<Transform>();
        for (int i = 1; i <= 5; i++)
        {
            Transform aiPivot = parentHands.Find($"AI{i}_Pivot");   // AI1,2,3,4,5 찾기

            if (aiPivot != null)
            {
                AIHandData curAIData = new AIHandData(aiPivot);
                foreach(Transform handPos in aiPivot.GetComponentsInChildren<Transform>())  // 위에서 찾은 AI Pivot 위치에서 자식 중 HandPos 찾기
                {
                    // 자식의 이름이 "HandPos"를 포함하고, 자기 자신 오브젝트는 아닌 경우
                    if (handPos != aiPivot && handPos.name.Contains("HandPos"))
                        curAIData.handPositons.Add(handPos);
                }
                AIHands.Add(curAIData);  // AIData를 전체 데이터 리스트에 추가
            }
        }
    }

    public void RoleCycle()
    {
        
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
