using System;

namespace Tetris.Views
{
    public interface IUiManager
    {
        void ShowNoMoreMovesScreen(Action onButtonClick);
        void HideNoMoreMovesScreen();
    }
}
