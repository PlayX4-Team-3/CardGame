using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : Singleton<SceneChange>
{
    private static SceneChange _instance;

    [HideInInspector]
    public int winnerIndex = 0;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this as SceneChange;
            DontDestroyOnLoad(gameObject);
        }

        else if (_instance != this)
        {
            //Debug.Log("���� Ÿ���� Singleton�� �̹� �����մϴ�. ���� ������ ������Ʈ�� �����մϴ�.");
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        if (_instance == this)
            _instance = null;
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void GoNextScene()
    {
        int currentSceneIndex = (SceneManager.GetActiveScene().buildIndex + 1) % 4;

        SceneManager.LoadScene(currentSceneIndex);
    }

    private void Update()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }
}
