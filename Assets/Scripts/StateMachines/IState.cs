namespace Tetris.StateMachines
{
    public interface IState
    {
        void EnterState();
        void ExitState();
    }
}