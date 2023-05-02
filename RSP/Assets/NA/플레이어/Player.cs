using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using ObserverPattern;
using DG.Tweening;

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

            //체력 초기화
            InitHp(10);
            //코스트 초기화
            InitCost(5);
            //방어력 초기화
            Defense_Figures = 0;
            //Hp 슬라이더 MaxValue 초기화
            hpBar.maxValue = this.MaxHp;
        }

        public void DataInit(GameData data)
        {
            this.data = data;
        }

        public void UpdateDisplay(int pHp, int eHp, int pDf, int eDf)
        {
            //현재 체력과 최대 체력 텍스트 출력
            this.hpText.text = $"{pHp:F0}/{this.MaxHp:F0}";
            //방어력 텍스트 출력
            this.dfText.text = pDf.ToString();

            //Hp 슬라이더 value 초기화
            hpBar.value = Hp;
        }

        /// <summary>
        /// Attack animation next Slash animation on
        /// </summary>
        public void PlayerSlash()
        {
            animator.SetTrigger("PisSlash");
        }

        // Animation part
        public void AttackAnim(GameObject target, float duration)
        {
            target.gameObject.transform.DOShakePosition(duration);

            // Camera Shake
            //GameObject.FindWithTag("BG").transform.DOShakePosition(2f);
        }

        public void DefenseAnim()
        {
            this.gameObject.transform.DOScale(new Vector3(1.8f, 1.8f, 1f), 0.3f).OnComplete(() =>
            {
                this.gameObject.transform.DOScale(new Vector3(1.5f, 1.5f, 1f), 0.3f);
            }
            );
        }

    }
}
