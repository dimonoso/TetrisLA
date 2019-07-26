using strange.extensions.command.impl;
using Tetris.Models;

namespace Tetris.Commands
{
    public class AddShapeToMapCommand : Command
    {
        [Inject]
        public bool[,] Shape { get; private set; }

        [Inject]
        public IndexedPosition Position { get; private set; }

        [Inject]
        public MapModel MapModel { get; private set; }

        public override void Execute()
        {
            if (Shape.GetLength(0) + Position.Y > MapModel.Map.GetLength(0))
            {
                Fail();
                return;
            }
            if (Shape.GetLength(1) + Position.X > MapModel.Map.GetLength(1))
            {
                Fail();
                return;
            }

            for (var i = 0; i < Shape.GetLength(0); i++)
            {
                for (var j = 0; j < Shape.GetLength(1); j++)
                {
                    MapModel.Map[Position.Y + i, Position.X + j] =
                        Shape[i, j] || MapModel.Map[Position.Y + i, Position.X + j];
                }
            }
        }
    }
}
