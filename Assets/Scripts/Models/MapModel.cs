using System.Collections.Generic;

namespace Tetris.Models
{
    public class MapModel
    {
        public bool[,] Map;

        public List<bool[,]> Shapes = new List<bool[,]>();
    }
}