using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class CardManager : Singleton<CardManager>
{
    [SerializeField]
    private List<GameObject> playerDeck = new List<GameObject>();
    [SerializeField]
    private List<GameObject> enemyDeck = new List<GameObject>();
    [SerializeField]
    private List<GameObject> graveDeck = new List<GameObject>();
    [SerializeField]
    public List<GameObject> handDeck = new List<GameObject>();
    [SerializeField]
    public List<GameObject> copiedPlayerDeck = new List<GameObject>();

    public GameObject HandArea;
    public GameObject GraveArea;
    public GameObject DeckArea;


    // 게임 시작때 덱 셔플, 덱 모두 사용 후 묘지 셔플
    public List<GameObject> DeckShuffle(List<GameObject> decksToShuffle)
    {
        for(int i = 0; i< decksToShuffle.Count; i++)
        {
            int randomIndex = Random.Range(0, decksToShuffle.Count);
            
            GameObject temp = decksToShuffle[randomIndex];
            decksToShuffle[randomIndex] = decksToShuffle[i];
            decksToShuffle[i] = temp;
        }

        return decksToShuffle;
    }

    // prefab을 바로 가져다쓰면 SetParent쓸 때 오류발생 -> 덱 복사 후 사용
    public void DeckInstantiate()
    {
        foreach(GameObject prefab in playerDeck)
        {
            GameObject copiedPrefab = Instantiate(prefab);
            copiedPlayerDeck.Add(copiedPrefab);
        }
    }

    public void Draw()
    {
        if (copiedPlayerDeck.Count != 0)
        {
            int drawIndex = copiedPlayerDeck.Count - 1;

            GameObject drawCard = copiedPlayerDeck[drawIndex];
            handDeck.Add(copiedPlayerDeck[drawIndex]);

            //drawCard.SetActive(true);
            copiedPlayerDeck.RemoveAt(drawIndex);

            drawCard.transform.SetParent(HandArea.transform, false);
            drawCard.transform.SetAsLastSibling();
            drawCard.SetActive(true);
        }

        else
            GraveToDeck();
    }

    public void GraveToDeck()
    {
        if (graveDeck.Count != 0)
        {
            DeckShuffle(graveDeck);

            while (graveDeck.Count > 0)
            {
                int graveIndex = graveDeck.Count - 1;

                GameObject graveCard = graveDeck[graveIndex];
                copiedPlayerDeck.Add(graveDeck[graveIndex]);

                graveCard.SetActive(false);
                graveDeck.RemoveAt(graveIndex);
            }

            Draw();
        }

        else
            Debug.Log("묘지에 카드가 없습니다.");
    }

    public void HandToGrave(GameObject card)
    {
        graveDeck.Add(card);
        handDeck.Remove(card);

        card.SetActive(false);
    }

}
