using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Niche.GherkinSyntax.Tests
{
    public class GivenSyntaxTests
    {
        private readonly string _context = "context";

        private readonly GivenSyntax<string> _given;

        public GivenSyntaxTests()
        {
            _given = new GivenSyntax<string>(_context);
        }

        public class Constructor : GivenSyntaxTests
        {
            [Fact]
            public void GivenContext_SetsProperty()
            {
                const string context = "context";
                var given = new GivenSyntax<string>(context);
                given.Context.Should().Be(context);
            }
        }

        public class AndWithFunc : GivenSyntaxTests
        {
            [Fact]
            public void GivenNullFunc_ThrowsExpectedException()
            {
                Func<string, int> func = null;

                // ReSharper disable once ExpressionIsAlwaysNull
                var exception =
                    Assert.Throws<ArgumentNullException>(
                        () => _given.And(func));
                exception.ParamName.Should().Be("configure");
            }

            [Fact]
            public void GivenFunc_ReturnsInstance()
            {
                var instance = _given.And(name => name + "Foo");
                instance.Should().NotBeNull();
            }

            [Fact]
            public void GivenFunc_CallsFunc()
            {
                var called = false;
                string Fn(string name)
                {
                    called = true;
                    return name + "Foo";
                }

                _given.And(Fn);
                called.Should().BeTrue();
            }
        }

        [SuppressMessage(
            "Class Design",
            "AV1000:Type name contains the word 'and', which suggests it has multiple purposes",
            Justification = "The name reflects the method signature being tested")]
        public class AndWithFuncAndParameter : GivenSyntaxTests
        {
            [Fact]
            public void GivenNullFunc_ThrowsExpectedException()
            {
                Func<string, int, int> func = null;

                // ReSharper disable once ExpressionIsAlwaysNull
                var exception =
                    Assert.Throws<ArgumentNullException>(
                        () => _given.And(func, 34));
                exception.ParamName.Should().Be("configure");
            }

            [Fact]
            public void GivenFunc_ReturnsInstance()
            {
                var instance = _given.And((name, value) => name + "Foo" + value, 13);
                instance.Should().NotBeNull();
            }

            [Fact]
            public void GivenFunc_CallsFunc()
            {
                var called = false;
                string Fn(string name, int value)
                {
                    called = true;
                    return name + "Foo" + value;
                }

                _given.And(Fn, 13);
                called.Should().BeTrue();
            }
        }

        public class AndAsyncWithFunc : GivenSyntaxTests
        {
            [Fact]
            public async Task GivenNullFunc_ThrowsExpectedException()
            {
                Func<string, Task<int>> func = null;

                // ReSharper disable once ExpressionIsAlwaysNull
                var exception =
                    await Assert.ThrowsAsync<ArgumentNullException>(
                            () => _given.AndAsync(func))
                        .ConfigureAwait(false);
                exception.ParamName.Should().Be("configure");
            }

            [Fact]
            public async Task GivenFunc_ReturnsInstance()
            {
                Task<string> Fn(string name) => Task.FromResult(name + "Foo");
                var instance = await _given.AndAsync(Fn)
                    .ConfigureAwait(false);
                instance.Should().NotBeNull();
            }

            [Fact]
            public async Task GivenFunc_CallsFunc()
            {
                var called = false;
                Task<string> Fn(string name)
                {
                    called = true;
                    return Task.FromResult(name + "Foo");
                }

                await _given.AndAsync(Fn)
                    .ConfigureAwait(false);
                called.Should().BeTrue();
            }
        }

        public class WhenWithFunc : GivenSyntaxTests
        {
            [Fact]
            public void GivenNull_ThrowsExpectedException()
            {
                Func<string, int> fn = null;

                // ReSharper disable once ExpressionIsAlwaysNull
                var exception =
                    Assert.Throws<ArgumentNullException>(
                            () => _given.When(fn));
                exception.ParamName.Should().Be("function");
            }

            [Fact]
            public void GivenFunction_ReturnsInstance()
            {
                var instance = _given.When(name => name.Length);
                instance.Should().NotBeNull();
            }

            [Fact]
            public void GivenFunction_CallsFunction()
            {
                var called = true;
                int Fn(string name)
                {
                    called = true;
                    return name.Length;
                }

                _given.When(Fn);
                called.Should().BeTrue();
            }
        }

        [SuppressMessage(
            "Class Design",
            "AV1000:Type name contains the word 'and', which suggests it has multiple purposes",
            Justification = "The name reflects the method signature being tested")]
        public class WhenWithFuncAndParameter : GivenSyntaxTests
        {
            [Fact]
            public void GivenNull_ThrowsExpectedException()
            {
                Func<string, int, int> fn = null;

                // ReSharper disable once ExpressionIsAlwaysNull
                var exception =
                    Assert.Throws<ArgumentNullException>(
                            () => _given.When(fn, 13));
                exception.ParamName.Should().Be("function");
            }

            [Fact]
            public void GivenFunction_ReturnsInstance()
            {
                var instance = _given.When((name, value) => name.Length * value, 13);
                instance.Should().NotBeNull();
            }

            [Fact]
            public void GivenFunction_CallsFunction()
            {
                var called = true;
                int Fn(string name, int value)
                {
                    called = true;
                    return name.Length * value;
                }

                _given.When(Fn, 13);
                called.Should().BeTrue();
            }
        }

        public class WhenAsyncWithFunc : GivenSyntaxTests
        {
            [Fact]
            public async Task GivenNull_ThrowsExpectedException()
            {
                Func<string, Task<int>> fn = null;

                // ReSharper disable once ExpressionIsAlwaysNull
                var exception =
                    await Assert.ThrowsAsync<ArgumentNullException>(
                            () => _given.WhenAsync(fn))
                        .ConfigureAwait(false);
                exception.ParamName.Should().Be("function");
            }

            [Fact]
            public async Task GivenFunction_ReturnsInstance()
            {
                async Task<int> Fn(string name)
                {
                    await Task.Yield();
                    return name.Length;
                }

                var instance = await _given.WhenAsync(Fn)
                    .ConfigureAwait(false);
                instance.Should().NotBeNull();
            }

            [Fact]
            public async Task GivenFunction_CallsFunction()
            {
                var called = true;
                async Task<int> Fn(string name)
                {
                    called = true;
                    await Task.Yield();
                    return name.Length;
                }

                await _given.WhenAsync(Fn)
                    .ConfigureAwait(false);
                called.Should().BeTrue();
            }
        }
    }
}
