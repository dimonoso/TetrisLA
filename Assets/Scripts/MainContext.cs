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

            CommandsBindings();

            commandBinder.Bind<AppStartSignal>().InSequence().To<ReloadMapCommand>().To<RestartGameCommand>().To<CreateShapesCommand>().Once();
        }

        private void CommandsBindings()
        {
            commandBinder.Bind<TryAddShapeToMapSignal>().To<TryAddShapeToMapCommand>().Pooled();
            commandBinder.Bind<AddShapeToMapSignal>().InSequence().To<AddShapeToMapCommand>().To<FindBlockToDeleteCommand>().Pooled();
            commandBinder.Bind<FailAddShapeToMapSignal>().To<NullCommand>();

            commandBinder.Bind<RestartGameSignal>().InSequence().To<ReloadMapCommand>().To<RestartGameCommand>().To<CreateShapesCommand>();
            commandBinder.Bind<NoMoreMovesSignal>().To<NoMoreMovesCommand>();
            commandBinder.Bind<CreateShapesSignal>().To<CreateShapesCommand>().Pooled();
            commandBinder.Bind<DeleteBlockSignal>().To<DeleteBlocksCommand>().Pooled();
            commandBinder.Bind<ShapesCreatedSignal>().To<NullCommand>();
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