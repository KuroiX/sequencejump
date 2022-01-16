using System;
using System.Collections;
using UnityEngine;

namespace Core.Queue
{
    public static class AnimationRoutines
    {
        public static IEnumerator LinearInterpolateRoutine(Vector2 from, Vector2 to, float animationTime, Action<Vector2> onStepCallback, bool isLooping = false, Action onFinishedCallback = null)
        {
            var direction = Direction(@from, to);
            
            float elapsedTime = 0f;

            bool isGoingForward = true;

            var position = from;

            while (true)
            {
                elapsedTime += Time.deltaTime;

                bool reachedEnd = elapsedTime >= animationTime;

                elapsedTime = reachedEnd ? animationTime : elapsedTime;

                var newSize = position + direction * elapsedTime / animationTime;
                
                onStepCallback(newSize);

                if (reachedEnd && isLooping) 
                {
                    elapsedTime = 0;
                    isGoingForward = !isGoingForward;
                    position = isGoingForward ? from : to;
                    direction *= -1;
                }
                else if (reachedEnd)
                {
                    break;
                }

                yield return null;
            }

            onFinishedCallback?.Invoke();
        }

        private static Vector2 Direction(Vector2 @from, Vector2 to)
        {
            return to - @from;
        }
        
        private static float Direction(float @from, float to)
        {
            return to - @from;
        }

        public static IEnumerator LinearInterpolateRoutine(float from, float to, float animationTime, Action<float> onStepCallback, bool isLooping = false, Action onFinishedCallback = null)
        {
            var direction = Direction(@from, to);
            
            float elapsedTime = 0f;

            bool isGoingForward = true;

            var position = from;

            while (true)
            {
                elapsedTime += Time.deltaTime;

                bool reachedEnd = elapsedTime >= animationTime;

                elapsedTime = reachedEnd ? animationTime : elapsedTime;

                var newSize = position + direction * elapsedTime / animationTime;
                
                onStepCallback(newSize);

                if (reachedEnd && isLooping) {
                    elapsedTime = 0;
                    isGoingForward = !isGoingForward;
                    position = isGoingForward ? from : to;
                    direction *= -1;
                } 
                else if (reachedEnd)
                {
                    break;
                }

                yield return null;
            }
            
            onFinishedCallback?.Invoke();
        }
    }
}