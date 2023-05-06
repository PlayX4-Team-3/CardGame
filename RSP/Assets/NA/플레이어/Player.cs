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

        public Slider hpBar;

        private GameData data;

        private Animator animator;

        public bool have207buff = false;

        public bool have308buff = false;
        public int duration308;

        private void Awake()
        {
            animator = GetComponent<Animator>();

            //체력 초기화
            InitHp(20);
            //코스트 초기화
            InitCost(5);
            //방어력 초기화
            Defense_Figures = 0;
            //Hp 슬라이더 MaxValue 초기화
            hpBar.maxValue = this.MaxHp;

            have207buff = false;
            have308buff = false;

            duration308 = 0;
        }

        public void DataInit(GameData data)
        {
            this.data = data;
        }

        public void UpdateDisplay(int pHp, int eHp, int pDf, int eDf)
        {
            //현재 체력과 최대 체력 텍스트 출력
            this.hpText.text = $"{pHp:F0}/{this.MaxHp:F0}";
            //방어력 텍스트 출력
            this.dfText.text = pDf.ToString();

            //Hp 슬라이더 value 초기화
            hpBar.value = Hp;
        }
    }
}
