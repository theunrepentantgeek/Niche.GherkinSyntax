using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Niche.GherkinSyntax.Tests
{
    public class WhenSyntaxExtensionsTests
    {
        private readonly IWhenSyntaxAsync<string> _syntax =
            new WhenSyntax<string>("context");

        private readonly Task<IWhenSyntaxAsync<string>> _taskSyntax;

        public WhenSyntaxExtensionsTests()
        {
            _taskSyntax = Task.FromResult(_syntax);
        }

        public class AndAsync : WhenSyntaxExtensionsTests
        {
            [Fact]
            public async Task GivenNull_ThrowsExpectedException()
            {
                Func<string, Task<int>> action = null;

                // ReSharper disable once ExpressionIsAlwaysNull
                var exception =
                    await Assert.ThrowsAsync<ArgumentNullException>(
                        () => _taskSyntax.AndAsync(action))
                        .ConfigureAwait(false);
                exception.ParamName.Should().Be("function");
            }

            [Fact]
            public async Task GivenFunction_ReturnsInstance()
            {
                async Task<int> Fn(string context)
                {
                    await Task.Yield();
                    return -1;
                }

                var syntax = await _taskSyntax.AndAsync(Fn)
                    .ConfigureAwait(false);
                syntax.Should().NotBeNull();
            }
        }

        public class AndAsyncWithParameter : WhenSyntaxExtensionsTests
        {
            [Fact]
            public async Task GivenNull_ThrowsExpectedException()
            {
                Func<string, int, Task<int>> action = null;

                // ReSharper disable once ExpressionIsAlwaysNull
                var exception =
                    await Assert.ThrowsAsync<ArgumentNullException>(
                        () => _taskSyntax.AndAsync(action, 42))
                        .ConfigureAwait(false);
                exception.ParamName.Should().Be("function");
            }

            [Fact]
            public async Task GivenFunction_ReturnsInstance()
            {
                async Task<int> Fn(string context, int value)
                {
                    await Task.Yield();
                    return -1;
                }

                var syntax = await _taskSyntax.AndAsync(Fn, 42)
                    .ConfigureAwait(false);
                syntax.Should().NotBeNull();
            }
        }

        public class ThenAsync : WhenSyntaxExtensionsTests
        {
            [Fact]
            public async Task GivenNull_ThrowsExpectedException()
            {
                Func<string, Task<int>> action = null;

                // ReSharper disable once ExpressionIsAlwaysNull
                var exception =
                    await Assert.ThrowsAsync<ArgumentNullException>(
                        () => _taskSyntax.ThenAsync(action))
                        .ConfigureAwait(false);
                exception.ParamName.Should().Be("action");
            }

            [Fact]
            public async Task GivenAction_ReturnsInstance()
            {
                async Task Ac(string context)
                {
                    await Task.Yield();
                }

                var syntax = await _taskSyntax.ThenAsync(Ac)
                    .ConfigureAwait(false);
                syntax.Should().NotBeNull();
            }
        }

        public class ThenAsyncWithParameter : WhenSyntaxExtensionsTests
        {
            [Fact]
            public async Task GivenNull_ThrowsExpectedException()
            {
                Func<string, int, Task<int>> action = null;

                // ReSharper disable once ExpressionIsAlwaysNull
                var exception =
                    await Assert.ThrowsAsync<ArgumentNullException>(
                        () => _taskSyntax.ThenAsync(action, 42))
                        .ConfigureAwait(false);
                exception.ParamName.Should().Be("action");
            }

            [Fact]
            public async Task GivenAction_ReturnsInstance()
            {
                async Task Ac(string context, int value)
                {
                    await Task.Yield();
                }

                var syntax = await _taskSyntax.ThenAsync(Ac, 42)
                    .ConfigureAwait(false);
                syntax.Should().NotBeNull();
            }
        }
    }
}
