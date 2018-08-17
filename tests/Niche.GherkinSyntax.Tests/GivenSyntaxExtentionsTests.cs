using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Niche.GherkinSyntax.Tests
{
    public class GivenSyntaxExtentionsTests
    {
        private readonly GivenSyntax<string> _syntax
            = new GivenSyntax<string>("context");

        private readonly Task<IGivenSyntaxAsync<string>> _taskSyntax;

        public GivenSyntaxExtentionsTests()
        {
            _taskSyntax = Task.FromResult<IGivenSyntaxAsync<string>>(_syntax);
        }

        public class AndAsync : GivenSyntaxExtentionsTests
        {
            [Fact]
            public async Task GivenNull_ThrowsExpectedException()
            {
                Func<string, Task<string>> function = null;

                // ReSharper disable once ExpressionIsAlwaysNull
                var exception =
                    await Assert.ThrowsAsync<ArgumentNullException>(
                            () => _taskSyntax.AndAsync(function))
                        .ConfigureAwait(false);
                exception.ParamName.Should().Be("configure");
            }

            [Fact]
            public async Task GivenFunction_ReturnsInstance()
            {
                async Task<string> Fn(string context)
                {
                    await Task.Yield();
                    return "result";
                }

                var syntax = await _taskSyntax.AndAsync(Fn)
                    .ConfigureAwait(false);
                syntax.Should().NotBeNull();
            }
        }

        public class AndMethod : GivenSyntaxExtentionsTests
        {
            [Fact]
            public void GivenNull_ThrowsExpectedException()
            {
                Func<string, string> function = null;

                // ReSharper disable once ExpressionIsAlwaysNull
                var exception =
                    Assert.Throws<ArgumentNullException>(
                        () => _syntax.And(function));
                exception.ParamName.Should().Be("configure");
            }

            [Fact]
            public void GivenFunction_ReturnsInstance()
            {
                string Fn(string context) => "result";

                var syntax = _syntax.And(Fn);
                syntax.Should().NotBeNull();
            }
        }

        public class WhenAsync : GivenSyntaxExtentionsTests
        {
            [Fact]
            public async Task GivenNull_ThrowsExpectedException()
            {
                Func<string, Task<string>> function = null;

                // ReSharper disable once ExpressionIsAlwaysNull
                var exception =
                    await Assert.ThrowsAsync<ArgumentNullException>(
                            () => _taskSyntax.WhenAsync(function))
                        .ConfigureAwait(false);
                exception.ParamName.Should().Be("function");
            }

            [Fact]
            public async Task GivenFunction_ReturnsInstance()
            {
                async Task<string> Fn(string context)
                {
                    await Task.Yield();
                    return "result";
                }

                var syntax = await _taskSyntax.WhenAsync(Fn)
                    .ConfigureAwait(false);
                syntax.Should().NotBeNull();
            }
        }

        public class WhenAsyncWithParameter : GivenSyntaxExtentionsTests
        {
            [Fact]
            public async Task GivenNull_ThrowsExpectedException()
            {
                Func<string, int, Task<string>> function = null;

                // ReSharper disable once ExpressionIsAlwaysNull
                var exception =
                    await Assert.ThrowsAsync<ArgumentNullException>(
                            async () => await _taskSyntax.WhenAsync(function, 4)
                                .ConfigureAwait(false))
                        .ConfigureAwait(false);
                exception.ParamName.Should().Be("function");
            }

            [Fact]
            public async Task GivenFunction_ReturnsInstance()
            {
                async Task<string> Fn(string context, int count)
                {
                    await Task.Yield();
                    return "result";
                }

                var syntax = await _taskSyntax.WhenAsync(Fn, 3)
                    .ConfigureAwait(false);
                syntax.Should().NotBeNull();
            }
        }
    }
}
