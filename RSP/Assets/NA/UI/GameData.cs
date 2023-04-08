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
        private int pDf;
        private int eDf;

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
                observer.UpdateDisplay(this.pHp, this.eHp, this.pDf, this.eDf);
            }
        }

        public void UpdateDisplay(int pHp, int eHp, int pDf, int eDf)
        {
            this.pHp = pHp;
            this.eHp = eHp;
            this.pDf = pDf;
            this.eDf = eDf;
            NotifyObservers();
        }
    }
}
