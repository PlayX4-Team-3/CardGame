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

    public List<GameObject> copiedPlayerDeck = new List<GameObject>();
    public GameObject HandArea;
    public GameObject GraveArea;


    // ���� ���۶� �� ����, �� ��� ��� �� ���� ����
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

    // prefab�� �ٷ� �����پ��� SetParent�� �� �����߻� -> �� ���� �� ���
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
            copiedPlayerDeck.RemoveAt(drawIndex);
            drawCard.transform.SetParent(HandArea.transform, false);
        }
        else
            GraveToDeck();
    }

    public void GraveToDeck()
    {
        DeckShuffle(graveDeck);

        while(graveDeck.Count > 0)
        {
            int graveIndex = graveDeck.Count - 1;
            GameObject graveCard = graveDeck[graveIndex];
            graveCard.SetActive(true);
            copiedPlayerDeck.Add(graveDeck[graveIndex]);
            graveDeck.RemoveAt(graveIndex);
        }
    }

    public void HandToGrave(GameObject card)
    {
        graveDeck.Add(card);
        handDeck.Remove(card);
        card.SetActive(false);
    }

}
