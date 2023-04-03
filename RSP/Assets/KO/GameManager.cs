using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    private CardManager cm;
    private TurnManager tm;

    private GameObject player;
    private GameObject enemy;

    public Button BtnTurnEnd;

    private void Start()
    {
        cm = CardManager.Instance;
        tm = TurnManager.Instance;

        cm.DeckInstantiate();
        cm.DeckShuffle(cm.copiedPlayerDeck);

        tm.onTurnEnd.AddListener(OnTurnEnd);
        tm.StartTurn(PlayerID.Player);

        cm.Draw();

        player = GameObject.FindWithTag("Player");
        enemy = GameObject.FindWithTag("Enemy");

    }

    public void OnTurnEnd(PlayerID nextPlayer)
    {
        tm.StartTurn(nextPlayer);

        // �÷��̾� ���϶��� �� ���� ��ư Ȱ��ȭ
        if (nextPlayer == PlayerID.Player)
        {
            cm.Draw();
            BtnTurnEnd.interactable = true;
        }

        else
        {
            EnemyTurn();
            BtnTurnEnd.interactable = false;
        }
    }

    private void EnemyTurn()
    {
        int action = Random.Range(0, 3);
        switch (action)
        {
            case 0:
                EnemyAttack();
                break;
            case 1:
                EnemyDefense();
                break;
            case 2:
                EnemyUtility();
                break;
        }

        StartCoroutine(EnemyTurnEndDelay());
    }

    private IEnumerator EnemyTurnEndDelay()
    {
        yield return new WaitForSeconds(2f);

        Debug.Log("Enemy Turn End!");
        OnTurnEnd(PlayerID.Player);
    }

    private void EnemyAttack()
    {
        //Debug.Log(player);
    }

    private void EnemyDefense()
    {
        //Debug.Log(player);
    }

    private void EnemyUtility()
    {
        //Debug.Log(player);
    }

    public void UseCard(Cards card)
    {
        int cost = card.cc;
        int cardAttribute = (int)card.ca;
        int cardType = (int)card.ct;
        int cardPower = card.cardPower;

        switch (cardType)
        {
            case 0: // �� ī��
                break;
            case 1: // ���� ī��
                /* ���� �ڵ� */
                break;
            case 2: // ��� ī��
                /* ��� �ڵ� */
                break;
            case 3: // ��ƿ��Ƽ ī��
                /* ��ƿ��Ƽ �ڵ� */
                break;
            default:
                Debug.LogError("�߸��� ī�� Ÿ���Դϴ�.");
                break;
        }
    }
}
