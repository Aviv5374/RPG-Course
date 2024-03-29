﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorHandler : MonoBehaviour
    {
        protected Animator myAnimator;
        protected List<AnimatorControllerParameter> myAnimtorParameters = new List<AnimatorControllerParameter>();

        public bool IsInIdle
        {
            get { return myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle"); }
        }
        
        protected virtual void Awake()
        {
            SetMyAnimator();            
        }

        void SetMyAnimator()//TODO:FINE Better Method Name
        {
            //This suppose to be in Awake and just in case also in a property
            if (!myAnimator)
            {
                myAnimator = GetComponent<Animator>();
            }
            //TODO: If myAnimator is still null after GetComponent<Animator>() I am Fuck. Fix it.

            //This suppose to be in Start 
            for (int index = 0; index < myAnimator.parameterCount; index++)
            {
                myAnimtorParameters.Add(myAnimator.GetParameter(index));
            }
        }

        public virtual void AnimatorControllerSwicher(AnimatorOverrideController animatorOverride)
        {
            var overrideController = myAnimator.runtimeAnimatorController as AnimatorOverrideController;
            if (animatorOverride)
            {
                myAnimator.runtimeAnimatorController = animatorOverride;
            }
            else if (overrideController)
            {
                myAnimator.runtimeAnimatorController = overrideController.runtimeAnimatorController;
            }
            
        }

        //TODO:Maybe private Update is Better? DO i need the Update method????
        //protected virtual void Update()
        //{
        //    if (IsInIdle)
        //    {
        //        return;
        //    }
        //}
    }
}