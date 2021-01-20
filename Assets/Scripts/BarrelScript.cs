using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class BarrelScript : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private FractionScript fraction;
    [SerializeField] private HorizontalLayoutGroup fractionText;
    private GameObject shapePrefab;

    private void OnMouseDown()
    {
        // Either take the shape held by the player, or give them the contents of the barrel
        if (PlayerController.Instance.Take("Shape") == null) PlayerController.Instance.Give(shapePrefab);
    }

    public void Init(ShapeScript shape, bool showFraction = false)
    {
        shapePrefab = shape.gameObject;
        text.text = shape.ShapeName;
        fraction.SetFraction(shape.Fraction);
        if (!showFraction) Destroy(fractionText.gameObject);
    }
}