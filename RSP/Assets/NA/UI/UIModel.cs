using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AllCharacter;

namespace manager
{
    public class UIModel : MonoBehaviour
    {
        [SerializeField]
        private Player player;
        [SerializeField]
        public Enemy enemy;

        /// <summary>
        /// 플레이어 체력 텍스트
        /// </summary>
        public int PHpbar
        { 
            get => player.Hp; 
            set => player.Hp = value; 
        }

        /// <summary>
        /// 적 체력 텍스트
        /// </summary>
        public int EHpbar
        {
            get => enemy.Hp;
            set => enemy.Hp = value;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="amount">플레이어 체력 입력</param>
        public void PlayerHpbar(int amount)
        {
            PHpbar += amount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="amount">적 체력 입력</param>
        public void EnemyHpbar(int amount)
        {
            EHpbar += amount;
        }
    }
}
