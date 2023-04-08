using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AllCharacter
{
    public abstract class Character : MonoBehaviour, ICharacter
    {
        [SerializeField] private int currentHp;

        [SerializeField] private int maxHp;

        [SerializeField] private int remainingCost;

        [SerializeField] private int maxCost;

        [SerializeField] private int defense_Figures;

        public int MaxHp
        {
            get => maxHp;
            set => maxHp = value;
        }

        public int Hp
        {
            get => currentHp;
            set
            {
                currentHp = Mathf.Clamp(value, 0, MaxHp);
            }
        }

        public int MaxCost
        {
            get => maxCost;
            set => maxCost = value;
        }

        public int Cost
        {
            get => remainingCost;
            set
            {
                remainingCost = Mathf.Clamp(value, 0, MaxCost);
            }
        }

        public int Defense_Figures
        {
            get => defense_Figures;
            set => defense_Figures = value;
        }

        /// <summary>
        /// �÷��̾� ü�� �ʱ�ȭ
        /// </summary>
        /// <param name="amount">ĳ���� �ִ� ü�� �Է�</param>
        public void InitHp(int amount)
        {
            MaxHp = amount;
            Hp = MaxHp;
        }
        /// <summary>
        /// �÷��̾� �ڽ�Ʈ �ʱ�ȭ
        /// </summary>
        /// <param name="amount">ĳ���� �ִ� �ڽ�Ʈ �Է�</param>
        public void InitCost(int amount)
        {
            MaxCost = amount;
            Cost = MaxCost;
        }
    }
}

