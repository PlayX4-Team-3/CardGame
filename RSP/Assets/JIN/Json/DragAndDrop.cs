using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
using DG.Tweening;

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

        //thisChildIndex = this.transform.GetSiblingIndex();

        card = jcm.cardDeck[int.Parse(this.name)];

        DOTween.Init(false, true, LogBehaviour.Default);
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

        Debug.Log(thisChildIndex);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector2.zero);

        if (hit.collider != null && hit.collider.CompareTag("DropArea") && jgm.player.Cost >= card.Cost)
        {
            jgm.dummy.transform.SetParent(canvasT);
            jgm.dummy.SetActive(false);

            jgm.player.Cost -= card.Cost;
            
            jgm.UseCard(card);
            jcm.HandToGrave(this.gameObject);

            this.transform.DOMove(jcm.graveArea.transform.position, 1f);
            this.transform.DOScale(Vector3.zero, 1f);
        }

        else
        {
            this.transform.SetParent(previousParentT);
            this.transform.SetSiblingIndex(thisChildIndex);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!jgm.isClick)
        {
            jgm.isClick = true;
            thisChildIndex = this.transform.GetSiblingIndex();

            //this.transform.localScale = magnifiedCardScale;
            this.transform.DOScale(magnifiedCardScale, 0.3f);
            this.transform.SetParent(canvasT);

            jgm.dummy.SetActive(true);
            jgm.dummy.transform.SetParent(previousParentT);
            jgm.dummy.transform.SetSiblingIndex(thisChildIndex);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (jgm.isClick)
        {
            jgm.dummy.transform.SetParent(canvasT);
            jgm.dummy.SetActive(false);

            //this.transform.localScale = restoredCardScale;
            this.transform.DOScale(restoredCardScale, 0.3f);
            this.transform.SetParent(previousParentT);
            this.transform.SetSiblingIndex(thisChildIndex);

            jgm.isClick = false;
        }
    }


}
