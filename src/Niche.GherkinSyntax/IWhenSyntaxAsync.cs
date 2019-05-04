using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Niche.GherkinSyntax
{
    /// <summary>
    /// Defines the asynchronous syntax available after 'When'.
    /// </summary>
    /// <typeparam name="C">Type of context available for modification.</typeparam>
    public interface IWhenSyntaxAsync<C>
    {
        /// <summary>
        /// Apply an additional transformation to our context.
        /// </summary>
        /// <remarks>
        /// The parameter <paramref name="function"/> should return the new effective context.
        /// </remarks>
        /// <typeparam name="R">Type of context returned.</typeparam>
        /// <param name="function">A function to test on our context.</param>
        /// <returns>A syntax implementation for method chaining.</returns>
        Task<IWhenSyntaxAsync<R>> AndAsync<R>(Func<C, Task<R>> function);

        /// <summary>
        /// Apply an additional transformation to our context with a parameter.
        /// </summary>
        /// <remarks>
        /// The parameter <paramref name="function"/> should return the new effective context.
        /// </remarks>
        /// <typeparam name="P">Type of the parameter passed.</typeparam>
        /// <typeparam name="R">Type of context returned.</typeparam>
        /// <param name="function">A function to test on our context.</param>
        /// <param name="parameter">
        /// Parameter value to use when configuring the test context.
        /// </param>
        /// <returns>A syntax implementation for method chaining.</returns>
        Task<IWhenSyntaxAsync<R>> AndAsync<P, R>(Func<C, P, Task<R>> function, P parameter);

        /// <summary>
        /// Apply a action to our context to verify the state.
        /// </summary>
        /// <param name="action">An action to verify  state.</param>
        /// <returns>A syntax implementation for method chaining.</returns>
        Task<IThenSyntaxAsync<C>> ThenAsync(Func<C, Task> action);

        /// <summary>
        /// Apply a action to our context to verify the state.
        /// </summary>
        /// <typeparam name="P">Type of the parameter passed.</typeparam>
        /// <param name="action">An action to verify  state.</param>
        /// <param name="parameter">
        /// Parameter value to use when configuring the test context.
        /// </param>
        /// <returns>A syntax implementation for method chaining.</returns>
        Task<IThenSyntaxAsync<C>> ThenAsync<P>(Func<C, P, Task> action, P parameter);
    }
}
