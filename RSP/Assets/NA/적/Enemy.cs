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

            //HP Sldier Value �ʱ�ȭ
            hpBar.maxValue = Hp;
        }

        private void Update()
        {
            hpBar.value = Hp;
            if (Input.GetKeyDown(KeyCode.E))
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
            this.hpText.text = eHp.ToString();
            this.dfText.text = eDf.ToString();
        }
    }
}


