using System.Collections.Generic;
using UnityEngine;

namespace Tetris.Views
{
    public class BlockPool : IBlockPool
    {
        [Inject]
        public IBlockFactory BlockFactory { get; private set; }

        [Inject("PreloadBlockCount")]
        public int PreloadBlockCount { get; private set; }

        private Stack<IBlockView> _blockViewPool;

        private GameObject _poolGameObject;

        [PostConstruct]
        public void PostConstruct()
        {
            _poolGameObject = new GameObject();
            _poolGameObject.name = "BlockPool";

            _blockViewPool = new Stack<IBlockView>(PreloadBlockCount);
            for (var i = 0; i < PreloadBlockCount; i++)
            {
                var view = CreateBlockFromFactory();
                Push(view);
            }
        }

        public IBlockView Pop()
        {
            if (_blockViewPool.Count > 0)
            {
                var block = _blockViewPool.Pop();
                block.SetActive(true);
                return block;
            }

            return CreateBlockFromFactory();
        }

        public void Push(IBlockView blockView)
        {
            _blockViewPool.Push(blockView);
            blockView.SetActive(false);
            blockView.SetParent(_poolGameObject.transform);
        }

        private IBlockView CreateBlockFromFactory()
        {
            return BlockFactory.Create();
        }
    }
}