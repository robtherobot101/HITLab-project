using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BarrelScript : MonoBehaviour
{
    [SerializeField] private GameObject shapePrefab;

    private void OnMouseDown()
    {
        PlayerController.Instance.Take("Shape");
        PlayerController.Instance.Give(shapePrefab);
    }

    public static GameObject Create(GameObject prefab, Vector3 position, Quaternion rotation, GameObject shape,
        bool showFraction = false)
    {
        var o = Instantiate(prefab, position, rotation);
        o.GetComponent<BarrelScript>().shapePrefab = shape;
        o.GetComponentInChildren<TMP_Text>().text = shape.GetComponent<ShapeScript>().ShapeName;
        o.GetComponentInChildren<FractionScript>().SetFraction(shape.GetComponent<ShapeScript>().Fraction);
        if (!showFraction) Destroy(o.GetComponentInChildren<HorizontalLayoutGroup>().gameObject);
        return o;
    }
}