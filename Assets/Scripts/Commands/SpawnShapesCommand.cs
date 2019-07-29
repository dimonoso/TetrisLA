using strange.extensions.command.impl;
using Tetris.Models;
using Tetris.Views;
using Tetris.Views.Table;
using UnityEngine;

namespace Tetris.Commands
{
    public class SpawnShapesCommand : Command
    {
        [Inject]
        public IBlockPool BlockPool { get; private set; }

        [Inject]
        public MapModel MapModel { get; private set; }

        [Inject]
        public ITableViewManager TableViewManager { get; private set; }

        public override void Execute()
        {
            for (var i = 0; i < MapModel.Shapes.Count; i++)
            {
                GenerateShape(MapModel.Shapes[i], TableViewManager.Shapes[i]);
            }
        }

        private void GenerateShape(bool[,] shape, ShapeView shapeView)
        {
            var height = shape.GetLength(0);
            var width = shape.GetLength(1);

            var blockViews = new IBlockView[height, width];
            shapeView.Shape = shape;

            for (var i = 0; i < height; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    if (shape[i, j])
                    {
                        var blockView = BlockPool.Pop();
                        blockView.SetParent(shapeView.BlockContainer);
                        blockView.SetLocalPosition(new Vector3(j - width / 2f + 0.5f, height / 2f - i - 0.5f));
                        blockViews[i, j] = blockView;
                    }
                }
            }
            shapeView.BlockViews = blockViews;
            shapeView.MininimazeScale();
        }
    }
}