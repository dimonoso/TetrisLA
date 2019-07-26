using System.Collections.Generic;
using strange.extensions.signal.impl;

namespace Tetris
{
    public class AppStartSignal : Signal { }
    public class RestartGameSignal : Signal { }
    public class NoMoreMovesSignal : Signal { }

    public class TryAddShapeToMapSignal : Signal<bool[,], IndexedPosition> { }
    public class AddShapeToMapSignal : Signal<bool[,], IndexedPosition> { }
    public class FailAddShapeToMapSignal : Signal { }
    public class CreateShapesSignal: Signal { }
    public class ShapesCreatedSignal : Signal { }
    public class DeleteBlockSignal : Signal<List<IndexedPosition>> { }
}