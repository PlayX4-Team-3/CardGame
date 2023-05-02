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


        private void Awake()
        {
            //체력 초기화
            InitHp(40);
            //방어력 초기화
            Defense_Figures = 0;

            //HP 슬라이더 MaxValue 초기화
            hpBar.maxValue = this.MaxHp;
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


        ////////////// Animation part
        ////////////public void AttackAnim(GameObject target, float duration)
        ////////////{
        ////////////    target.gameObject.transform.DOShakePosition(duration);
        ////////////}

        ////////////public void DefenseAnim()
        ////////////{
        ////////////    this.gameObject.transform.DOScale(new Vector3(2.3f, 2.3f, 1f), 0.3f).OnComplete(() =>
        ////////////    {
        ////////////        this.gameObject.transform.DOScale(new Vector3(2f, 2f, 1f), 0.3f);
        ////////////    }
        ////////////    );
        ////////////}

        public void CheckBuff()
        {
            Debug.Log("Check Enemy Buff");
        }

        public void CheckDebuff()
        {
            Debug.Log("Check Enemy Debuff");

            if (is109Debuff)
            {
                ////////////////AttackAnim(this.gameObject, 0.2f);

                Hp--;
                duration109++;

                if (duration109 == 3)
                {
                    is109Debuff = false;
                    duration109 = 0;
                }
            }

            if(is307Debuff)
            {

            }
        }

    }
}