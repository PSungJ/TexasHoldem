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
        // ����Ʈ�� ������ ��Һ��� ù ��° ��ұ��� �ݺ�
        for (int i = cardDeck.Count - 1; i > 0; i--)
        {
            // ���� ���(i)�� 0���� i������ ������ �ε���(j)�� ����
            int j = Random.Range(0, i + 1);

            // ���� ��ҿ� �������� ���õ� ����� ��ġ�� �ٲߴϴ�.
            GameObject temp = cardDeck[i];
            cardDeck[i] = cardDeck[j];
            cardDeck[j] = temp;
        }
    }

    public GameObject DealCard()    // ��ī��
    {
        if (cardDeck.Count == 0)
            return null;

        GameObject dealtCard = cardDeck[0]; // ���� �� �� ī��
        cardDeck.RemoveAt(0);              // ������ ����

        Debug.Log($"Burn Card: {dealtCard.name}, ���� ī��: {cardDeck.Count}");
        return dealtCard;
    }

    public List<GameObject> DealCards(int count, List<Transform> targetPositions)   // �÷��̾� �� Ŀ�´�Ƽ ī�� �й�
    {
        List<GameObject> dealtCards = new List<GameObject>();

        if (targetPositions == null || targetPositions.Count < count)
        {
            Debug.LogError("DealCards: targetPositions ����Ʈ�� null�̰ų� �й��� ī�� ������ �����ϴ�.");
            return dealtCards;
        }
        for (int i = 0; i < count; i++)
        {
            GameObject card = DealCard(); // ������ ī�� �� �� ��������
            if (card != null)
            {
                // ī�带 Ȱ��ȭ�ϰ� ��ǥ ��ġ�� �̵���ŵ�ϴ�.
                card.SetActive(true);
                card.transform.position = targetPositions[i].position;

                dealtCards.Add(card);
            }
            else
            {
                Debug.LogWarning("���� ī�尡 �����Ͽ� ��� ī�带 �й��� �� �����ϴ�.");
                break; // ī�尡 �����ϸ� ���� �ߴ�
            }
        }
        return dealtCards;
    }

    public void DiscardCard(GameObject card)    // �� ī��� ������ ī�� ���� ����
    {
        if (card != null)
        {
            discardPile.Add(card);
            card.SetActive(false);
        }
    }
}
