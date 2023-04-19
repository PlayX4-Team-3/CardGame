using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using DG.Tweening;

public class JsonCardManager : Singleton<JsonCardManager>
{
    public TextAsset textJson;
    
    public RectTransform deckArea;
    public RectTransform handArea;
    public RectTransform graveArea;

    private CardList cardList = new CardList();

    private Font font;

    public Dictionary<int, Card> cardDeck = new Dictionary<int, Card>();

    [SerializeField]
    public List<GameObject> deckList = new List<GameObject>();
    [SerializeField]
    public List<GameObject> handList = new List<GameObject>();
    [SerializeField]
    public List<GameObject> graveList = new List<GameObject>();

    private Vector2 originSize = new Vector3(0.6f, 0.6f, 1f);

    private void Awake()
    {
        font = Resources.Load<Font>("MaplestoryFont_TTF/Maplestory Light");

        LoadCardsFromJson();
        CreateCardSprite();
    }

    #region 카드 생성
    // Json 파일 불러오기
    private void LoadCardsFromJson()
    {
        cardList = JsonUtility.FromJson<CardList>(textJson.text);

        foreach (Card card in cardList.CardDB)
            cardDeck.Add(card.ID, card);
    }

    // 불러온 데이터로 카드 객체 만들기
    private void CreateCardSprite()
    {
        foreach (Card card in cardList.CardDB)
        {
            int cardMainIndex = card.ID / 100;

            string cardName = card.Name;
            string cardDescription = card.Description;
            int cardCost = card.Cost;

            // Enemy Card가 아닌 경우
            if (cardMainIndex != 4)
            {
                // 카드 이미지 생성
                GameObject go = new GameObject(card.ID.ToString());

                Image img = go.AddComponent<Image>();
                string sprite = card.ImageLink;
                img.sprite = Resources.Load<Sprite>(sprite);
                img.SetNativeSize();

                go.transform.SetParent(deckArea, false);

                // 묵찌빠 이미지 생성
                GameObject go2 = new GameObject();
                go2.transform.SetParent(go.transform);
                go2.transform.localPosition = new Vector2(125f, 195f);
                go2.transform.localScale = new Vector2(0.06f, 0.06f);

                Image img2 = go2.AddComponent<Image>();
                string sprite2 = card.RPSLink;
                img2.sprite = Resources.Load<Sprite>(sprite2);
                img2.SetNativeSize();

                CreateCardDescription(go, cardDescription, cardName, cardCost);

                go.AddComponent<DragAndDrop>();

                deckList.Add(go);
                go.SetActive(false);
            }
        }
    }

    // 카드 이름, 설명, 코스트 부분 텍스트 오브젝트 추가
    private void CreateCardDescription(GameObject go, string description, string name, int cost)
    {
        // Card Description part
        GameObject descriptionTextObject = new GameObject();
        RectTransform drt = descriptionTextObject.AddComponent<RectTransform>();
        Text dtxt = descriptionTextObject.AddComponent<Text>();

        descriptionTextObject.transform.SetParent(go.transform);
        descriptionTextObject.transform.position = go.transform.position;

        dtxt.text = description;
        dtxt.font = font;
        dtxt.fontSize = 30;
        
        // 텍스트 크기 자동으로 조절
        dtxt.resizeTextForBestFit = true;

        drt.anchoredPosition = new Vector2(0f, -135f);
        drt.localScale = new Vector2(1f, 1f);
        drt.sizeDelta = new Vector2(215f, 80f);

        
        // Card Name part
        GameObject nameTextObject = new GameObject();
        RectTransform nrt = nameTextObject.AddComponent<RectTransform>();
        Text ntxt = nameTextObject.AddComponent<Text>();

        nameTextObject.transform.SetParent(go.transform);
        nameTextObject.transform.position = go.transform.position;

        ntxt.text = name;
        ntxt.font = font;
        ntxt.fontSize = 30;
        ntxt.verticalOverflow = VerticalWrapMode.Overflow;
        ntxt.alignment = TextAnchor.MiddleCenter;

        nrt.anchoredPosition = new Vector2(0f, 205f);
        nrt.localScale = new Vector2(1f, 1f);
        nrt.sizeDelta = new Vector2(225f, 80f);

        // Card Cost part
        GameObject costTextObject = new GameObject();
        RectTransform crt = costTextObject.AddComponent<RectTransform>();
        Text ctxt = costTextObject.AddComponent<Text>();

        costTextObject.transform.SetParent(go.transform);
        costTextObject.transform.position = go.transform.position;

        ctxt.text = cost.ToString();
        ctxt.font = font;
        ctxt.fontSize = 30;
        ctxt.verticalOverflow = VerticalWrapMode.Overflow;
        ctxt.alignment = TextAnchor.MiddleCenter;

        crt.anchoredPosition = new Vector2(-125f, 195f);
        crt.localScale = new Vector2(1f, 1f);
        crt.sizeDelta = new Vector2(225f, 80f);
    }
    #endregion

    #region 카드 이동
    public void DeckInit()
    {
        DeckShuffle(deckList);
        DeckShuffle(deckList);
        DeckShuffle(deckList);
    }

    public void DeckShuffle(List<GameObject> decksToShuffle)
    {
        for (int i = 0; i < decksToShuffle.Count; i++)
        {
            int randomIndex = Random.Range(0, decksToShuffle.Count);

            GameObject temp = decksToShuffle[randomIndex];
            decksToShuffle[randomIndex] = decksToShuffle[i];
            decksToShuffle[i] = temp;
        }
    }

    public void DrawCard(int n = 1)
    {
        if (deckList.Count > 0)
        {
            for (int i = 0; i < n; i++)
            {
                GameObject go = deckList[deckList.Count - 1].gameObject;

                GameObject dummy = JsonGameManager.Instance.dummy.gameObject;

                if (handList.Count >= 9)
                {
                    Debug.Log("카드가 너무 많습니다." + go.name);
                    graveList.Add(go);
                    deckList.Remove(go);

                    go.transform.SetParent(graveArea);
                }
                else
                {
                    handList.Add(go);
                    deckList.Remove(go);

                    go.SetActive(true);

                    go.transform.SetParent(handArea);
                    go.GetComponent<Image>().SetNativeSize();
                    go.transform.localScale = originSize;
                }
            }
        }
        else
            GraveToDeck();
    }


    public void HandToGrave(GameObject useCard)
    {
        handList.Remove(useCard);
        graveList.Add(useCard);

        useCard.transform.SetParent(graveArea);

        useCard.SetActive(false);
    }

    public void GraveToDeck()
    {
        if (graveList.Count > 0)
        {
            for(int i = 0; i < graveList.Count; i++)
                deckList.Add(graveList[i]);
            
            graveList.Clear();
            DrawCard();
        }
        else
            Debug.Log("묘지에 카드가 없습니다.");

    }
    #endregion
}