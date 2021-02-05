using System;
using System.Collections;
using System.Numerics;
using Facilitator;
using Grinder;
using Managers;
using Scenarios.Goals;
using UnityEngine;
using UnityEngine.Animations;
using Utils;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class CannonScript : MonoBehaviour
{
    private const float MoveTime = 2f;

    // Coefficients for trajectory polynomial
    private const float a = 1.0015f;
    private const float b = 0.8825f;
    private const float c = 12.032f;

    [SerializeField] private AudioClip fireSound;
    [SerializeField] private AudioClip moveSound;
    [SerializeField] private ParticleSystem explosionEffect;
    [SerializeField] private GameObject cannonBallPrefab;

    [SerializeField] private Transform firingPosition;
    [SerializeField] private Transform preparingPosition;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Transform cannonBallSpawn;
    [SerializeField] private Material material;
    private bool _firingPosition;
    
    private Color _initialColour;
    private bool _moving;
    private bool _primed;

    private void Reset()
    {
        StartCoroutine(_moveCannon(preparingPosition.position, preparingPosition.rotation, MoveTime));
        _firingPosition = false;
    }

    // Start is called before the first frame update
    private void Start()
    {
        // EventManager.Instance.missed += Reset;
        EventManager.Instance.sunk += Reset;
    }

    private void OnMouseDown()
    {
        if (_moving) return;
        if (_firingPosition)
        {
            if (_primed) Fire();
            return;
        }

        var o = PlayerController.Instance.Take("Gunpowder");
        if (o == null) return;

        FacilitatorScript.Instance.Hide();
        StartCoroutine(_moveCannon(firingPosition.position, Quaternion.Euler(Vector3.Scale(Quaternion.LookRotation(GameManager.Instance.EnemyPosition - transform.position).eulerAngles, Vector3.up)), MoveTime));
        _firingPosition = true;
        _primed = true;
    }

    // private void OnMouseEnter()
    // {
    //     _initialColour = material.color;
    //     material.color = Color.yellow;
    // }
    //
    // private void OnMouseExit()
    // {
    //     material.color = _initialColour;
    // }

    private void Fire()
    {
        audioSource.PlayOneShot(fireSound);
        _primed = false;
        var cannonBall = Instantiate(cannonBallPrefab);
        cannonBall.transform.position = cannonBallSpawn.position;
        cannonBall.transform.rotation = transform.rotation;

        EventManager.Instance.cannonFired?.Invoke();
        var rb = cannonBall.GetComponent<Rigidbody>();
        var goalsOutcome = GameManager.Instance.GoalsOutcome();

        var distanceToShip = (GameManager.Instance.EnemyPosition - transform.position).magnitude;
        var velocityMultiplier = (float) Polynomial.QuadraticFormula(a, b, c - distanceToShip).Item1;

        rb.velocity = velocityMultiplier * (transform.forward * 5 + Vector3.up);
        switch (goalsOutcome)
        {
            case Outcome.Over:
                rb.velocity *= 2;
                break;
            case Outcome.Under:
                rb.velocity /= 2;
                break;
            case Outcome.Achieved:
                break;
            case Outcome.NotAchieved:
                rb.velocity *= 0;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        explosionEffect.Play();
        StartCoroutine(Coroutines.OneAfterTheOther(
            Lerper.Lerp(transform, transform.position - transform.forward * 0.7f - Vector3.down * 0.3f, transform.rotation * Quaternion.Euler(-5, 0, 10), 0.1f),
            Lerper.Lerp(transform, transform.position, transform.rotation, 0.5f)));
    }

    private IEnumerator _moveCannon(Vector3 goalPos, Quaternion goalRot, float duration)
    {
        _moving = true;
        audioSource.PlayOneShot(moveSound);
        yield return Lerper.Lerp(transform, goalPos, goalRot, duration);
        _moving = false;
    }
}