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
                Debug.Log("적의 카드 입니다.");
                break;

            default:
                Debug.LogError("잘못된 타입의 카드 입니다.");
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

        // 독구슬 109번 만들어줘야함
        if ((id % 100) == 9)
        {
            enemy.is109Debuff = true;
        }

        player.AttackAnim(enemy.gameObject, 0.5f);
    }

    private void Defense(int id, int dfigure)
    {
        player.Defense_Figures += dfigure;

        // 반사 207번 만들어야함
        if ((id % 100) == 7)
            Debug.Log("Reflect");

        player.DefenseAnim();
    }

    private void Utility(Card card, int key)
    {
        switch(key)
        {
            case 1:
                // 코스트 회복
                player.Cost += card.Power;

                for (int i = 0; i < player.Cost; i++)
                    GameManager.Instance.playerCostImg[i].gameObject.SetActive(true);
                break;
            case 2:
                // 적 공격 무효화
                GameManager.Instance.canEAttack = false;
                break;
            case 3:
                // 드로우
                CardManager.Instance.DrawCard(card.Power);
                break;
            case 4:
                // 드로우
                CardManager.Instance.DrawCard(card.Power);
                break;
            case 5:
                // 힐
                player.Hp += card.Power;
                break;
            case 6:
                // 적 방어도 파괴
                enemy.Defense_Figures = 0;
                break;
            case 7:
                // 적 행동 봉인
                break;
            case 8:
                // 받는 피해 감소 2회 버프
                break;
        }
    }
}
