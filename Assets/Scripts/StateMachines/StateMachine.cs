namespace Tetris.StateMachines
{
    public class StateMachine : IStateMachine
    {
        private IState _currentState;

        public IState CurrentState
        {
            get { return _currentState; }
        }

        public void ChangeState(IState state)
        {
            if (state == null)
            {
                return;
            }

            if (_currentState != null)
            {
                _currentState.EnterState();
            }

            _currentState = state;
            _currentState.EnterState();
        }
    }
}