using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Niche.GherkinSyntax.Tests
{
    public class WhenSyntaxTests
    {
        private readonly string _context = "context";

        private readonly WhenSyntax<string> _syntax;

        public WhenSyntaxTests()
        {
            _syntax = new WhenSyntax<string>(_context);
        }

        public class Constructor : WhenSyntaxTests
        {
            [Fact]
            public void GivenContext_SetsProperty()
            {
                const string context = "context";
                var instance = new WhenSyntax<string>(context);
                instance.Context.Should().Be(context);
            }
        }

        public class AndMethod : WhenSyntaxTests
        {
            [Fact]
            public void GivenNull_ThrowsExpectecException()
            {
                Func<string, string> function = null;

                // ReSharper disable once ExpressionIsAlwaysNull
                var exception =
                    Assert.Throws<ArgumentNullException>(
                        () => _syntax.And(function));
                exception.ParamName.Should().Be("function");
            }

            [Fact]
            public void GivenFunction_ReturnsInstance()
            {
                string Fn(string context)
                {
                    return _context;
                }

                var instance = _syntax.And(Fn);
                instance.Should().NotBeNull();
            }

            [Fact]
            public void GivenFunction_CallsAction()
            {
                var called = false;
                string Fn(string context)
                {
                    called = true;
                    return _context;
                }

                _syntax.And(Fn);
                called.Should().BeTrue();
            }
        }

        public class AndMethodWithParameter : WhenSyntaxTests
        {
            [Fact]
            public void GivenNull_ThrowsExpectecException()
            {
                Func<string, int, string> function = null;

                // ReSharper disable once ExpressionIsAlwaysNull
                var exception =
                    Assert.Throws<ArgumentNullException>(
                        () => _syntax.And(function, 42));
                exception.ParamName.Should().Be("function");
            }

            [Fact]
            public void GivenFunction_ReturnsInstance()
            {
                string Fn(string context, int value)
                {
                    return _context + value;
                }

                var instance = _syntax.And(Fn, 42);
                instance.Should().NotBeNull();
            }

            [Fact]
            public void GivenFunction_CallsAction()
            {
                var called = false;
                string Fn(string context, int value)
                {
                    called = true;
                    return _context + value;
                }

                _syntax.And(Fn, 42);
                called.Should().BeTrue();
            }
        }

        public class AndAsync : WhenSyntaxTests
        {
            [Fact]
            public async Task GivenNull_ThrowsExpectecException()
            {
                Func<string, Task<string>> function = null;

                // ReSharper disable once ExpressionIsAlwaysNull
                var exception =
                    await Assert.ThrowsAsync<ArgumentNullException>(
                        () => _syntax.AndAsync(function))
                        .ConfigureAwait(false);
                exception.ParamName.Should().Be("function");
            }

            [Fact]
            public async Task GivenFunction_ReturnsInstance()
            {
                async Task<string> Fn(string context)
                {
                    await Task.Yield();
                    return _context;
                }

                var instance = await _syntax.AndAsync(Fn)
                    .ConfigureAwait(false);
                instance.Should().NotBeNull();
            }

            [Fact]
            public async Task GivenFunction_CallsAction()
            {
                var called = false;
                async Task<string> Fn(string context)
                {
                    called = true;
                    await Task.Yield();
                    return _context;
                }

                await _syntax.AndAsync(Fn)
                    .ConfigureAwait(false);
                called.Should().BeTrue();
            }
        }

        public class ThenMethod : WhenSyntaxTests
        {
            [Fact]
            public void GivenNull_ThrowsExpectedException()
            {
                var exception =
                    Assert.Throws<ArgumentNullException>(
                        () => _syntax.Then(null));
                exception.ParamName.Should().Be("action");
            }

            [Fact]
            public void GivenAction_ReturnsInstance()
            {
                var instance = _syntax.Then(_ => { });
                instance.Should().NotBeNull();
            }

            [Fact]
            public void GivenAction_CallsAction()
            {
                var called = false;
                _syntax.Then(_ => called = true);
                called.Should().BeTrue();
            }
        }

        public class ThenMethodWithParameter : WhenSyntaxTests
        {
            [Fact]
            public void GivenNull_ThrowsExpectedException()
            {
                var exception =
                    Assert.Throws<ArgumentNullException>(
                        () => _syntax.Then(null, 42));
                exception.ParamName.Should().Be("action");
            }

            [Fact]
            public void GivenAction_ReturnsInstance()
            {
                void Fn(string context, int value)
                {
                }

                var instance = _syntax.Then(Fn, 42);
                instance.Should().NotBeNull();
            }

            [Fact]
            public void GivenAction_CallsAction()
            {
                var called = false;

                void Fn(string context, int value)
                {
                    called = true;
                }

                _syntax.Then(Fn, 42);
                called.Should().BeTrue();
            }
        }

        public class ThenAsync : WhenSyntaxTests
        {
            [Fact]
            public async Task GivenNull_ThrowsExpectedException()
            {
                Func<string, Task> function = null;

                // ReSharper disable once ExpressionIsAlwaysNull
                var exception =
                    await Assert.ThrowsAsync<ArgumentNullException>(
                        () => _syntax.ThenAsync(function))
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

                var instance = await _syntax.ThenAsync(Ac)
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

                await _syntax.ThenAsync(Ac)
                    .ConfigureAwait(false);
                called.Should().BeTrue();
            }
        }
    }
}
