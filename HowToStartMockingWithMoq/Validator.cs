using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HowToStartMockingWithMoq
{
    public class Validator
    {
        Dictionary<string, List<IValidatorRule>> _rules = new Dictionary<string, List<IValidatorRule>>();

        public bool IsValidating { get; set; }

        public void AddRule(string fieldName, IValidatorRule rule)
        {
            if (_rules.ContainsKey(fieldName))
            {
                _rules[fieldName].Add(rule);
            }
            else
            {
                _rules.Add(fieldName, new List<IValidatorRule> { rule });
            }

            rule.IsValidating += Rule_IsValidating;
        }

        private void Rule_IsValidating(object sender, bool isValidating)
        {
            IsValidating = isValidating;
        }

        public bool IsValid(string fieldName, object value)
        {
            if(_rules.ContainsKey(fieldName))
            {
                var rules = _rules[fieldName];

                foreach (var rule in rules)
                {
                    var isValid = rule.IsValid(value);

                    if(!isValid)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
