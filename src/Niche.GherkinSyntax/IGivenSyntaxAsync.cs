using System;
using System.Threading.Tasks;

namespace Niche.GherkinSyntax
{
    /// <summary>
    /// Defines the asynchronous syntax available after 'Given'.
    /// </summary>
    /// <typeparam name="C">Type of context contained.</typeparam>
    public interface IGivenSyntaxAsync<C>
    {
        /// <summary>
        /// Configure or transform our test context with a parameter.
        /// </summary>
        /// <remarks>
        /// The parameter <paramref name="configure"/> should return the new effective context.
        /// </remarks>
        /// <typeparam name="R">Type of returned context.</typeparam>
        /// <param name="configure">
        /// A function to configure or modify the context.
        /// </param>
        /// <returns>A syntax implementation for method chaining.</returns>
        Task<IGivenSyntaxAsync<R>> AndAsync<R>(Func<C, Task<R>> configure);

        /// <summary>
        /// Configure or transform our test context.
        /// </summary>
        /// <remarks>
        /// The parameter <paramref name="configure"/> should return the new effective context.
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
        Task<IGivenSyntaxAsync<R>> AndAsync<P, R>(
            Func<C, P, Task<R>> configure, P parameter);

        /// <summary>
        /// Apply a transformation to our original context.
        /// </summary>
        /// <typeparam name="R">Type of returned context.</typeparam>
        /// <param name="function">A function to take on our context.</param>
        /// <returns>A syntax implementation for method chaining.</returns>
        Task<IWhenSyntaxAsync<R>> WhenAsync<R>(Func<C, Task<R>> function);

        /// <summary>
        /// Apply a transformation to our original context with a parameter.
        /// </summary>
        /// <typeparam name="P">Type of the parameter passed.</typeparam>
        /// <typeparam name="R">Type of returned context.</typeparam>
        /// <param name="function">A function to take on our context.</param>
        /// <param name="parameter">
        /// Parameter value to use when configuring the test context.
        /// </param>
        /// <returns>A syntax implementation for method chaining.</returns>
        Task<IWhenSyntaxAsync<R>> WhenAsync<P, R>(
            Func<C, P, Task<R>> function, P parameter);
    }
}
