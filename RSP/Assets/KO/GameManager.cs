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

    // 게임 상태 (게임 중, 게임 끝)
    private GameState gs;

    [SerializeField]
    private Image[] playerCostImg = new Image[5];

    [SerializeField]
    private Display display;

    public int drawAmount;

    private void Start()
    {
        cm = CardManager.Instance;
        tm = TurnManager.Instance;

        cm.DeckInstantiate();
        cm.DeckShuffle(cm.copiedPlayerDeck);

        tm.onTurnEnd.AddListener(OnTurnEnd);
        tm.StartTurn(PlayerID.Player);

        while (cm.handDeck.Count < drawAmount) // 3은 게임 시작시 패 드로우 수
            cm.Draw();

        gs = GameState.Playing;

        display.UpdateCharacterState();
    }

    #region 턴 종료 시
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
    #endregion

    #region 적 행동 패턴
    private void EnemyTurn()
    {
        enemy.Defense_Figures = 0;

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
        display.UpdateCharacterState();
        OnTurnEnd(PlayerID.Player);
    }

    private void EnemyAttack()
    {
        player.Hp -= Random.Range(1, 4);

        if (player.Hp == 0)
            GameEnd(tm.currentPlayer);
    }

    private void EnemyDefense()
    {
        enemy.Defense_Figures += Random.Range(1, 4);
    }

    private void EnemyUtility()
    {
        //Debug.Log(player);
    }
    #endregion

    #region 플레이어 카드 사용
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
                if (enemy.Defense_Figures > 0)
                {
                    enemy.Defense_Figures -= cardPower;
                    if (enemy.Defense_Figures <= 0)
                    {
                        enemy.Hp = enemy.Hp + enemy.Defense_Figures;
                        enemy.Defense_Figures = 0;
                    }
                }

                else
                    enemy.Hp -= cardPower;
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

        // 체력, 방어 상태 업데이트
        display.UpdateCharacterState();

        // 적 체력이 0이 되면 게임 승리 판정
        if (enemy.Hp == 0)
            GameEnd(tm.currentPlayer);
    }
    #endregion

    #region 게임 승패 판정
    private void GameEnd(PlayerID currentPlayer)
    {
        PlayerID winnerPlayer = currentPlayer;

        Debug.Log("winner is " + winnerPlayer);
        gs = GameState.GameEnd;

        StartCoroutine(GameEndDelay(currentPlayer));
    }

    private IEnumerator GameEndDelay(PlayerID player)
    {
        yield return new WaitForSeconds(1f);
        Debug.Log(player);
        SceneChange.Instance.GoResultScene();
    }
    #endregion
}
