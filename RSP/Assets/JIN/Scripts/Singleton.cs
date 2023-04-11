using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(T)) as T;

                if (_instance == null)
                    Debug.Log("해당하는 " + typeof(T) + "타입의 클래스가 해당 씬에 존재하지 않음");
            }
            return _instance;
        }
    }

    //void Awake()
    //{
    //    if (_instance == null)
    //    {
    //        _instance = this as T;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //    else if (_instance != this)
    //    {
    //        Debug.Log("같은 타입의 Singleton이 이미 존재합니다. 새로 생성된 오브젝트를 삭제합니다.");
    //        Destroy(gameObject);
    //    }
    //}

    //void OnDestroy()
    //{
    //    if (_instance == this)
    //    {
    //        _instance = null;
    //    }
    //}
}