using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management_Bot_Console.Helpers
{
    static class MaybeMonadHelper
    {
        public static TResult With<TInput, TResult>(this TInput input, Func<TInput, TResult> evaluator) where TInput : class
        {
            if (input == null) return default;
            return evaluator(input);
        }
    }
}
