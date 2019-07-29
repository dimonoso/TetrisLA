namespace Tetris.Views.Table
{
    public interface IShapeView
    {
        IShapeContainer ShapeContainer { get; set; }
        bool[,] Shape { get; set; }
        IBlockView[,] BlockViews { get; set; }
        void AddShapesToPosition(IndexedPosition indexedPosition);
    }
}
