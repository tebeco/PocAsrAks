using System;

namespace MyHub
{
    public class RandomDataGenerator
    {
        private Random rnd = new Random();
        private double _value;

        public RandomDataGenerator(double seed)
        {
            _value = seed;
        }

        public double Next() =>
            _value = _value + (rnd.NextDouble() - 0.5);
    }
}