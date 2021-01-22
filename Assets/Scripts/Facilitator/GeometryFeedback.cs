using UnityEngine;

namespace Facilitator
{
    public class GeometryFeedback : MonoBehaviour
    {
        [SerializeField] private Transform facesMessage;
        [SerializeField] private Transform verticesMessage;
        [SerializeField] private Transform edgesMessage;

        [SerializeField] private GameObject faceHelper;
        [SerializeField] private GameObject vertexHelper;
        [SerializeField] private GameObject edgeHelper;


        public void ShowFaceHelp()
        {
            var o = Instantiate(faceHelper, transform.parent.parent.parent.parent.parent, false);
            o.transform.Translate(Vector3.up * 2.5f);
        }

        public void ShowVertexHelp()
        {
            var o = Instantiate(vertexHelper, transform.parent.parent.parent.parent.parent, false);
            o.transform.Translate(Vector3.up * 2.5f);
        }

        public void ShowEdgeHelp()
        {
            var o = Instantiate(edgeHelper, transform.parent.parent.parent.parent.parent, false);
            o.transform.Translate(Vector3.up * 2.5f);
        }
    
    }
}
