using System;

namespace HowToStartMockingWithMoq
{
    //public class PositiveNumberRule : IValidatorRule
    //{
    //    public bool IsValid(object obj)
    //    {
    //        if(long.TryParse(obj?.ToString(), out var l))
    //        {
    //            return l >= 0;
    //        }

    //        if (double.TryParse(obj?.ToString(), out var d))
    //        {
    //            return d >= 0;
    //        }

    //        return false;
    //    }
    //}

    public class PositiveNumberRule : IValidatorRule
    {
        public event EventHandler<bool> IsValidating;

        public bool IsValid(object obj)
        {
            if (long.TryParse(obj?.ToString(), out var l))
            {
                return l >= 0;
            }

            if (double.TryParse(obj?.ToString(), out var d))
            {
                return d >= 0;
            }

            return false;
        }
    }
}
