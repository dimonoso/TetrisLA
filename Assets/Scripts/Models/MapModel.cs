using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tetris.Models
{
    [Serializable]
    public struct ShapeSettings
    {
        public bool[,] ShapeMatrix;
        public Color ShapeColor;

        public ShapeSettings(bool[,] matrix, Color color)
        {
            ShapeMatrix = matrix;
            ShapeColor = color;
        }
    }

    public class MapModel
    {
        public bool[,] Map;

        public readonly List<ShapeSettings> Shapes = new List<ShapeSettings>();
    }
}