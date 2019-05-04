using FluentAssertions;
using Xunit;
using static Niche.GherkinSyntax.GherkinFactory;

namespace GherkinSyntaxDemos
{
    public class CalculatorTests
    {
        [Fact]
        public void CanDoBasicAddition()
            => Given(ANewCalculator)
                .When(UserEnters, 42.0)
                .And(UserAdds, 15.0)
                .Then(AnswerIs, 57.0);

        [Fact]
        public void CanDoBasicSubtraction()
            => Given(ANewCalculator)
                .When(UserEnters, 42.0)
                .And(UserSubtracts, 15.0)
                .Then(AnswerIs, 27.0);

        private static Calculator ANewCalculator() => new Calculator();

        private static Calculator UserEnters(Calculator calculator, double value)
        {
            calculator.Enter(value);
            return calculator;
        }

        private static Calculator UserAdds(Calculator calculator, double value)
        {
            calculator.Add(value);
            return calculator;
        }

        private static Calculator UserSubtracts(Calculator calculator, double value)
        {
            calculator.Subtract(value);
            return calculator;
        }

        private static void AnswerIs(Calculator calculator, double value)
            => calculator.Answer.Should().BeApproximately(value, 0.01);
    }
}
