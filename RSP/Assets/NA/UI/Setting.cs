using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public GameObject OptionSet;

    private void Update()
    {
        if (OptionSet.activeSelf)
            OptionSet.SetActive(true);
        else
            OptionSet.SetActive(false);
    }
}
