namespace Ascendium.Core;

public class GameStateManager
    {
        /*
        #region Field Region

        private readonly Stack<GameState> gameStates = new Stack<GameState>();

        #endregion

        #region Event Handler Region

        public event EventHandler StateChanged;

        #endregion

        #region Property Region

        public GameState CurrentState
        {
            get { return gameStates.Peek(); }
        }

        #endregion

        #region Constructor Region

        public GameStateManager(Game game) : base(game)
        {
            Game.Services.AddService(typeof(IStateManager), this);
        }

        #endregion

        #region Method Region

        public void PushState(GameState state)
        {
            drawOrder += drawOrderInc;
            AddState(state);
            OnStateChanged();
        }

        private void AddState(GameState state)
        {
            gameStates.Push(state);
            Game.Components.Add(state);
            StateChanged += state.StateChanged;
        }

        public void PopState()
        {
            if (gameStates.Count != 0)
            {
                RemoveState();
                OnStateChanged();
            }
        }

        private void RemoveState()
        {
            GameState state = gameStates.Peek();

            StateChanged -= state.StateChanged;
            Game.Components.Remove(state);
            gameStates.Pop();
        }

        public void ChangeState(GameState state)
        {
            while (gameStates.Count > 0)
                RemoveState();

            AddState(state);
            OnStateChanged();
        }

        public bool ContainsState(GameState state)
        {
            return gameStates.Contains(state);
        }

        protected internal virtual void OnStateChanged()
        {
            if (StateChanged != null)
                StateChanged(this, null);
        }
        
        #endregion
        */
    }