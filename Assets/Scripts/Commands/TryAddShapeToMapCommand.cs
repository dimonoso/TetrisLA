using strange.extensions.command.impl;
using System;
using Tetris.Models;

namespace Tetris.Commands
{
    public class TryAddShapeToMapCommand : Command
    {
        [Inject]
        public bool[,] Shape { get; private set; }

        [Inject]
        public IndexedPosition Position { get; private set; }

        [Inject]
        public MapModel MapModel { get; private set; }

        [Inject]
        public AddShapeToMapSignal AddShapeSignal { get; private set; }

        [Inject]
        public Action<IndexedPosition> OkAction { get; private set; }

        public override void Execute()
        {
            if (Shape.GetLength(0) + Position.Y > MapModel.Map.GetLength(0))
            {
                return;
            }
            if (Shape.GetLength(1) + Position.X > MapModel.Map.GetLength(1))
            {
                return;
            }

            for (var i = 0; i < Shape.GetLength(0); i++)
            {
                for (var j = 0; j < Shape.GetLength(1); j++)
                {
                    if (MapModel.Map[Position.Y + i, Position.X + j] && Shape[i, j])
                    {
                        return;
                    }
                }
            }

            OkAction(Position);

            AddShapeSignal.Dispatch(Shape, Position);
        }
    }
}