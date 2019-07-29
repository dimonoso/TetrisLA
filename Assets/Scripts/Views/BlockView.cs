using UnityEngine;

namespace Tetris.Views
{
    public class BlockView : IBlockView
    {
        [Inject]
        public GameObject BlockGameObject { get; private set; }

        public void SetActive(bool isActive)
        {
            BlockGameObject.SetActive(isActive);
        }

        public void SetParent(Transform parent)
        {
            BlockGameObject.transform.parent = parent;
        }

        public void SetLocalPosition(Vector3 position)
        {
            BlockGameObject.transform.localPosition = position;
        }
    }
}