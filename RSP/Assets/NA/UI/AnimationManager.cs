using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using AllCharacter;

namespace manager
{
    public class AnimationManager : MonoBehaviour
    {
        public UnityEvent animationEvent;
        private Character character;
        public Animator[] animator = new Animator[2];
        Material material;

        public void PlayerAttack()
        {
            animator[0].SetTrigger("PisAttack"); 
        }

        public void PlayerHit()
        {
            animator[0].SetTrigger("PisHit");
        }

        public void EnemyAttack()
        {
            animator[1].SetTrigger("EisAttack");
        }

        public void EnemyHit()
        {
            animator[1].SetTrigger("EisHit");
        }
    }
}
