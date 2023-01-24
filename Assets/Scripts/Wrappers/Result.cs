using OmniDi.Library.Util;

namespace OmniDi.Library.Wrappers
{
    /// <summary>
    /// Struct that holds a result from a method call.
    /// </summary>
    /// <typeparam name="T">The type that should be returned with the <see cref="Result{T}"/>.</typeparam>
    public struct Result<T>
    {
        private T _value;
        private string _error;

        public bool HasValue { get; private set; }

        public T Value
        {
            get => _value;
            set
            {
                HasValue = true;
                _value = value;
            }
        }

        public string Error
        {
            get => _error;
            set
            {
                HasValue = false;
                _error = value;
            }
        }

        /// <summary>
        /// Returns a new <see cref="Result{T}"/> containing a value.
        /// </summary>
        /// <param name="value">The value to be contained by the Result.</param>
        public static Result<T> Ok(T value)
        {
            return new Result<T>
            {
                Value = value
            };
        }

        /// <summary>
        /// Returns a new Result containing an error.
        /// </summary>
        /// <param name="error">The error message to be contained by the Result.</param>
        public static Result<T> Err(string error)
        {
            return new Result<T>
            {
                Error = error
            };
        }

        /// <summary>
        /// Returns the contained value or the default of that type if there is no value.
        /// </summary>
        public T ValueOrDefault()
        {
            return HasValue ? _value : default;
        }

        /// <summary>
        /// Returns the contained value or throws and exception if there is no value.
        /// </summary>
        /// <exception cref="NoValueException">Thrown when there is an error.</exception>
        public T ValueOrThrow()
        {
            if (HasValue) return _value;
            throw new NoValueException(_error);
        }

        /// <summary>
        /// Returns the contained value or the specified value if there is no value.
        /// </summary>
        /// <param name="defaultValue">The default value to return if there is no contained value.</param>
        public T ValueOr(T defaultValue)
        {
            return HasValue ? _value : defaultValue;
        }
    }

    /// <summary>
    /// Value-free version of <see cref="Result{T}"/>.
    /// </summary>
    public struct Result
    {
        public string Error { get; }

        public bool HasValue;

        private Result(string error)
        {
            Error = error;
            HasValue = false;
        }

        public static Result Ok()
        {
            return new Result
            {
                HasValue = true
            };
        }

        public static Result Err(string error)
        {
            return new Result(error);
        }
    }
}