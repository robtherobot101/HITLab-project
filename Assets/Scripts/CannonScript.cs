using System;
using System.Collections;
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
    private AudioSource _audio;
    private bool _firingPosition;
    private Fraction _gunpowder;


    private Color _initialColour;
    private Material _material;
    private bool _moving;
    private bool _primed;
    private readonly Quaternion _firingRotation = Quaternion.Euler(0, 0, 0);
    private readonly Quaternion _preparingRotation = Quaternion.Euler(0, 90, 0);

    private void Reset()
    {
        StartCoroutine(_moveCannon(preparingPosition, _preparingRotation, MoveTime));
        _firingPosition = false;
    }

    // Start is called before the first frame update
    private void Start()
    {
        _audio = gameObject.GetComponent<AudioSource>();
        _material = gameObject.GetComponent<Renderer>().material;
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
        StartCoroutine(_moveCannon(firingPosition, _firingRotation, MoveTime));
        _firingPosition = true;
        _primed = true;
    }

    private void OnMouseEnter()
    {
        _initialColour = _material.color;
        _material.color = Color.yellow;
    }

    private void OnMouseExit()
    {
        _material.color = _initialColour;
    }

    private void Fire()
    {
        _audio.PlayOneShot(fireSound);
        _primed = false;
        var cannonBall = Instantiate(cannonBallPrefab);
        StartCoroutine(FacilitatorScript.Instance.Bounce());
        var rb = cannonBall.GetComponent<Rigidbody>();
        // rb.velocity *= _gunpowder.Value();
        var goalsOutcome = GameManager.Instance.GoalsOutcome();
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
        _audio.PlayOneShot(moveSound);
        yield return Lerper.Lerp(gameObject.transform, goalPos, goalRot, duration);
        _moving = false;
    }
}