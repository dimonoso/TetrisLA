using Tetris.Commands;
using Tetris.Models;
using Tetris.Views;
using Tetris.Views.Table;
using UnityEngine;

namespace Tetris
{
    public class MainContext : SignalContext
    {
        private readonly string _pathToGameSetting;

        private readonly string _pathToBlockPrefab;

        private readonly int _preloadBlockCount;

        private readonly ITableViewManager _tableViewManager;

        private readonly IUiManager _uiManager;

        public MainContext(string pathToGameSettings, string pathToBlockPrefab, int preloadBlockCount, ITableViewManager tableViewManager, IUiManager uiManager, MonoBehaviour view) : base(view)
        {
            _pathToGameSetting = pathToGameSettings;
            _pathToBlockPrefab = pathToBlockPrefab;
            _preloadBlockCount = preloadBlockCount;
            _tableViewManager = tableViewManager;
            _uiManager = uiManager;
        }

        protected override void mapBindings()
        {
            base.mapBindings();

            LoadScriptableObjects();
            ModelBindings();
            ViewBindings();

            CommandsBindings();

            commandBinder.Bind<AppStartSignal>().InSequence()
                .To<ConnectViewCommand>()
                .To<ReloadMapCommand>()
                .To<CreateShapesCommand>()
                .To<SpawnShapesCommand>().Once();
        }

        private void CommandsBindings()
        {
            commandBinder.Bind<TryAddShapeToMapSignal>().To<TryAddShapeToMapCommand>().Pooled();
            commandBinder.Bind<AddShapeToMapSignal>().InSequence()
                .To<AddShapeToMapCommand>()
                .To<FindBlockToDeleteCommand>()
                .To<CheckOnMovesCommand>().Pooled();

            commandBinder.Bind<RestartGameSignal>().InSequence()
                .To<ClearTableCommand>()
                .To<ReloadMapCommand>()
                .To<CreateShapesCommand>()
                .To<SpawnShapesCommand>();

            commandBinder.Bind<NoMoreMovesSignal>().To<ShowNoMovesScreenCommand>();

            commandBinder.Bind<CreateShapesSignal>().InSequence()
                .To<CreateShapesCommand>()
                .To<SpawnShapesCommand>().Pooled();

            commandBinder.Bind<DeleteBlockSignal>()
                .To<DeleteBlocksViewsCommand>().Pooled();
        }

        private void ModelBindings()
        {
            injectionBinder.Bind<MapModel>().ToSingleton();
        }

        private void ViewBindings()
        {
            injectionBinder.Bind<ITableViewManager>().ToValue(_tableViewManager).ToSingleton();
            injectionBinder.Bind<IUiManager>().ToValue(_uiManager).ToSingleton();

            injectionBinder.Bind<string>().ToValue(_pathToBlockPrefab).ToName("BlockPrefabPath");
            injectionBinder.Bind<int>().ToValue(_preloadBlockCount).ToName("PreloadBlockCount");

            injectionBinder.Bind<IShapeContainer>().To<ShapeContainer>();
            injectionBinder.Bind<IBlockView>().To<BlockView>();
            injectionBinder.Bind<IBlockFactory>().To<BlockFactory>().ToSingleton();
            injectionBinder.Bind<IBlockPool>().To<BlockPool>().ToSingleton();
        }

        private void LoadScriptableObjects()
        {
            var settings = Resources.Load<GameSettingsScriptableObject>(_pathToGameSetting);
            injectionBinder.Bind<GameSettingsScriptableObject>().ToValue(settings).ToSingleton();
        }
    }
}