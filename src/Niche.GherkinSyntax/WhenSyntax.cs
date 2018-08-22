using System;
using System.Threading.Tasks;

namespace Niche.GherkinSyntax
{
    /// <summary>
    /// Implements the syntax available after 'When'
    /// </summary>
    /// <typeparam name="C">Type of context available for modification.</typeparam>
    public class WhenSyntax<C> : IWhenSyntax<C>, IWhenSyntaxAsync<C>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WhenSyntax{C}"/> class
        /// </summary>
        /// <param name="context">
        /// Context information to make available to the rest of the statement.
        /// </param>
        public WhenSyntax(C context)
        {
            Context = context;
        }

        /// <summary>
        /// Gets the context information held by this syntax instance
        /// </summary>
        public C Context { get; }

        /// <summary>
        /// Apply a function
        /// </summary>
        /// <typeparam name="R">Type of returned context.</typeparam>
        /// <param name="function">A function to test on our context.</param>
        /// <returns>A syntax implementation for method chaining.</returns>
        public IWhenSyntax<R> And<R>(Func<C, R> function)
        {
            if (function == null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            var newContext = function(Context);
            return new WhenSyntax<R>(newContext);
        }

        /// <summary>
        /// Apply an additional transformation to our context
        /// </summary>
        /// <remarks>
        /// The func "function" should return the new effective context.
        /// </remarks>
        /// <typeparam name="P">Type of the parameter passed.</typeparam>
        /// <typeparam name="R">Type of context returned.</typeparam>
        /// <param name="function">A function to test on our context.</param>
        /// <param name="parameter">
        /// Parameter value to use when configuring the test context.
        /// </param>
        /// <returns>A syntax implementation for method chaining.</returns>
        public IWhenSyntax<R> And<P, R>(Func<C, P, R> function, P parameter)
        {
            if (function == null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return And(context => function(context, parameter));
        }

        /// <summary>
        /// Apply an additional transformation to our context
        /// </summary>
        /// <remarks>
        /// The func "function" should return the new effective context.
        /// </remarks>
        /// <typeparam name="R">Type of context returned.</typeparam>
        /// <param name="function">A function to test on our context.</param>
        /// <returns>A syntax implementation for method chaining.</returns>
        public async Task<IWhenSyntaxAsync<R>> AndAsync<R>(Func<C, Task<R>> function)
        {
            if (function == null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            var newContext = await function(Context)
                .ConfigureAwait(false);
            return new WhenSyntax<R>(newContext);
        }

        /// <summary>
        /// Apply a action to our context to verify the state
        /// </summary>
        /// <param name="action">An action to verify  state.</param>
        /// <returns>A syntax implementation for method chaining.</returns>
        public IThenSyntax<C> Then(Action<C> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            action(Context);
            return new ThenSyntax<C>(Context);
        }

        /// <summary>
        /// Apply a action to our context to verify the state
        /// </summary>
        /// <typeparam name="P">Type of the parameter passed.</typeparam>
        /// <param name="action">An action to verify  state.</param>
        /// <param name="parameter">
        /// Parameter value to use when configuring the test context.
        /// </param>
        /// <returns>A syntax implementation for method chaining.</returns>
        public IThenSyntax<C> Then<P>(Action<C, P> action, P parameter)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return Then(context => action(context, parameter));
        }

        /// <summary>
        /// Apply a action to our context to verify the state
        /// </summary>
        /// <param name="action">An action to verify  state.</param>
        /// <returns>A syntax implementation for method chaining.</returns>
        public async Task<IThenSyntaxAsync<C>> ThenAsync(Func<C, Task> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            await action(Context).ConfigureAwait(false);
            return new ThenSyntax<C>(Context);
        }
    }
}
