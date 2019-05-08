# Scenario Testing

This is an experiment in creating a Gherkin style syntax for scenario testing within the confines of the C# language.

## Example

Let's take a pair of very simple tests - the kind you might write if you're creating a basic four-function calculator.

Scenario: Can do basic addition
Given: a new calculator
When: the user enters 42.0
And: the user adds 15.0
Then: the answer is 57.0

Scenario: Can do basic subtraction
Given: a new calculator
When: the user enters 42.0
And: the user subtracts 15.0
Then: the answer is 57.0

With this library, you can fairly directly express these tests in pure C#:

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

