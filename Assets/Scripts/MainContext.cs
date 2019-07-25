using Tetris.Commands;
using Tetris.Models;
using Tetris.StateMachines;
using Tetris.StateMachines.States;
using UnityEngine;

namespace Tetris
{
    public class MainContext : SignalContext
    {
        private readonly string _pathToGameSetting;

        public MainContext(string pathToGameSettings, MonoBehaviour view) : base(view)
        {
            _pathToGameSetting = pathToGameSettings;
        }

        protected override void mapBindings()
        {
            base.mapBindings();

            LoadScriptableObjects();
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

        private void LoadScriptableObjects()
        {
            var settings = Resources.Load<GameSettingsScriptableObject>(_pathToGameSetting);
            injectionBinder.Bind<GameSettingsScriptableObject>().ToValue(settings).ToSingleton();
        }
    }
}