using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    public Button quarterFinal;
    public Button semiFinal;
    public Button final;

    private void Start()
    {
        quarterFinal = GameObject.Find("First").GetComponent<Button>();
        semiFinal = GameObject.Find("Second").GetComponent<Button>();
        final = GameObject.Find("Final").GetComponent<Button>();

        SetRoundIndex();
    }

    private void SetRoundIndex()
    {
        quarterFinal.interactable = false;
        semiFinal.interactable = false;
        final.interactable = false;

        SceneChange.Instance.roundIndex = SceneChange.Instance.roundIndex % 3;

        switch(SceneChange.Instance.roundIndex)
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

        this.gameObject.SetActive(false);
    }
}
