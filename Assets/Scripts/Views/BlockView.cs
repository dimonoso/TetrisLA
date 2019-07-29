using UnityEngine;

namespace Tetris.Views
{
    public class BlockView : IBlockView
    {
        [Inject]
        public IBlock Block { get; private set; }

        [Inject]
        public IBlockPool BlockPool { get; private set; }

        public void SetActive(bool isActive)
        {
            Block.SetActive(isActive);
        }

        public void SetParent(Transform parent)
        {
            Block.Transform.parent = parent;
        }

        public void SetLocalPosition(Vector3 position)
        {
            Block.Transform.localPosition = position;
        }

        public void Remove()
        {
            Block.Remove(ToPool);
        }

        public void ToPool()
        {
            BlockPool.Push(this);
        }
    }
}