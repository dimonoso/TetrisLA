namespace Tetris.Views.Table
{
    public class ShapeContainer : IShapeContainer
    {
        [Inject]
        public TryAddShapeToMapSignal TryAddShapeToMapSignal { get; private set; }

        private IShapeView _shapeView;

        public IShapeView ShapeView
        {
            get { return _shapeView; }
            set { _shapeView = value; }
        }

        public void SendTryAddShapeSignal(IndexedPosition position)
        {
            if (position.Y < 0 || position.X < 0)
            {
                return;
            }
            TryAddShapeToMapSignal.Dispatch(ShapeView.Shape, position, AddShapes);
        }

        private void AddShapes(IndexedPosition position)
        {
            ShapeView.AddShapesToPosition(position);
        }
    }
}