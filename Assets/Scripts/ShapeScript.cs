using System;
using UnityEngine;
using Utils;

public class ShapeScript : MonoBehaviour
{

    private Camera _camera;
    private const float ZDistance = 2f;
    private const float SpinFrequency = 0.5f;

    public int Faces => faces;
    [SerializeField] private int faces;

    public int Edges => edges;
    [SerializeField] private int edges;

    public int Vertices => vertices;
    [SerializeField] private int vertices;

    public string ShapeName => shapeName;
    [SerializeField] private string shapeName;

    public int Numerator => numerator;
    [SerializeField] private int numerator;
    
    public int Denominator => denominator;
    [SerializeField] private int denominator;

    public Fraction Fraction => _fraction;
    private Fraction _fraction;

    private void OnEnable()
    {
        _fraction = new Fraction(numerator, denominator);
    }

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _camera.ScreenToWorldPoint(Input.mousePosition + ZDistance * Vector3.forward);
        transform.Rotate(Vector3.up, Time.deltaTime * 360 * SpinFrequency);
    }
}