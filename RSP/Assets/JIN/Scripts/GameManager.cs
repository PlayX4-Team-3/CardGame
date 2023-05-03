using System.Collections;
using UnityEngine;

using manager;
using AllCharacter;
using UnityEngine.UI;
//using DG.Tweening;

public enum GameState
{
    Playing,
    GameEnd
}

public class GameManager : Singleton<GameManager>
{
    private CardManager cm;
    private TurnManager tm;
    private CardAbility ca;
    private DotweenManager dm;

    public Player player;
    public Enemy enemy;

    public Button BtnTurnEnd;

    private GameState gs;

    public Image[] playerCostImg = new Image[5];

    public Display display;

    private int enemyActionRate;
    private int enemyActionIndex;
    public Text enemyActionText;

    // enemy Debuff
    public bool canEAttack = true;

    public GameObject dummy;

    public AnimationManager animationManager;

    private void Start()
    {
        cm = CardManager.Instance;
        tm = TurnManager.Instance;
        ca = CardAbility.Instance;
        dm = DotweenManager.Instance;

        dummy = GameObject.FindWithTag("DummyCard").gameObject;

        tm.onTurnEnd.AddListener(OnTurnEnd);
        tm.StartTurn(PlayerID.Player);

        //cm.DeckInit();
        //cm.DrawCard(3);
        //StartCoroutine(StartDelay());

        Invoke("StartDelay", 0.5f);

        gs = GameState.Playing;
        display.UpdateCharacterState();

        EnemyAction();
    }

    private void StartDelay()
    { 
        cm.DeckInit();
        cm.DrawCard(3);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            cm.DrawCard();
    }

    public void OnTurnEnd(PlayerID nextPlayer)
    {
        if (gs != GameState.Playing)
        {
            Debug.Log("게임 중이 아닙니다.");
            return;
        }

        tm.StartTurn(nextPlayer);

        // player turn init
        if (nextPlayer == PlayerID.Player)
        {
            player.CheckBuff();
            player.CheckDebuff();

            cm.DrawCard();

            BtnTurnEnd.interactable = true;

            player.Cost = player.MaxCost;

            for (int i = 0; i < player.MaxCost; i++)
                playerCostImg[i].gameObject.SetActive(true);

            EnemyAction();
        }

        // enemy turn init
        else
        {
            BtnTurnEnd.interactable = false;

            EnemyTurn();

            enemy.CheckBuff();
            enemy.CheckDebuff();
        }
    }

    private void EnemyAction()
    {
        if (!canEAttack)
            canEAttack = true;

        enemyActionRate = Random.Range(0, 1000);

        if (enemyActionRate >= 0 && enemyActionRate < 400) // 40% Attack rate
        {
            enemyActionIndex = 0;
            enemyActionText.text = "Attack";
        }
        else if (enemyActionRate >= 400 && enemyActionRate < 800) // 40% Defense rate
        {
            enemyActionIndex = 1;
            enemyActionText.text = "Defense";
        }
        else if (enemyActionRate >= 800) // 20% Utility rate
        {
            enemyActionIndex = 2;
            enemyActionText.text = "Utility";
        }
    }

    private void EnemyTurn()
    {
        if (!enemy.is307Debuff)
            // turn start delay
            StartCoroutine(EnemyTurnStartDelay());

        // turn end delay
        StartCoroutine(EnemyTurnEndDelay());
    }

    private IEnumerator EnemyTurnStartDelay()
    {
        yield return new WaitForSeconds(1f);

        // enemy defense init
        enemy.Defense_Figures = 0;

        switch (enemyActionIndex)
        {
            case 0:
                if (canEAttack)
                {
                    EnemyAttack();
                    //animationManager.PlayerHit();
                    //animationManager.EnemyAttack();
                    dm.AttackAnim(player.gameObject, 0.5f);
                }
                break;

            case 1:
                EnemyDefense();
                dm.DefenseAnim(enemy.gameObject);
                break;

            case 2:
                EnemyUtility();
                break;
        }
    }

    private IEnumerator EnemyTurnEndDelay()
    {
        yield return new WaitForSeconds(2f);

        if (enemy.is307Debuff)
        {
            DotweenManager.Instance.IcicleAnimation(SpellImageMaker.Instance.spells[9], 1);
            enemy.is307Debuff = false;
        }

        player.Defense_Figures = 0;
        display.UpdateCharacterState();

        OnTurnEnd(PlayerID.Player);
    }

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

    private void EnemyDefense()
    {
        enemy.Defense_Figures += 2;
    }

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
            Debug.Log("게임 중이 아닙니다.");
            return;
        }

        //if (card.Type == "Attack")
        //{
        //    animationManager.PlayerAttack();
        //    animationManager.EnemyHit();
        //}

        // 카드 능력 발동 부분
        CardAbility.Instance.UseCard(card);

        // 사용한 플레이어 cost 이미지 끄기
        for (int i = player.MaxCost - 1; i >= player.Cost; i--)
            playerCostImg[i].gameObject.SetActive(false);

        // 체력, 방어력 UI 업데이트
        display.UpdateCharacterState();

        // 적의 체력이 0이 되면 게임 종료
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