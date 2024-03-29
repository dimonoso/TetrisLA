﻿using strange.extensions.command.impl;
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

        private static readonly Random Random = new Random();

        public override void Execute()
        {
            for (var i = 0; i < GameSettings.NewShapesCount; i++)
            {
                CreateShape();
            }

            Debug.Log("ShapesCreated");
        }

        private void CreateShape()
        {
            var newShapeIndex = Random.Next(GameSettings.Shapes.Count);
            var rotateMethod = GameSettings.IsRotateShapesOnCreate ? Random.Next(4) : 0;

            var color = GameSettings.Shapes[newShapeIndex].ShapeColor;
            bool[,] matrix;

            switch (rotateMethod)
            {
                default:
                    Debug.Log(GameSettings.Shapes[newShapeIndex].ShapeName + " - 0");
                    matrix = GetNewShapeNoRotate(GameSettings.Shapes[newShapeIndex]);
                    break;
                case 1:
                    Debug.Log(GameSettings.Shapes[newShapeIndex].ShapeName + " - 90");
                    matrix = GetNewShapeRotate90(GameSettings.Shapes[newShapeIndex]);
                    break;
                case 2:
                    Debug.Log(GameSettings.Shapes[newShapeIndex].ShapeName + " - 180");
                    matrix = GetNewShapeRotate180(GameSettings.Shapes[newShapeIndex]);
                    break;
                case 3:
                    Debug.Log(GameSettings.Shapes[newShapeIndex].ShapeName + " - 270");
                    matrix = GetNewShapeRotate270(GameSettings.Shapes[newShapeIndex]);
                    break;
            }
            MapModel.Shapes.Add(new ShapeSettings(matrix, color));
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
                    newShape[i, j] = shape.At(shape.Height - 1 - i, shape.Width - 1 - j);
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
