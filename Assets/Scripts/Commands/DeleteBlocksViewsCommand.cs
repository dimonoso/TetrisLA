using System.Collections.Generic;
using strange.extensions.command.impl;
using Tetris.Views.Table;

namespace Tetris.Commands
{
    public class DeleteBlocksViewsCommand : Command
    {
        [Inject]
        public List<IndexedPosition> BlocksToRemove { get; private set; }

        [Inject]
        public ITableViewManager TableViewManager { get; private set; }

        public override void Execute()
        {
            TableViewManager.RemoveSelectedBlocks(BlocksToRemove);
        }
    }
}
