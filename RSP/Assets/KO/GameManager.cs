using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum State { start, playerTurn, enemyTurn, win, lose}

    public State state;
    public bool EnemyIsLive; //�� ���� ����
    [Header("Player")]
    public bool isLive; //���� ���� ����
    public int currentHP;
    public int maxHP=100; 

    void Awake()
    {
        state = State.start; //���� ���� �˸�
        Debug.Log("���� ����");
        StartFight(); 
    }

    void StartFight()
    {
        state = State.playerTurn; //�÷��̾��� ������ ����
        Debug.Log("�÷��̾��� ��"); 
    }

    public void Attack()
    {
        if (state != State.playerTurn) //��ư�� ��� �����°� ����
        {
            return; 
        }
        StartCoroutine(PlayerAttack()); 
    }

    IEnumerator PlayerAttack()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("���� ����");

        //������ �ڵ� �ۼ�

        if (!EnemyIsLive)
        {
            state = State.win;
            EndBattle(); 
        }
        else
        {
            Debug.Log("���� ��"); 
            state = State.enemyTurn;
            StartCoroutine(EnemyAttack()); 
        }
    }

    IEnumerator EnemyAttack()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("���� ����"); 

        if(!isLive)
        {
            state = State.lose;
            EndBattle(); 
        }
        else
        {
            Debug.Log("���� ��"); 
            state = State.playerTurn; 
        }
    }

    void EndBattle()
    {
        
    }
}
