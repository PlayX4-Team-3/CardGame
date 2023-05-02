using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    GameManager gm;
    CardManager cm;

    private Transform canvasT;
    private Transform previousParentT;

    private Vector2 startPos;
    private Vector2 offset;

    private Vector2 magnifiedCardScale;
    private Vector2 restoredCardScale;

    private int thisChildIndex;

    private Card card;

    private bool isUsed;

    private Tween tweenMove;
    private Tween tweenScale;
    private Tween tweenRotate;

    private void Start()
    {
        gm = GameManager.Instance;
        cm = CardManager.Instance;

        canvasT = GameObject.FindWithTag("Canvas").transform;
        previousParentT = GameObject.FindWithTag("Hand").transform;

        magnifiedCardScale = new Vector2(0.8f, 0.8f);
        restoredCardScale = new Vector2(0.6f, 0.6f);

        card = cm.cardDeck[int.Parse(this.name)];

        DOTween.Init(false, true, LogBehaviour.Default);
    }

    // ?????? ???????? ?? ???? ?????????? ???????? ?? ??
    private void OnDisable()
    {
        isUsed = false;

        tweenMove.Kill();
        tweenScale.Kill();
        tweenRotate.Kill();

        this.transform.localScale = restoredCardScale;
        this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (TurnManager.Instance.currentPlayer == PlayerID.Player)
        {
            startPos = this.transform.position;
            offset = startPos - eventData.position;

            this.transform.SetParent(canvasT);
            this.transform.SetAsLastSibling();
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
                isUsed = true;

                gm.player.Cost -= card.Cost;

                tweenScale = this.transform.DOScale(Vector3.zero, 1f);
                tweenRotate = this.transform.DORotate(new Vector3(0f, 0f, -360f), 0.25f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
                tweenMove = this.transform.DOMove(cm.graveArea.transform.position, 1f).OnComplete(() =>
                {
                    gm.UseCard(card);
                    cm.HandToGrave(this.gameObject);

                    gm.dummy.transform.SetParent(canvasT);

                    cm.SetHandCardPosition();
                });
            }

            // Card Not in DropArea
            else
            {
                isUsed = true;

                tweenScale = this.transform.DOScale(restoredCardScale, 0.15f);
                tweenMove = this.transform.DOMove(startPos, 0.15f).OnComplete(() =>
                {
                    gm.dummy.transform.SetParent(canvasT);
                    gm.dummy.SetActive(false);

                    this.transform.SetParent(previousParentT);
                    this.transform.SetSiblingIndex(thisChildIndex);

                    isUsed = false;
                });
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isUsed)
        {
            thisChildIndex = this.transform.GetSiblingIndex();

            tweenScale = this.transform.DOScale(magnifiedCardScale, 0.25f);

            this.transform.SetParent(canvasT);

            gm.dummy.SetActive(true);
            gm.dummy.transform.SetParent(previousParentT);
            gm.dummy.transform.SetSiblingIndex(thisChildIndex);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Card Not Used
        if (!isUsed)
        {
            gm.dummy.transform.SetParent(canvasT);
            gm.dummy.SetActive(false);

            this.transform.SetParent(previousParentT);
            this.transform.SetSiblingIndex(thisChildIndex);

            tweenScale = this.transform.DOScale(restoredCardScale, 0.25f);
        }
    }
}
