using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Card
{
    public int ID;
    public string Name;
    public string Attribute;
    public string Type;
    public int Cost;
    public int Power;
    public string ImageLink;
    public string Description;
    public string RPSLink;
}

[System.Serializable]
public class CardList
{
    public Card[] CardDB;
}