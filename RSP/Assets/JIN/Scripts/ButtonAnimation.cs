using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ButtonAnimation : MonoBehaviour
{
    public Button btn, ntb;

    private bool isUp = false;
    private bool isMouseExit = false;

    private void Update()
    {
        if (isUp)
        {
            btn.GetComponent<Image>().fillAmount += 3f * Time.deltaTime;
            if (btn.GetComponent<Image>().fillAmount >= 1f)
            {
                btn.GetComponent<Image>().fillAmount = 1f;
                isUp = false;
            }
            ntb.GetComponent<Image>().fillAmount += 3f * Time.deltaTime;
            if (ntb.GetComponent<Image>().fillAmount >= 1f)
            {
                ntb.GetComponent<Image>().fillAmount = 1f;
                isUp = false;
            }
        }

        if (isMouseExit)
        {
            btn.GetComponent<Image>().fillAmount -= 3f * Time.deltaTime;
            if (btn.GetComponent<Image>().fillAmount <= 0f)
            {
                btn.GetComponent<Image>().fillAmount = 0f;
                isMouseExit = false;
            }
            ntb.GetComponent<Image>().fillAmount -= 3f * Time.deltaTime;
            if (ntb.GetComponent<Image>().fillAmount <= 0f)
            {
                ntb.GetComponent<Image>().fillAmount = 0f;
                isMouseExit = false;
            }
        }

    }

    public void ASD()
    {
        isUp = true;
        isMouseExit = false;
    }

    public void QWE()
    {
        isMouseExit = true;
        isUp = false;
    }
}
