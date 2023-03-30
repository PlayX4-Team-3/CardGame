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
            model = GetComponent<UIModel>();
            view = GetComponent<UIView>();
        }

        void Update()
        {
            view.UpdatePHpbar(model.PHpbar);
            view.UpdateEHpbar(model.EHpbar);
        }
    }
}
