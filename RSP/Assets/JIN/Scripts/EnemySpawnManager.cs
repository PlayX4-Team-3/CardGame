using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AllCharacter;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject[] enemys;

    void Start()
    {
        GameObject temp;
        
        foreach(var go in enemys)
        {
            go.SetActive(false);
        }

        int rand = Random.Range(0, 2);

        //if (SceneChange.Instance.roundIndex == 0)
        //{
        //    if (rand == 0)
        //    {
        //        enemys[1].SetActive(true);
        //        temp = enemys[1];
        //    }
        //    else
        //    {
        //        enemys[4].SetActive(true);
        //        temp = enemys[4];
        //    }
        //}
        //else if (SceneChange.Instance.roundIndex == 1)
        //    if (rand == 0)
        //    {
        //        enemys[2].SetActive(true);
        //        temp = enemys[2];
        //    }
        //    else
        //    {
        //        enemys[5].SetActive(true);
        //        temp = enemys[5];
        //    }
        //else
        //{
            if (rand == 0)
            {
                enemys[3].SetActive(true);
                temp = enemys[3];
            }
            else
            {
                enemys[6].SetActive(true);
                temp = enemys[6];
            //}
        }

        GameManager.Instance.enemy = temp.GetComponent<Enemy>();
    }
}
