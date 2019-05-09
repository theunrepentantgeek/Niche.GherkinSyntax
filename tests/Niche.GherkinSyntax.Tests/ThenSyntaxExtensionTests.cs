using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Niche.GherkinSyntax.Tests
{
    public class ThenSyntaxExtensionTests
    {
        private readonly IThenSyntaxAsync<string> _syntax
            = new ThenSyntax<string>("context");

        private readonly Task<IThenSyntaxAsync<string>> _taskSyntax;

        public ThenSyntaxExtensionTests()
        {
            _taskSyntax = Task.FromResult(_syntax);
        }

        public class AndAsync : ThenSyntaxExtensionTests
        {
            [Fact]
            public async Task GivenNull_ThrowsExpectedException()
            {
                Func<string, Task> action = null;

                // ReSharper disable once ExpressionIsAlwaysNull
                var exception =
                    await Assert.ThrowsAsync<ArgumentNullException>(
                            () => _taskSyntax.AndAsync(action))
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

                var syntax = await _taskSyntax.AndAsync(Ac)
                    .ConfigureAwait(false);
                syntax.Should().NotBeNull();
            }
        }
    }
}
