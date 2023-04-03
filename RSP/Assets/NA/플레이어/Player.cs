using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AllCharacter
{
    public class Player : Character
    {
        private void Awake()
        {
            InitHp(100);
            InitCost(5);
        }
    }
}
