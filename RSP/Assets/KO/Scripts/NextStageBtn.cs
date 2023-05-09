using UnityEngine;

using UnityEngine.UI;

public class NextStageBtn : Singleton<NextStageBtn>
{
    public Image SecondStage;
    public Image SecondStage1;
    public Image SecondStage2;
    public Image SecondStage3;

    public Image ThirdStage;
    public Image ThirdStage1;

    public Image FinalStage;

    public Image One;
    public Image Two;
    public Image Three;
    public Image Four;
    public Image Five;
    public Image Six;
    public Image Seven;
    public Image Eight;

    private float duration = 0.7f;

    void Update()
    {
        switch (SceneChange.Instance.roundIndex)
        {
            case 0:
                One.GetComponent<Image>().fillAmount += duration * Time.deltaTime;
                Two.GetComponent<Image>().fillAmount += duration * Time.deltaTime;
                Three.GetComponent<Image>().fillAmount += duration * Time.deltaTime;
                Four.GetComponent<Image>().fillAmount += duration * Time.deltaTime;
                Five.GetComponent<Image>().fillAmount += duration * Time.deltaTime;
                Six.GetComponent<Image>().fillAmount += duration * Time.deltaTime;
                Seven.GetComponent<Image>().fillAmount += duration * Time.deltaTime;
                Eight.GetComponent<Image>().fillAmount += duration * Time.deltaTime;
                break;
            case 1:
                SecondStage.GetComponent<Image>().fillAmount += duration * Time.deltaTime;
                SecondStage1.GetComponent<Image>().fillAmount += duration * Time.deltaTime;
                SecondStage2.GetComponent<Image>().fillAmount += duration * Time.deltaTime;
                SecondStage3.GetComponent<Image>().fillAmount += duration * Time.deltaTime;

                One.GetComponent<Image>().fillAmount -= duration * Time.deltaTime;
                Two.GetComponent<Image>().fillAmount -= duration * Time.deltaTime;
                Three.GetComponent<Image>().fillAmount -= duration * Time.deltaTime;
                Four.GetComponent<Image>().fillAmount -= duration * Time.deltaTime;
                Five.GetComponent<Image>().fillAmount -= duration * Time.deltaTime;
                Six.GetComponent<Image>().fillAmount -= duration * Time.deltaTime;
                Seven.GetComponent<Image>().fillAmount -= duration * Time.deltaTime;
                Eight.GetComponent<Image>().fillAmount -= duration * Time.deltaTime;
                break;

            case 2:
                ThirdStage.GetComponent<Image>().fillAmount += duration * Time.deltaTime;
                ThirdStage1.GetComponent<Image>().fillAmount += duration * Time.deltaTime;

                SecondStage.GetComponent<Image>().fillAmount -= duration * Time.deltaTime;
                SecondStage1.GetComponent<Image>().fillAmount -= duration * Time.deltaTime;
                SecondStage2.GetComponent<Image>().fillAmount -= duration * Time.deltaTime;
                SecondStage3.GetComponent<Image>().fillAmount -= duration * Time.deltaTime;
                break;

            case 3:
                FinalStage.GetComponent<Image>().fillAmount += duration * Time.deltaTime;

                ThirdStage.GetComponent<Image>().fillAmount -= duration * Time.deltaTime;
                ThirdStage1.GetComponent<Image>().fillAmount -= duration * Time.deltaTime;
                break;
        }
    }
}
