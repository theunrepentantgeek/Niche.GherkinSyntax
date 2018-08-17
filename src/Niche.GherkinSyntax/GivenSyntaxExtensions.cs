using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Niche.GherkinSyntax
{
    /// <summary>
    /// Extensions for working with <see cref="IGivenSyntax{C}"/> instances
    /// </summary>
    public static class GivenSyntaxExtensions
    {
        /// <summary>
        /// Configure our test context with an additional function
        /// </summary>
        /// <typeparam name="C">Type of context consumed.</typeparam>
        /// <typeparam name="R">Type of returned context.</typeparam>
        /// <param name="task">Task returning our <see cref="GivenSyntax{C}"/>.</param>
        /// <param name="configure">A function to configure the context.</param>
        /// <returns>A syntax implementation for method chaining.</returns>
        public static async Task<IGivenSyntaxAsync<R>> AndAsync<C, R>(
            this Task<IGivenSyntaxAsync<C>> task, Func<C, Task<R>> configure)
        {
            var syntax = await task.ConfigureAwait(false);
            return await syntax.AndAsync(configure).ConfigureAwait(false);
        }

        /// <summary>
        /// Declare a function  to be tested
        /// </summary>
        /// <typeparam name="C">Type of context consumed.</typeparam>
        /// <typeparam name="R">Type of returned context.</typeparam>
        /// <param name="task">Task returning our <see cref="GivenSyntax{C}"/>.</param>
        /// <param name="function">A function to take on our context.</param>
        /// <returns>A syntax implementation for method chaining.</returns>
        [SuppressMessage(
            "Maintainability",
            "AV1551:Method overload should call another overload",
            Justification = "This is the base method, used to call the method on the syntax implementation within task.")]
        public static async Task<IWhenSyntaxAsync<R>> WhenAsync<C, R>(
            this Task<IGivenSyntaxAsync<C>> task, Func<C, Task<R>> function)
        {
            if (function == null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            var syntax = await task.ConfigureAwait(false);
            return await syntax.WhenAsync(function).ConfigureAwait(false);
        }

        /// <summary>
        /// Declare a function  to be tested, with a parameter
        /// </summary>
        /// <typeparam name="C">Type of context consumed.</typeparam>
        /// <typeparam name="P">Type of parameter.</typeparam>
        /// <typeparam name="R">Type of returned context.</typeparam>
        /// <param name="task">Task returning our <see cref="GivenSyntax{C}"/>.</param>
        /// <param name="function">A function to take on our context.</param>
        /// <param name="parameter">Parameter value.</param>
        /// <returns>A syntax implementation for method chaining.</returns>
        public static Task<IWhenSyntaxAsync<R>> WhenAsync<C, P, R>(
            this Task<IGivenSyntaxAsync<C>> task, Func<C, P, Task<R>> function, P parameter)
        {
            if (function == null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return WhenAsync(task, context => function(context, parameter));
        }
    }
}
