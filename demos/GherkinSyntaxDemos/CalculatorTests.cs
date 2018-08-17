using System;
using System.Threading.Tasks;
using FluentAssertions;
using Niche.GherkinSyntax;
using Xunit;
using static Niche.GherkinSyntax.GherkinFactory;

namespace GherkinSyntaxDemos
{
    public class CalculatorTests
    {
//        [Fact]
//        public void Foo()
//            => Given(ANewCalculator)
//                .When(EnterValue, 42)
//                .And(AddValue, 15)
//                .Then(ExpectedValueIs, 57);

        public Calculator ANewCalculator() => new Calculator();

        public void EnterValue(Calculator calculator, double value) => calculator.Enter(value);

        public void AddValue(Calculator calculator, double value) => calculator.Add(value);

        public void ExpectedValueIs(Calculator calculator, double value)
            => calculator.Answer.Should().BeApproximately(value, 0.01);
    }
}
