using strange.extensions.command.impl;
using Tetris.Models;
using UnityEngine;

namespace Tetris.Commands
{
    public class ReloadMapCommand : Command
    {
        [Inject]
        public MapModel MapModel { get; private set; }
        
        [Inject]
        public GameSettingsScriptableObject GameSettings { get; private set; }

        public override void Execute()
        {
            MapModel.Map = new bool[GameSettings.TableHeight, GameSettings.TableWidth];
            MapModel.Shapes.Clear();
            Debug.Log("Map is loaded");
        }
    }
}
