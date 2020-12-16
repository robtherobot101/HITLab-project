using System.Collections;
using Managers;
using UnityEngine;

public class CannonBallScript : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        // Set initial velocity
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(50, 10, 0);
        StartCoroutine(nameof(CheckHeight));
    }

    private IEnumerator CheckHeight()
    {
        yield return new WaitUntil(() => transform.position.y <= 0);
        EventManager.Instance.missed?.Invoke();
        Destroy(gameObject);
    }
}