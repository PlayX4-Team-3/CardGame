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
        /// �÷��̾� ü�� �ؽ�Ʈ
        /// </summary>
        public int PHpbar
        { 
            get => player.Hp; 
            set => player.Hp = value; 
        }

        /// <summary>
        /// �� ü�� �ؽ�Ʈ
        /// </summary>
        public int EHpbar
        {
            get => enemy.Hp;
            set => enemy.Hp = value;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="amount">�÷��̾� ü�� �Է�</param>
        public void PlayerHpbar(int amount)
        {
            PHpbar += amount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="amount">�� ü�� �Է�</param>
        public void EnemyHpbar(int amount)
        {
            EHpbar += amount;
        }
    }
}
