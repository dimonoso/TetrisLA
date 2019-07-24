using Tetris.Commands;
using Tetris.Models;
using Tetris.StateMachines;
using Tetris.StateMachines.States;
using UnityEngine;

namespace Tetris
{
    public class MainContext : SignalContext
    {
        public MainContext(MonoBehaviour view) : base(view)
        {
        }

        protected override void mapBindings()
        {
            base.mapBindings();

            ModelBindings();
            StateMachineBindings();

            commandBinder.Bind<AppStartSignal>().To<ReloadMapCommand>().To<RestartGameCommand>().Once();
        }

        private void ModelBindings()
        {
            injectionBinder.Bind<MapModel>().ToSingleton();
        }

        private void StateMachineBindings()
        {
            injectionBinder.Bind<InGameState>().ToSingleton();
            injectionBinder.Bind<NoMoreMovesState>().ToSingleton();

            injectionBinder.Bind<IStateMachine>().To<StateMachine>().ToSingleton();
        }
    }
}