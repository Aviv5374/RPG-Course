﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Characters.Player
{
    public class PlayerAnimatorHandler : CharacterAnimatorHandler
    {
        protected override void Awake()
        {
            base.Awake();
        }

        //public void UpdateLocomotion(float speed)
        //{
        //    myAnimator.SetFloat(myAnimtorParameters[0].name, speed);
        //}

        //public void TriggerAttack()
        //{
        //    myAnimator.SetTrigger(myAnimtorParameters[1].name);            
        //}

    }
}