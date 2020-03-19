using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Characters
{
    public class CharacterAnimatorHandler : AnimatorHandler
    {
        protected override void Awake()
        {
            base.Awake();            
        }

        public void UpdateLocomotion(float speed)
        {
            myAnimator.SetFloat(myAnimtorParameters[0].name, speed);
        }

        public void TriggerAttack()
        {
            myAnimator.SetTrigger(myAnimtorParameters[1].name);
        }

        public void ResetAttackTrigger()
        {
            myAnimator.ResetTrigger(myAnimtorParameters[1].name);
        }

        public void TriggerStopAttack()
        {
            myAnimator.SetTrigger(myAnimtorParameters[2].name);
        }

        public void ResetStopAttackTrigger()
        {
            myAnimator.ResetTrigger(myAnimtorParameters[2].name);
        }

        public void TriggerDeath()
        {
            myAnimator.SetTrigger(myAnimtorParameters[3].name);
        }

    }
}