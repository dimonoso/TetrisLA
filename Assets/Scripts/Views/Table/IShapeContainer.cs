namespace Tetris.Views.Table
{
    public interface IShapeContainer
    {
        IShapeView ShapeView { get; set; }

        void SendTryAddShapeSignal(IndexedPosition position);
    }
}