using UnityEngine;
using Utils;

public class ShapeScript : MonoBehaviour
{
    protected bool Equals(ShapeScript other)
    {
        return faces == other.faces && edges == other.edges && vertices == other.vertices && shapeName == other.shapeName && fraction.Equals(other.fraction);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((ShapeScript) obj);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hashCode = base.GetHashCode();
            hashCode = (hashCode * 397) ^ faces;
            hashCode = (hashCode * 397) ^ edges;
            hashCode = (hashCode * 397) ^ vertices;
            hashCode = (hashCode * 397) ^ (shapeName != null ? shapeName.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ fraction.GetHashCode();
            return hashCode;
        }
    }

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