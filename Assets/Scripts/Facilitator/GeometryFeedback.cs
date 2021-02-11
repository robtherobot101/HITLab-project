using UnityEngine;

namespace Facilitator
{
    public class GeometryFeedback : MonoBehaviour
    {
        [SerializeField] private GameObject facesMessage;
        [SerializeField] private GameObject verticesMessage;
        [SerializeField] private GameObject edgesMessage;

        [SerializeField] private GameObject faceHelper;
        [SerializeField] private GameObject vertexHelper;
        [SerializeField] private GameObject edgeHelper;

        public void FacesWrong() => facesMessage.SetActive(true);
        public void VerticesWrong() => verticesMessage.SetActive(true);
        public void EdgesWrong() => edgesMessage.SetActive(true);

        public void ShowFaceHelp()
        {
            var o = Instantiate(faceHelper, FacilitatorScript.Instance.transform, false);
            o.transform.Translate(Vector3.up * 2.5f);
        }

        public void ShowVertexHelp()
        {
            var o = Instantiate(vertexHelper, FacilitatorScript.Instance.transform, false);
            o.transform.Translate(Vector3.up * 2.5f);
        }

        public void ShowEdgeHelp()
        {
            var o = Instantiate(edgeHelper, FacilitatorScript.Instance.transform, false);
            o.transform.Translate(Vector3.up * 2.5f);
        }
    }
}