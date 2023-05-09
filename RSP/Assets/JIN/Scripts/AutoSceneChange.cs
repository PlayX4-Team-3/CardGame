using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSceneChange : MonoBehaviour
{
    private NextStageBtn nsb;
    private int roundIndex;

    private void Start()
    {
        nsb = NextStageBtn.Instance;
        roundIndex = SceneChange.Instance.roundIndex;

        if(roundIndex == 1)
        {
            nsb.One.fillAmount = 1;
            nsb.Two.fillAmount = 1;
            nsb.Three.fillAmount = 1;
            nsb.Four.fillAmount = 1;
            nsb.Five.fillAmount = 1;
            nsb.Six.fillAmount = 1;
            nsb.Seven.fillAmount = 1;
            nsb.Eight.fillAmount = 1;
        }

        if(roundIndex ==  2)
        {
            nsb.SecondStage.fillAmount = 1;
            nsb.SecondStage1.fillAmount = 1;
            nsb.SecondStage2.fillAmount = 1;
            nsb.SecondStage3.fillAmount = 1;
        }

        //if (roundIndex == 3)
        //{
        //    nsb.ThirdStage.fillAmount = 1;
        //    nsb.ThirdStage1.fillAmount = 1;
        //}

        Invoke("GoNextScene", 2f);
    }
    private void GoNextScene()
    {
        SceneChange.Instance.GoNextScene();
    }
}
