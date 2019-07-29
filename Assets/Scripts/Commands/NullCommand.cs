using strange.extensions.command.impl;
using UnityEngine;

namespace Tetris.Commands
{
    public class NullCommand : Command
    {
        public override void Execute()
        {
            Debug.LogError("Null command is implemented");
        }
    }
}