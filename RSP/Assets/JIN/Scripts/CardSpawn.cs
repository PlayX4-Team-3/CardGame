using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawn : MonoBehaviour
{
    private CardPoolManager cpm;
    private DeckMaker dm;

    private void Start()
    {
        cpm = CardPoolManager.Instance;
        dm = DeckMaker.Instance;
    }

    public void Draw()
    {
        if (dm.copiedDeck.Count != 0)
        {
            int drawIndex = dm.copiedDeck.Count - 1;
            GameObject drawCard = dm.copiedDeck[drawIndex];
            dm.copiedDeck.RemoveAt(drawIndex);
            drawCard.transform.SetParent(this.transform, false);
        }
        else
            Debug.Log("deck is empty");
    }

    public void Shuffle()
    {
        if (dm.copiedDeck.Count != 0)
            DeckMaker.Instance.DeckShuffle();
    }
}
