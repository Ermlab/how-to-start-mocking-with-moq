using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HowToStartMockingWithMoq
{
    //public interface IValidatorRule
    //{
    //    bool IsValid(object obj);
    //}

    public interface IValidatorRule
    {
        bool IsValid(object obj);
        event EventHandler<bool> IsValidating;
    }
}
