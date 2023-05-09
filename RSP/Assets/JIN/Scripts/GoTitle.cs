using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoTitle : MonoBehaviour
{
    public void GoTitleScene()
    {
        DG.Tweening.DOTween.KillAll();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
