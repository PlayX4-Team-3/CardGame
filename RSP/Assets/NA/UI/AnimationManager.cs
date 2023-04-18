using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AllCharacter;

namespace manager
{
    public class AnimationManager : MonoBehaviour
    {
        private Character character;
        public Animator animator;
        Material material;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void PlayerAttack()
        {
            animator.SetBool("isAttack", true);
        }
        public void EnemyAttack()
        {
            animator.SetBool("isAttack", true);
        }

        public IEnumerator OnDamage()
        {
            material.color = Color.red;
            yield return new WaitForSeconds(0.2f);

            if (character.Hp > 0)
            {
                material.color = new Color(1, 1, 1);
            }
        }
    }
}
