using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public List<GameObject>cardDeck = new List<GameObject>();
    public List<GameObject> discardPile = new List<GameObject>();

    void Awake()
    {
        GameObject mainDeck = GameObject.Find("BlueBackCards");
        if (mainDeck != null)
        {
            Transform[] cards = mainDeck.GetComponentsInChildren<Transform>();
            foreach (Transform card in cards)
            {
                if (card.gameObject != mainDeck)
                    cardDeck.Add(card.gameObject);
            }
        }
    }

    public void ShuffleCard()
    {
        // 리스트의 마지막 요소부터 첫 번째 요소까지 반복
        for (int i = cardDeck.Count - 1; i > 0; i--)
        {
            // 현재 요소(i)와 0부터 i까지의 무작위 인덱스(j)를 선택
            int j = Random.Range(0, i + 1);

            // 현재 요소와 무작위로 선택된 요소의 위치를 바꿉니다.
            GameObject temp = cardDeck[i];
            cardDeck[i] = cardDeck[j];
            cardDeck[j] = temp;
        }
    }

    public GameObject DealCard()    // 번카드
    {
        if (cardDeck.Count == 0)
            return null;

        GameObject dealtCard = cardDeck[0]; // 덱의 맨 위 카드
        cardDeck.RemoveAt(0);              // 덱에서 제거

        Debug.Log($"Burn Card: {dealtCard.name}, 남은 카드: {cardDeck.Count}");
        return dealtCard;
    }

    public List<GameObject> DealCards(int count, List<Transform> targetPositions)   // 플레이어 및 커뮤니티 카드 분배
    {
        List<GameObject> dealtCards = new List<GameObject>();

        if (targetPositions == null || targetPositions.Count < count)
        {
            Debug.LogError("DealCards: targetPositions 리스트가 null이거나 분배할 카드 수보다 적습니다.");
            return dealtCards;
        }
        for (int i = 0; i < count; i++)
        {
            GameObject card = DealCard(); // 덱에서 카드 한 장 가져오기
            if (card != null)
            {
                // 카드를 활성화하고 목표 위치로 이동시킵니다.
                card.SetActive(true);
                card.transform.position = targetPositions[i].position;

                dealtCards.Add(card);
            }
            else
            {
                Debug.LogWarning("덱에 카드가 부족하여 모든 카드를 분배할 수 없습니다.");
                break; // 카드가 부족하면 루프 중단
            }
        }
        return dealtCards;
    }

    public void DiscardCard(GameObject card)    // 번 카드로 버리는 카드 따로 보관
    {
        if (card != null)
        {
            discardPile.Add(card);
            card.SetActive(false);
        }
    }
}
