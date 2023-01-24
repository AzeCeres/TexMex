using UnityEngine.InputSystem;

namespace OmniDi.Library.CharacterControl.Input
{
    /// <summary>
    /// A struct that holds the name and value of a character input.
    /// </summary>
    /// <typeparam name="T">The type of the held value.</typeparam>
    public struct CharacterInputValue<T>
    {
        public T Value { get; set; }
        public string Action { get; set; }

        public CharacterInputValue(T value, string action)
        {
            Value = value;
            Action = action;
        }
    }
}