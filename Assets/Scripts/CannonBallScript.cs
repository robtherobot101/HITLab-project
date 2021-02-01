using System.Collections;
using Managers;
using UnityEngine;

public class CannonBallScript : MonoBehaviour
{

    [SerializeField] private AudioSource splashSound;

    [SerializeField] private TrailRenderer trail;
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(nameof(CheckHeight));
    }

    private IEnumerator CheckHeight()
    {
        yield return new WaitUntil(() => transform.position.y <= 0);
        splashSound.Play();
        EventManager.Instance.sunk?.Invoke();
        trail.enabled = false;
        Destroy(gameObject, 3);
    }
}