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
            //ü�� �ʱ�ȭ
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
            
            //���� �ʱ�ȭ
            Defense_Figures = 0;

            //HP �����̴� MaxValue �ʱ�ȭ
            hpBar.maxValue = this.MaxHp;

            rps = RPS.None;
        }

        public void DataInit(GameData data)
        {
            this.data = data;
        }

        public void UpdateDisplay(int pHp, int eHp, int pDf, int eDf)
        {
            //���� ü�°� �ִ� ü�� �ؽ�Ʈ ���
            this.hpText.text = $"{eHp:F0}/{this.MaxHp:F0}";
            //���� �ؽ�Ʈ ���
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