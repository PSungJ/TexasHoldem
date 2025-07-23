using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager G_Instance;

    public enum Round { PRE, FLOP, TURN, RIVER , SHOWDOWN, END }    //Pre-Flop > Flop > Turn > River ������ ���� ����
    public enum Role { BTN, SB, BB, UTG, MP, CO }    // ������ư, ��������ε�, �����ε�, �������, �̵�������, �ƿ��� ���� ����
    public Round round;

    [System.Serializable]
    public class PlayerInfo
    {
        public string playerName;
        public Transform[] handPositions = new Transform[2];
        public Role currentRole; // ���� ���忡���� �÷��̾� ����
        public bool isActiveInRound = true; // ���忡 ���� ������ Ȯ��(���� ���� ��)
        public int playerIndex; // �÷��̾��� ���� �ε��� (���̺� ����)
    }
    public List<PlayerInfo> players = new List<PlayerInfo>();

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
        MoveDealerBtn();
        RoleChange();
        DeckManager.D_Instance.ShuffleCard();
        SetRoundState(Round.PRE);
    }

    public void PreRound()
    {
        // �� ī�� ���� ������
        DeckManager.D_Instance.DiscardCard(DeckManager.D_Instance.DealCard());
        // �÷��̾�鿡�� 2�徿 ī�� �й�
        // ī�� �й� ����: SB -> BB -> UTG -> MP -> CO -> BTN
        // �÷��̾� �ε����� �������� ī�� �й� �������� ��� (SB���� ����)
        int numPlayers = players.Count;
        int sbIndex = (dealerButtonIndex + 1) % numPlayers; // SB�� �ε���

        // ī�� �й踦 ���� ���ĵ� �÷��̾� ��� ���� (SB���� ����)
        List<PlayerInfo> dealOrderPlayers = new List<PlayerInfo>();
        for (int i = 0; i < numPlayers; i++)
        {
            dealOrderPlayers.Add(players[(sbIndex + i) % numPlayers]);
        }

        // �� �÷��̾�� ī�� 2�徿 �й�
        for (int cardNum = 0; cardNum < 2; cardNum++) // 2���� ī�带 �й�
        {
            foreach (PlayerInfo player in dealOrderPlayers)
            {
                if (player.isActiveInRound) // ���忡 Ȱ��ȭ�� �÷��̾�Ը� �й�
                {
                    // ī�带 ���� ��ġ�� player.handPositions[cardNum]�� �˴ϴ�.
                    List<Transform> targetCardPosition = new List<Transform>() { player.handPositions[cardNum] };
                    List<GameObject> dealtCard = DeckManager.D_Instance.DealCards(1, targetCardPosition);
                }
            }
        }
        // TODO: ���� ���� ���� ���� �߰� (����/�� ����ε� ���� ����, �׼� ���� ��)
        // ù �׼��� BB ������ UTG���� �����մϴ�.
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

    public void ShowDown()
    {

    }

    public void EndGame()
    {

    }

    public void MoveDealerBtn()
    {
        // ù ���尡 �ƴ϶�� ���� ��ư�� ���� �÷��̾�� �̵�
        if (dealerButtonIndex < players.Count - 1)
        {
            dealerButtonIndex++;
        }
        else // ������ �÷��̾��� ó������ ���ư�
        {
            dealerButtonIndex = 0;
        }
    }

    public void RoleChange()
    {
        int numPlayers = players.Count;

        // ��� �÷��̾��� ���� �ʱ�ȭ
        foreach (var player in players)
        {
            player.currentRole = Role.MP; // �⺻������ ���� (Ȥ�� �ƹ� ������)
        }
        // ���� �Ҵ�
        players[dealerButtonIndex].currentRole = Role.BTN; // ���� ��ư

        // ���� ����ε� (SB): ���� ��ư ���� (�ð� ����)
        int sbIndex = (dealerButtonIndex + 1) % numPlayers;
        players[sbIndex].currentRole = Role.SB;

        // �� ����ε� (BB): ���� ����ε� ����
        int bbIndex = (sbIndex + 1) % numPlayers;
        players[bbIndex].currentRole = Role.BB;

        for (int i = 0; i < numPlayers; i++)
        {
            if (i == dealerButtonIndex || i == sbIndex || i == bbIndex) continue;

            // ���� ��ư ���� �ð� �������� ������ �Ҵ�
            int positionOffset = (i - bbIndex + numPlayers) % numPlayers; // BB�κ����� �Ÿ�

            if (positionOffset == 1) // BB �ٷ� ����
            {
                players[i].currentRole = Role.UTG;
            }
            else if (positionOffset == 2) // UTG ����
            {
                players[i].currentRole = Role.MP; // UTG+1 Ȥ�� ù MP
            }
            else
            {
                players[i].currentRole = Role.CO;
            }
        }
    }
}
