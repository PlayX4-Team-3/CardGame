using AllCharacter;
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

public class Cards : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    private CardManager cm;
    private TurnManager tm;

    public int cardID;
    public int cc; // cost
    public CardType ct; // att def utilty
    public CardAttribute ca; // rock sissors paper

    public int cardPower; // att(2damage), def(2shield), util(+2 or -2)...

    private Vector2 startPos;
    private Vector2 offset;

    private Player player;

    private int childIndex;

    private void Start()
    {
        cm = CardManager.Instance;
        tm = TurnManager.Instance;

        player = GameObject.Find("Player").GetComponent<Player>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (tm.currentPlayer == PlayerID.Player)
        {
            cm.isClick = true;

            startPos = this.transform.position;
            offset = startPos - (Vector2)eventData.position;

            this.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            childIndex = this.transform.GetSiblingIndex();
            this.transform.SetAsLastSibling();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (tm.currentPlayer == PlayerID.Player)
            this.transform.position = eventData.position + offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (tm.currentPlayer == PlayerID.Player)
        {
            RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag("DropArea") && player.Cost >= cc)
            {
                cm.HandToGrave(this.gameObject);
                player.Cost -= cc;

                GameManager.Instance.UseCard(this.gameObject.GetComponent<Cards>());
            }

            else
            {
                this.transform.SetSiblingIndex(childIndex);
                this.transform.position = startPos;
            }

            cm.isClick = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (cm.isClick == false)
            this.transform.localScale = new Vector3(this.transform.localScale.x * 1.3f, this.transform.localScale.y * 1.3f, 1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (cm.isClick == false)
            this.transform.localScale = new Vector3(0.7f, 0.7f, 1f);
    }

    private void OnEnable()
    {
        this.transform.localScale = new Vector3(0.7f, 0.7f, 1f);
    }
}
