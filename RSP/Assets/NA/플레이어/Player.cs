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
            //ü�� �ʱ�ȭ
            InitHp(10);
            //�ڽ�Ʈ �ʱ�ȭ
            InitCost(5);
            //���� �ʱ�ȭ
            Defense_Figures = 0;
            //Hp �����̴� MaxValue ����
            hpBar.maxValue = Hp;
        }

        private void Update()
        {
            //Hp �����̴� value ����
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
