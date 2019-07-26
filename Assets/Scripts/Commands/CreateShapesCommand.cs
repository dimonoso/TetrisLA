using strange.extensions.command.impl;
using Tetris.Models;
using UnityEngine;
using Random = System.Random;

namespace Tetris.Commands
{
    public class CreateShapesCommand : Command
    {
        [Inject]
        public MapModel MapModel { get; private set; }

        [Inject]
        public GameSettingsScriptableObject GameSettings { get; private set; }

        [Inject]
        public ShapesCreatedSignal ShapesCreatedSignal { get; private set; }

        private static readonly Random Random = new Random();

        public override void Execute()
        {
            for (var i = 0; i < GameSettings.NewShapesCount; i++)
            {
                CreateShape();
            }

            Debug.Log("ShapesCreated");
            ShapesCreatedSignal.Dispatch();
        }

        private void CreateShape()
        {
            var newShapeIndex = Random.Next(GameSettings.Shapes.Count);
            var rotateMethod = GameSettings.IsRotateShapesOnCreate ? Random.Next(4) : 0;

            switch (rotateMethod)
            {
                case 0:
                    MapModel.Shapes.Add(GetNewShapeNoRotate(GameSettings.Shapes[newShapeIndex]));
                    break;
                case 1:
                    MapModel.Shapes.Add(GetNewShapeRotate90(GameSettings.Shapes[newShapeIndex]));
                    break;
                case 2:
                    MapModel.Shapes.Add(GetNewShapeRotate180(GameSettings.Shapes[newShapeIndex]));
                    break;
                case 3:
                    MapModel.Shapes.Add(GetNewShapeRotate270(GameSettings.Shapes[newShapeIndex]));
                    break;
            }
        }

        private bool[,] GetNewShapeNoRotate(Shape shape)
        {
            var newShape = new bool[shape.Height, shape.Width];

            for (var i = 0; i < shape.Height; i++)
            {
                for (var j = 0; j < shape.Width; j++)
                {
                    newShape[i, j] = shape.At(i, j);
                }
            }

            return newShape;
        }

        private bool[,] GetNewShapeRotate90(Shape shape)
        {
            var newShape = new bool[shape.Width, shape.Height];

            for (var i = 0; i < shape.Width; i++)
            {
                for (var j = 0; j < shape.Height; j++)
                {
                    newShape[i, j] = shape.At(shape.Height - 1 - j, i);
                }
            }

            return newShape;
        }

        private bool[,] GetNewShapeRotate180(Shape shape)
        {
            var newShape = new bool[shape.Height, shape.Width];

            for (var i = 0; i < shape.Height; i++)
            {
                for (var j = 0; j < shape.Width; j++)
                {
                    newShape[i, j] = shape.At(shape.Height - i, shape.Width - j);
                }
            }

            return newShape;
        }

        private bool[,] GetNewShapeRotate270(Shape shape)
        {
            var newShape = new bool[shape.Width, shape.Height];

            for (var i = 0; i < shape.Width; i++)
            {
                for (var j = 0; j < shape.Height; j++)
                {
                    newShape[i, j] = shape.At(j, shape.Width - 1 - i);
                }
            }

            return newShape;
        }
    }
}
