using strange.extensions.context.impl;

namespace Tetris
{
    public class MainContextView : ContextView
    {
        private void Start()
        {
            context = new MainContext(this);
            context.Start();
        }
    }
}