using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class DotweenManager : Singleton<DotweenManager>
{
    private SpellImageMaker sm;
    private GameManager gm;

    private void Start()
    {
        sm = SpellImageMaker.Instance;
        gm = GameManager.Instance;
    }


    // Character Animation Part
    public void AttackAnim(GameObject target, float duration = 0.5f)
    {
        target.gameObject.transform.DOShakePosition(duration);
    }

    public void DefenseAnim(GameObject target)
    {
        if (target.name == "Player")
            target.transform.DOScale(new Vector3(1.8f, 1.8f, 1f), 0.3f).OnComplete(() => target.transform.DOScale(new Vector3(1.5f, 1.5f, 1f), 0.3f));

        else
            target.transform.DOScale(new Vector3(2.3f, 2.3f, 1f), 0.3f).OnComplete(() => target.transform.DOScale(new Vector3(2f, 2f, 1f), 0.3f));
    }


    // Spell Animation Part
    public void HalfLifeDamage(GameObject spellObj)
    {
        spellObj.transform.position = new Vector3(-4f, 0.3f, 0f);
        spellObj.transform.rotation = Quaternion.Euler(new Vector3(0f, 66f, 0f));

        spellObj.transform.DOScale(Vector3.one, 0.5f).SetLoops(2, LoopType.Yoyo).OnComplete(() => spellObj.SetActive(false));
    }

    public void BreakDefense(GameObject spellObj, GameObject target)
    {
        spellObj.transform.localScale = new Vector3(0f, 1f, 1f);

        AttackAnim(target, 0.5f);
        spellObj.transform.DOScaleX(15f, 0.8f).OnComplete(() =>
        {
            spellObj.GetComponent<SpriteRenderer>().material.DOFade(0f, 0.8f).OnComplete(() =>
            {
                Color color = spellObj.GetComponent<SpriteRenderer>().material.color;
                color.a = 1;

                spellObj.GetComponent<SpriteRenderer>().material.color = color;
                spellObj.SetActive(false);
            });
        });
    }

    public void ReflectBuff(GameObject spellObj)
    {
        spellObj.transform.position += new Vector3(1f, 0.3f, 0f);
        spellObj.transform.rotation = Quaternion.Euler(new Vector3(0f, 66f, 0f));

        spellObj.transform.DOScale(Vector3.one, 0.5f).SetLoops(2, LoopType.Yoyo).OnComplete(() => spellObj.SetActive(false));
    }

    public void MagicBallAnimaition(GameObject target, GameObject spellObj, int spellType = 0)
    {
        if (spellType == 0)
        {
            spellObj.transform.DOScale(new Vector3(0.5f, 0.5f, 1f), 0.3f).OnComplete(() =>
          {
              spellObj.transform.DOMove(target.transform.position, 0.5f).OnComplete(() =>
              {
                  spellObj.GetComponent<SpriteRenderer>().material.DOFade(0f, 0.3f).OnComplete(() =>
                  {
                      Color color = spellObj.GetComponent<SpriteRenderer>().material.color;
                      color.a = 1;

                      spellObj.GetComponent<SpriteRenderer>().material.color = color;
                      spellObj.SetActive(false);
                  });

                  AttackAnim(target, 0.5f);
              });
          });
        }

        if (spellType == 1)
        {
            spellObj.transform.DOScale(new Vector3(0.5f, 0.5f, 1f), 0.3f).OnComplete(() =>
            {
                spellObj.transform.DOMoveY(target.transform.position.y + 3f, 0.25f).SetEase(Ease.OutQuad).OnComplete(() => { spellObj.transform.DOMoveY(target.transform.position.y, 0.25f).SetEase(Ease.InQuad); });
                spellObj.transform.DOMoveX(target.transform.position.x, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    spellObj.GetComponent<SpriteRenderer>().material.DOFade(0f, 0.3f).OnComplete(() =>
                    {
                        Color color = spellObj.GetComponent<SpriteRenderer>().material.color;
                        color.a = 1;

                        spellObj.GetComponent<SpriteRenderer>().material.color = color;
                        spellObj.SetActive(false);
                    });

                    AttackAnim(target, 0.5f);
                });
            });
        }
    }

    public void IcicleAnimation(GameObject spellObj, int tmp = 0) // 0 : 내가 공격했을 때 애니메이션, 1 : 카드 효과가 끝날 때 애니메이션
    {
        if (tmp == 0)
            spellObj.transform.DOScale(Vector3.one * 0.5f, 0.3f);

        else
        {
            sm.spells[9].transform.DOScale(Vector3.zero, 0.5f).OnComplete(() =>
            {
                sm.spells[9].transform.localScale = Vector3.one;
                sm.spells[9].SetActive(false);
            });
        }
    }

    public void HealNManaAnimation(GameObject spellObj, GameObject target)
    {
        spellObj.transform.localScale = Vector3.one;
        spellObj.transform.DOMove(target.gameObject.transform.position + (Vector3.up * 2f), 2f);
        spellObj.GetComponent<SpriteRenderer>().material.DOFade(0f, 2f).OnComplete(() =>
        {
            Color color = spellObj.GetComponent<SpriteRenderer>().material.color;
            color.a = 1;

            spellObj.GetComponent<SpriteRenderer>().material.color = color;
            spellObj.SetActive(false);
        });
    }


    // Card Animation Part
    public void UseCardAnimation(GameObject go, Card card)
    {
        GameManager.Instance.UseCard(card);

        go.transform.DOScale(Vector3.zero, 1f);
        go.transform.DORotate(new Vector3(0f, 0f, -360f), 0.25f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
        go.transform.DOMove(CardManager.Instance.graveArea.transform.position, 1f).OnComplete(() =>
        {
            CardManager.Instance.HandToGrave(go);

            go.transform.DOKill();
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
            go.transform.DOKill();
            CardManager.Instance.SetHandCardPosition();
        });
    }

    public void SetHandCardPositionAnimation(List<GameObject> handList, Transform handArea, float duration = 0.2f)
    {
        int mid = handList.Count / 2;

        for (int i = 0; i < handList.Count; i++)
        {
            if (handList.Count == 1)
                handList[i].transform.DOMove(handArea.position, duration);

            else if (handList.Count % 2 == 1)
            {
                if (i < mid)
                    handList[i].transform.DOMove(handArea.transform.position + ((Vector3.left * 124f) * Mathf.Abs(i - mid)), duration);
                else if (i == mid)
                    handList[i].transform.DOMove(handArea.transform.position, duration);
                else
                    handList[i].transform.DOMove(handArea.transform.position - ((Vector3.left * 124f) * Mathf.Abs(i - mid)), duration);
            }

            else
            {
                if (i < mid)
                    handList[i].transform.DOMove(handArea.transform.position + ((Vector3.left * 124f) * (Mathf.Abs(i - mid) - 0.5f)), duration);
                else
                    handList[i].transform.DOMove(handArea.transform.position - ((Vector3.left * 124f) * (Mathf.Abs(i - mid) + 0.5f)), duration);
            }
        }
    }
}
