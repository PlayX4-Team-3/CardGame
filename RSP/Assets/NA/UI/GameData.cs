using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObserverPattern
{
    public class GameData : MonoBehaviour, ISubject
    {
        private List<IObserver> list_Observers = new List<IObserver>();

        private int pHp;
        private int eHp;

        public void RegisterObserver(IObserver observer)
        {
            list_Observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            list_Observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach(IObserver observer in list_Observers)
            {
                observer.UpdateHpText(this.pHp, this.eHp);
            }
        }

        public void UpdateHpText(int pHp, int eHp)
        {
            this.pHp = pHp;
            this.eHp = eHp;
            NotifyObservers();
        }
    }
}
