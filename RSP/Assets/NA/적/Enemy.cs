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
        [SerializeField]
        private Text dfText;

        private GameData data;

        private void Awake()
        {
            InitHp(10);
            Defense_Figures = 0;
        }
        private void Update()
        {
            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    Hp -= 10;
            //    Debug.Log(Hp);
            //}
        }

        public void DataInit(GameData data)
        {
            this.data = data;
        }

        public void UpdateDisplay(int pHp, int eHp, int pDf, int eDf)
        {
            this.hpText.text = eHp.ToString();
            this.dfText.text = eDf.ToString();
        }
    }
}


