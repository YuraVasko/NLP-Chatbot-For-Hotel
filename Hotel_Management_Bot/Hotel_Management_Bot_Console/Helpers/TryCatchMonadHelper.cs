using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management_Bot_Console.Helpers
{
    static class TryCatchMonadHelper
    {
        public static TResult Try<TInput, TResult>(this TInput input, Func<TInput, TResult> func, Func<TInput, TResult> exeptionHandler)
        {
            try
            {
                return func(input);
            }
            catch (Exception e)
            {
                return exeptionHandler(input);
            }
        }

        public static void Try<TInput>(this TInput input, Action<TInput> func, Action<TInput> exeptionHandler)
        {
            try
            {
                func(input);
            }
            catch (Exception e)
            {
                exeptionHandler(input);
            }
        }
    }
}
