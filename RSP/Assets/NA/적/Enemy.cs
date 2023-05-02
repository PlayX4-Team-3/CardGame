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
            //ü�� �ʱ�ȭ
            InitHp(40);
            //���� �ʱ�ȭ
            Defense_Figures = 0;

            //HP �����̴� MaxValue �ʱ�ȭ
            hpBar.maxValue = this.MaxHp;
        }

        private void Update()
        {
            //HP �����̴� Value �ʱ�ȭ
            hpBar.value = Hp;
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

            //HP �����̴� Value �ʱ�ȭ
            hpBar.value = Hp;
        }
    }
}


