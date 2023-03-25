using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType
{
    None = 0,
    Attack,
    Defense,
    Utility
}

public enum CardAttribute
{
    None = 0,
    Rock,
    Sissors,
    Paper
}

public class Card
{
    public int CardCost { get; set; }

    public CardType CardType { get; set;}

    public CardAttribute CardAttribute { get; set;}

    public int OffensivePower { get; set; }

    public int DefensivePower { get; set; }

    public int UtilityPower { get; set; }

    public Card(int cardCost, CardType cardType, CardAttribute cardAttribute, int power)
    {
        CardCost = cardCost;
        CardType = cardType;
        CardAttribute = cardAttribute;

        if(cardType == CardType.Attack) 
            OffensivePower = power;
        else if(cardType == CardType.Defense)
            DefensivePower = power;

        if(cardType == CardType.Utility)
            UtilityPower = power;
    }
}

public class CardManager : MonoBehaviour
{
    public List<Card> cards = new List<Card>();

    void Start()
    {
        // cost, type, attribute, cardPower
        // 1코, 공격카드, ( ), 1데미지
        cards.Add(new Card(1, CardType.Attack, CardAttribute.Rock, 1)); 
        cards.Add(new Card(1, CardType.Attack, CardAttribute.Sissors, 1)); 
        cards.Add(new Card(1, CardType.Attack, CardAttribute.Paper, 1));

        // 2코, 공격카드, ( ), 1데미지
        cards.Add(new Card(2, CardType.Attack, CardAttribute.Rock, 2));
        cards.Add(new Card(2, CardType.Attack, CardAttribute.Sissors, 2));
        cards.Add(new Card(2, CardType.Attack, CardAttribute.Paper, 2));

        // 1코, 방어카드, ( ), 1데미지만큼 방어
        cards.Add(new Card(1, CardType.Defense, CardAttribute.Rock, 1));
        cards.Add(new Card(1, CardType.Defense, CardAttribute.Sissors, 1));
        cards.Add(new Card(1, CardType.Defense, CardAttribute.Paper, 1));

        // 1코, 방어카드, ( ), 1데미지만큼 방어
        cards.Add(new Card(1, CardType.Defense, CardAttribute.Rock, 1));
        cards.Add(new Card(1, CardType.Defense, CardAttribute.Sissors, 1));
        cards.Add(new Card(1, CardType.Defense, CardAttribute.Paper, 1));


        cards.Add(new Card(1, CardType.Utility, CardAttribute.Rock, 1));
        cards.Add(new Card(1, CardType.Utility, CardAttribute.Sissors, 1));
        cards.Add(new Card(1, CardType.Utility, CardAttribute.Paper, 1));


        //ShowCardList();
    }

    public void ShowCardList()
    {
        for(int i =0; i < cards.Count; i++)
            Debug.Log(cards[i].CardCost.ToString() +  cards[i].CardType.ToString() + cards[i].CardAttribute.ToString());
    }
}
