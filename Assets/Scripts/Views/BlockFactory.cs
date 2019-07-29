using strange.extensions.injector.api;
using UnityEngine;

namespace Tetris.Views
{
    public class BlockFactory : IBlockFactory
    {
        [Inject("BlockPrefabPath")]
        public string PrefabPath { get; private set; }

        [Inject]
        public IInjectionBinder InjectionBinder { get; private set; }

        private GameObject _prefab;

        [PostConstruct]
        public void PostConstruct()
        {
            _prefab = Resources.Load<GameObject>(PrefabPath);
        }

        public IBlockView Create()
        {
            var go = GameObject.Instantiate(_prefab);
            InjectionBinder.Bind<GameObject>().ToValue(go).ToSingleton();

            var blockView = InjectionBinder.GetInstance<IBlockView>();

            InjectionBinder.Unbind<GameObject>();

            return blockView;
        }
    }
}