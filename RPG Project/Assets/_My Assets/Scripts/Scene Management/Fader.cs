using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvasGroup;
        Coroutine currentActiveFade = null;

        void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();

            //StartCoroutine(FadeOutIn());//For Testing
        }

        public void FadeOutImmediate()
        {
            canvasGroup.alpha = 1;
        }

        IEnumerator FadeOutIn()
        {
            yield return FadeOut(3f);
            print("Faded out");
            yield return FadeIn(1f);
            print("Faded in");
        }

        public IEnumerator FadeOut(float time)
        {
            return Fade(1, time);
        }

        public IEnumerator FadeIn(float time)
        {
            return Fade(0, time);
        }

        IEnumerator Fade(float target, float time)
        {
            if (currentActiveFade != null)
            {
                StopCoroutine(currentActiveFade);
            }
            currentActiveFade = StartCoroutine(FadeRoutine(target, time));
            yield return currentActiveFade;
        }

        IEnumerator FadeRoutine(float target, float time)
        {
            while (!Mathf.Approximately(canvasGroup.alpha, target))
            {
                canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, target, Time.deltaTime / time);
                yield return null;
            }
        }
    }
}
