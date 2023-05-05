using AllCharacter;
using DG.Tweening;
using UnityEngine;

public class CardAbility : Singleton<CardAbility>
{
    private SpellImageMaker sm;
    private DotweenManager dm;

    private Player player;
    private Enemy enemy;

    private GameObject spellObj;

    public GameObject Att1;
    public GameObject Att2;
    public GameObject Def1;

    private void Start()
    {
        sm = SpellImageMaker.Instance;
        dm = DotweenManager.Instance;

        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        enemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
    }

    public void UseCard(Card card)
    {
        

        string type = card.Type;
        string attribute = card.Attribute;

        int typeNum = card.ID / 100;
        int detailTypeNum = card.ID % 100;

        switch (typeNum)
        {
            case 1:
                Attack(card.ID, card.Power);
                break;

            case 2:
                Defense(card.ID, card.Power);
                break;

            case 3:
                Utility(card, detailTypeNum);
                break;

            case 4:
                Debug.Log("적의 카드 입니다.");
                break;

            default:
                Debug.LogError("잘못된 타입의 카드 입니다.");
                break;
        }
    }

    private void Attack(int id, int damage)
    {
        bool isSpell = (id % 100) > 6 ? true : false;
        int spellNum = id % 100;
        GameObject spell;

        if (spellObj != null)
            spellObj = null;

        if (spellNum <= 3)
            spell = Instantiate(Att2);
        else
            spell = Instantiate(Att1);

        spell.transform.SetParent(player.gameObject.transform);
        spell.transform.position = player.transform.position + Vector3.right;

        if (enemy.Defense_Figures > 0)
        {
            enemy.Defense_Figures -= damage;

            if (enemy.Defense_Figures <= 0)
            {
                enemy.Hp = enemy.Hp + enemy.Defense_Figures;
                enemy.Defense_Figures = 0;
            }
        }

        else
            enemy.Hp -= damage;

        if (!isSpell)
        {
            spell.transform.DOScale(Vector3.one * 0.6f, 0.2f).OnComplete(() =>
            {
                spell.transform.DOMove(enemy.gameObject.transform.position, 0.5f).OnComplete(() =>
                {
                    dm.AttackAnim(enemy.gameObject, 0.5f);
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

        if (isSpell)
        {
            // 불덩이
            if (spellNum == 7)
            {
                spellObj = sm.spells[1];

                sm.SetSpell(player.gameObject, spellObj, 1.25f);

                dm.MagicBallAnimaition(enemy.gameObject, spellObj);
            }

            // 독구슬
            if (spellNum == 9)
            {
                spellObj = sm.spells[14];

                sm.SetSpell(player.gameObject, spellObj, 1.25f);

                dm.MagicBallAnimaition(enemy.gameObject, spellObj);
                enemy.is109Debuff = true;
            }

            // 폭탄
            if (spellNum == 8)
            {
                spellObj = sm.spells[20];

                sm.SetSpell(player.gameObject, spellObj, 1.25f);

                dm.MagicBallAnimaition(enemy.gameObject, spellObj, 1);
            }
        }
    }

    private void Defense(int id, int dfigure)
    {
        bool isSpell = (id % 100) > 6 ? true : false;
        int spellNum = id % 100;
        GameObject spell;
        if (spellObj != null)
            spellObj = null;

        if (!isSpell)
        {// 일반 방어 카드
            dm.DefenseAnim(player.gameObject);
            player.Defense_Figures += dfigure;

            spell = Instantiate(Def1);
            spell.transform.SetParent(player.gameObject.transform);
            spell.transform.position = player.transform.position + Vector3.right;

            spell.transform.DOScale(Vector3.one * 1f, 0.2f).SetLoops(2, LoopType.Yoyo).OnComplete(() =>
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
        // 반사 207번
        if (spellNum == 7)
        {
            spellObj = sm.spells[4];

            sm.SetSpell(player.gameObject, spellObj);
            player.have207buff = true;
        }

        dm.DefenseAnim(player.gameObject);
    }

    private void Utility(Card card, int key)
    {
        if (spellObj != null)
            spellObj = null;

        switch (key)
        {
            case 1:
                // 코스트 회복
                player.Cost += card.Power;

                for (int i = 0; i < player.Cost; i++)
                    GameManager.Instance.playerCostImg[i].gameObject.SetActive(true);

                spellObj = sm.spells[21];

                sm.SetSpell(player.gameObject, spellObj);
                dm.HealNManaAnimation(spellObj, player.gameObject);
                break;
            case 2:
                // 적 공격 무효화
                enemy.isbind = true;
                spellObj = sm.spells[15];

                sm.SetSpell(enemy.gameObject, spellObj);
                dm.BindAttack(spellObj, enemy.gameObject);
                break;
            case 3:
            case 4:
                // 드로우
                CardManager.Instance.DrawCard(card.Power);
                break;
            case 5:
                // 힐
                player.Hp += card.Power;

                spellObj = sm.spells[12];

                sm.SetSpell(player.gameObject, spellObj);
                dm.HealNManaAnimation(spellObj, player.gameObject);

                break;
            case 6:
                // 적 방어도 파괴
                enemy.Defense_Figures = 0;
                spellObj = sm.spells[8];

                sm.SetSpell(player.gameObject, spellObj);
                dm.BreakDefense(spellObj, enemy.gameObject);
                break;
            case 7:
                // 적 행동 봉인
                enemy.is307Debuff = true;
                spellObj = sm.spells[9];

                sm.SetSpell(enemy.gameObject, spellObj);

                spellObj.transform.DOScale(Vector3.one * 0.5f, 0.3f);
                break;
            case 8:
                // 받는 피해 감소 2회 버프
                player.have308buff = true;
                spellObj = sm.spells[17];

                sm.SetSpell(player.gameObject, spellObj);

                dm.HalfLifeDamage(spellObj);
                break;
        }
    }
}
