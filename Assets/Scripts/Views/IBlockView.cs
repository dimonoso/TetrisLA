﻿using UnityEngine;

namespace Tetris.Views
{
    public interface IBlockView
    {
        void SetActive(bool isActive);
        void SetParent(Transform parent);
        void SetLocalPosition(Vector3 position);
        void Remove();
        void ToPool();
        void SetColor(Color color);
    }
}