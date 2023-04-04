using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Close : MonoBehaviour
{
    public GameObject go;
    public void close()
    {
        go.SetActive(false);
    }
}
