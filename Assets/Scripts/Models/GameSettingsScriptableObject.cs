using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tetris.Models
{
    [Serializable]
    public class ShapeRow
    {
        public List<bool> Row;

        public ShapeRow(int width)
        {
            Row = new List<bool>();
            for (var i = 0; i < width; i++)
            {
                Row.Add(false);
            }
        }
    }

    [Serializable]
    public class Shape
    {
        [SerializeField]
        private int _width = 4;
        public int Width
        {
            get { return _width; }
        }

        [SerializeField]
        private int _height = 4;
        public int Height
        {
            get { return _height; }
        }

        public string ShapeName;
        [SerializeField]
        private List<ShapeRow> _shapeMap;

        public Color ShapeColor = Color.white;

        public Shape()
        {
            _shapeMap = new List<ShapeRow>();
            for (var i = 0; i < _height; i++)
            {
                _shapeMap.Add(new ShapeRow(_width));
            }
        }

        #region EditorFunctions
        public bool At(int y, int x)
        {
            if (y >= _height || x >= _width || y < 0 || x < 0)
            {
                return false;
            }

            return _shapeMap[y].Row[x];
        }

        public void SetAt(int y, int x, bool value)
        {

            if (y >= _height || x >= _width)
            {
                return;
            }

            _shapeMap[y].Row[x] = value;
        }

        public void AddRow()
        {
            _height++;
            _shapeMap.Add(new ShapeRow(_width));
        }

        public void AddColum()
        {
            _width++;
            for (var i = 0; i < _height; i++)
            {
                _shapeMap[i].Row.Add(false);
            }
        }

        public void OptimizeShape()
        {
            while (DeleteRowIfEmpty(0)) { }
            while (DeleteRowIfEmpty(_height - 1)) { }
            while (DeleteColumIfEmpty(0)) { }
            while (DeleteColumIfEmpty(_width - 1)) { }
        }

        private bool DeleteColumIfEmpty(int columIndex)
        {
            if (IsColumEmpty(columIndex))
            {
                _width--;
                foreach (var row in _shapeMap)
                {
                    row.Row.RemoveAt(columIndex);
                }
                return true;
            }

            return false;
        }

        private bool IsColumEmpty(int columIndex)
        {
            foreach (var row in _shapeMap)
            {
                if (row.Row[columIndex])
                {
                    return false;
                }
            }

            return true;
        }

        private bool DeleteRowIfEmpty(int rowIndex)
        {
            if (IsRowEmpty(rowIndex))
            {
                _height--;
                _shapeMap.RemoveAt(rowIndex);
                return true;
            }

            return false;
        }

        private bool IsRowEmpty(int rowIndex)
        {
            foreach (var element in _shapeMap[rowIndex].Row)
            {
                if (element)
                {
                    return false;
                }
            }

            return true;
        }

        #endregion
    }

    [CreateAssetMenu(menuName = "Game/SettingsData")]
    public class GameSettingsScriptableObject : ScriptableObject
    {
        public int TableWidth;
        public int TableHeight;
        public bool IsRotateShapesOnCreate;
        public int NewShapesCount;
        public List<Shape> Shapes = new List<Shape>();
    }
}