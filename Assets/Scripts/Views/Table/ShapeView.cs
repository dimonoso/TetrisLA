using UnityEngine;

namespace Tetris.Views.Table
{
    public class ShapeView : MonoBehaviour, IShapeView
    {
        public Transform BlockContainer;

        public ITableViewManager TableViewManager;

        private IShapeContainer _shapeContainer;
        public IShapeContainer ShapeContainer
        {
            get { return _shapeContainer; }
            set
            {
                _shapeContainer = value;
                _shapeContainer.ShapeView = this;
            }
        }

        [SerializeField]
        private Animator _animator;

        private bool[,] _shape;
        public bool[,] Shape
        {
            get { return _shape; }
            set
            {
                if (value == null)
                {
                    MaximazeScale();
                }

                _shape = value;
                GoToCenter();
            }
        }

        private IBlockView[,] _blockViews;

        public IBlockView[,] BlockViews
        {
            get { return _blockViews; }
            set { _blockViews = value; }
        }

        private bool _isDrag;
        private Vector3 _lastMousePosition;

        private void GoToCenter()
        {
            transform.localPosition = Vector3.zero;
        }

        public void MininimazeScale()
        {
            _animator.SetBool("Idle", true);
        }

        public void MaximazeScale()
        {
            _animator.SetBool("Idle", false);
        }

        private void OnMouseDown()
        {
            if (Shape == null)
            {
                return;
            }

            _isDrag = true;
            _lastMousePosition = GetMousePosition();
            _animator.SetBool("Take", true);
        }

        private void OnMouseDrag()
        {
            if (!_isDrag)
            {
                return;
            }

            var newMousePosition = GetMousePosition();
            transform.position = transform.position + newMousePosition - _lastMousePosition;

            _lastMousePosition = newMousePosition;
        }

        private void OnMouseUp()
        {
            if (!_isDrag)
            {
                return;
            }

            var height = Shape.GetLength(0);
            var width = Shape.GetLength(1);

            var topLeftShapePos = new Vector3(0.5f - width / 2f, height / 2f - 0.5f);

            var topLeftPos = TableViewManager.TopLeftAnchorPosition - topLeftShapePos;
            var yIndex = topLeftPos.y - transform.position.y + 0.5f;
            var xIndex = transform.position.x - topLeftPos.x + 0.5f;

            var indexedPosition = new IndexedPosition((int)yIndex, (int)xIndex);

            _isDrag = false;
            _animator.SetBool("Take", false);
            GoToCenter();

            ShapeContainer.SendTryAddShapeSignal(indexedPosition);
        }

        private Vector3 GetMousePosition()
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        public void AddShapesToPosition(IndexedPosition indexedPosition)
        {
            var parent = TableViewManager.ContainerTransform;
            var height = BlockViews.GetLength(0);
            var width = BlockViews.GetLength(1);
            Shape = null;

            var index = 0;

            for (var i = 0; i < height; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    if (BlockViews[i, j] != null)
                    {
                        BlockViews[i, j].SetParent(parent);
                        BlockViews[i, j].SetLocalPosition(new Vector3(j + indexedPosition.X, -i - indexedPosition.Y));
                        TableViewManager.BlockViews[indexedPosition.Y + i, indexedPosition.X + j] = BlockViews[i, j];
                    }
                }
            }

            BlockViews = null;
        }

        public void RemoveShapes()
        {
            if (BlockViews == null)
            {
                return;
            }

            var height = BlockViews.GetLength(0);
            var width = BlockViews.GetLength(1);
            Shape = null;

            for (var i = 0; i < height; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    if (BlockViews[i, j] != null)
                    {
                        BlockViews[i, j].ToPool();
                    }
                }
            }

            BlockViews = null;
        }
    }
}