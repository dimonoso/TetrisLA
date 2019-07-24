using strange.extensions.command.impl;
using Tetris.Models;
using UnityEngine;

namespace Tetris.Commands
{
    public class ReloadMapCommand : Command
    {
        [Inject]
        public MapModel MapModel { get; private set; }

        private const string PathToSettings = "GameSettings";

        private GameSettingsScriptableObject _gameSettings;

        private GameSettingsScriptableObject GameSettings
        {
            get
            {
                if (_gameSettings == null)
                {
                    _gameSettings = Resources.Load<GameSettingsScriptableObject>(PathToSettings);
                }

                return _gameSettings;
            }
        }

        public override void Execute()
        {
            MapModel.Map = new bool[GameSettings.TableHeight, GameSettings.TableWidth];
            Debug.Log("Map is loaded");
        }
    }
}
