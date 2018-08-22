using System;

namespace Niche.GherkinSyntax
{
    /// <summary>
    /// Defines the syntax available after 'Given'
    /// </summary>
    /// <typeparam name="C">Type of context contained.</typeparam>
    public interface IGivenSyntax<C>
    {
        /// <summary>
        /// Configure or transform our test context
        /// </summary>
        /// <remarks>
        /// The func "configure" should return the new effective context.
        /// </remarks>
        /// <typeparam name="R">Type of returned context.</typeparam>
        /// <param name="configure">
        /// A function to configure or modify the context.
        /// </param>
        /// <returns>A syntax implementation for method chaining.</returns>
        IGivenSyntax<R> And<R>(Func<C, R> configure);

        /// <summary>
        /// Configure or transform our test context with a parameter
        /// </summary>
        /// <remarks>
        /// The func "configure" should return the new effective context.
        /// </remarks>
        /// <typeparam name="P">Type of the parameter passed.</typeparam>
        /// <typeparam name="R">Type of returned context.</typeparam>
        /// <param name="configure">
        /// A function to configure or modify the context.
        /// </param>
        /// <param name="parameter">
        /// Parameter value to use when configuring the test context.
        /// </param>
        /// <returns>A syntax implementation for method chaining.</returns>
        IGivenSyntax<R> And<P, R>(Func<C, P, R> configure, P parameter);

        /// <summary>
        /// Apply a transformation to our original context
        /// </summary>
        /// <typeparam name="R">Type of returned context.</typeparam>
        /// <param name="function">A function to take on our context.</param>
        /// <returns>A syntax implementation for method chaining.</returns>
        IWhenSyntax<R> When<R>(Func<C, R> function);

        /// <summary>
        /// Apply a transformation to our original context
        /// </summary>
        /// <typeparam name="P">Type of the parameter passed.</typeparam>
        /// <typeparam name="R">Type of returned context.</typeparam>
        /// <param name="function">A function to take on our context.</param>
        /// <param name="parameter">
        /// Parameter value to use when configuring the test context.
        /// </param>
        /// <returns>A syntax implementation for method chaining.</returns>
        IWhenSyntax<R> When<P, R>(Func<C, P, R> function, P parameter);
    }
}
