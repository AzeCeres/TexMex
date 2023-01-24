using OmniDi.Library.Util;

namespace OmniDi.Library.Controllers
{
    /// <summary>
    /// The class that handles the control of the game state.
    /// </summary>
    public class GameController : SingletonPersistent<GameController>
    {
        /// <summary>
        /// The event that calls for the game to pause.
        /// </summary>
        public static event Delegates.Notification PauseGame;
        /// <summary>
        /// The event that calls for the game to unpause.
        /// </summary>
        public static event Delegates.Notification UnPauseGame;

        private static bool _paused;

        /// <summary>
        /// Invokes the <see cref="PauseGame">PauseGame</see> event.
        /// </summary>
        public static void Pause()
        {
            PauseGame?.Invoke();
        }
        
        /// <summary>
        /// Invokes the <see cref="UnPauseGame">UnPauseGame</see> event.
        /// </summary>
        public static void UnPause()
        {
            UnPauseGame?.Invoke();
        }

        /// <summary>
        /// If the game is paused, unpause it. If the game is unpaused, pause it.
        /// </summary>
        public static void TogglePause()
        {
            if (_paused)
            {
                UnPause();
                _paused = false;
                return;
            }

            Pause();
            _paused = true;
        }
    }
}