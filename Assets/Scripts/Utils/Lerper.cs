using System;
using System.Collections;
using UnityEngine;

namespace Utils
{
    public static class Lerper
    {
        public static IEnumerator Lerp(Transform tf, float duration, Vector3? goalPos=null, Quaternion? goalRot=null)
        {
            float time = 0;
            var startPos = tf.position;
            var startRot = tf.rotation;

            while (time < duration)
            {
                var t = time / duration;
                t = t * t * (3f - 2f * t);
                if (goalPos.HasValue) tf.position = Vector3.Lerp(startPos, goalPos.Value, t);
                if (goalRot.HasValue) tf.rotation = Quaternion.Slerp(startRot, goalRot.Value, t);
                time += Time.deltaTime;
                yield return null;
            }

            if (goalPos.HasValue) tf.position = goalPos.Value;
            if (goalRot.HasValue) tf.rotation = goalRot.Value;
        }

        public static IEnumerator Lerp(Transform tf, float duration, float degrees, Vector3 axis)
        {
            float time = 0;
            var startRot = tf.rotation;

            while (time < duration)
            {
                var t = time / duration;
                t = t * t * (3f - 2f * t);
                tf.rotation = Quaternion.SlerpUnclamped(startRot, startRot * Quaternion.AngleAxis(degrees / 2f, axis), 2 * t);
                time += Time.deltaTime;
                yield return null;
            }
        }
    }
}