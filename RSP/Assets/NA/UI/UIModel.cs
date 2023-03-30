using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AllCharacter;

namespace manager
{
    public class UIModel : MonoBehaviour
    {
        public Player player;
        public Enemy enemy;

        private void Awake()
        {
            
        }

        /// <summary>
        /// 플레이어 체력 텍스트
        /// </summary>
        public int PHpbar { get; set; }

        /// <summary>
        /// 적 체력 텍스트
        /// </summary>
        public int EHpbar { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="amount">플레이어 체력 입력</param>
        public void PlayerHpbar(int amount)
        {
            player.Hp += amount;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="amount">적 체력 입력</param>
        public void EnemyHpbar(int amount)
        {
            enemy.Hp += amount;
        }
    }
}
