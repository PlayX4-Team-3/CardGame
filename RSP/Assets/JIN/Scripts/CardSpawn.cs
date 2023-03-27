using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawn : MonoBehaviour
{
    private CardPoolManager cpm;

    private void Start()
    {
        cpm = CardPoolManager.Instance;
    }

    public void Draw()
    {
        GameObject go = Instantiate(cpm.cards[Random.Range(0, cpm.cards.Count)]);

        go.transform.SetParent(this.transform, false);
    }

    public void Shuffle()
    {

    }
}
