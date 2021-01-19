using System.Collections;
using Managers;
using UnityEngine;

public class CannonBallScript : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(nameof(CheckHeight));
    }

    private IEnumerator CheckHeight()
    {
        yield return new WaitUntil(() => transform.position.y <= 0);
        EventManager.Instance.missed?.Invoke();
        Destroy(gameObject);
    }
}