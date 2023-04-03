using System.Collections;
using System.Collections.Generic;
using AllCharacter;
using UnityEngine;

namespace AllCharacter
{
    public class Enemy : Character
    {
        private void Awake()
        {
            InitHp(100);
        }
    }
}


