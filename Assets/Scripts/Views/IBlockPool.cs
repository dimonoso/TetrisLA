namespace Tetris.Views
{
    public interface IBlockPool
    {
        IBlockView Pop();
        void Push(IBlockView blockView);
    }
}