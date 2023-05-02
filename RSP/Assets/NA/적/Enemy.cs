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
            //ü�� �ʱ�ȭ
            InitHp(40);
            //���� �ʱ�ȭ
            Defense_Figures = 0;

            //HP �����̴� MaxValue �ʱ�ȭ
            hpBar.maxValue = this.MaxHp;
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