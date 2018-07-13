using System;

namespace HowToStartMockingWithMoq.Tests
{
    //public class FakeRule : IValidatorRule
    //{
    //    public bool IsValid(object obj)
    //    {
    //        return false;
    //    }
    //}

    //public class FakeRule : IValidatorRule
    //{
    //    bool _whatToReturn;

    //    public FakeRule(bool whatToReturn)
    //    {
    //        _whatToReturn = whatToReturn;
    //    }


    //    public bool IsValid(object obj)
    //    {
    //        return _whatToReturn;
    //    }
    //}

    public class FakeRule : IValidatorRule
    {
        bool _whatToReturn;

        public event EventHandler<bool> IsValidating;

        public FakeRule(bool whatToReturn)
        {
            _whatToReturn = whatToReturn;
        }


        public bool IsValid(object obj)
        {
            return _whatToReturn;
        }
    }
}
