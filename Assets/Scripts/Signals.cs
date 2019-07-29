using strange.extensions.signal.impl;
using System;
using System.Collections.Generic;

namespace Tetris
{
    public class AppStartSignal : Signal { }
    public class RestartGameSignal : Signal { }
    public class NoMoreMovesSignal : Signal { }

    public class TryAddShapeToMapSignal : Signal<bool[,], IndexedPosition, Action<IndexedPosition>> { }
    public class AddShapeToMapSignal : Signal<bool[,], IndexedPosition> { }
    public class CreateShapesSignal: Signal { }
    public class DeleteBlockSignal : Signal<List<IndexedPosition>> { }
}