using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager G_Instance;
    public DeckManager deckManager;

    public enum Round { PRE, FLOP, TURN, RIVER , SHOWDOWN, END }    //Pre-Flop > Flop > Turn > River ������ ���� ����
    public enum Role { BTN, SB, BB, UTG, MP, CO }    // ������ư, ��������ε�, �����ε�, �������, �̵�������, �ƿ��� ���� ����
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
    public Transform[] flopPositions = new Transform[3];    // �÷� ī�� 3�� ��ġ
    public Transform turnPosition;                          // �� ī�� 1�� ��ġ
    public Transform riverPosition;                         // ���� ī�� 1�� ��ġ

    [Header("Dealer")]
    public Transform dealerTr;
    public GameObject DealerBTN;
    public int dealerButtonIndex = 0;

    public void SetRoundState(Round newState)
    {
        round = newState;
        Debug.Log($"���� ���� ����: {round}");

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
            Transform aiPivot = parentHands.Find($"AI{i}_Pivot");   // AI1,2,3,4,5 ã��

            if (aiPivot != null)
            {
                AIHandData curAIData = new AIHandData(aiPivot);
                foreach(Transform handPos in aiPivot.GetComponentsInChildren<Transform>())  // ������ ã�� AI Pivot ��ġ���� �ڽ� �� HandPos ã��
                {
                    // �ڽ��� �̸��� "HandPos"�� �����ϰ�, �ڱ� �ڽ� ������Ʈ�� �ƴ� ���
                    if (handPos != aiPivot && handPos.name.Contains("HandPos"))
                        curAIData.handPositons.Add(handPos);
                }
                AIHands.Add(curAIData);  // AIData�� ��ü ������ ����Ʈ�� �߰�
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
        // �� ī�� ���� ������
        deckManager.DiscardCard(deckManager.DealCard());
        // �÷��̾�鿡�� 2�徿 ī�� �й�
        // ī�� �й� ����: SB -> BB -> UTG -> MP -> CO -> BTN
    }

    public void FlopRound()
    {
        // �� ī�� ���� ������
        deckManager.DiscardCard(deckManager.DealCard());
        // Ŀ�´�Ƽ ī�� 3�� ����
    }

    public void TurnRound()
    {
        // �� ī�� ���� ������
        deckManager.DiscardCard(deckManager.DealCard());
        // Ŀ�´�Ƽ ī�� 1�� ����
    }

    public void RiverRound()
    {
        // �� ī�� ���� ������
        deckManager.DiscardCard(deckManager.DealCard());
        // Ŀ�´�Ƽ ī�� 1�� ����
    }

    public void ShowDown()
    {

    }

    public void EndGame()
    {

    }
}
