using strange.extensions.command.impl;
using Tetris.Views.Table;

namespace Tetris.Commands
{
    public class ClearTableCommand : Command
    {
        [Inject]
        public ITableViewManager TableViewManager { get; private set; }

        public override void Execute()
        {
            TableViewManager.RemoveAllBlocks();
        }
    }
}
