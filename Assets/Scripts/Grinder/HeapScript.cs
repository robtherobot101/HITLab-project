using System.Collections;
using UnityEngine;
using Utils;

namespace Grinder
{
    public class HeapScript : MonoBehaviour
    {

        public Fraction Size => size;
        [SerializeField] private Fraction size = new Fraction(0, 1);
        private bool _growing = false;


        public IEnumerator Grow(Fraction fraction)
        {
            _growing = true;
            yield return new WaitForSeconds(0.8f);
            float timePassed = 0;
            while (timePassed < 4)
            {
                transform.localScale += Vector3.up * (Time.deltaTime * fraction.Value() / 2);
                timePassed += Time.deltaTime;
                yield return null;
            }
            size += fraction;
            _growing = false;
        }

        private void OnMouseDown()
        {
            if (!_growing)
            {
                PlayerController.Instance.Give(gameObject);
                Revert();
            }
        }

        private void Revert()
        {
            transform.localScale -= new Vector3(0, transform.localScale.y, 0);
            size = new Fraction(0, 1);
        }
    }
}
