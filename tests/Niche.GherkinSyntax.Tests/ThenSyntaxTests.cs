using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Niche.GherkinSyntax.Tests
{
    public class ThenSyntaxTests
    {
        private readonly string _context = "context";

        private readonly ThenSyntax<string> _syntax;

        public ThenSyntaxTests()
        {
            _syntax = new ThenSyntax<string>(_context);
        }

        public class Constructor : ThenSyntaxTests
        {
            [Fact]
            public void GivenContext_SetsProperty()
            {
                var instance = new ThenSyntax<string>(_context);
                instance.Context.Should().Be(_context);
            }
        }

        public class AndMethod : ThenSyntaxTests
        {
            [Fact]
            public void GivenNull_ThrowsExpectedException()
            {
                var exception =
                     Assert.Throws<ArgumentNullException>(
                        () => _syntax.And(null));
                exception.ParamName.Should().Be("action");
            }

            [Fact]
            public void GivenAction_ReturnsInstance()
            {
                var instance = _syntax.And(_ => { });
                instance.Should().NotBeNull();
            }

            [Fact]
            public void GivenAction_CallsAction()
            {
                var called = false;
                _syntax.And(_ => called = true);
                called.Should().BeTrue();
            }
        }

        public class AndAsync : ThenSyntaxTests
        {
            [Fact]
            public async Task GivenNull_ThrowsExpectedException()
            {
                Func<string, Task> action = null;

                // ReSharper disable once ExpressionIsAlwaysNull
                var exception =
                    await Assert.ThrowsAsync<ArgumentNullException>(
                            () => _syntax.AndAsync(action))
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

                var instance = await _syntax.AndAsync(Ac)
                    .ConfigureAwait(false);
                instance.Should().NotBeNull();
            }

            [Fact]
            public async Task GivenAction_CallsAction()
            {
                var called = false;
                async Task Ac(string context)
                {
                    await Task.Yield();
                    called = true;
                }

                await _syntax.AndAsync(Ac)
                    .ConfigureAwait(false);
                called.Should().BeTrue();
            }
        }
    }
}
