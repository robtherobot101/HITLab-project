using UnityEngine;

namespace Facilitator.GeometryExamples
{
    public class Vertices : MonoBehaviour
    {
        [SerializeField] private Material markersMaterial;

        // Update is called once per frame
        private void Update()
        {
            transform.Rotate(Vector3.up, Time.deltaTime * 60);
            markersMaterial.color = new Color(markersMaterial.color.r, markersMaterial.color.g, markersMaterial.color.b,
                Mathf.Clamp(Mathf.Sin(Time.time * 4) * 2, 0f, 1f));
        }
    }
}