﻿using System.Collections.Generic;
using UnityEngine;

namespace Tetris.Views.Table
{
    public interface ITableViewManager
    {
        List<ShapeView> Shapes { get; }

        Vector3 TopLeftAnchorPosition { get; }

        Transform ContainerTransform { get; }

        void RemoveSelectedBlocks(List<IndexedPosition> blocksToRemove);

        void RemoveAllBlocks();

        IBlockView[,] BlockViews { get; }
    }
}