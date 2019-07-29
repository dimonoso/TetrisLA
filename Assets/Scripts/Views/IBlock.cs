using System;
using UnityEngine;

namespace Tetris.Views
{
    public interface IBlock
    {
        void Remove(Action onRemovedAction);
        void SetActive(bool isActive);
        Transform Transform { get; }
        void SetColor(Color color);
    }
}