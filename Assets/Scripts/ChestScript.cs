using UnityEngine;
using Utils;

public class ChestScript : MonoBehaviour
{
    private void OnEnable()
    {
        var t = transform;
        var initialPosition = t.position;
        t.position = initialPosition + 10 * Vector3.down + 50 * Vector3.right;

        StartCoroutine(Lerper.Lerp(t, initialPosition, t.rotation, 3));
    }
}