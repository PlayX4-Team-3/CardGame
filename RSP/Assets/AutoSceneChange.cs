using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSceneChange : MonoBehaviour
{
    private void Start()
    {
        Invoke("GoNextScene", 3f);
    }
    private void GoNextScene()
    {
        SceneChange.Instance.GoNextScene();
    }
}
