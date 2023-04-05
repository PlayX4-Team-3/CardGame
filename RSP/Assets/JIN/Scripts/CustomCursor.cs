using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    public Texture2D defaultCursor;
    public Texture2D clickCursor;

    void Start()
    {
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
            Cursor.SetCursor(clickCursor, Vector2.zero, CursorMode.Auto);
        else
            Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }
}