using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class DotweenManager : Singleton<DotweenManager>
{


    // Character Animation Part

    // Card Animation Part
    public void UseCardAnimation(GameObject go, Card card)
    {
        GameManager.Instance.UseCard(card);

        go.transform.DOScale(Vector3.zero, 1f);
        go.transform.DORotate(new Vector3(0f, 0f, -360f), 0.25f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
        go.transform.DOMove(CardManager.Instance.graveArea.transform.position, 1f).OnComplete(() =>
        {
            CardManager.Instance.HandToGrave(go);
        });
    }

    public void PointedCardAnimation(GameObject go, Vector3 scale, float duration)
    {
        go.transform.DOScale(scale, duration);
    }

    public void DrawCardAnimation(GameObject go, Transform handArea, float duration)
    {
        go.transform.DOMove(handArea.parent.transform.position, duration).OnComplete(() =>
        {
            CardManager.Instance.SetHandCardPosition();
        });
    }

    public void SetHandCardPositionAnimation(List<GameObject> handList, Transform handArea, float duration = 0.2f)
    {
        Sequence sequence = DOTween.Sequence();
        Tween[] tweens = new Tween[handList.Count];
        
        int mid = handList.Count / 2;

        for (int i = 0; i < handList.Count; i++)
        {
            if (handList.Count == 1)
            {
                tweens[i] = handList[i].transform.DOMove(handArea.position, duration);
                sequence.Insert(0f, tweens[i]);
            }

            else if (handList.Count % 2 == 1)
            {
                if (i < mid)
                    tweens[i] = handList[i].transform.DOMove(handArea.transform.position + ((Vector3.left * 124f) * Mathf.Abs(i - mid)), duration);
                else if (i == mid)
                    tweens[i] = handList[i].transform.DOMove(handArea.transform.position, duration);
                else
                    tweens[i] = handList[i].transform.DOMove(handArea.transform.position - ((Vector3.left * 124f) * Mathf.Abs(i - mid)), duration);

                sequence.Insert(0f, tweens[i]);
            }

            else
            {
                if (i < mid)
                    tweens[i] = handList[i].transform.DOMove(handArea.transform.position + ((Vector3.left * 124f) * (Mathf.Abs(i - mid) - 0.5f)), duration);
                else
                    tweens[i] = handList[i].transform.DOMove(handArea.transform.position - ((Vector3.left * 124f) * (Mathf.Abs(i - mid) + 0.5f)), duration);

                sequence.Insert(0f, tweens[i]);
            }
        }
        
        sequence.Play();
    }
}
