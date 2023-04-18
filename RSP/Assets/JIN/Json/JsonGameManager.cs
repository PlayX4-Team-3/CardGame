using System.Collections;
using UnityEngine;

using AllCharacter;
using UnityEngine.UI;

public enum GameState
{
    Playing,
    GameEnd
}

public class JsonGameManager : Singleton<JsonGameManager>
{
    private JsonCardManager jcm;
    private TurnManager tm;
    private CardAbility ca;

    public Player player;
    public Enemy enemy;

    public Button BtnTurnEnd;

    private GameState gs;

    public Image[] playerCostImg = new Image[5];

    public Display display;

    private int enemyActionRate;
    private int enemyActionIndex;
    public Text enemyActionText;

    public bool canEAttack = false; 

    private void Start()
    {
        jcm = JsonCardManager.Instance;
        tm = TurnManager.Instance;
        ca = CardAbility.Instance;

        tm.onTurnEnd.AddListener(OnTurnEnd);
        tm.StartTurn(PlayerID.Player);

        jcm.DeckInit();
        jcm.DrawCard(5);

        gs = GameState.Playing;
        display.UpdateCharacterState();

        EnemyAction();

        canEAttack = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            jcm.DrawCard();
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
            jcm.DrawCard();

            BtnTurnEnd.interactable = true;

            player.Cost = player.MaxCost;

            for (int i = 0; i < player.MaxCost; i++)
                playerCostImg[i].gameObject.SetActive(true);

            EnemyAction();
        }

        else
        {
            EnemyTurn();
            BtnTurnEnd.interactable = false;
        }
    }

    private void EnemyAction()
    {
        if (!canEAttack)
            canEAttack = true;

        enemyActionRate = Random.Range(0, 1000);

        if (enemyActionRate >= 0 && enemyActionRate < 400) // 40% 공격확률
        {
            enemyActionIndex = 0;
            enemyActionText.text = "Attack";
        }
        else if (enemyActionRate >= 400 && enemyActionRate < 800) // 40% 방어확률
        {
            enemyActionIndex = 1;
            enemyActionText.text = "Defense";
        }
        else if (enemyActionRate >= 800) // 20% 유틸확률
        {
            enemyActionIndex = 2;
            enemyActionText.text = "Utility";
        }
    }

    private void EnemyTurn()
    {
        // 적 턴 시작 시 적 방어력 0으로 초기화
            enemy.Defense_Figures = 0;

            // 적 행동 패턴 랜덤으로 선택
            //int action = Random.Range(0, 3);

            switch (enemyActionIndex)
            {
                case 0:
                if(canEAttack)
                    EnemyAttack();
                break;

                case 1:
                    EnemyDefense();
                    break;

                case 2:
                    EnemyUtility();
                    break;
            }

        // 적 턴 동안 딜레이를 줘 행동하는 듯한 느낌을 줌
        StartCoroutine(EnemyTurnEndDelay());
    }

    private IEnumerator EnemyTurnEndDelay()
    {
        yield return new WaitForSeconds(2f);
        
        player.Defense_Figures = 0;
        display.UpdateCharacterState();

        OnTurnEnd(PlayerID.Player);
    }

    // 적 공격 패턴
    private void EnemyAttack()
    {
        int randomDamage = Random.Range(3, 6);

        if (player.Defense_Figures > 0)
        {
            player.Defense_Figures -= randomDamage;
            if (player.Defense_Figures <= 0)
            {
                player.Hp = player.Hp + player.Defense_Figures;
                player.Defense_Figures = 0;
            }
        }

        else
            player.Hp -= randomDamage;

        display.UpdateCharacterState();

        if (player.Hp == 0)
            GameEnd(tm.currentPlayer);
    }

    // 적 방어 패턴
    private void EnemyDefense()
    {
        enemy.Defense_Figures += 2;
    }

    // 적 유틸 패턴
    private void EnemyUtility()
    {
        int rand = Random.Range(0, 3);

        switch (rand)
        {
            case 0:
                enemy.Hp += 3;
                break;
            default:
                enemy.Defense_Figures += 5;
                break;
        }
    }

    public void UseCard(Card card)
    {
        if (gs != GameState.Playing)
        {
            Debug.Log("게임 종료 상태입니다.");
            return;
        }

        // 카드 능력이 발동되는 곳
        CardAbility.Instance.UseCard(card);

        // 사용한 Cost 만큼 이미지 비활성화
        for (int i = player.MaxCost - 1; i >= player.Cost; i--)
            playerCostImg[i].gameObject.SetActive(false);

        // 체력, 방어 상태 업데이트
        display.UpdateCharacterState();

        // 적 체력이 0이 되면 게임 승리 판정
        if (enemy.Hp == 0)
            GameEnd(tm.currentPlayer);
    }

    private void GameEnd(PlayerID currentPlayer)
    {
        PlayerID winnerPlayer = currentPlayer;

        gs = GameState.GameEnd;

        StartCoroutine(GameEndDelay(currentPlayer));
    }

    private IEnumerator GameEndDelay(PlayerID player)
    {
        yield return new WaitForSeconds(1f);

        SceneChange.Instance.winnerIndex = (int)player;
        //SceneChange.Instance.GoResultScene();
        SceneChange.Instance.GoNextScene();
    }
}