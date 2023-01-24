using System;
using System.Collections.Generic;
using System.Linq;
using OmniDi.Library.Wrappers;
using UnityEngine;

namespace OmniDi.Library.CharacterControl.Input
{
    /// <summary>
    /// A class that holds and handles several lists of <see cref="CharacterInputValue{T}"/>.
    /// </summary>
    public class InputState
    {
        public List<CharacterInputValue<bool>> BoolValues { get; set; }
        public List<CharacterInputValue<float>> FloatValues { get; set; }
        public List<CharacterInputValue<int>> IntValues { get; set; }
        public List<CharacterInputValue<Vector2>> Vector2Values { get; set; }
        public List<CharacterInputValue<Vector3>> Vector3Values { get; set; }

        public InputState()
        {
            BoolValues = new List<CharacterInputValue<bool>>();
            FloatValues = new List<CharacterInputValue<float>>();
            IntValues = new List<CharacterInputValue<int>>();
            Vector2Values = new List<CharacterInputValue<Vector2>>();
            Vector3Values = new List<CharacterInputValue<Vector3>>();
        }

        /// <summary>
        /// Constructs and adds a new <see cref="CharacterInputValue{T}"/> based on the parameter values.
        /// </summary>
        /// <param name="value">The value to add</param>
        /// <param name="action">The name of the action</param>
        public void AddValue(bool value, string action)
        {
            BoolValues.Add(new CharacterInputValue<bool>(value, action));
        }

        /// <summary>
        /// Constructs and adds a new <see cref="CharacterInputValue{T}"/> based on the parameter values.
        /// </summary>
        /// <param name="value">The value to add</param>
        /// <param name="action">The name of the action</param>
        public void AddValue(float value, string action)
        {
            FloatValues.Add(new CharacterInputValue<float>(value, action));
        }

        /// <summary>
        /// Constructs and adds a new <see cref="CharacterInputValue{T}"/> based on the parameter values.
        /// </summary>
        /// <param name="value">The value to add</param>
        /// <param name="action">The name of the action</param>
        public void AddValue(int value, string action)
        {
            IntValues.Add(new CharacterInputValue<int>(value, action));
        }

        /// <summary>
        /// Constructs and adds a new <see cref="CharacterInputValue{T}"/> based on the parameter values.
        /// </summary>
        /// <param name="value">The value to add</param>
        /// <param name="action">The name of the action</param>
        public void AddValue(Vector2 value, string action)
        {
            Vector2Values.Add(new CharacterInputValue<Vector2>(value, action));
        }

        /// <summary>
        /// Constructs and adds a new <see cref="CharacterInputValue{T}"/> based on the parameter values.
        /// </summary>
        /// <param name="value">The value to add</param>
        /// <param name="action">The name of the action</param>
        public void AddValue(Vector3 value, string action)
        {
            Vector3Values.Add(new CharacterInputValue<Vector3>(value, action));
        }

        /// <summary>
        /// Adds a <see cref="CharacterInputValue{T}"/>.
        /// </summary>
        /// <param name="inputValue">The <see cref="CharacterInputValue{T}"/> to add.</param>
        public void AddValue(CharacterInputValue<bool> inputValue)
        {
            BoolValues.Add(inputValue);
        }

        /// <summary>
        /// Adds a <see cref="CharacterInputValue{T}"/>.
        /// </summary>
        /// <param name="inputValue">The <see cref="CharacterInputValue{T}"/> to add.</param>
        public void AddValue(CharacterInputValue<float> inputValue)
        {
            FloatValues.Add(inputValue);
        }

        /// <summary>
        /// Adds a <see cref="CharacterInputValue{T}"/>.
        /// </summary>
        /// <param name="inputValue">The <see cref="CharacterInputValue{T}"/> to add.</param>
        public void AddValue(CharacterInputValue<int> inputValue)
        {
            IntValues.Add(inputValue);
        }
        /// <summary>
        /// Adds a <see cref="CharacterInputValue{T}"/>.
        /// </summary>
        /// <param name="inputValue">The <see cref="CharacterInputValue{T}"/> to add.</param>

        public void AddValue(CharacterInputValue<Vector2> inputValue)
        {
            Vector2Values.Add(inputValue);
        }

        /// <summary>
        /// Adds a <see cref="CharacterInputValue{T}"/>.
        /// </summary>
        /// <param name="inputValue">The <see cref="CharacterInputValue{T}"/> to add.</param>
        public void AddValue(CharacterInputValue<Vector3> inputValue)
        {
            Vector3Values.Add(inputValue);
        }

        /// <summary>
        /// Returns an <see cref="Option{T}"/> containing the requested <see cref="CharacterInputValue{T}"/>.
        /// </summary>
        /// <param name="actionName">The name of the action to return.</param>
        /// <returns>Returns <see cref="Option{T}.Some"/> if the requested action is found, otherwise <see cref="Option{T}.None"/></returns>
        public Option<CharacterInputValue<bool>> GetBoolValueFromActionName(string actionName)
        {
            CharacterInputValue<bool> value;
            try
            {
                value = BoolValues.First(value => value.Action == actionName);
            }
            catch (Exception)
            {
                return Option<CharacterInputValue<bool>>.None();
            }

            return Option<CharacterInputValue<bool>>.Some(value);
        }

        /// <summary>
        /// Returns an <see cref="Option{T}"/> containing the requested <see cref="CharacterInputValue{T}"/>.
        /// </summary>
        /// <param name="actionName">The name of the action to return.</param>
        /// <returns>Returns <see cref="Option{T}.Some"/> if the requested action is found, otherwise <see cref="Option{T}.None"/></returns>
        public Option<CharacterInputValue<float>> GetFloatValueFromActionName(string actionName)
        {
            CharacterInputValue<float> value;
            try
            {
                value = FloatValues.First(value => value.Action == actionName);
            }
            catch (Exception)
            {
                return Option<CharacterInputValue<float>>.None();
            }

            return Option<CharacterInputValue<float>>.Some(value);
        }

        /// <summary>
        /// Returns an <see cref="Option{T}"/> containing the requested <see cref="CharacterInputValue{T}"/>.
        /// </summary>
        /// <param name="actionName">The name of the action to return.</param>
        /// <returns>Returns <see cref="Option{T}.Some"/> if the requested action is found, otherwise <see cref="Option{T}.None"/></returns>
        public Option<CharacterInputValue<int>> GetIntValueFromActionName(string actionName)
        {
            CharacterInputValue<int> value;
            try
            {
                value = IntValues.First(value => value.Action == actionName);
            }
            catch (Exception)
            {
                return Option<CharacterInputValue<int>>.None();
            }

            return Option<CharacterInputValue<int>>.Some(value);
        }

        /// <summary>
        /// Returns an <see cref="Option{T}"/> containing the requested <see cref="CharacterInputValue{T}"/>.
        /// </summary>
        /// <param name="actionName">The name of the action to return.</param>
        /// <returns>Returns <see cref="Option{T}.Some"/> if the requested action is found, otherwise <see cref="Option{T}.None"/></returns>
        public Option<CharacterInputValue<Vector2>> GetVector2ValueFromActionName(string actionName)
        {
            CharacterInputValue<Vector2> value;
            try
            {
                value = Vector2Values.First(value => value.Action == actionName);
            }
            catch (Exception)
            {
                return Option<CharacterInputValue<Vector2>>.None();
            }

            return Option<CharacterInputValue<Vector2>>.Some(value);
        }

        /// <summary>
        /// Returns an <see cref="Option{T}"/> containing the requested <see cref="CharacterInputValue{T}"/>.
        /// </summary>
        /// <param name="actionName">The name of the action to return.</param>
        /// <returns>Returns <see cref="Option{T}.Some"/> if the requested action is found, otherwise <see cref="Option{T}.None"/></returns>
        public Option<CharacterInputValue<Vector3>> GetVector3ValueFromActionName(string actionName)
        {
            CharacterInputValue<Vector3> value;
            try
            {
                value = Vector3Values.First(value => value.Action == actionName);
            }
            catch (Exception)
            {
                return Option<CharacterInputValue<Vector3>>.None();
            }

            return Option<CharacterInputValue<Vector3>>.Some(value);
        }
    }
}