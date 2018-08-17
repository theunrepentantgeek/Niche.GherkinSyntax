using System;
using System.Threading.Tasks;

namespace Niche.GherkinSyntax
{
    /// <summary>
    /// Defines the asynchronous syntax available after 'Given'
    /// </summary>
    /// <typeparam name="C">Type of context contained.</typeparam>
    public interface IGivenSyntaxAsync<C>
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
        Task<IGivenSyntaxAsync<R>> AndAsync<R>(Func<C, Task<R>> configure);

        /// <summary>
        /// Apply a transformation to our original context
        /// </summary>
        /// <typeparam name="R">Type of returned context.</typeparam>
        /// <param name="function">A function to take on our context.</param>
        /// <returns>A syntax implementation for method chaining.</returns>
        Task<IWhenSyntaxAsync<R>> WhenAsync<R>(Func<C, Task<R>> function);
    }
}
