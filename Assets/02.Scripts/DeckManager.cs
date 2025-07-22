using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public static DeckManager D_Instance;

    [SerializeField] private List<GameObject>cardDeck = new List<GameObject>();

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
}
