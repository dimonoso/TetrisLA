using strange.extensions.command.impl;
using Tetris.Models;
using UnityEngine;

namespace Tetris.Commands
{
    public class CheckOnMovesCommand : Command
    {
        [Inject]
        public MapModel MapModel { get; private set; }

        [Inject]
        public NoMoreMovesSignal NoMoreMovesSignal { get; private set; }

        public override void Execute()
        {
            Debug.Log("CheckOnMovesCommand");

            var height = MapModel.Map.GetLength(0);
            var width = MapModel.Map.GetLength(1);

            for (var i = 0; i < height; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    if (!MapModel.Map[i, j])
                    {
                        if (CheckShapes(new IndexedPosition(i, j)))
                        {
                            return;
                        }
                    }
                }
            }

            NoMoreMovesSignal.Dispatch();
        }

        private bool CheckShapes(IndexedPosition position)
        {
            foreach (var shape in MapModel.Shapes)
            {
                if (CheckShape(position, shape.ShapeMatrix))
                {
                    return true;
                }
            }

            return false;
        }

        private bool CheckShape(IndexedPosition position, bool[,] shape)
        {
            if (MapModel.Map.GetLength(0) < position.Y + shape.GetLength(0))
            {
                return false;
            }
            if (MapModel.Map.GetLength(1) < position.X + shape.GetLength(1))
            {
                return false;
            }

            for (var i = 0; i < shape.GetLength(0); i++)
            {
                for (var j = 0; j < shape.GetLength(1); j++)
                {
                    if (shape[i, j] && MapModel.Map[position.Y + i, position.X + j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}