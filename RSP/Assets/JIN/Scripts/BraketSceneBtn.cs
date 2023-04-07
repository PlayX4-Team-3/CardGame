using UnityEngine;

using UnityEngine.UI;

public class BraketSceneBtn : MonoBehaviour
{
    private void Start()
    {
        // 버튼 오브젝트 가져오기
        Button myButton = GetComponent<Button>();

        // 버튼 클릭 시 호출될 함수 연결
        myButton.onClick.AddListener(MyButtonClickFunction);
    }

    private void MyButtonClickFunction()
    {
        SceneChange.Instance.GoGameScene();
    }
}
