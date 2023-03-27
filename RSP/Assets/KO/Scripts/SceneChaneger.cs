using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class SceneChaneger : MonoBehaviour
{
    public static SceneChaneger sceneChaneger; 

    public void GoBracketScene()
    {
        SceneManager.LoadScene(1);

    }

    public void GoGameScene()
    {
        SceneManager.LoadScene(2);
    }

    public void GoResultScene()
    {
        SceneManager.LoadScene(3);
    }

    public void GoTitleScene()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
