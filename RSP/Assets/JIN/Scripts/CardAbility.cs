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

        Debug.Log(typeNum);
        Debug.Log(detailTypeNum);

        switch(typeNum)
        {
            case 1:
                Attack(card.Power);
                break;

            case 2:
                Defense(card.Power);
                break;

            case 3:
                Utility(detailTypeNum);
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

    private void Utility(int key)
    {
        Debug.Log("Utility!");
    }

}
