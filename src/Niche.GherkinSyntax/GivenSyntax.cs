using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Niche.GherkinSyntax
{
    /// <summary>
    /// Defines the syntax available after 'Given'.
    /// </summary>
    /// <typeparam name="C">Type of context contained.</typeparam>
    public sealed class GivenSyntax<C> : IGivenSyntax<C>, IGivenSyntaxAsync<C>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GivenSyntax{C}"/> class.
        /// </summary>
        /// <param name="context">
        /// Context information to make available to the rest of the statement.
        /// </param>
        public GivenSyntax(C context)
        {
            Context = context;
        }

        /// <summary>
        /// Gets the context information wrapped by this syntax instance.
        /// </summary>
        public C Context { get; }

        /// <summary>
        /// Configure our test context with an additional function.
        /// </summary>
        /// <typeparam name="R">Type of returned context.</typeparam>
        /// <param name="configure">A function to configure the context.</param>
        /// <returns>A syntax implementation for method chaining.</returns>
        public IGivenSyntax<R> And<R>(Func<C, R> configure)
        {
            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            var context = configure(Context);
            return new GivenSyntax<R>(context);
        }

        /// <summary>
        /// Configure or transform our test context.
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
        public IGivenSyntax<R> And<P, R>(Func<C, P, R> configure, P parameter)
        {
            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            return And(context => configure(context, parameter));
        }

        /// <summary>
        /// Configure or transform our test context.
        /// </summary>
        /// <remarks>
        /// The func "configure" should return the new effective context.
        /// </remarks>
        /// <typeparam name="R">Type of returned context.</typeparam>
        /// <param name="configure">
        /// A function to configure or modify the context.
        /// </param>
        /// <returns>A syntax implementation for method chaining.</returns>
        public async Task<IGivenSyntaxAsync<R>> AndAsync<R>(Func<C, Task<R>> configure)
        {
            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            var context = await configure(Context)
                .ConfigureAwait(false);
            return new GivenSyntax<R>(context);
        }

        /// <summary>
        /// Configure or transform our test context.
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
        public Task<IGivenSyntaxAsync<R>> AndAsync<P, R>(
            Func<C, P, Task<R>> configure, P parameter)
        {
            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            return AndAsync(context => configure(context, parameter));
        }

        /// <summary>
        /// Declare a function  to be tested.
        /// </summary>
        /// <typeparam name="R">Type of context returned.</typeparam>
        /// <param name="function">A function to take on our context.</param>
        /// <returns>A syntax implementation for method chaining.</returns>
        public IWhenSyntax<R> When<R>(Func<C, R> function)
        {
            if (function == null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            var context = function(Context);
            return new WhenSyntax<R>(context);
        }

        /// <summary>
        /// Apply a transformation to our original context.
        /// </summary>
        /// <typeparam name="P">Type of the parameter passed.</typeparam>
        /// <typeparam name="R">Type of returned context.</typeparam>
        /// <param name="function">A function to take on our context.</param>
        /// <param name="parameter">
        /// Parameter value to use when configuring the test context.
        /// </param>
        /// <returns>A syntax implementation for method chaining.</returns>
        public IWhenSyntax<R> When<P, R>(Func<C, P, R> function, P parameter)
        {
            if (function == null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return When(context => function(context, parameter));
        }

        /// <summary>
        /// Apply a transformation to our original context.
        /// </summary>
        /// <typeparam name="R">Type of returned context.</typeparam>
        /// <param name="function">A function to take on our context.</param>
        /// <returns>A syntax implementation for method chaining.</returns>
        public async Task<IWhenSyntaxAsync<R>> WhenAsync<R>(Func<C, Task<R>> function)
        {
            if (function == null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            var context = await function(Context).ConfigureAwait(false);
            return new WhenSyntax<R>(context);
        }

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
        public Task<IWhenSyntaxAsync<R>> WhenAsync<P, R>(
            Func<C, P, Task<R>> function, P parameter)
        {
            if (function == null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return WhenAsync(context => function(context, parameter));
        }
    }
}
