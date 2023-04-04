using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ObserverPattern;

namespace AllCharacter
{
    public class Player : Character, IObserver
    {
        [SerializeField]
        private Text hpText;

        private GameData data;
        private void Awake()
        {
            InitHp(100);
            InitCost(5);
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Hp -= 10;
                Debug.Log(Hp);
            }
        }
        
        public void DataInit(GameData data)
        {
            this.data = data;
        }

        public void UpdateHpText(int pHp, int eHp)
        {
            this.hpText.text = pHp.ToString();
        }
    }
}
