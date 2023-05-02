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

        public Slider hpBar;

        private GameData data;

        private void Awake()
        {
            //체력 초기화
            InitHp(40);
            //방어력 초기화
            Defense_Figures = 0;

            //HP 슬라이더 MaxValue 초기화
            hpBar.maxValue = this.MaxHp;
        }

        private void Update()
        {
            //HP 슬라이더 Value 초기화
            hpBar.value = Hp;
        }


        public void DataInit(GameData data)
        {
            this.data = data;
        }

        public void UpdateDisplay(int pHp, int eHp, int pDf, int eDf)
        {
            //현재 체력과 최대 체력 텍스트 출력
            this.hpText.text = $"{eHp:F0}/{this.MaxHp:F0}";
            //방어력 텍스트 출력
            this.dfText.text = eDf.ToString();

            //HP 슬라이더 Value 초기화
            hpBar.value = Hp;
        }
    }
}


