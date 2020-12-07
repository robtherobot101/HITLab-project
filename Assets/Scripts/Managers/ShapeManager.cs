using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using UnityEngine;
using Utils;

namespace Managers
{
    public class ShapeManager : MonoSingleton<ShapeManager>
    {
        public Action onChanged;
        public IEnumerable<ShapeScript> CollectedShapes => _collectedShapes;
        private ObservableCollection<ShapeScript> _collectedShapes = new ObservableCollection<ShapeScript>();

        private void Start()
        {
            EventManager.Instance.reset += Clear;
            _collectedShapes.CollectionChanged += (o, a) =>
            {
                onChanged?.Invoke();
            };
        }

        public void AddShape(ShapeScript o)
        {
            _collectedShapes.Add(o);
        }

        public void Clear()
        {
            _collectedShapes.Clear();
        }
    }
}
