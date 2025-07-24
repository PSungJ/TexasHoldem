using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager G_Instance;

    public enum Round { PRE, FLOP, TURN, RIVER , SHOWDOWN, END }    //Pre-Flop > Flop > Turn > River ������ ���� ����
    public enum Role { BTN, SB, BB, UTG, MP, CO }    // ������ư, ��������ε�, �����ε�, �������, �̵�������, �ƿ��� ���� ����
    public Round round;
    public DeckManager deckManager;
    public GameObject DealerBTN;

    // Ŀ�´�Ƽ ī�� ��ġ
    public Transform[] flopPositions = new Transform[3]; // �÷� ī�� 3�� ��ġ
    public Transform turnPosition;                      // �� ī�� 1�� ��ġ
    public Transform riverPosition;                     // ���� ī�� 1�� ��ġ

    // ������ư ��ġ
    private int dealerButtonIndex = 0;

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
