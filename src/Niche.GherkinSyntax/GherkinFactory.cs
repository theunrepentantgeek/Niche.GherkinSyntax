using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Niche.GherkinSyntax
{
    /// <summary>
    /// Entry point for creating Gherkin style test statements
    /// </summary>
    /// <example>
    /// Import this class via <c>using static</c> so that the methods are directly usable
    /// without needing to specify <c>GherkinFactory.</c> as a prefix.
    /// <code>
    /// using static Niche.GherkinSyntax.GherkinFactory;
    ///
    /// [Fact]
    /// public void CanDoBasicAddition()
    ///     => Given(ANewCalculator) ...
    /// </code>
    /// </example>
    [SuppressMessage(
        "Class Design",
        "AV1008:Class should not be static",
        Justification = "The entry point needs to be static, and these aren't extension methods.")]
    public static class GherkinFactory
    {
        /// <summary>
        /// Start an asynchronous scenario test, specifying a function that creates the context for the test
        /// </summary>
        /// <remarks>
        /// See <seealso cref="GivenSyntaxExtensions"/> for extension methods that allow method chaning without needing
        /// to await the task first.
        /// </remarks>
        /// <typeparam name="C">Type of context returned.</typeparam>
        /// <param name="createContext">A function to create the test context.</param>
        /// <returns>A task returning a syntax implementation for method chaining.</returns>
        public static async Task<IGivenSyntaxAsync<C>> GivenAsync<C>(
            Func<Task<C>> createContext)
        {
            if (createContext == null)
            {
                throw new ArgumentNullException(nameof(createContext));
            }

            var context = await createContext().ConfigureAwait(false);
            return new GivenSyntax<C>(context);
        }

        /// <summary>
        /// Start an asynchronous scenario test, specifying a function that creates the context for the test and a
        /// parameter for that function
        /// </summary>
        /// <remarks>
        /// See <seealso cref="GivenSyntaxExtensions"/> for extension methods that allow method chaning without needing
        /// to await the task first.
        /// </remarks>
        /// <typeparam name="C">Type of context returned.</typeparam>
        /// <typeparam name="P">Type of parameter passed.</typeparam>
        /// <param name="createContext">A function to create the test context.</param>
        /// <param name="parameter">Parameter value to use when creating the test context.</param>
        /// <returns>A syntax implementation for method chaining.</returns>
        public static Task<IGivenSyntaxAsync<C>> GivenAsync<C, P>(
            Func<P, Task<C>> createContext, P parameter)
        {
            if (createContext == null)
            {
                throw new ArgumentNullException(nameof(createContext));
            }

            return GivenAsync(() => createContext(parameter));
        }

        /// <summary>
        /// Start a scenario test, specifying a function that creates the context for the test
        /// </summary>
        /// <typeparam name="C">Type of context returned.</typeparam>
        /// <param name="createContext">A function to create the test context.</param>
        /// <returns>A syntax implementation for method chaining.</returns>
        public static IGivenSyntax<C> Given<C>(Func<C> createContext)
        {
            if (createContext == null)
            {
                throw new ArgumentNullException(nameof(createContext));
            }

            var context = createContext();
            return new GivenSyntax<C>(context);
        }

        /// <summary>
        /// Start a scenario test, specifying a function that creates the context for the test
        /// </summary>
        /// <typeparam name="C">Type of context returned.</typeparam>
        /// <typeparam name="P">Type of the parameter passed.</typeparam>
        /// <param name="createContext">A function to create the test context.</param>
        /// <param name="parameter">Parameter value to use when creating the test context.</param>
        /// <returns>A syntax implementation for method chaining.</returns>
        public static IGivenSyntax<C> Given<C, P>(
            Func<P, C> createContext, P parameter)
        {
            if (createContext == null)
            {
                throw new ArgumentNullException(nameof(createContext));
            }

            return Given(() => createContext(parameter));
        }
    }
}
