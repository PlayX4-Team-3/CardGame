using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using ObserverPattern;
//using DG.Tweening;

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

        private void Awake()
        {
            animator = GetComponent<Animator>();

            //ü�� �ʱ�ȭ
            InitHp(10);
            //�ڽ�Ʈ �ʱ�ȭ
            InitCost(5);
            //���� �ʱ�ȭ
            Defense_Figures = 0;
            //Hp �����̴� MaxValue �ʱ�ȭ
            hpBar.maxValue = this.MaxHp;
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

        /// <summary>
        /// Attack animation next Slash animation on
        /// </summary>
        public void PlayerSlash()
        {
            animator.SetTrigger("PisSlash");
        }

        //////////// Animation part
        //////////public void AttackAnim(GameObject target, float duration)
        //////////{
        //////////    target.gameObject.transform.DOShakePosition(duration);

        //////////    // Camera Shake
        //////////    //GameObject.FindWithTag("BG").transform.DOShakePosition(2f);
        //////////}

        //////////public void DefenseAnim()
        //////////{
        //////////    this.gameObject.transform.DOScale(new Vector3(1.8f, 1.8f, 1f), 0.3f).OnComplete(() =>
        //////////    {
        //////////        this.gameObject.transform.DOScale(new Vector3(1.5f, 1.5f, 1f), 0.3f);
        //////////    }
        //////////    );
        //////////}

        public void CheckBuff()
        {
            Debug.Log("Buff Check");
        }

        public void CheckDebuff()
        {
            Debug.Log("Check Debuff");
        }

    }
}
