using strange.extensions.context.impl;
using UnityEngine;

namespace Tetris
{
    public class MainContextView : ContextView
    {
        [SerializeField]
        private string _pathToGameSettings;

        private void Start()
        {
            context = new MainContext(_pathToGameSettings, this);
            context.Start();
        }
    }
}