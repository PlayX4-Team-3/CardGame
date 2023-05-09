using UnityEngine;

public class CustomCursor : Singleton<CustomCursor>
{
    private static CustomCursor _instance;

    public Texture2D defaultCursor;
    public Texture2D clickCursor;

    private bool isClick;
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this as CustomCursor;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Debug.Log("���� Ÿ���� Singleton�� �̹� �����մϴ�. ���� ������ ������Ʈ�� �����մϴ�.");
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }

    void Start()
    {
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);

        isClick = false;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (isClick == false)
            {
                Cursor.SetCursor(clickCursor, Vector2.zero, CursorMode.Auto);
                SoundManager.Instance.SFXPlay("ButtonClick");
                isClick = true;
            }
        }
        else
        {
            Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
            isClick = false;
        }
    }
}