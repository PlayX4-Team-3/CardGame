using System;
using System.Collections.Generic;
using Unity;

namespace AllCharacter
{
    public interface ICharacter
    {
        /// <summary>
        /// �÷��̾� / �� ü��
        /// </summary>
        int Hp { get; set; }

        /// <summary>
        /// �÷��̾� �ִ� ü��
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
        /// �÷��̾� ���� ����
        /// </summary>
        bool IsDie { get; set; }
    }
}
