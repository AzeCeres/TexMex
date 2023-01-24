using System;
using OmniDi.Library.Util;
using UnityEngine;

namespace OmniDi.Library.Wrappers
{

    [Serializable]
    public struct Option<T>
    {
        [SerializeField] private bool hasValue;
        [SerializeField] private T value;

        public bool HasValue
        {
            get => hasValue;
            set => hasValue = value;
        }

        public T Value
        {
            get
            {
                if (hasValue) return value;
                throw new NoValueException("Attempt at directly accessing Option<T> value has failed due to no value being present.");
            }
            set
            {
                HasValue = true;
                this.value = value;
            }
        }

        /// <summary>
        /// Returns a new Option that contains a value.
        /// </summary>
        /// <param name="input">The value that the Option struct will contain.</param>
        public static Option<T> Some(T input)
        {
            return new Option<T>
            {
                value = input,
                hasValue = true
            };
        }

        /// <summary>
        /// Returns a new Option that contains no value.
        /// </summary>
        public static Option<T> None()
        {
            return new Option<T>();
        }

        /// <summary>
        /// Returns the contained value or the default of that type if there is no value.
        /// </summary>
        public T ValueOrDefault()
        {
            return hasValue ? value : default;
        }

        /// <summary>
        /// Returns the contained value or throws and exception if there is no value.
        /// </summary>
        /// <exception cref="ArgumentNullException">No value was found when one was requested.</exception>
        public T ValueOrThrow()
        {
            if (hasValue) return value;
            throw new NoValueException("No value was found when one was requested.");
        }

        /// <summary>
        /// Returns the contained value or the specified value if there is no value.
        /// </summary>
        /// <param name="defaultValue">The default value to return if there is no contained value.</param>
        public T ValueOr(T defaultValue)
        {
            return hasValue ? value : defaultValue;
        }
    }
}