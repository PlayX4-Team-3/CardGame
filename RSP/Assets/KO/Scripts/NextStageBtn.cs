using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class NextStageBtn : MonoBehaviour
{
    public Image SecondStage;
    public Image SecondStage1;
    public Image SecondStage2;
    public Image SecondStage3;
    public Image ThirdStage;
    public Image ThirdStage1; 
    public Image FinalStage;

    public Image Player;

    public Image Two;
    public Image Three;
    public Image Four;
    public Image Five;
    public Image Six;
    public Image Seven;
    public Image Eight; 


    public int VictoryPoint;

    private bool MouseUp=false; 

    void Update()
    {
        switch (VictoryPoint)
        {
            case 1:
                SecondStage.GetComponent<Image>().fillAmount += 3f * Time.deltaTime;
                SecondStage1.GetComponent<Image>().fillAmount += 3f * Time.deltaTime;
                SecondStage2.GetComponent<Image>().fillAmount += 3f * Time.deltaTime;
                SecondStage3.GetComponent<Image>().fillAmount += 3f * Time.deltaTime;

                ThirdStage.GetComponent<Image>().fillAmount -= 3f * Time.deltaTime;
                ThirdStage1.GetComponent<Image>().fillAmount -= 3f * Time.deltaTime;

                FinalStage.GetComponent<Image>().fillAmount -= 3f * Time.deltaTime;

                Player.GetComponent<Image>().fillAmount -= 3f * Time.deltaTime;
                Two.GetComponent<Image>().fillAmount -= 3f * Time.deltaTime;
                Three.GetComponent<Image>().fillAmount -= 3f * Time.deltaTime;
                Four.GetComponent<Image>().fillAmount -= 3f * Time.deltaTime;
                Five.GetComponent<Image>().fillAmount -= 3f * Time.deltaTime;
                Six.GetComponent<Image>().fillAmount -= 3f * Time.deltaTime;
                Seven.GetComponent<Image>().fillAmount -= 3f * Time.deltaTime;
                Eight.GetComponent<Image>().fillAmount -= 3f * Time.deltaTime;
                break;

            case 2:
                ThirdStage.GetComponent<Image>().fillAmount += 3f * Time.deltaTime;
                ThirdStage1.GetComponent<Image>().fillAmount += 3f * Time.deltaTime;

                SecondStage.GetComponent<Image>().fillAmount -= 3f * Time.deltaTime;
                SecondStage1.GetComponent<Image>().fillAmount -= 3f * Time.deltaTime;
                SecondStage2.GetComponent<Image>().fillAmount -= 3f * Time.deltaTime;
                SecondStage3.GetComponent<Image>().fillAmount -= 3f * Time.deltaTime;

                FinalStage.GetComponent<Image>().fillAmount -= 3f * Time.deltaTime;

                Player.GetComponent<Image>().fillAmount -= 3f * Time.deltaTime;
                Two.GetComponent<Image>().fillAmount -= 3f * Time.deltaTime;
                Three.GetComponent<Image>().fillAmount -= 3f * Time.deltaTime;
                Four.GetComponent<Image>().fillAmount -= 3f * Time.deltaTime;
                Five.GetComponent<Image>().fillAmount -= 3f * Time.deltaTime;
                Six.GetComponent<Image>().fillAmount -= 3f * Time.deltaTime;
                Seven.GetComponent<Image>().fillAmount -= 3f * Time.deltaTime;
                Eight.GetComponent<Image>().fillAmount -= 3f * Time.deltaTime;
                break;

            case 3:
                FinalStage.GetComponent<Image>().fillAmount += 3f * Time.deltaTime;

                SecondStage.GetComponent<Image>().fillAmount -= 3f * Time.deltaTime;

                ThirdStage.GetComponent<Image>().fillAmount -= 3f * Time.deltaTime;
                ThirdStage1.GetComponent<Image>().fillAmount -= 3f * Time.deltaTime;

                Player.GetComponent<Image>().fillAmount -= 3f * Time.deltaTime;
                Two.GetComponent<Image>().fillAmount -= 3f * Time.deltaTime;
                Three.GetComponent<Image>().fillAmount -= 3f * Time.deltaTime;
                Four.GetComponent<Image>().fillAmount -= 3f * Time.deltaTime;
                Five.GetComponent<Image>().fillAmount -= 3f * Time.deltaTime;
                Six.GetComponent<Image>().fillAmount -= 3f * Time.deltaTime;
                Seven.GetComponent<Image>().fillAmount -= 3f * Time.deltaTime;
                Eight.GetComponent<Image>().fillAmount -= 3f * Time.deltaTime;
                break;

            case 0:
                Player.GetComponent<Image>().fillAmount += 3f * Time.deltaTime;
                Two.GetComponent<Image>().fillAmount += 3f * Time.deltaTime;
                Three.GetComponent<Image>().fillAmount += 3f * Time.deltaTime;
                Four.GetComponent<Image>().fillAmount += 3f * Time.deltaTime;
                Five.GetComponent<Image>().fillAmount += 3f * Time.deltaTime;
                Six.GetComponent<Image>().fillAmount += 3f * Time.deltaTime;
                Seven.GetComponent<Image>().fillAmount += 3f * Time.deltaTime;
                Eight.GetComponent<Image>().fillAmount += 3f * Time.deltaTime;

                SecondStage.GetComponent<Image>().fillAmount -= 3f * Time.deltaTime;
                SecondStage1.GetComponent<Image>().fillAmount -= 3f * Time.deltaTime;
                SecondStage2.GetComponent<Image>().fillAmount -= 3f * Time.deltaTime;
                SecondStage3.GetComponent<Image>().fillAmount -= 3f * Time.deltaTime;

                ThirdStage.GetComponent<Image>().fillAmount -= 3f * Time.deltaTime;
                ThirdStage1.GetComponent<Image>().fillAmount -= 3f * Time.deltaTime;

                FinalStage.GetComponent<Image>().fillAmount -= 3f * Time.deltaTime;
                break;
        }
    }
}
