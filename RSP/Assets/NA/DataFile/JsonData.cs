using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonData : MonoBehaviour
{
    public TextAsset textJSON;

    [Serializable]
    public class CardData
    {
        public int CardIndex;
        public string CardType;
        public string CardAttribute;
        public int CardPower;
        public int CardCost;
    }
    [Serializable]
    public class CardDataList
    {
        public CardData[] cardData;
    }

    public CardDataList cardDataList = new CardDataList();

    private void Start()
    {
        cardDataList = JsonUtility.FromJson<CardDataList>(textJSON.text);
    }

}
