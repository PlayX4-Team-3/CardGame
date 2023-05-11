using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPSMoving : MonoBehaviour
{
    public bool isUsed = false;

    private void OnEnable()
    {
        isUsed = false;

        RectTransform rt = this.GetComponent<RectTransform>();

        rt.anchoredPosition3D = Vector3.zero;
        rt.localScale = Vector2.one * 0.18f;

        DotweenManager.Instance.RPSMove(this.gameObject);
    }

    public void UseRPS()
    {
        if (isUsed == false)
            DotweenManager.Instance.RPSUse(this.gameObject);
    }
}
