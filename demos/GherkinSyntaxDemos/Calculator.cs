using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Text;

namespace GherkinSyntaxDemos
{
    public class Calculator
    {
        public double Answer { get; private set; }

        public double Memory { get; private set; }

        public void Clear() => Answer = 0;

        public void Enter(double value) => Answer = value;

        public void Add(double value) => Answer += value;

        public void Subtract(double value) => Answer -= value;

        public void Multiply(double value) => Answer *= value;

        public void Divide(double value) => Answer /= value;

        public void MemoryPlus() => Memory += Answer;

        public void MemoryClear() => Memory = 0;
    }
}
