using System.Collections;
using Managers;
using UnityEngine;

public class CannonBallScript : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        // Set initial velocity
        // gameObject.GetComponent<Rigidbody>().velocity = transform.forward * 50 + Vector3.up * 10;
        StartCoroutine(nameof(CheckHeight));
    }

    private IEnumerator CheckHeight()
    {
        yield return new WaitUntil(() => transform.position.y <= 0);
        EventManager.Instance.missed?.Invoke();
        Destroy(gameObject);
    }
}