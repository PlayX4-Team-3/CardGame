using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class WhoisWinner : MonoBehaviour
{
    public GameObject playerWin;
    public GameObject playerLose;

    public Image playerWinImg;
    public Image playerLoseImg;

    public SpriteRenderer playerWinSR;
    public SpriteRenderer playerLoseSR;

    private SceneChange sc;

    public Button goTitleBtn;


    private void Awake()
    {
        sc = SceneChange.Instance;

        playerWin.SetActive(false);
        playerLose.SetActive(false);

        playerWinImg.gameObject.SetActive(false);
        playerLoseImg.gameObject.SetActive(false);

        playerWinSR.gameObject.SetActive(false);
        playerLoseSR.gameObject.SetActive(false);

        if(TurnManager.Instance.currentPlayer == PlayerID.Player)
        {
            playerWin.SetActive(true);
            playerWinImg.gameObject.SetActive(true);
            playerWinSR.gameObject.SetActive(true);
        }
        else
        {
            playerLose.SetActive(true);
            playerLoseImg.gameObject.SetActive(true);
            playerLoseSR.gameObject.SetActive(true);
        }

        goTitleBtn.onClick.AddListener(GoTitleBtnClickFunc);
    }

    void GoTitleBtnClickFunc()
    {
        SceneChange.Instance.GoTitleScene();
    }
}
