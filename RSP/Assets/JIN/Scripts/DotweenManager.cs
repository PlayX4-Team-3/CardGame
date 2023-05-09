using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

using AllCharacter;

public class DotweenManager : Singleton<DotweenManager>
{
    private SpellImageMaker sm;
    private GameManager gm;

    private void Start()
    {
        sm = SpellImageMaker.Instance;
        gm = GameManager.Instance;

        DOTween.SetTweensCapacity(1000, 50);
    }

    // RPS 움직임
    public void RPSMove(GameObject go)
    {
        go.transform.DOKill();
        Vector3 origin = go.transform.localScale;
        go.transform.localScale = Vector3.zero;

        go.transform.DOScale(origin, 0.5f);
        go.transform.DOMoveY(go.transform.position.y + 50f, 1f).SetLoops(-1, LoopType.Yoyo);
    }
    public void RPSUse(GameObject go)
    {
        go.transform.DOKill();
        go.transform.DOMoveY(go.transform.position.y + 220f, 1f).OnComplete(() =>
        {
            go.transform.DOScale(Vector3.zero, 0.5f).OnComplete(() =>
            {
                go.SetActive(false);
            });
        });
    }

    // Character Animation Part
    public void AttackAnim(GameObject target, float duration = 0.5f)
    {
        target.gameObject.transform.DOShakePosition(duration);
    }

    public void AttackAura(GameObject target, GameObject spell, int isPlayerAttack = 1)
    {
        spell.transform.DOScale(Vector3.one * 0.6f * isPlayerAttack, 0.2f).OnComplete(() =>
        {
            SoundManager.Instance.SFXPlay("Swing");
            spell.transform.DOMove(target.gameObject.transform.position, 0.5f).OnComplete(() =>
            {
                AttackAnim(target.gameObject);

                spell.GetComponent<SpriteRenderer>().material.DOFade(0f, 0.3f).OnComplete(() =>
                {
                    Color color = spell.GetComponent<SpriteRenderer>().material.color;
                    color.a = 1;

                    spell.GetComponent<SpriteRenderer>().material.color = color;
                    Destroy(spell);
                });
            });
        });
    }

    public void DefenseAnim(GameObject target, GameObject spell, int isPlayerDefence = 1)
    {
        if (target.tag == "Player")
            target.transform.DOScale(new Vector3(1.8f, 1.8f, 1f), 0.3f).OnComplete(() => target.transform.DOScale(new Vector3(1.5f, 1.5f, 1f), 0.3f));

        else
            target.transform.DOScale(new Vector3(2.3f, 2.3f, 1f), 0.3f).OnComplete(() => target.transform.DOScale(new Vector3(2f, 2f, 1f), 0.3f));

        spell.transform.DOScale(Vector3.one * isPlayerDefence, 0.2f).SetLoops(2, LoopType.Yoyo).OnComplete(() =>
        {
            spell.GetComponent<SpriteRenderer>().material.DOFade(0f, 0.3f).OnComplete(() =>
            {
                Color color = spell.GetComponent<SpriteRenderer>().material.color;
                color.a = 1;

                spell.GetComponent<SpriteRenderer>().material.color = color;
                Destroy(spell);
            });
        });
    }


    // Spell Animation Part
    public void BindAttack(GameObject spellObj, GameObject target, int attacker = 1)
    {
        spellObj.transform.localScale = Vector3.right;
        spellObj.transform.localPosition = new Vector3(0f, -0.5f * attacker, 0f);
        spellObj.transform.rotation = Quaternion.Euler(new Vector3(0f, 33f, 0f));
        spellObj.transform.DOScaleY(1f, 1f);
        SoundManager.Instance.SFXPlay("Wood");
    }

    public void EndBind(GameObject target)
    {
        GameObject spell = target.transform.Find("15").gameObject;
        GameObject papa = target.gameObject;
        spell.transform.SetParent(papa.transform.parent);

        spell.transform.DOScaleY(0f, 1f).OnComplete(() =>
        {
            spell.transform.SetParent(papa.transform);
            spell.SetActive(false);
        });
    }

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
        SoundManager.Instance.SFXPlay("Beam");
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
        SoundManager.Instance.SFXPlay("Crash");
    }

    public void MagicBallAnimaition(GameObject target, GameObject spellObj, int spellType = 0)
    {
        if (spellType == 0)
        {
            spellObj.transform.DOScale(new Vector3(0.5f, 0.5f, 1f), 0.3f).OnComplete(() =>
          {
              SoundManager.Instance.SFXPlay("FireBall");
              spellObj.transform.DOMove(target.transform.position, 0.5f).OnComplete(() =>
              {
                  spellObj.GetComponent<SpriteRenderer>().material.DOFade(0f, 0.3f).OnComplete(() =>
                  {
                      Color color = spellObj.GetComponent<SpriteRenderer>().material.color;
                      color.a = 1;

                      spellObj.GetComponent<SpriteRenderer>().material.color = color;
                      spellObj.SetActive(false);
                  });

                  AttackAnim(target);
              });
          });
        }

        if (spellType == 1)
        {
            spellObj.transform.DOScale(new Vector3(0.5f, 0.5f, 1f), 0.3f).OnComplete(() =>
            {
                SoundManager.Instance.SFXPlay("Boom");
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
        {
            spellObj.transform.DOScale(Vector3.one * 0.5f, 0.3f);
            SoundManager.Instance.SFXPlay("Ice");
        }

        else
        {
            sm.spells[9].transform.DOScale(Vector3.zero, 0.5f).OnComplete(() =>
            {
                SoundManager.Instance.SFXPlay("Ice");
                sm.spells[9].transform.localScale = Vector3.one;
                sm.spells[9].SetActive(false);
            });
        }
    }

    public void HealNManaAnimation(GameObject spellObj, GameObject target)
    {
        SoundManager.Instance.SFXPlay("Drink");
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
    private void LookAtTarget(Transform self, Transform target, int drawing = -1)
    {
        Vector3 targetPosition = target.position;
        targetPosition.z = self.position.z;
        Vector3 direction = (targetPosition - self.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);

        self.DORotate(targetRotation.eulerAngles * drawing, 0.5f);
    }

    public void UseCardAnimation(GameObject go, Card card, Transform target)
    {
        GameManager.Instance.UseCard(card);

        CardManager.Instance.handList.Remove(go);
        CardManager.Instance.graveList.Add(go);

        go.transform.DOScale(Vector3.zero, 1f).OnUpdate(() => LookAtTarget(go.transform, target));
        go.transform.DOMove(CardManager.Instance.graveArea.transform.position, 1f).OnComplete(() =>
        {
            CardManager.Instance.HandToGrave(go);
            this.transform.localScale = new Vector3(0.6f, 0.6f, 0f);
            go.transform.localRotation = Quaternion.identity;
            go.transform.DOKill();

            SetHandCardPositionAnimation(CardManager.Instance.handList, CardManager.Instance.handArea.transform);
        });
    }

    public void PointedCardAnimation(GameObject go, Vector3 scale, float duration)
    {
        go.transform.DOScale(scale, duration);
    }

    public void DrawCardAnimation(GameObject go, Transform handArea, float duration)
    {
        SoundManager.Instance.SFXPlay("CardShuffle");
        go.transform.DOMove(handArea.parent.transform.position, duration).OnUpdate(() => LookAtTarget(go.transform, handArea.parent, -1)).OnComplete(() =>
        {
            go.transform.DOKill();
            go.transform.DORotate(Vector3.zero, 0.2f);
            CardManager.Instance.SetHandCardPosition();
        });
    }

    public void SetHandCardPositionAnimation(List<GameObject> handList, Transform handArea, float duration = 0.2f)
    {
        int mid = handList.Count / 2;

        for (int i = 0; i < handList.Count; i++)
        {
            if (handList.Count % 2 == 1)
            {
                if (i < mid)
                    handList[i].transform.DOMove(handArea.position + ((Vector3.left * 124f * 1.5f) * Mathf.Abs(i - mid)), duration);
                else if (i == mid)
                    handList[i].transform.DOMove(handArea.position, duration);
                else
                    handList[i].transform.DOMove(handArea.position - ((Vector3.left * 124f * 1.5f) * Mathf.Abs(i - mid)), duration);
            }

            else
            {
                if (i < mid)
                    handList[i].transform.DOMove(handArea.position + ((Vector3.left * 124f * 1.5f) * (Mathf.Abs(i - mid) - 0.5f)), duration);
                else
                    handList[i].transform.DOMove(handArea.position - ((Vector3.left * 124f * 1.5f) * (Mathf.Abs(i - mid) + 0.5f)), duration);
            }
        }
    }
}
