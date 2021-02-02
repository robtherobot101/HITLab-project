using System.Collections;
using UnityEngine;

namespace Utils
{
    public static class Lerper
    {
        public static IEnumerator Lerp(Transform tf, Vector3 goalPos, Quaternion goalRot, float duration)
        {
            float time = 0;
            var startPos = tf.localPosition;
            var startRot = tf.localRotation;

            while (time < duration)
            {
                var t = time / duration;
                t = t * t * (3f - 2f * t);
                tf.localPosition = Vector3.Lerp(startPos, goalPos, t);
                tf.localRotation = Quaternion.Lerp(startRot, goalRot, t);
                time += Time.deltaTime;
                yield return null;
            }

            tf.localPosition = goalPos;
            tf.localRotation = goalRot;
        }

        public static IEnumerator Lerp(Transform tf, Vector3 goalPos, float duration)
        {
            yield return Lerp(tf, goalPos, tf.localRotation, duration);
        }
    }
}