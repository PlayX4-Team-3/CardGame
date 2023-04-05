using System;
using System.Collections.Generic;
using Unity;

namespace AllCharacter
{
    public interface ICharacter
    {
        /// <summary>
        /// 플레이어 / 적 체력
        /// </summary>
        int Hp { get; set; }

        /// <summary>
        /// 플레이어 최대 체력
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
        /// 플레이어 생존 여부
        /// </summary>
        bool IsDie { get; set; }
    }
}
