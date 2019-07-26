using System.Collections.Generic;
using strange.extensions.command.impl;
using Tetris.Models;
using UnityEngine;

namespace Tetris.Commands
{
    public class DeleteBlocksCommand : Command
    {
        [Inject]
        public List<IndexedPosition> BlocksToRemove { get; private set; }

        [Inject]
        public MapModel MapModel { get; private set; }

        public override void Execute()
        {
            foreach (var blockPosition in BlocksToRemove)
            {
                MapModel.Map[blockPosition.Y, blockPosition.X] = false;
            }
            Debug.Log("Blocks Removed");
        }
    }
}
