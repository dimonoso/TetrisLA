using strange.extensions.command.impl;
using Tetris.Models;
using UnityEngine;

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

        [Inject]
        public CreateShapesSignal CreateShapesSignal { get; private set; }

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

            FindShapeAndDelete();

            if (MapModel.Shapes.Count == 0)
            {
                Debug.Log("CreateShapesSignal");
                CreateShapesSignal.Dispatch();
            }
        }

        private void FindShapeAndDelete()
        {
            for (var i = 0; i < MapModel.Shapes.Count; i++)
            {
                if (CheckShapes(MapModel.Shapes[i], Shape))
                {
                    MapModel.Shapes.RemoveAt(i);
                    return;
                }
            }
        }

        private bool CheckShapes(bool[,] firstShape, bool[,] secondShape)
        {
            var height = firstShape.GetLength(0);
            var width = firstShape.GetLength(1);

            if (height != secondShape.GetLength(0) ||
                width != secondShape.GetLength(1))
            {
                return false;
            }

            for (var i = 0; i < height; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    if (firstShape[i, j] != secondShape[i, j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
