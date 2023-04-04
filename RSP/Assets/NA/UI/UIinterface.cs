using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObserverPattern
{
    public interface ISubject
    {
        /// <summary>
        /// ������ ���
        /// </summary>
        /// <param name="observer"></param>
        void RegisterObserver(IObserver observer);
        
        /// <summary>
        /// ������ ����
        /// </summary>
        /// <param name="observer"></param>
        void RemoveObserver(IObserver observer);


        /// <summary>
        /// �������鿡�� ���� ����
        /// </summary>
        void NotifyObservers();
    }

    public interface IObserver
    {
        /// <summary>
        /// �÷��̾�, �� ü�� ������Ʈ
        /// </summary>
        /// <param name="php">�÷��̾� ü�� ��</param>
        /// <param name="ehp">�� ü�� ��</param>
        void UpdateHpText(int php, int ehp);
    }
}
