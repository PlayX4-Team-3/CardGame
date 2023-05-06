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

            //ü�� �ʱ�ȭ
            InitHp(20);
            //�ڽ�Ʈ �ʱ�ȭ
            InitCost(5);
            //���� �ʱ�ȭ
            Defense_Figures = 0;
            //Hp �����̴� MaxValue �ʱ�ȭ
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
            //���� ü�°� �ִ� ü�� �ؽ�Ʈ ���
            this.hpText.text = $"{pHp:F0}/{this.MaxHp:F0}";
            //���� �ؽ�Ʈ ���
            this.dfText.text = pDf.ToString();

            //Hp �����̴� value �ʱ�ȭ
            hpBar.value = Hp;
        }
    }
}
