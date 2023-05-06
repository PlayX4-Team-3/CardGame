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
                    Debug.Log("�ش��ϴ� " + typeof(T) + "Ÿ���� Ŭ������ �ش� ���� �������� ����");
            }
            return _instance;
        }
    }
}