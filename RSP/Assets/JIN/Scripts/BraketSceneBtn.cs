using UnityEngine;

using UnityEngine.UI;

public class BraketSceneBtn : MonoBehaviour
{
    private void Start()
    {
        // ��ư ������Ʈ ��������
        Button myButton = GetComponent<Button>();

        // ��ư Ŭ�� �� ȣ��� �Լ� ����
        myButton.onClick.AddListener(MyButtonClickFunction);
    }

    private void MyButtonClickFunction()
    {
        SceneChange.Instance.GoGameScene();
    }
}
