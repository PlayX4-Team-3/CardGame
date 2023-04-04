using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObserverPattern
{
    public interface ISubject
    {
        /// <summary>
        /// 옵저버 등록
        /// </summary>
        /// <param name="observer"></param>
        void RegisterObserver(IObserver observer);
        
        /// <summary>
        /// 옵저버 제거
        /// </summary>
        /// <param name="observer"></param>
        void RemoveObserver(IObserver observer);


        /// <summary>
        /// 옵저버들에게 내용 전달
        /// </summary>
        void NotifyObservers();
    }

    public interface IObserver
    {
        /// <summary>
        /// 플레이어, 적 체력 업데이트
        /// </summary>
        /// <param name="php">플레이어 체력 값</param>
        /// <param name="ehp">적 체력 값</param>
        void UpdateHpText(int php, int ehp);
    }
}
