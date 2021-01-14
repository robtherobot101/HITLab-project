using System.Collections;
using Managers;
using UnityEngine;
using Utils;

public class EnemyScript : MonoBehaviour
{
    private const float SinkHeight = -65f;
    private Vector3 _initialPosition;
    private Quaternion _initialRotation;
    private Rigidbody _rb;
    private Transform _tr;

    private void Reset()
    {
        _rb.useGravity = false;

        _rb.angularVelocity = Vector3.zero;
        _rb.velocity = Vector3.zero;
        _tr.rotation = _initialRotation;
        _tr.position = _initialPosition - 500 * _tr.forward + 10 * Vector3.down;

        StartCoroutine(Lerper.Lerp(_tr, _initialPosition, _initialRotation, 3));
    }

    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _tr = gameObject.transform;
        _initialPosition = _tr.position;
        _initialRotation = _tr.rotation;
        //EventManager.Instance.reset += Reset;
        Reset();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CannonBall"))
        {
            Destroy(other.gameObject);
            StartCoroutine(nameof(Sinking));
        }
    }

    private IEnumerator Sinking()
    {
        _rb.useGravity = true;
        _rb.angularVelocity = Vector3.back / 5;
        yield return new WaitUntil(() => transform.position.y < SinkHeight);
        EventManager.Instance.sunk?.Invoke();
        Reset();
    }
}