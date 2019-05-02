using System;
using System.Threading.Tasks;

namespace Niche.GherkinSyntax
{
    /// <summary>
    /// Extension methods for working with <see cref="IWhenSyntax{C}"/> instances.
    /// </summary>
    public static class WhenSyntaxExtensions
    {
        /// <summary>
        /// Apply a function.
        /// </summary>
        /// <typeparam name="C">Type of context consumed.</typeparam>
        /// <typeparam name="R">Type of context returned.</typeparam>
        /// <param name="task">Task returning our <see cref="GivenSyntax{C}"/>.</param>
        /// <param name="function">A function to test on our context.</param>
        /// <returns>A syntax implementation for method chaining.</returns>
        public static Task<IWhenSyntaxAsync<R>> AndAsync<C, R>(
            this Task<IWhenSyntaxAsync<C>> task, Func<C, Task<R>> function)
        {
            var syntax = task.Result;
            return syntax.AndAsync(function);
        }

        /// <summary>
        /// Apply a function with a parameter.
        /// </summary>
        /// <typeparam name="C">Type of context consumed.</typeparam>
        /// <typeparam name="P">Type of the parameter passed.</typeparam>
        /// <typeparam name="R">Type of context returned.</typeparam>
        /// <param name="task">Task returning our <see cref="GivenSyntax{C}"/>.</param>
        /// <param name="function">A function to test on our context.</param>
        /// <param name="parameter">
        /// Parameter value to use when configuring the test context.
        /// </param>
        /// <returns>A syntax implementation for method chaining.</returns>
        public static Task<IWhenSyntaxAsync<R>> AndAsync<C, P, R>(
            this Task<IWhenSyntaxAsync<C>> task, Func<C, P, Task<R>> function, P parameter)
        {
            var syntax = task.Result;
            return syntax.AndAsync(function, parameter);
        }

        /// <summary>
        /// Apply a action to our context to verify the state.
        /// </summary>
        /// <typeparam name="C">Type of context.</typeparam>
        /// <param name="task">Task returning our <see cref="GivenSyntax{C}"/>.</param>
        /// <param name="action">An action to verify  state.</param>
        /// <returns>A syntax implementation for method chaining.</returns>
        public static async Task<IThenSyntaxAsync<C>> ThenAsync<C>(
            this Task<IWhenSyntaxAsync<C>> task, Func<C, Task> action)
        {
            var syntax = await task.ConfigureAwait(false);
            return await syntax.ThenAsync(action)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Apply a action to our context to verify the state.
        /// </summary>
        /// <typeparam name="C">Type of context.</typeparam>
        /// <typeparam name="P">Type of the parameter passed.</typeparam>
        /// <param name="task">Task returning our <see cref="GivenSyntax{C}"/>.</param>
        /// <param name="action">An action to verify  state.</param>
        /// <param name="parameter">
        /// Parameter value to use when configuring the test context.
        /// </param>
        /// <returns>A syntax implementation for method chaining.</returns>
        public static async Task<IThenSyntaxAsync<C>> ThenAsync<C, P>(
            this Task<IWhenSyntaxAsync<C>> task, Func<C, P, Task> action, P parameter)
        {
            var syntax = await task.ConfigureAwait(false);
            return await syntax.ThenAsync(action, parameter)
                .ConfigureAwait(false);
        }
    }
}
