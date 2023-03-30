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
        /// �÷��̾� ü�� �ؽ�Ʈ
        /// </summary>
        public int PHpbar { get; set; }

        /// <summary>
        /// �� ü�� �ؽ�Ʈ
        /// </summary>
        public int EHpbar { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="amount">�÷��̾� ü�� �Է�</param>
        public void PlayerHpbar(int amount)
        {
            player.Hp += amount;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="amount">�� ü�� �Է�</param>
        public void EnemyHpbar(int amount)
        {
            enemy.Hp += amount;
        }
    }
}
