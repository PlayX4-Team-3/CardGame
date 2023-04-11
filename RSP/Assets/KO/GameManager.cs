using AllCharacter;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : Singleton<GameManager>
{
    private CardManager cm;
    private TurnManager tm;

    [SerializeField]
    private Player player;
    private Enemy enemy;

    private Button BtnTurnEnd;

    // ���� ���� (���� ��, ���� ��)
    private GameState gs;

    [SerializeField]
    private Image[] playerCostImg = new Image[5];

    private Display display;

    public int drawAmount;

    private int enemyActionIndex;
    public Text enemyActionText;

    private void Start()
    {
        cm = CardManager.Instance;
        tm = TurnManager.Instance;

        player = GameObject.Find("Player").GetComponent<Player>();
        enemy = GameObject.Find("Enemy").GetComponent<Enemy>();
        BtnTurnEnd = GameObject.Find("BtnTurnEnd").GetComponent<Button>();
        display = GameObject.Find("UIManager").GetComponent<Display>();

        tm.onTurnEnd.AddListener(OnTurnEnd);
        tm.StartTurn(PlayerID.Player);

        cm.CardInit();

        cm.DeckInstantiate();
        cm.DeckShuffle(cm.copiedPlayerDeck);

        while (cm.handDeck.Count < drawAmount) // 3�� ���� ���۽� �� ��ο� ��
            cm.Draw();

        gs = GameState.Playing;
        display.UpdateCharacterState();

        enemyActionIndex = Random.Range(0, 3);
        EnemyActionTextUpdate();
    }

    #region �� ���� ��
    public void OnTurnEnd(PlayerID nextPlayer)
    {
        if (gs != GameState.Playing)
        {
            Debug.Log("���� ����");
            return;
        }

        tm.StartTurn(nextPlayer);

        // �÷��̾� ���϶��� �� ���� ��ư Ȱ��ȭ
        if (nextPlayer == PlayerID.Player)
        {
            cm.Draw();

            BtnTurnEnd.interactable = true;

            player.Cost = player.MaxCost;

            for (int i = 0; i < player.MaxCost; i++)
                playerCostImg[i].gameObject.SetActive(true);

            enemyActionIndex = Random.Range(0, 3);
            EnemyActionTextUpdate();
        }

        else
        {
            EnemyTurn();
            BtnTurnEnd.interactable = false;
        }
    }
    #endregion

    #region �� �ൿ ����
    private void EnemyActionTextUpdate()
    {
        if (enemyActionIndex == 0)
            enemyActionText.text = "Attack";
        else if (enemyActionIndex == 1)
            enemyActionText.text = "Defense";
        else
            enemyActionText.text = "Utility";
    }

    private void EnemyTurn()
    {
        // �� �� ���� �� �� ���� 0���� �ʱ�ȭ
        enemy.Defense_Figures = 0;

        // �� �ൿ ���� �������� ����
        //int action = Random.Range(0, 3);

        switch (enemyActionIndex)
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

        // �� �� ���� �����̸� �� �ൿ�ϴ� ���� ������ ��
        StartCoroutine(EnemyTurnEndDelay());
    }

    private IEnumerator EnemyTurnEndDelay()
    {
        yield return new WaitForSeconds(2f);

        player.Defense_Figures = 0;
        display.UpdateCharacterState();

        OnTurnEnd(PlayerID.Player);
    }

    // �� ���� ����
    private void EnemyAttack()
    {
        int randomDamage = Random.Range(1, 5);

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

    // �� ��� ����
    private void EnemyDefense()
    {
        enemy.Defense_Figures += Random.Range(1, 5);
    }

    // �� ��ƿ ����
    private void EnemyUtility()
    {
        //Debug.Log(player);
    }
    #endregion

    #region �÷��̾� ī�� ���
    public void UseCard(Cards card)
    {
        if (gs != GameState.Playing)
        {
            Debug.Log("���� ���� �����Դϴ�.");
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
            case 0: // ���� ī��
                /* ���� �ڵ� */
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

            case 1: // ��� ī��
                /* ��� �ڵ� */
                player.Defense_Figures += cardPower;
                break;

            case 2: // ��ƿ��Ƽ ī��
                /* ��ƿ��Ƽ �ڵ� */
                break;

            default:
                Debug.Log("�߸��� ī�� Ÿ���Դϴ�.");
                break;
        }

        // ü��, ��� ���� ������Ʈ
        display.UpdateCharacterState();

        // �� ü���� 0�� �Ǹ� ���� �¸� ����
        if (enemy.Hp == 0)
            GameEnd(tm.currentPlayer);
    }
    #endregion

    #region ���� ���� ����
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
    #endregion
}
