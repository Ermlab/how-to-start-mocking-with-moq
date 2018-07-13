using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HowToStartMockingWithMoq
{
    //public class RequiredRule : IValidatorRule
    //{
    //    public bool IsValid(object obj)
    //    {
    //        if (obj is string s)
    //        {
    //            return !string.IsNullOrEmpty(s);
    //        }

    //        return obj != null;
    //    }
    //}

    public class RequiredRule : IValidatorRule
    {
        public event EventHandler<bool> IsValidating;

        public bool IsValid(object obj)
        {
            if (obj is string s)
            {
                return !string.IsNullOrEmpty(s);
            }

            return obj != null;
        }
    }
}
