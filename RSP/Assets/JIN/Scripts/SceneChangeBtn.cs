using UnityEngine;

using UnityEngine.UI;

public class SceneChangeBtn : MonoBehaviour
{
    private void Start()
    {
        // ��ư ������Ʈ ��������
        Button myButton = GetComponent<Button>();

        // ��ư Ŭ�� �� ȣ��� �Լ� ����
        if (myButton.gameObject.name == "Exit")
            myButton.onClick.AddListener(ExitBtn);
        else
            myButton.onClick.AddListener(MyButtonClickFunction);
    }

    private void MyButtonClickFunction()
    {
        SceneChange.Instance.GoNextScene();
    }

    private void ExitBtn()
    {
        SceneChange.Instance.Quit();
    }
}
