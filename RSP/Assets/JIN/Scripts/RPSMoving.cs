using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPSMoving : MonoBehaviour
{
    //public bool isUsed;

    private void OnEnable()
    {
        //isUsed = false;

        RectTransform rt = this.GetComponent<RectTransform>();

        rt.anchoredPosition3D = Vector3.zero;
        rt.localScale = Vector2.one * 0.18f;

        DotweenManager.Instance.RPSMove(this.gameObject);
    }

    //private void OnDisable()
    //{
    //    isUsed = false;
    //}

    public void UseRPS()
    {
        //isUsed = true;

        DotweenManager.Instance.RPSUse(this.gameObject);
    }
}
