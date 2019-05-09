using System;
using System.Threading.Tasks;

namespace Niche.GherkinSyntax
{
    /// <summary>
    /// Implements the syntax available after 'Then'.
    /// </summary>
    /// <typeparam name="C">Type of context available for verification actions.</typeparam>
    public class ThenSyntax<C> : IThenSyntax<C>, IThenSyntaxAsync<C>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ThenSyntax{C}"/> class.
        /// </summary>
        /// <param name="context">
        /// Context information to make available to the rest of the statement.
        /// </param>
        public ThenSyntax(C context)
        {
            Context = context;
        }

        /// <summary>
        /// Gets the context information held by this syntax instance.
        /// </summary>
        public C Context { get; }

        /// <summary>
        /// Apply another action to our context to verify the state.
        /// </summary>
        /// <param name="action">An action to verify state.</param>
        /// <returns>A syntax implementation for method chaining.</returns>
        public IThenSyntax<C> And(Action<C> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            action(Context);
            return this;
        }

        /// <summary>
        /// Apply another action to our context to verify the state.
        /// </summary>
        /// <typeparam name="P">Type of the parameter passed.</typeparam>
        /// <param name="action">An action to verify state.</param>
        /// <param name="parameter">
        /// Parameter value to use when configuring the test context.
        /// </param>
        /// <returns>A syntax implementation for method chaining.</returns>
        public IThenSyntax<C> And<P>(Action<C, P> action, P parameter)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            action(Context, parameter);
            return this;
        }

        /// <summary>
        /// Apply another action to our context to verify the state.
        /// </summary>
        /// <param name="action">An action to verify state.</param>
        /// <returns>A syntax implementation for method chaining.</returns>
        public async Task<IThenSyntaxAsync<C>> AndAsync(Func<C, Task> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            await action(Context).ConfigureAwait(false);
            return this;
        }

        /// <summary>
        /// Apply another action to our context to verify the state.
        /// </summary>
        /// <typeparam name="P">Type of the parameter passed.</typeparam>
        /// <param name="action">An action to verify state.</param>
        /// <param name="parameter">
        /// Parameter value to use when configuring the test context.
        /// </param>
        /// <returns>A syntax implementation for method chaining.</returns>
        public async Task<IThenSyntaxAsync<C>> AndAsync<P>(Func<C, P, Task> action, P parameter)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            await action(Context, parameter).ConfigureAwait(false);
            return this;
        }
    }
}
