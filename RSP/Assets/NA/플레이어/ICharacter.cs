using System;
using System.Collections.Generic;
using Unity;

namespace AllCharacter
{
    public interface ICharacter
    {
        /// <summary>
        /// ĳ���� ü��
        /// </summary>
        int Hp { get; set; }

        /// <summary>
        /// ĳ���� �ִ� ü��
        /// </summary>
        int MaxHp { get; set; }

        /// <summary>
        /// �÷��̾� �ڽ�Ʈ
        /// </summary>
        int Cost { get; set; }

        /// <summary>
        /// �÷��̾� �ִ� �ڽ�Ʈ
        /// </summary>
        int MaxCost { get; set; }

        /// <summary>
        /// ĳ���� ����ġ
        /// </summary>
        int Defense_Figures { get; set; }
    }
}
