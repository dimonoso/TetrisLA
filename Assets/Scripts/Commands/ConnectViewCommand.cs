using strange.extensions.command.impl;
using strange.extensions.injector.api;
using Tetris.Views.Table;

namespace Tetris.Commands
{
    public class ConnectViewCommand : Command
    {
        [Inject]
        public ITableViewManager TableViewManager { get; private set; }
        [Inject]
        public IInjectionBinder InjectionBinder { get; private set; }

        public override void Execute()
        {
            foreach (var shape in TableViewManager.Shapes)
            {
                shape.ShapeContainer = InjectionBinder.GetInstance<IShapeContainer>();
            }
        }
    }
}
