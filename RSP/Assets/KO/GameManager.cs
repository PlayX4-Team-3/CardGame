using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using AllCharacter;


public class GameManager : Singleton<GameManager>
{
    private CardManager cm;
    private TurnManager tm;

    public Player player;
    public Enemy enemy;

    public Button BtnTurnEnd;

    private GameState gs;
    [SerializeField]
    private Image[] playerCostImg = new Image[5];

    private void Start()
    {
        cm = CardManager.Instance;
        tm = TurnManager.Instance;

        cm.DeckInstantiate();
        cm.DeckShuffle(cm.copiedPlayerDeck);

        tm.onTurnEnd.AddListener(OnTurnEnd);
        tm.StartTurn(PlayerID.Player);

        while (cm.handDeck.Count < 9) // 3은 게임 시작시 패 드로우 수
            cm.Draw();

        //player = GameObject.FindWithTag("Player");
        //enemy = GameObject.FindWithTag("Enemy");

        gs = GameState.Playing;
    }

    public void OnTurnEnd(PlayerID nextPlayer)
    {
        if (gs != GameState.Playing)
        {
            Debug.Log("게임 종료");
            return;
        }

        tm.StartTurn(nextPlayer);

        // 플레이어 턴일때만 턴 종료 버튼 활성화
        if (nextPlayer == PlayerID.Player)
        {
            cm.Draw();

            BtnTurnEnd.interactable = true;

            player.Cost = player.MaxCost;
            for (int i = 0; i < player.MaxCost; i++)
                playerCostImg[i].gameObject.SetActive(true);
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

        OnTurnEnd(PlayerID.Player);
    }

    private void EnemyAttack()
    {
        player.Hp -= 2;

        if (player.Hp == 0)
            GameEnd(tm.currentPlayer);
    }

    private void EnemyDefense()
    {
        enemy.Defense_Figures(2);
        Debug.Log("Def");
    }

    private void EnemyUtility()
    {
        //Debug.Log(player);
    }

    public void UseCard(Cards card)
    {
        if(gs != GameState.Playing)
        {
            Debug.Log("게임 종료 상태입니다.");
            return;
        }

        int cost = card.cc;
        int cardAttribute = (int)card.ca;
        int cardType = (int)card.ct;
        int cardPower = card.cardPower;

        for (int i = player.MaxCost - 1; i >= player.Cost; i--)
            playerCostImg[i].gameObject.SetActive(false);

        switch (cardType)
        {
            case 0: // 공격 카드
                /* 공격 코드 */
                enemy.Hp -= cardPower;

                if (enemy.Hp == 0)
                    GameEnd(tm.currentPlayer);
                break;
            case 1: // 방어 카드
                /* 방어 코드 */
                break;
            case 2: // 유틸리티 카드
                /* 유틸리티 코드 */
                break;
            default:
                Debug.Log("잘못된 카드 타입입니다.");
                break;
        }

    }

    private void GameEnd(PlayerID currentPlayer)
    {
        PlayerID winnerPlayer = currentPlayer;

        Debug.Log("winner is " + winnerPlayer);
        gs = GameState.GameEnd;

        StartCoroutine(GameEndDelay());
    }

    private IEnumerator GameEndDelay()
    {
        yield return new WaitForSeconds(1f);

        SceneChange.Instance.GoResultScene();
    }
}
