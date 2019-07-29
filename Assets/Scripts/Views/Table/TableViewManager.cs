using System.Collections.Generic;
using UnityEngine;

namespace Tetris.Views.Table
{
    public class TableViewManager : MonoBehaviour, ITableViewManager
    {
        [SerializeField]
        private GameObject _anchor;
        [SerializeField]
        private Transform _containerTransform;

        [SerializeField]
        private List<ShapeView> _shapes;

        private void Start()
        {
            foreach (var shape in _shapes)
            {
                shape.TableViewManager = this;
            }
        }

        public Transform ContainerTransform
        {
            get { return _containerTransform; }
        }

        public Vector3 TopLeftAnchorPosition
        {
            get { return _anchor.transform.position; }
        }

        public List<ShapeView> Shapes
        {
            get { return _shapes; }
        }
    }
}