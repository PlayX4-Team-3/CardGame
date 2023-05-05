using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using ObserverPattern;
//using DG.Tweening;

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

        public bool is109Debuff = false;
        public int duration109 = 0;

        public bool is307Debuff = false;

        public bool isbind = false;

        public RPS rps;


        private void Awake()
        {
            //체력 초기화
            if (SceneChange.Instance != null)
            {
                if (SceneChange.Instance.roundIndex == 0)
                    InitHp(30);
                else if (SceneChange.Instance.roundIndex == 1)
                    InitHp(45);
                else
                    InitHp(60);
            }
            else
                InitHp(30);
            
            //방어력 초기화
            Defense_Figures = 0;

            //HP 슬라이더 MaxValue 초기화
            hpBar.maxValue = this.MaxHp;

            rps = RPS.None;
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

            hpBar.value = Hp;
        }

        public void CheckBuff()
        {
        }

        public void CheckDebuff()
        {
            if (is109Debuff)
            {
                DotweenManager.Instance.AttackAnim(this.gameObject, 0.25f);
                
                Hp--;
                duration109++;

                if (duration109 == 3)
                {
                    is109Debuff = false;
                    duration109 = 0;
                }
            }

            //if(isbind)
            //{
            //    DotweenManager.Instance.EndBind(this.gameObject);
            //}
        }

    }
}