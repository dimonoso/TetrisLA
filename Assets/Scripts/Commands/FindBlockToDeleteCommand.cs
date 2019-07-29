using System.Collections.Generic;
using strange.extensions.command.impl;
using Tetris.Models;

namespace Tetris.Commands
{
    public class FindBlockToDeleteCommand : Command
    {
        [Inject]
        public MapModel MapModel { get; private set; }

        [Inject]
        public DeleteBlockSignal DeleteBlockSignal { get; private set; }

        public override void Execute()
        {
            var height = MapModel.Map.GetLength(0);
            var width = MapModel.Map.GetLength(1);
            var rowsNotemove = new bool[height];
            var columsNoRemove = new bool[width];

            for (var i = 0; i < height; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    if (!MapModel.Map[i, j])
                    {
                        rowsNotemove[i] = true;
                        columsNoRemove[j] = true;
                    }
                }
            }

            var removeList = new List<IndexedPosition>();
            
            for (var i = 0; i < height; i++)
            {
                if (!rowsNotemove[i])
                {
                    removeList.AddRange(RemoveRow(i));
                }
            }
            
            for (var i = 0; i < height; i++)
            {
                if (!columsNoRemove[i])
                {
                    removeList.AddRange(RemoveColum(i));
                }
            }

            if (removeList.Count > 0)
            {
                DeleteBlockSignal.Dispatch(removeList);
            }
        }

        private List<IndexedPosition> RemoveRow(int rowIndex)
        {
            var removeList = new List<IndexedPosition>();
            for (var i = 0; i < MapModel.Map.GetLength(1); i++)
            {
                if (MapModel.Map[rowIndex, i])
                {
                    MapModel.Map[rowIndex, i] = false;
                    removeList.Add(new IndexedPosition(rowIndex, i));
                }
            }

            return removeList;
        }

        private List<IndexedPosition> RemoveColum(int columIndex)
        {
            var removeList = new List<IndexedPosition>();
            for (var i = 0; i < MapModel.Map.GetLength(0); i++)
            {
                if (MapModel.Map[i, columIndex])
                {
                    MapModel.Map[i, columIndex] = false;
                    removeList.Add(new IndexedPosition(i, columIndex));
                }
            }

            return removeList;
        }
    }
}