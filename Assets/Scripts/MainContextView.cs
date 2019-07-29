using strange.extensions.context.impl;
using Tetris.Views;
using Tetris.Views.Table;
using UnityEngine;

namespace Tetris
{
    public class MainContextView : ContextView
    {
        [SerializeField]
        private string _pathToGameSettings;

        [SerializeField]
        private string _pathToBlockPrefab;

        [SerializeField]
        private int _preloadBlockCount;

        [SerializeField]
        private TableViewManager _tableViewManager;

        [SerializeField]
        private UiManager _uiManager;

        private void Start()
        {
            context = new MainContext(_pathToGameSettings, _pathToBlockPrefab, _preloadBlockCount, _tableViewManager, _uiManager, this);
            context.Start();
        }
    }
}