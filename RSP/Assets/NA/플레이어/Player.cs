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

        private void Awake()
        {
            //체력 초기화
            InitHp(10);
            //코스트 초기화
            InitCost(5);
            //방어력 초기화
            Defense_Figures = 0;
            //Hp 슬라이더 MaxValue 설정
            hpBar.maxValue = Hp;
        }

        private void Update()
        {
            //Hp 슬라이더 value 설정
            hpBar.value = Hp;
            
            if(Input.GetKeyDown(KeyCode.E))
            {
                Hp -= 1;
            }
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
