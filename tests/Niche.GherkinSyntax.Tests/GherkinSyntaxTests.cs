using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Niche.GherkinSyntax.Tests
{
    [SuppressMessage(
        "Design",
        "CA1052:Static holder types should be Static or NotInheritable",
        Justification = "This class groups related tests.")]
    [SuppressMessage(
        "Design",
        "RCS1102:Make class static.",
        Justification = "This class groups related tests.")]
    public class GherkinSyntaxTests
    {
        public class GivenAsync : GherkinSyntaxTests
        {
            [Fact]
            public async Task GivenNull_ThrowsException()
            {
                Func<Task<int>> createContext = null;

                // ReSharper disable once ExpressionIsAlwaysNull
                var exception =
                    await Assert.ThrowsAsync<ArgumentNullException>(
                        async () => await GherkinFactory.GivenAsync(createContext)
                            .ConfigureAwait(false))
                        .ConfigureAwait(false);
                exception.ParamName.Should().Be("createContext");
            }

            [Fact]
            public async Task GivenFunction_ReturnsInstance()
            {
                var instance = await GherkinFactory.GivenAsync(CreateContext)
                    .ConfigureAwait(false);
                instance.Should().NotBeNull();
            }

            [Fact]
            public async Task GivenFunction_ReturnsContextWithResult()
            {
                var instance = await GherkinFactory.GivenAsync(CreateContext)
                    .ConfigureAwait(false);
                var syntax = (GivenSyntax<int>)instance;
                syntax.Context.Should().Be(1);
            }

            private Task<int> CreateContext() => Task.FromResult(1);
        }

        public class GivenAsyncWithParameter : GherkinSyntaxTests
        {
            [Fact]
            public async Task GivenNull_ThrowsException()
            {
                Func<int, Task<int>> createContext = null;

                // ReSharper disable once ExpressionIsAlwaysNull
                var exception =
                    await Assert.ThrowsAsync<ArgumentNullException>(
                        async () => await GherkinFactory.GivenAsync(createContext, 1)
                            .ConfigureAwait(false))
                        .ConfigureAwait(false);
                exception.ParamName.Should().Be("createContext");
            }

            [Fact]
            public async Task GivenFunction_ReturnsInstance()
            {
                var instance = await GherkinFactory.GivenAsync(CreateContext, 40)
                    .ConfigureAwait(false);
                instance.Should().NotBeNull();
            }

            [Fact]
            public async Task GivenFunction_ReturnsContextWithResult()
            {
                var instance = await GherkinFactory.GivenAsync(CreateContext, 40)
                    .ConfigureAwait(false);
                var syntax = (GivenSyntax<int>)instance;
                syntax.Context.Should().Be(40);
            }

            private Task<int> CreateContext(int value) => Task.FromResult(value);
        }

        public class GivenMethod : GherkinSyntaxTests
        {
            [Fact]
            public void GivenNull_ThrowsException()
            {
                Func<int> createContext = null;

                // ReSharper disable once ExpressionIsAlwaysNull
                var exception =
                     Assert.Throws<ArgumentNullException>(
                             () => GherkinFactory.Given(createContext));
                exception.ParamName.Should().Be("createContext");
            }

            [Fact]
            public void GivenFunction_ReturnsInstance()
            {
                GherkinFactory.Given(CreateContext).Should().NotBeNull();
            }

            [Fact]
            public void GivenFunction_ReturnsContextWithResult()
            {
                var instance = GherkinFactory.Given(CreateContext);
                var syntax = (GivenSyntax<int>)instance;
                syntax.Context.Should().Be(1);
            }

            private int CreateContext() => 1;
        }

        public class GivenMethodWithParameter : GherkinSyntaxTests
        {
            [Fact]
            public void GivenNull_ThrowsException()
            {
                Func<int, int> createContext = null;

                // ReSharper disable once ExpressionIsAlwaysNull
                var exception =
                     Assert.Throws<ArgumentNullException>(
                             () => GherkinFactory.Given(createContext, 42));
                exception.ParamName.Should().Be("createContext");
            }

            [Fact]
            public void GivenFunction_ReturnsInstance()
            {
                GherkinFactory.Given(CreateContext, 42).Should().NotBeNull();
            }

            [Fact]
            public void GivenFunction_ReturnsContextWithResult()
            {
                var instance = GherkinFactory.Given(CreateContext, 42);
                var syntax = (GivenSyntax<int>)instance;
                syntax.Context.Should().Be(42);
            }

            private int CreateContext(int value) => value;
        }
    }
}
