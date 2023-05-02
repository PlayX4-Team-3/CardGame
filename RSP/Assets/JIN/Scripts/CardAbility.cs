using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AllCharacter;
using DG.Tweening;

public class CardAbility : Singleton<CardAbility>
{
    private Player player;
    private Enemy enemy;

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

        // ������ 109�� ����������
        if ((id % 100) == 9)
        {
            enemy.is109Debuff = true;
        }

        player.AttackAnim(enemy.gameObject, 0.5f);
    }

    private void Defense(int id, int dfigure)
    {
        player.Defense_Figures += dfigure;

        // �ݻ� 207�� ��������
        if ((id % 100) == 7)
            Debug.Log("Reflect");

        player.DefenseAnim();
    }

    private void Utility(Card card, int key)
    {
        switch(key)
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
                break;
            case 6:
                // �� �� �ı�
                enemy.Defense_Figures = 0;
                break;
            case 7:
                // �� �ൿ ����
                break;
            case 8:
                // �޴� ���� ���� 2ȸ ����
                break;
        }
    }
}
