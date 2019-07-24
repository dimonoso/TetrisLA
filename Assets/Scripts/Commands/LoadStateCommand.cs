using strange.extensions.command.impl;
using Tetris.StateMachines;

namespace Tetris.Commands
{
    public class LoadStateCommand<T> : Command where T : IState
    {
        [Inject]
        public IStateMachine StateMachine { get; private set; }

        public override void Execute()
        {
            StateMachine.ChangeState(injectionBinder.GetInstance<T>());
        }
    }
}