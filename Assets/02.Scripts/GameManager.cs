using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager G_Instance;

    public enum Round { PRE, FLOP, TURN, RIVER , SHOWDOWN, END }    //Pre-Flop > Flop > Turn > River 순으로 라운드 진행
    public enum Role { BTN, SB, BB, UTG, MP, CO }    // 딜러버튼, 스몰블라인드, 빅블라인드, 언더더건, 미들포지션, 컷오프 역할 지정
    public Round round;

    [System.Serializable]
    public class PlayerInfo
    {
        public string playerName;
        public Transform[] handPositions = new Transform[2];
        public Role currentRole; // 현재 라운드에서의 플레이어 역할
        public bool isActiveInRound = true; // 라운드에 참여 중인지 확인(폴드 여부 등)
        public int playerIndex; // 플레이어의 고유 인덱스 (테이블 순서)
    }
    public List<PlayerInfo> players = new List<PlayerInfo>();

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
        MoveDealerBtn();
        RoleChange();
        DeckManager.D_Instance.ShuffleCard();
        SetRoundState(Round.PRE);
    }

    public void PreRound()
    {
        // 번 카드 한장 버리기
        DeckManager.D_Instance.DiscardCard(DeckManager.D_Instance.DealCard());
        // 플레이어들에게 2장씩 카드 분배
        // 카드 분배 순서: SB -> BB -> UTG -> MP -> CO -> BTN
        // 플레이어 인덱스를 기준으로 카드 분배 시작점을 계산 (SB부터 시작)
        int numPlayers = players.Count;
        int sbIndex = (dealerButtonIndex + 1) % numPlayers; // SB의 인덱스

        // 카드 분배를 위한 정렬된 플레이어 목록 생성 (SB부터 시작)
        List<PlayerInfo> dealOrderPlayers = new List<PlayerInfo>();
        for (int i = 0; i < numPlayers; i++)
        {
            dealOrderPlayers.Add(players[(sbIndex + i) % numPlayers]);
        }

        // 각 플레이어에게 카드 2장씩 분배
        for (int cardNum = 0; cardNum < 2; cardNum++) // 2장의 카드를 분배
        {
            foreach (PlayerInfo player in dealOrderPlayers)
            {
                if (player.isActiveInRound) // 라운드에 활성화된 플레이어에게만 분배
                {
                    // 카드를 받을 위치는 player.handPositions[cardNum]이 됩니다.
                    List<Transform> targetCardPosition = new List<Transform>() { player.handPositions[cardNum] };
                    List<GameObject> dealtCard = DeckManager.D_Instance.DealCards(1, targetCardPosition);
                }
            }
        }
        // TODO: 베팅 라운드 시작 로직 추가 (스몰/빅 블라인드 강제 베팅, 액션 순서 등)
        // 첫 액션은 BB 왼쪽의 UTG부터 시작합니다.
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
        // 첫 라운드가 아니라면 딜러 버튼을 다음 플레이어로 이동
        if (dealerButtonIndex < players.Count - 1)
        {
            dealerButtonIndex++;
        }
        else // 마지막 플레이어라면 처음으로 돌아감
        {
            dealerButtonIndex = 0;
        }
    }

    public void RoleChange()
    {
        int numPlayers = players.Count;

        // 모든 플레이어의 역할 초기화
        foreach (var player in players)
        {
            player.currentRole = Role.MP; // 기본값으로 설정 (혹은 아무 포지션)
        }
        // 역할 할당
        players[dealerButtonIndex].currentRole = Role.BTN; // 딜러 버튼

        // 스몰 블라인드 (SB): 딜러 버튼 다음 (시계 방향)
        int sbIndex = (dealerButtonIndex + 1) % numPlayers;
        players[sbIndex].currentRole = Role.SB;

        // 빅 블라인드 (BB): 스몰 블라인드 다음
        int bbIndex = (sbIndex + 1) % numPlayers;
        players[bbIndex].currentRole = Role.BB;

        for (int i = 0; i < numPlayers; i++)
        {
            if (i == dealerButtonIndex || i == sbIndex || i == bbIndex) continue;

            // 딜러 버튼 기준 시계 방향으로 포지션 할당
            int positionOffset = (i - bbIndex + numPlayers) % numPlayers; // BB로부터의 거리

            if (positionOffset == 1) // BB 바로 다음
            {
                players[i].currentRole = Role.UTG;
            }
            else if (positionOffset == 2) // UTG 다음
            {
                players[i].currentRole = Role.MP; // UTG+1 혹은 첫 MP
            }
            else
            {
                players[i].currentRole = Role.CO;
            }
        }
    }
}
