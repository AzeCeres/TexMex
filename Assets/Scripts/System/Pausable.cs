using OmniDi.Library.Controllers;
using UnityEngine;

namespace OmniDi.Library.Util
{
    /// <summary>
    /// An abstract class for classes that should alter their behavior when the game is paused.
    /// </summary>
    public abstract class Pauseable : MonoBehaviour
    {
        protected bool paused;
        
        protected virtual void OnEnable()
        {
            GameController.PauseGame += Pause;
            GameController.UnPauseGame += UnPause;
        }
        
        
        protected virtual void OnDisable()
        {
            GameController.PauseGame -= Pause;
            GameController.UnPauseGame -= UnPause;
        }
        
        
        protected virtual void Pause()
        {
            paused = true;
        }
        
        
        protected virtual void UnPause()
        {
            paused = false;
        }
    }
}