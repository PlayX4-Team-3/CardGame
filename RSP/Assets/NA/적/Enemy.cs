using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ObserverPattern;

namespace AllCharacter
{
    public class Enemy : Character, IObserver
    {
        [SerializeField]
        private Text hpText;

        private GameData data;
        private Cards cards;
        private void Awake()
        {
            cards = GetComponent<Cards>();
            InitHp(100);
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Hp -= 10;
                Debug.Log(Hp);
            }
            Defense_Figures(cards.cardPower);
        }

        public void DataInit(GameData data)
        {
            this.data = data;
        }

        public void UpdateDisplay(int pHp, int eHp)
        {
            this.hpText.text = eHp.ToString();
        }
    }
}


