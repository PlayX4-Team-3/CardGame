using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using DG.Tweening;

public class ButtonAnimation : MonoBehaviour
{
    public Button btn, ntb;
    public Image title;

    private bool isUp1 = false;
    private bool isMouseExit1 = false;

    private bool isUp2 = false;
    private bool isMouseExit2 = false;

    private void Start()
    {
        UIMoving();
    }

    private void Update()
    {
        if (isUp1)
        {
            btn.GetComponent<Image>().fillAmount += 3f * Time.deltaTime;
            if (btn.GetComponent<Image>().fillAmount >= 1f)
            {
                btn.GetComponent<Image>().fillAmount = 1f;
                isUp1 = false;
            }
        }

        if (isMouseExit1)
        {
            btn.GetComponent<Image>().fillAmount -= 3f * Time.deltaTime;
            if (btn.GetComponent<Image>().fillAmount <= 0f)
            {
                btn.GetComponent<Image>().fillAmount = 0f;
                isMouseExit1 = false;
            }
        }

        if (isUp2)
        {
            ntb.GetComponent<Image>().fillAmount += 3f * Time.deltaTime;
            if (ntb.GetComponent<Image>().fillAmount >= 1f)
            {
                ntb.GetComponent<Image>().fillAmount = 1f;
                isUp2 = false;
            }
        }

        if (isMouseExit2)
        {
            ntb.GetComponent<Image>().fillAmount -= 3f * Time.deltaTime;
            if (ntb.GetComponent<Image>().fillAmount <= 0f)
            {
                ntb.GetComponent<Image>().fillAmount = 0f;
                isMouseExit2 = false;
            }
        }

    }

    public void Btn1Up()
    {
        isUp1 = true;
        isMouseExit1 = false;
    }

    public void Btn1Exit()
    {
        isMouseExit1 = true;
        isUp1 = false;
    }

    public void Btn2Up()
    {
        isUp2 = true;
        isMouseExit2 = false;
    }

    public void Btn2Exit()
    {
        isMouseExit2 = true;
        isUp2 = false;
    }

    private void UIMoving()
    {
        title.transform.DOMove(title.transform.position + Vector3.up * 30f, 1f).SetLoops(-1, LoopType.Yoyo);
        //btn.transform.DOMove(btn.transform.position + Vector3.up * 30f, 1f).SetLoops(-1, LoopType.Yoyo);
        //ntb.transform.DOMove(ntb.transform.position + Vector3.up * 30f, 1f).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDestroy()
    {
        DOTween.KillAll();
    }
}
