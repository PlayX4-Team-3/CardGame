using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class RoundManager : Singleton<RoundManager>
{
    private static RoundManager _instance;

    public Button quarterFinal;
    public Button semiFinal;
    public Button final;

    public int roundIndex = 0;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this as RoundManager;
            DontDestroyOnLoad(gameObject);
        }

        else if (_instance != this)
        {
            //Debug.Log("???? ?????? Singleton?? ???? ??????????. ???? ?????? ?????????? ??????????.");
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        quarterFinal = GameObject.Find("First").GetComponent<Button>();
        semiFinal = GameObject.Find("Second").GetComponent<Button>();
        final = GameObject.Find("Final").GetComponent<Button>();

        SetRoundIndex();
    }

    void OnDestroy()
    {
        if (_instance == this)
            _instance = null;
    }

    private void SetRoundIndex()
    {
        quarterFinal.interactable = false;
        semiFinal.interactable = false;
        final.interactable = false;

        roundIndex = roundIndex % 3;
        Debug.Log(roundIndex);

        switch(roundIndex)
        {
            case 0:
                quarterFinal.interactable = true;
                break;
            case 1:
                semiFinal.interactable = true;
                break;
            case 2:
                final.interactable = true;
                break;
            default:
                SetRoundIndex();
                break;
        }

        roundIndex++;

        this.gameObject.SetActive(false);
    }
}
