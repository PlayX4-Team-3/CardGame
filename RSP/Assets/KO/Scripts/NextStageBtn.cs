using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class NextStageBtn : MonoBehaviour
{
    public Button SecondStage;
    public Button ThirdStage;
    public Button FinalStage;

    private int VictoryPoint;

    private bool MouseUp=false; 

    void Update()
    {
        //switch (VictoryPoint)
        //{
        //    case 1:
        //        SecondStage.GetComponent<Image>().fillAmount += 3f * Time.deltaTime;
        //break; 

        //    case 2:
        //        ThirdStage.GetComponent<Image>().fillAmount += 3f * Time.deltaTime;
        //break; 

        //    case 3:
        //        FinalStage.GetComponent<Image>().fillAmount += 3f * Time.deltaTime;
        //break;
        //}

        if(MouseUp)
        {
            SecondStage.GetComponent<Image>().fillAmount += 3f * Time.deltaTime;
            if (SecondStage.GetComponent<Image>().fillAmount >= 1f)
            {
                SecondStage.GetComponent<Image>().fillAmount = 1f;
                MouseUp = false;
            }
        }
    }

    public void Up()
    {
        MouseUp = true; 
    }
}
