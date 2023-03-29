using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPoolManager : MonoBehaviour
{
    private static CardPoolManager _instance;

    public List<GameObject> cards = new List<GameObject>();

    public static CardPoolManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(CardPoolManager)) as CardPoolManager;
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

        DontDestroyOnLoad(gameObject);
    }
}