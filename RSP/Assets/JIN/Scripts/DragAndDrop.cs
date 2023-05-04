//using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    private GameManager gm;
    private CardManager cm;
    private DotweenManager dm;

    private Transform canvasT;
    private Transform previousParentT;

    private Vector2 previousPos;
    private Vector2 offset;

    private Vector2 magnifiedCardScale;
    private Vector2 restoredCardScale;

    private int thisChildIndex;

    private Card card;

    private bool isUsed;

    private void Start()
    {
        gm = GameManager.Instance;
        cm = CardManager.Instance;
        dm = DotweenManager.Instance;

        canvasT = GameObject.FindWithTag("Canvas").transform;
        previousParentT = GameObject.FindWithTag("Hand").transform;

        magnifiedCardScale = new Vector2(0.8f, 0.8f);
        restoredCardScale = new Vector2(0.6f, 0.6f);

        card = cm.cardDeck[int.Parse(this.name)];

        isUsed = false;
    }

    // 카드 비활성화시 초기화
    private void OnDisable()
    {
        isUsed = false;

        this.transform.localScale = restoredCardScale;
        this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (TurnManager.Instance.currentPlayer == PlayerID.Player && isUsed == false)
        {
            previousPos = this.transform.position;
            offset = previousPos - eventData.position;

            this.transform.SetParent(canvasT);
            this.transform.SetAsLastSibling();

            isUsed = true;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (TurnManager.Instance.currentPlayer == PlayerID.Player)
            this.transform.position = eventData.position + offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (TurnManager.Instance.currentPlayer == PlayerID.Player)
        {
            RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector2.zero);

            // Card in DropArea
            if (hit.collider != null && hit.collider.CompareTag("DropArea") && gm.player.Cost >= card.Cost)
            {
                gm.player.Cost -= card.Cost;

                dm.UseCardAnimation(this.gameObject, card, cm.graveArea);
            }

            // Card Not in DropArea
            else
            {
                if (isUsed)
                    dm.SetHandCardPositionAnimation(cm.handList, cm.handArea.transform);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        thisChildIndex = this.transform.GetSiblingIndex();

        dm.PointedCardAnimation(this.gameObject, magnifiedCardScale, 0.15f);

        this.transform.SetParent(canvasT);

        gm.dummy.SetActive(true);
        gm.dummy.transform.SetParent(previousParentT);
        gm.dummy.transform.SetSiblingIndex(thisChildIndex);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Card Not Used
        gm.dummy.transform.SetParent(canvasT);
        gm.dummy.SetActive(false);

        this.transform.SetParent(previousParentT);
        this.transform.SetSiblingIndex(thisChildIndex);

        dm.PointedCardAnimation(this.gameObject, restoredCardScale, 0.15f);
    }
}
