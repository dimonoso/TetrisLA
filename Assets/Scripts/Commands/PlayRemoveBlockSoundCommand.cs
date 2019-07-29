﻿using strange.extensions.command.impl;
using Tetris.Audio;

namespace Tetris.Commands
{
    public class PlayRemoveBlockSoundCommand : Command
    {
        [Inject]
        public IAudioManager AudioManager { get; private set; }

        public override void Execute()
        {
            AudioManager.Play("Drop");
        }
    }
}
