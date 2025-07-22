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
}
