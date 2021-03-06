﻿using System;

namespace Atata
{
    /// <summary>
    /// Represents the dynamic object source that gets an object using function.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public class DynamicObjectSource<TValue> : IObjectSource<TValue>
    {
        private readonly Func<TValue> valueGetFunction;

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicObjectSource{TValue}"/> class.
        /// </summary>
        /// <param name="valueGetFunction">The value get function.</param>
        public DynamicObjectSource(Func<TValue> valueGetFunction)
        {
            this.valueGetFunction = valueGetFunction.CheckNotNull(nameof(valueGetFunction));
        }

        /// <inheritdoc/>
        public TValue Value =>
            valueGetFunction.Invoke();

        /// <inheritdoc/>
        public string SourceProviderName => null;

        /// <inheritdoc/>
        public bool IsDynamic => true;
    }
}
