using System.Collections;
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

        // Start is called before the first frame update
        protected virtual void Awake()
        {
            SetMyAnimator();            
        }

        void SetMyAnimator()//TODO:FINE Better Method Name
        {
            if (!myAnimator)//??????
            {
                myAnimator = GetComponent<Animator>();
            }
            for (int index = 0; index < myAnimator.parameterCount; index++)
            {
                myAnimtorParameters.Add(myAnimator.GetParameter(index));
            }
        }

        public virtual void AnimatorControllerSwicher(AnimatorOverrideController animatorOverride)
        {
            myAnimator.runtimeAnimatorController = animatorOverride;
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