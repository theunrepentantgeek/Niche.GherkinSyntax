using System;
using System.Threading.Tasks;

namespace Niche.GherkinSyntax
{
    /// <summary>
    /// Extension methods for working with <see cref="IThenSyntax{C}"/> instances
    /// </summary>
    public static class ThenSyntaxExtensions
    {
        /// <summary>
        /// Apply another action to our context to verify the state
        /// </summary>
        /// <typeparam name="C">Type of context.</typeparam>
        /// <param name="task">Task returning our <see cref="IThenSyntax{C}"/>.</param>
        /// <param name="action">An action to verify state.</param>
        /// <returns>A syntax implementation for method chaining.</returns>
        public static Task<IThenSyntaxAsync<C>> AndAsync<C>(
            this Task<IThenSyntaxAsync<C>> task, Func<C, Task> action)
        {
            var syntax = task.Result;
            return syntax.AndAsync(action);
        }
    }
}
