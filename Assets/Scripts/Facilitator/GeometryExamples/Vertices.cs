using UnityEngine;

namespace Facilitator.GeometryExamples
{
    public class Vertices : MonoBehaviour
    {

        [SerializeField] private Material markersMaterial;
    
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(Vector3.up, Time.deltaTime * 60);
            markersMaterial.color = new Color(markersMaterial.color.r, markersMaterial.color.g, markersMaterial.color.b, Mathf.Clamp(Mathf.Sin(Time.time * 4) * 2, 0f, 1f));
        }
    }
}
