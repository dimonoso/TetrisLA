using System;
using UnityEngine;
using UnityEngine.UI;

namespace Tetris.Views
{
    public class UiManager : MonoBehaviour, IUiManager
    {
        [SerializeField]
        private GameObject _noMoreMovesScreen;

        [SerializeField]
        private Button _restartButton;

        public void ShowNoMoreMovesScreen(Action onButtonClick)
        {
            _noMoreMovesScreen.SetActive(true);
            _restartButton.onClick.AddListener(() =>
            {
                onButtonClick();
            });
        }

        public void HideNoMoreMovesScreen()
        {
            _restartButton.onClick.RemoveAllListeners();
            _noMoreMovesScreen.SetActive(false);
        }
    }
}
