using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Characters.Player
{
    public class PlayerAnimatorHandler : AnimatorHandler
    {
        protected override void Start()
        {
            base.Start();
        }

        public void UpdateLocomotion(float speed)
        {
            myAnimator.SetFloat(myAnimtorParameters[0].name, speed);
        }



    }
}