# Scenario Testing

This is an experiment in creating a Gherkin style syntax for scenario testing within the confines of the C# language.

I tried to create a similar library some years ago, but the boilerplate overhead required by C# made the library unusable. Recent improvements to the C# language (mostly changes in the C# 7 series), have inspired me to try again and I think the result is at least somewhat usable.

## Motivation

I'm a fan of the [Gherkin syntax](https://docs.cucumber.io/gherkin/reference/) for behaviour driven development, though I've not managed to successfully evangelise its adoption on any serious projects. (Btw, this is not a reflection on the quality of tools like [Cucumber](https://docs.cucumber.io/) or [SpecFlow](https://github.com/techtalk/SpecFlow), more that the teams on those projects weren't ready to adopt this level of test automation.)

This project is an experiment to see how close we can get to Gherkin syntax within the confines of legal C# code. Recent extensions to the C# language (in the 7.x releases) have made this much more achievable than it was when I tried a similar experiment several years ago.

## Example

Let's take a pair of very simple tests - the kind you might write if you're creating a basic four-function calculator.

``` text
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
```

With this library, you can fairly directly express these tests in pure C#:

``` csharp
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
```

## How it works

The `Given()` method is declared on the static class `GherkinFactory`. Methods on this class are brought into scope at the top of the file:

``` csharp
using static Niche.GherkinSyntax.GherkinFactory;
```

When called, `Given()` returns an `IGivenSyntax` implementation, defining the available syntax; each call returns another instance, allowing method chaining through to the end.

The **precondition** method `ANewCalculator()` is converted into a lambda expression by the C# compiler. The return value from this method is used as the **context** of the test - it carries the state information of the test.

``` csharp
private Calculator ANewCalculator() ...
```

The methods (`UserEnters()`, `UserAdds()` and `UserSubtracts()`) are **action** methods that modify the context of the test.

``` csharp
private Calculator UserEnters(Calculator calculator, double value) {...}
private Calculator UserAdds(Calculator calculator, double value) { ... }
private Calculator UserSubtracts(Calculator calculator, double value) { ... }
```

Each action method returns the new context for the next step.

Finally, the **expectation** method `AnswerIs()` is used to check that the answer is the one we want.

``` csharp
private static void AnswerIs(Calculator calculator, double value) ...
```
