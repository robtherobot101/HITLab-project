using UnityEngine;
using Utils;

public class ShapeScript : MonoBehaviour
{
    [SerializeField] private int faces;
    [SerializeField] private int edges;
    [SerializeField] private int vertices;
    [SerializeField] private string shapeName;
    [SerializeField] private Fraction fraction;

    public int Faces => faces;

    public int Edges => edges;

    public int Vertices => vertices;

    public string ShapeName => shapeName;

    public Fraction Fraction => fraction;

    private void Start()
    {
        transform.localScale += transform.localScale * (4 * fraction.Value());
    }
}