using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Niche.GherkinSyntax.Tests
{
    public class GherkinSyntaxTests
    {
        private Task<int> CreateAsyncContext() => Task.FromResult(1);

        private int CreateContext() => 1;

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
                var instance = await GherkinFactory.GivenAsync(CreateAsyncContext)
                    .ConfigureAwait(false);
                instance.Should().NotBeNull();
            }

            [Fact]
            public async Task GivenFunction_ReturnsContextWithResult()
            {
                var instance = await GherkinFactory.GivenAsync(CreateAsyncContext)
                    .ConfigureAwait(false);
                var syntax = (GivenSyntax<int>)instance;
                syntax.Context.Should().Be(1);
            }
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
        }
    }
}
