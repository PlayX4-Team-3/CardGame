using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    JsonGameManager jgm;
    JsonCardManager jcm;

    private Transform canvasT;
    private Transform previousParentT;

    private Vector2 startPos;
    private Vector2 offset;

    private Vector2 magnifiedCardScale;
    private Vector2 restoredCardScale;

    private int thisChildIndex;

    private Card card;

    private void Start()
    {
        jgm = JsonGameManager.Instance;
        jcm = JsonCardManager.Instance;

        canvasT = GameObject.FindWithTag("Canvas").transform;
        previousParentT = GameObject.FindWithTag("Hand").transform;

        magnifiedCardScale = new Vector2(0.8f, 0.8f);
        restoredCardScale = new Vector2(0.6f, 0.6f);

        thisChildIndex = this.transform.GetSiblingIndex();

        card = JsonCardManager.Instance.cardDeck[int.Parse(this.name)];
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPos = this.transform.position;
        offset = startPos - eventData.position;

        this.transform.SetParent(canvasT);
        this.transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position + offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector2.zero);

        if (hit.collider != null && hit.collider.CompareTag("DropArea") && jgm.player.Cost >= card.Cost)
        {
            jgm.player.Cost -= card.Cost;
            
            for (int i = jgm.player.MaxCost - 1; i >= jgm.player.Cost; i--)
                jgm.playerCostImg[i].gameObject.SetActive(false);

            jgm.UseCard(card);
            JsonCardManager.Instance.HandToGrave(this.gameObject);
        }
        else
        {
            this.transform.SetParent(previousParentT);
            this.transform.SetSiblingIndex(thisChildIndex);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        this.transform.localScale = magnifiedCardScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.transform.localScale = restoredCardScale;
    }
}
