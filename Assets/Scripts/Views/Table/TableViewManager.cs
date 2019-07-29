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

        [SerializeField]
        private int _width;

        [SerializeField]
        private int _height;

        private IBlockView[,] _shapeView;
        public IBlockView[,] BlockViews
        {
            get { return _shapeView; }
        }

        private void Start()
        {
            _shapeView = new IBlockView[_height, _width];
            foreach (var shape in _shapes)
            {
                shape.TableViewManager = this;
            }
        }

        public Transform ContainerTransform
        {
            get { return _containerTransform; }
        }

        public void RemoveSelectedBlocks(List<IndexedPosition> blocksToRemove)
        {
            foreach (var index in blocksToRemove)
            {
                BlockViews[index.Y, index.X].Remove();
                BlockViews[index.Y, index.X] = null;
            }
        }

        public void RemoveAllBlocks()
        {
            for (var i = 0; i < _height; i++)
            {
                for (var j = 0; j < _width; j++)
                {
                    if (BlockViews[i, j] != null)
                    {
                        BlockViews[i, j].ToPool();
                    }
                }
            }

            foreach (var shape in Shapes)
            {
                shape.RemoveShapes();
            }
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