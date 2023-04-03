using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

public class Cards : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public CardManager cm;

    public int cardID;
    public int cc; // cost
    public CardType ct; // att def utilty
    public CardAttribute ca; // rock sissors paper

    public int cardPower; // att(2damage), def(2shield), util(+2 or -2)...

    private Vector2 startPos;
    private Vector2 offset;

    private void Start()
    {
        cm = CardManager.Instance;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPos = transform.position;
        offset = startPos - (Vector2)eventData.position;

        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position + offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero);
        if (hit.collider != null && hit.collider.CompareTag("DropArea"))
        {
            //GameManager.Instance.UseCard(this.gameObject.GetComponent<Cards>());
            Debug.Log("Use Card!" + this.gameObject.name);
            cm.HandToGrave(this.gameObject);
        }
        else
        {
            transform.position = startPos;
            Debug.Log("no hit!!");
        }
    }
}
