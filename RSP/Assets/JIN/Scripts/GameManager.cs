using System.Collections;
using UnityEngine;
using AllCharacter;
using UnityEngine.UI;

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

    public GameObject[] EnemyActionsRPS;

    private int enemyActionRate;
    private int enemyActionIndex;
    public Text enemyActionText;

    public GameObject dummy;

    public bool isEnemyAttackMode;

    public GameObject[] buffIcons;

    private void Start()
    {
        cm = CardManager.Instance;
        tm = TurnManager.Instance;
        ca = CardAbility.Instance;
        dm = DotweenManager.Instance;

        dummy = GameObject.FindWithTag("DummyCard").gameObject;

        tm.onTurnEnd.AddListener(OnTurnEnd);
        tm.StartTurn(PlayerID.Player);

        Invoke("StartDelay", 0.5f);

        gs = GameState.Playing;
        display.UpdateCharacterState();

        foreach (GameObject go in EnemyActionsRPS)
            go.SetActive(false);

        foreach (GameObject go in buffIcons)
            go.SetActive(false);

        int rand = Random.Range(0, EnemyActionsRPS.Length);
        EnemyActionsRPS[rand].SetActive(true);

        EnemyAction();

        isEnemyAttackMode = false;
    }

    private void StartDelay()
    { 
        cm.DeckInit();
        cm.DrawCard(3);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            cm.DrawCard();

        if (Input.GetKeyDown(KeyCode.W))
            cm.WantCardDraw();

        if (Input.GetKeyDown(KeyCode.E))
            isEnemyAttackMode = !isEnemyAttackMode;

        if (Input.GetKeyDown(KeyCode.R))
        {
            player.Cost = player.MaxCost;

            for (int i = 0; i < player.MaxCost; i++)
                playerCostImg[i].gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            enemy.Hp = 1;
            display.UpdateCharacterState();
        }

        if(Input.GetKeyDown(KeyCode.Y))
        {
            player.Hp = 1;
            display.UpdateCharacterState();
        }
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
            BtnTurnEnd.interactable = true;

            string enemyRPS = "";
            foreach (GameObject go in EnemyActionsRPS)
                if (go.activeInHierarchy)
                {
                    enemyRPS = go.tag;
                    go.GetComponent<RPSMoving>().UseRPS();
                }

            int rand = Random.Range(0, EnemyActionsRPS.Length);
            EnemyActionsRPS[rand].SetActive(true);

            cm.DrawCard();

            player.Cost = player.MaxCost;

            for (int i = 0; i < player.MaxCost; i++)
                playerCostImg[i].gameObject.SetActive(true);

            EnemyAction();
        }

        // enemy turn init
        else
        {
            BtnTurnEnd.interactable = false;

            enemy.Defense_Figures = 0;

            enemy.CheckBuff();
            enemy.CheckDebuff();
            display.UpdateCharacterState();

            EnemyTurn();
        }
    }

    private void EnemyAction()
    {
        enemyActionRate = Random.Range(0, 1000);

        if (isEnemyAttackMode == false)
        {
            if (enemyActionRate >= 0 && enemyActionRate < 600) // 60% Attack rate
            {
                enemyActionIndex = 0;
                enemyActionText.text = "Attack";
            }
            else if (enemyActionRate >= 600 && enemyActionRate < 900) // 30% Defense rate
            {
                enemyActionIndex = 1;
                enemyActionText.text = "Defense";
            }
            else if (enemyActionRate >= 900) // 10% Utility rate
            {
                enemyActionIndex = 2;
                enemyActionText.text = "Heal";
            }
        }

        else
        {
            enemyActionIndex = 0;
            enemyActionText.text = "AttackMode";
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

        switch (enemyActionIndex)
        {
            case 0:
                if(enemy.isbind == false)
                    EnemyAttack();
                break;

            case 1:
                EnemyDefense();

                break;

            case 2:
                EnemyUtility();
                break;
        }
    }

    private IEnumerator EnemyTurnEndDelay()
    {
        yield return new WaitForSeconds(2f);

        if (enemy.isbind == true)
        {
            enemy.isbind = false;
            dm.EndBind(enemy.gameObject);
            buffIcons[3].SetActive(false);
        }

        if (enemy.is307Debuff)
        {
            DotweenManager.Instance.IcicleAnimation(SpellImageMaker.Instance.spells[9], 1);
            enemy.is307Debuff = false;

            buffIcons[4].SetActive(false);
        }

        player.Defense_Figures = 0;
        display.UpdateCharacterState();

        OnTurnEnd(PlayerID.Player);
    }

    private void EnemyAttack()
    {
        int randomDamage = Random.Range(2, 5);

        GameObject aura;

        int rand = Random.Range(0, 2);
        if (rand == 0)
            aura = Instantiate(ca.Att1);
        else
            aura = Instantiate(ca.Att2);

        aura.transform.SetParent(enemy.gameObject.transform);
        aura.transform.position = enemy.transform.position + Vector3.left;

        // 데미지 반감
        if (player.have308buff == true && player.have207buff == false)
        {
            // 플레이어 데미지 반감 버프 애니메이션
            GameObject spell = player.gameObject.transform.Find("17").gameObject;
            spell.SetActive(true);
            dm.HalfLifeDamage(spell);

            if (player.Defense_Figures > 0)
            {
                player.Defense_Figures -= (randomDamage / 2);

                if (player.Defense_Figures <= 0)
                {
                    player.Hp = player.Hp + player.Defense_Figures;
                    player.Defense_Figures = 0;
                }
            }

            else
                player.Hp -= (randomDamage / 2);

            // 플레이어 버프 턴수 관리
            player.duration308++;
            if (player.duration308 == 2)
            {
                player.have308buff = false;
                player.duration308 = 0;
                buffIcons[1].SetActive(false);
            }

            dm.AttackAura(player.gameObject, aura, -1);
        }

        // 공격 반사
        else if (player.have207buff == true)
        {
            GameObject spell = player.gameObject.transform.Find("04").gameObject;
            spell.SetActive(true);
            dm.ReflectBuff(spell);
            buffIcons[0].SetActive(false);

            if (enemy.Defense_Figures > 0)
            {
                enemy.Defense_Figures -= randomDamage;

                if (enemy.Defense_Figures <= 0)
                {
                    enemy.Hp = enemy.Hp + enemy.Defense_Figures;
                    enemy.Defense_Figures = 0;
                }
            }

            else
            {
                enemy.Hp -= randomDamage;
                //dm.AttackAnim(enemy.gameObject);
                aura.transform.SetParent(player.gameObject.transform);
                aura.transform.position = player.transform.position + Vector3.right;
                dm.AttackAura(enemy.gameObject, aura);
            }

            player.have207buff = false;

        }


        // 일반적인 상황
        else
        {
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

            //dm.AttackAnim(player.gameObject);
            dm.AttackAura(player.gameObject, aura, -1);
        }

        display.UpdateCharacterState();

        if (player.Hp == 0)
            GameEnd(tm.currentPlayer);
    }

    private void EnemyDefense()
    {
        enemy.Defense_Figures += 2;
        GameObject def;
        def = Instantiate(ca.Def1);
        def.transform.SetParent(enemy.gameObject.transform);
        def.transform.position = enemy.transform.position + Vector3.left;

        dm.DefenseAnim(enemy.gameObject, def, -1);
    }

    private void EnemyUtility()
    {
        enemy.Hp += 3;

        GameObject spellObj = SpellImageMaker.Instance.spells[12];

        SpellImageMaker.Instance.SetSpell(enemy.gameObject, spellObj);
        dm.HealNManaAnimation(spellObj, enemy.gameObject);
    }

    public void UseCard(Card card)
    {
        if (gs != GameState.Playing)
        {
            Debug.Log("게임 중이 아닙니다.");
            return;
        }

        string enemyRPS = "";

        foreach (GameObject go in EnemyActionsRPS)
            if (go.activeInHierarchy)
            {
                enemyRPS = go.tag;
                go.GetComponent<RPSMoving>().UseRPS();
            }

        int rand = Random.Range(0, EnemyActionsRPS.Length);

        if (EnemyActionsRPS[rand].activeInHierarchy == true)
        {
            while (true)
            {
                int rand2 = Random.Range(0, EnemyActionsRPS.Length);

                if (rand2 != rand)
                {
                    EnemyActionsRPS[rand2].SetActive(true);
                    break;
                }
            }

        }
        else
            EnemyActionsRPS[rand].SetActive(true);

        int rpsWin = 0;

        // 가위 바위 보 승패 판정
        if (card.Attribute.ToString() == enemyRPS)
            rpsWin = 2;
        else if (card.Attribute.ToString() == "Rock" && enemyRPS == "Paper")
            rpsWin = 1;
        else if (card.Attribute.ToString() == "Rock" && enemyRPS == "Sissors")
            rpsWin = 0;
        else if (card.Attribute.ToString() == "Paper" && enemyRPS == "Rock")
            rpsWin = 0;
        else if (card.Attribute.ToString() == "Paper" && enemyRPS == "Sissors")
            rpsWin = 1;
        else if (card.Attribute.ToString() == "Sissors" && enemyRPS == "Rock")
            rpsWin = 1;
        else
            rpsWin = 0;

        // 카드 능력 발동 부분
        CardAbility.Instance.UseCard(card, rpsWin);

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

        if (winnerPlayer == PlayerID.Player)
            SceneChange.Instance.roundIndex++;
        else
            SceneChange.Instance.roundIndex = 0;

        gs = GameState.GameEnd;

        StartCoroutine(GameEndDelay(currentPlayer));
    }

    private IEnumerator GameEndDelay(PlayerID player)
    {
        yield return new WaitForSeconds(1f);

        SceneChange.Instance.winnerIndex = (int)player;
        SceneChange.Instance.GoAccordingToResultScene();
    }
}