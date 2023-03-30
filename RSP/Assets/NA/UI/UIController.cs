using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace manager
{
    public class UIController : MonoBehaviour
    {
        private UIModel model;
        private UIView view;

        void Start()
        {
            model = new UIModel();
            view = GetComponent<UIView>();
            view.UpdatePHpbar(model.PHpbar);
            view.UpdateEHpbar(model.EHpbar);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="amount"></param>
        public void PlayerHpbar(int amount)
        {
            model.PlayerHpbar(amount);
            view.UpdatePHpbar(model.PHpbar);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="amount"></param>
        public void EnemyHpbar(int amount)
        {
            model.EnemyHpbar(amount);
            view.UpdateEHpbar(model.EHpbar);
        }
    }
}
