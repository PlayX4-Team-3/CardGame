using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace manager
{
    public class UIView : MonoBehaviour
    {
        public Text phpText;
        public Text ehpText;
        //public Slider phpSlider;
        //public Slider ehpSlider;

        public void UpdatePHpbar(int phpbar)
        {
            phpText.text = "HP : " + phpbar ;
        }
        public void UpdateEHpbar(int ehpbar)
        {
            ehpText.text = "HP : " + ehpbar;
        }
    }
}
