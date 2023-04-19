using DG.Tweening;
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

    private Transform previousThisT = null;

    private bool isUsed;

    private Tween tweenMove;
    private Tween tweenScale;
    private Tween tweenRotate;

    private void Start()
    {
        jgm = JsonGameManager.Instance;
        jcm = JsonCardManager.Instance;

        canvasT = GameObject.FindWithTag("Canvas").transform;
        previousParentT = GameObject.FindWithTag("Hand").transform;

        magnifiedCardScale = new Vector2(0.8f, 0.8f);
        restoredCardScale = new Vector2(0.6f, 0.6f);

        card = jcm.cardDeck[int.Parse(this.name)];

        DOTween.Init(false, true, LogBehaviour.Default);
    }

    // 카드가 사용되고 난 뒤에 오브젝트가 비활성화 될 때
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

            // 카드가 사용 구역에서 사용되었을 때
            if (hit.collider != null && hit.collider.CompareTag("DropArea") && jgm.player.Cost >= card.Cost)
            {
                isUsed = true;

                jgm.player.Cost -= card.Cost;

                //// 사용한 Cost 만큼 이미지 비활성화
                //for (int i = jgm.player.MaxCost - 1; i >= jgm.player.Cost; i--)
                //    jgm.playerCostImg[i].gameObject.SetActive(false);

                tweenScale = this.transform.DOScale(Vector3.zero, 1f);
                tweenRotate = this.transform.DORotate(new Vector3(0f, 0f, -360f), 0.25f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
                tweenMove = this.transform.DOMove(jcm.graveArea.transform.position, 1f).OnComplete(() =>
                {
                    jgm.UseCard(card);
                    jcm.HandToGrave(this.gameObject);

                    jgm.dummy.transform.SetParent(canvasT);
                });
            }

            // 카드가 사용 구역에서 사용되지 않았을 때
            else
            {
                tweenScale = this.transform.DOScale(restoredCardScale, 0.15f);
                tweenMove = this.transform.DOMove(jgm.dummy.transform.position, 0.15f).OnComplete(() =>
                {
                    this.transform.SetParent(previousParentT);
                    this.transform.SetSiblingIndex(thisChildIndex);
                 
                    jgm.dummy.transform.SetParent(canvasT);
                });
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isUsed)
        {
            thisChildIndex = this.transform.GetSiblingIndex();

            tweenScale = this.transform.DOScale(magnifiedCardScale, 0.15f);
            this.transform.SetParent(canvasT);

            jgm.dummy.SetActive(true);
            jgm.dummy.transform.SetParent(previousParentT);
            jgm.dummy.transform.SetSiblingIndex(thisChildIndex);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // 카드가 사용되지 않았을 때만 실행
        if (!isUsed)
        {
            jgm.dummy.transform.SetParent(canvasT);
            jgm.dummy.SetActive(false);

            this.transform.SetParent(previousParentT);
            this.transform.SetSiblingIndex(thisChildIndex);

            tweenScale = this.transform.DOScale(restoredCardScale, 0.15f);
        }
    }
}
