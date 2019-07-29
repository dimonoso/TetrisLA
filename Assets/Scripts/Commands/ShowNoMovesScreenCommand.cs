using strange.extensions.command.impl;
using Tetris.Views;

namespace Tetris.Commands
{
    public class ShowNoMovesScreenCommand : Command
    {
        [Inject]
        public IUiManager UiManager { get; private set; }

        [Inject]
        public RestartGameSignal RestartGameSignal { get; private set; }

        public override void Execute()
        {
            UiManager.ShowNoMoreMovesScreen(() =>
            {
                UiManager.HideNoMoreMovesScreen();
                RestartGameSignal.Dispatch();
            });
        }
    }
}