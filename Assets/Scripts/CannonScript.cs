using System;
using System.Collections;
using Facilitator;
using Grinder;
using Managers;
using Scenarios.Goals;
using UnityEngine;
using Utils;

public class CannonScript : MonoBehaviour
{
    private const float MoveTime = 2f;

    [SerializeField] private AudioClip fireSound;
    [SerializeField] private AudioClip moveSound;
    [SerializeField] private GameObject cannonBallPrefab;

    [SerializeField] private Vector3 firingPosition = new Vector3(8.11462307f, 4.34617138f, 0.218954653f);
    [SerializeField] private Vector3 preparingPosition = new Vector3(6.20499992f, 4.34617138f, 0.218954653f);
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Transform cannonBallSpawn;
    [SerializeField] private Material material;
    private readonly Quaternion _preparingRotation = Quaternion.Euler(0, 180, 0);
    private bool _firingPosition;

    private Fraction _gunpowder;

    private Color _initialColour;
    private bool _moving;
    private bool _primed;

    // Coefficients for trajectory polynomial
    private const float a = 1.0015f;
    private const float b = 0.8825f;
    private const float c = 12.032f;

    private void Reset()
    {
        StartCoroutine(_moveCannon(preparingPosition, _preparingRotation, MoveTime));
        _firingPosition = false;
    }

    // Start is called before the first frame update
    private void Start()
    {
        EventManager.Instance.missed += Reset;
        EventManager.Instance.reset += Reset;
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
        _gunpowder = o.GetComponent<HeapScript>().Size;

        var lookDirection = Quaternion.LookRotation(GameManager.Instance.EnemyPosition - transform.position).eulerAngles
            .y;
        StartCoroutine(_moveCannon(firingPosition, Quaternion.Euler(0, lookDirection, 0), MoveTime));
        _firingPosition = true;
        _primed = true;
    }

    private void OnMouseEnter()
    {
        _initialColour = material.color;
        material.color = Color.yellow;
    }

    private void OnMouseExit()
    {
        material.color = _initialColour;
    }

    private void Fire()
    {
        audioSource.PlayOneShot(fireSound);
        _primed = false;
        var cannonBall = Instantiate(cannonBallPrefab);
        cannonBall.transform.position = cannonBallSpawn.position;
        cannonBall.transform.rotation = transform.rotation;

        StartCoroutine(FacilitatorScript.Instance.Bounce());
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
    }

    private IEnumerator _moveCannon(Vector3 goalPos, Quaternion goalRot, float duration)
    {
        _moving = true;
        audioSource.PlayOneShot(moveSound);
        yield return Lerper.Lerp(gameObject.transform, goalPos, goalRot, duration);
        _moving = false;
    }
}