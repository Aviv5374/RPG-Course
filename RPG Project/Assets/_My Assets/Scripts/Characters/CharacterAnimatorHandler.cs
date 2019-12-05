using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Characters
{
    public class CharacterAnimatorHandler : AnimatorHandler
    {
        protected override void Start()
        {
            base.Start();
        }

        public void UpdateLocomotion(float speed)
        {
            myAnimator.SetFloat(myAnimtorParameters[0].name, speed);
        }

        public void TriggerAttack()
        {
            myAnimator.SetTrigger(myAnimtorParameters[1].name);
        }

    }
}