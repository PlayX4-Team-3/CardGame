using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum State { start, playerTurn, enemyTurn, win, lose}

    public State state;
    public bool EnemyIsLive; //적 생존 여부
    [Header("Player")]
    public bool isLive; //나의 생존 여부
    public int currentHP;
    public int maxHP=100; 

    void Awake()
    {
        state = State.start; //전투 시작 알림
        Debug.Log("게임 시작");
        StartFight(); 
    }

    void StartFight()
    {
        state = State.playerTurn; //플레이어의 턴으로 시작
        Debug.Log("플레이어의 턴"); 
    }

    public void Attack()
    {
        if (state != State.playerTurn) //버튼이 계속 눌리는걸 방지
        {
            return; 
        }
        StartCoroutine(PlayerAttack()); 
    }

    IEnumerator PlayerAttack()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("나의 공격");

        //데미지 코드 작성

        if (!EnemyIsLive)
        {
            state = State.win;
            EndBattle(); 
        }
        else
        {
            Debug.Log("적의 턴"); 
            state = State.enemyTurn;
            StartCoroutine(EnemyAttack()); 
        }
    }

    IEnumerator EnemyAttack()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("적의 공격"); 

        if(!isLive)
        {
            state = State.lose;
            EndBattle(); 
        }
        else
        {
            Debug.Log("나의 턴"); 
            state = State.playerTurn; 
        }
    }

    void EndBattle()
    {
        
    }
}
