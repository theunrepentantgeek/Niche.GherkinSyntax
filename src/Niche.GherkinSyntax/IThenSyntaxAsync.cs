using System;
using System.Threading.Tasks;

namespace Niche.GherkinSyntax
{
    /// <summary>
    /// Defines asynchronous the syntax available after 'Then'
    /// </summary>
    /// <typeparam name="C">Type of context available for verification actions.</typeparam>
    public interface IThenSyntaxAsync<C>
    {
        /// <summary>
        /// Apply another action to our context to verify the state
        /// </summary>
        /// <param name="action">An action to verify state.</param>
        /// <returns>A syntax implementation for method chaining.</returns>
        Task<IThenSyntaxAsync<C>> AndAsync(Func<C, Task> action);
    }
}
