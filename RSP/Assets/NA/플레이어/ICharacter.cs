using System;
using System.Collections.Generic;
using Unity;

namespace AllCharacter
{
    public interface ICharacter
    {
        /// <summary>
        /// 캐릭터 체력
        /// </summary>
        int Hp { get; set; }

        /// <summary>
        /// 캐릭터 최대 체력
        /// </summary>
        int MaxHp { get; set; }

        /// <summary>
        /// 플레이어 코스트
        /// </summary>
        int Cost { get; set; }

        /// <summary>
        /// 플레이어 최대 코스트
        /// </summary>
        int MaxCost { get; set; }

        /// <summary>
        /// 캐릭터 방어수치
        /// </summary>
        int Defense_Figures { get; set; }
    }
}
