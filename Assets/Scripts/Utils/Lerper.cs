using System.Collections;
using UnityEngine;

namespace Utils
{
    public static class Lerper
    {
        public static IEnumerator Lerp(Transform tf, Vector3 goalPos, Quaternion goalRot, float duration)
        {
            float time = 0;
            var startPos = tf.position;
            var startRot = tf.rotation;

            while (time < duration)
            {
                var t = time / duration;
                t = t * t * (3f - 2f * t);
                tf.position = Vector3.Lerp(startPos, goalPos, t);
                tf.rotation = Quaternion.Lerp(startRot, goalRot, t);
                time += Time.deltaTime;
                yield return null;
            }

            tf.position = goalPos;
            tf.rotation = goalRot;
        }

        public static IEnumerator Lerp(Transform tf, Vector3 goalPos, float duration)
        {
            yield return Lerp(tf, goalPos, tf.rotation, duration);
        }
    }
}