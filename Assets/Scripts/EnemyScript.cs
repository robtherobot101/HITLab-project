using System.Collections;
using Managers;
using UnityEngine;
using Utils;

public class EnemyScript : MonoBehaviour
{

    [SerializeField] private AudioSource destroySound;
    
    private const float SinkHeight = -55f;
    private Vector3 _initialPosition;
    private Quaternion _initialRotation;
    private Rigidbody _rb;
    private Transform _tr;

    // Start is called before the first frame update
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _tr = gameObject.transform;
        _initialPosition = _tr.position;
        _initialRotation = _tr.rotation;
        //EventManager.Instance.reset += Reset;
        //Reset();
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _rb.useGravity = false;

        _rb.angularVelocity = Vector3.zero;
        _rb.velocity = Vector3.zero;
        _tr.rotation = _initialRotation;
        _tr.position = _initialPosition - 500 * _tr.forward + 10 * Vector3.down;

        StartCoroutine(Coroutines.OneAfterTheOther(Lerper.Lerp(_tr, _initialPosition, _initialRotation, 3), Rock()));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("CannonBall")) return;
        destroySound.Play();
        Destroy(other.gameObject);
        StartCoroutine(nameof(Sinking));
    }

    private IEnumerator Sinking()
    {
        _rb.useGravity = true;
        _rb.angularVelocity = Vector3.back / 5;
        yield return new WaitUntil(() => transform.position.y < SinkHeight);
        gameObject.SetActive(false);
        EventManager.Instance.sunk?.Invoke();
        //Reset();
    }

    private IEnumerator Rock()
    {
        var tx = 0f;
        var tz = 0f;
        var initialRotation = _tr.rotation;
        while (!_rb.useGravity)
        {
            _tr.rotation = initialRotation * Quaternion.SlerpUnclamped(Quaternion.identity, Quaternion.Euler(1, 0, 0), Mathf.Sin(tx))
                                           * Quaternion.SlerpUnclamped(Quaternion.identity, Quaternion.Euler(0, 0, 2), Mathf.Sin(tz));
            tx += Time.deltaTime * Random.Range(0.5f, 2f);
            tz += Time.deltaTime * Random.Range(0.5f, 2f);;
            yield return null;
        }
    }
}