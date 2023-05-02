using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AllCharacter;
//using DG.Tweening;

public class CardAbility : Singleton<CardAbility>
{
    private Player player;
    private Enemy enemy;

    private GameObject spellObj;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        enemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
    }

    public void UseCard(Card card)
    {
        string type = card.Type;
        string attribute = card.Attribute;

        int typeNum = card.ID / 100;
        int detailTypeNum = card.ID % 100;

        switch(typeNum)
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
                Debug.Log("���� ī�� �Դϴ�.");
                break;

            default:
                Debug.LogError("�߸��� Ÿ���� ī�� �Դϴ�.");
                break;
        }
    }

    private void Attack(int id, int damage)
    {
        bool isSpell = (id % 100) > 6 ? true : false;

        //Debug.Log(isSpell);

        if (spellObj != null)
            spellObj = null;

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

        // �ҵ���
        if((id % 100) == 7)
        {
            spellObj = SpellImageMaker.Instance.spells[1];

            SpellImageMaker.Instance.SetSpell(player.gameObject, spellObj);

            spellObj.transform.position += Vector3.right;
            //////////////////////spellObj.transform.DOScale(new Vector3(0.5f, 0.5f, 1f), 0.1f).OnComplete(() =>
            //////////////////////{
            //////////////////////    spellObj.transform.DOMove(enemy.transform.position, 0.4f).OnComplete(() =>
            //////////////////////    {
            //////////////////////        spellObj.GetComponent<SpriteRenderer>().material.DOFade(0f, 0.3f).OnComplete(() =>
            //////////////////////        {
            //////////////////////            Color color = spellObj.GetComponent<SpriteRenderer>().material.color;
            //////////////////////            color.a = 1;

            //////////////////////            spellObj.GetComponent<SpriteRenderer>().material.color = color;
            //////////////////////            spellObj.SetActive(false);
            //////////////////////        });

            //////////////////////        player.AttackAnim(enemy.gameObject, 0.5f);
            //////////////////////    });
            //////////////////////});
        }

        // ������ 109�� ����������
        if ((id % 100) == 9)
        {
            spellObj = SpellImageMaker.Instance.spells[14];

            SpellImageMaker.Instance.SetSpell(player.gameObject, spellObj);

            spellObj.transform.position += Vector3.right * 0.5f;
            ////////////////////spellObj.transform.DOScale(new Vector3(0.5f, 0.5f, 1f), 0.3f).OnComplete(() =>
            ////////////////////{
            ////////////////////    spellObj.transform.DOMove(enemy.transform.position, 0.5f).OnComplete(() =>
            ////////////////////    {
            ////////////////////        spellObj.GetComponent<SpriteRenderer>().material.DOFade(0f, 0.3f).OnComplete(() =>
            ////////////////////        {
            ////////////////////            Color color = spellObj.GetComponent<SpriteRenderer>().material.color;
            ////////////////////            color.a = 1;

            ////////////////////            spellObj.GetComponent<SpriteRenderer>().material.color = color;
            ////////////////////            spellObj.SetActive(false);
            ////////////////////        });

            ////////////////////        player.AttackAnim(enemy.gameObject, 0.5f);
            ////////////////////    });
            ////////////////////});

            enemy.is109Debuff = true;
        }

        //if (!isSpell)
            ////////////////////player.AttackAnim(enemy.gameObject, 0.5f);
    }

    private void Defense(int id, int dfigure)
    {
        player.Defense_Figures += dfigure;

        // �ݻ� 207�� ��������
        if ((id % 100) == 7)
            Debug.Log("Reflect");

        ////////////////player.DefenseAnim();
    }

    private void Utility(Card card, int key)
    {
        if (spellObj != null)
            spellObj = null;

        switch (key)
        {
            case 1:
                // �ڽ�Ʈ ȸ��
                player.Cost += card.Power;

                for (int i = 0; i < player.Cost; i++)
                    GameManager.Instance.playerCostImg[i].gameObject.SetActive(true);
                break;
            case 2:
                // �� ���� ��ȿȭ
                GameManager.Instance.canEAttack = false;
                break;
            case 3:
                // ��ο�
                CardManager.Instance.DrawCard(card.Power);
                break;
            case 4:
                // ��ο�
                CardManager.Instance.DrawCard(card.Power);
                break;
            case 5:
                // ��
                player.Hp += card.Power;

                spellObj = SpellImageMaker.Instance.spells[12];

                SpellImageMaker.Instance.SetSpell(player.gameObject, spellObj);
                spellObj.transform.localScale = Vector3.one;
                ////////////////spellObj.transform.DOMove(player.gameObject.transform.position + (Vector3.up * 2f), 2f);
                ////////////////spellObj.GetComponent<SpriteRenderer>().material.DOFade(0f, 2f).OnComplete(() =>
                ////////////////{
                ////////////////    Color color = spellObj.GetComponent<SpriteRenderer>().material.color;
                ////////////////    color.a = 1;
                
                ////////////////    spellObj.GetComponent<SpriteRenderer>().material.color = color;
                ////////////////    spellObj.SetActive(false);
                ////////////////});

                break;
            case 6:
                // �� �� �ı�
                enemy.Defense_Figures = 0;
                break;
            case 7:
                // �� �ൿ ����
                enemy.is307Debuff = true;
                spellObj = SpellImageMaker.Instance.spells[9];

                SpellImageMaker.Instance.SetSpell(enemy.gameObject, spellObj);

                //////////////////spellObj.transform.DOScale(Vector3.one * 0.5f, 0.3f);
                break;
            case 8:
                // �޴� ���� ���� 2ȸ ����
                break;
        }
    }
}
