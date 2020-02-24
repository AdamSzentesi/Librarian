using System;
using System.Collections;
using UnityEngine;

namespace Librarian
{
    public class SpriteAnimator : MonoBehaviour
    {
        public SpriteAnimation Animation;

        private Coroutine _AnimateCoroutine;

        public void StartAnimation(Action onAnimationEnd)
        {
            if (Animation)
            {
                _AnimateCoroutine = StartCoroutine(Animate(onAnimationEnd, Animation.Duration));
            }
        }

        private IEnumerator Animate(Action onAnimationEnd, float duration)
        {
            float time = 0.0f;

            while (time < duration)
            {
                yield return null;
                time += Time.deltaTime;
            }

            if (onAnimationEnd != null)
            {
                onAnimationEnd.Invoke();
            }
        }

        private void OnDestroy()
        {
            if (_AnimateCoroutine != null)
            {
                StopCoroutine(_AnimateCoroutine);
            }
        }

    }
}
