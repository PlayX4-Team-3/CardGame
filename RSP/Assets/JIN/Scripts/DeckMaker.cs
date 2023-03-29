using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class DeckMaker : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> deck = new List<GameObject>();
    [SerializeField]
    private List<GameObject> enemyDeck = new List<GameObject>();

    public List<GameObject> copiedDeck = new List<GameObject>();


    private static DeckMaker _instance;

    public static DeckMaker Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(DeckMaker)) as DeckMaker;
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
            _instance = this;

        else if (_instance != this)
            Destroy(gameObject);

        DeckInstantiate();
    }

    public void DeckShuffle()
    {
        for(int i = 0; i< copiedDeck.Count; i++)
        {
            int randomIndex = Random.Range(0, copiedDeck.Count);
            
            GameObject temp = copiedDeck[randomIndex];
            copiedDeck[randomIndex] = copiedDeck[i];
            copiedDeck[i] = temp;
        }
    }

    public void DeckInstantiate()
    {
        foreach(GameObject prefab in deck)
        {
            GameObject copiedPrefab = Instantiate(prefab);
            copiedDeck.Add(copiedPrefab);
        }
    }
}
