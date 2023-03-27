using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class HUD : MonoBehaviour
{
    public enum InfoType { HP, Damage, Guard}
    public InfoType type;

    Text text;
    Slider slider; 

    void Awake()
    {
        text = GetComponent<Text>();
        slider = GetComponent<Slider>(); 
    }

    void LateUpdate()
    {
        switch(type)
        {
            case InfoType.HP:
                 
                break;

            case InfoType.Damage:

                break;

            case InfoType.Guard:

                break; 
        }
    }
}
