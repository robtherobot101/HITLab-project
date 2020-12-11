using System;
using UnityEngine;
using Utils;

public class ShapeScript : MonoBehaviour
{
    
    public int Faces => faces;
    [SerializeField] private int faces;

    public int Edges => edges;
    [SerializeField] private int edges;

    public int Vertices => vertices;
    [SerializeField] private int vertices;

    public string ShapeName => shapeName;
    [SerializeField] private string shapeName;

    public Fraction Fraction => fraction;
    [SerializeField] private Fraction fraction;
}