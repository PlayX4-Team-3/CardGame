using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AllCharacter;

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
                Attack(card.Power);
                break;

            case 2:
                Defense(card.Power);
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

    private void Attack(int damage)
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
    }

    private void Defense(int dfigure)
    {
        player.Defense_Figures += dfigure;
    }

    private void Utility(Card card, int key)
    {
        switch(key)
        {
            case 1:
                // �ڽ�Ʈ ȸ��
                player.Cost += card.Power;

                for (int i = 0; i < player.Cost; i++)
                    JsonGameManager.Instance.playerCostImg[i].gameObject.SetActive(true);
                break;
            case 2:
                Attack(card.Power);
                break;
            case 3:
                // �� ���� ��ȿȭ
                JsonGameManager.Instance.canEAttack = false;
                break;
            case 4:
                Attack(card.Power);
                break;
            case 5:
            case 6:
                // ��ο�
                JsonCardManager.Instance.DrawCard(card.Power);
                break;

        }
    }

}
