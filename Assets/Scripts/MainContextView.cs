using strange.extensions.context.impl;
using Tetris.Audio;
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

        [SerializeField]
        private AudioManager _audioManager;

        private void Start()
        {
            context = new MainContext(_pathToGameSettings, _pathToBlockPrefab, _preloadBlockCount, _tableViewManager, _uiManager, _audioManager, this);
            context.Start();
        }
    }
}