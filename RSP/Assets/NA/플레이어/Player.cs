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
        [SerializeField]
        private Text dfText;
        [SerializeField]
        private Slider hpSlider;

        private GameData data;

        private void Awake()
        {
            InitHp(10);
            InitCost(5);
            Defense_Figures = 0;
            hpSlider.value = Hp;
        }

        public void DataInit(GameData data)
        {
            this.data = data;
        }

        public void UpdateDisplay(int pHp, int eHp, int pDf, int eDf)
        {
            this.hpText.text = pHp.ToString();
            this.dfText.text = pDf.ToString();
        }
    }
}
