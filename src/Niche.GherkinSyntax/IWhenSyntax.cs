﻿using System;

namespace Niche.GherkinSyntax
{
    /// <summary>
    /// Defines the syntax available after 'When'
    /// </summary>
    /// <typeparam name="C">Type of context available for modification.</typeparam>
    public interface IWhenSyntax<C>
    {
        /// <summary>
        /// Apply an additional transformation to our context
        /// </summary>
        /// <remarks>
        /// The func "function" should return the new effective context.
        /// </remarks>
        /// <typeparam name="R">Type of context returned.</typeparam>
        /// <param name="function">A function to test on our context.</param>
        /// <returns>A syntax implementation for method chaining.</returns>
        IWhenSyntax<R> And<R>(Func<C, R> function);

        /// <summary>
        /// Apply a action to our context to verify the state
        /// </summary>
        /// <param name="action">An action to verify  state.</param>
        /// <returns>A syntax implementation for method chaining.</returns>
        IThenSyntax<C> Then(Action<C> action);
    }
}
