using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HowToStartMockingWithMoq.Tests
{
    [TestFixture]
    public class ValidatorTests
    {
        [Test]
        public void IsValidShouldReturnTrueIfThereAreNoRules()
        {
            var myField = -1;
            var v = new Validator();

            var actual = v.IsValid(nameof(myField), myField);

            Assert.That(actual, Is.True);
        }


        //[Test]
        //public void IsValidShouldReturnFalseIfAtLeastOneRuleReturnsFalse()
        //{
        //    var myField = -1;
        //    var v = new Validator();
        //    v.AddRule(nameof(myField), new PositiveNumberRule());

        //    var actual = v.IsValid(nameof(myField), myField);

        //    Assert.That(actual, Is.False);
        //}


        //[Test]
        //public void IsValidShouldReturnFalseIfAtLeastOneRuleReturnsFalse()
        //{
        //    var myField = -1;
        //    var v = new Validator();
        //    v.AddRule(nameof(myField), new FakeRule());

        //    var actual = v.IsValid(nameof(myField), myField);

        //    Assert.That(actual, Is.False);
        //}


        //[Test]
        //public void IsValidShouldReturnFalseIfAtLeastOneRuleReturnsFalse()
        //{
        //    var myField = -1;
        //    var v = new Validator();
        //    v.AddRule(nameof(myField), new FakeRule(false));

        //    var actual = v.IsValid(nameof(myField), myField);

        //    Assert.That(actual, Is.False);
        //}

        //[Test]
        //public void IsValidShouldReturnTrueIfAllRulesReturnTrue()
        //{
        //    var myField = -1;
        //    var v = new Validator();
        //    v.AddRule(nameof(myField), new FakeRule(true));

        //    var actual = v.IsValid(nameof(myField), myField);

        //    Assert.That(actual, Is.True);
        //}


        [Test]
        public void IsValidShouldReturnFalseIfAtLeastOneRuleReturnsFalse()
        {
            var myField = -1;
            var v = new Validator();
            var rule = new Mock<IValidatorRule>();
            rule.Setup(s => s.IsValid(myField)).Returns(false);
            v.AddRule(nameof(myField), rule.Object);

            var actual = v.IsValid(nameof(myField), myField);

            Assert.That(actual, Is.False);
        }

        [Test]
        public void IsValidShouldReturnTrueIfAllRulesReturnTrue()
        {
            var myField = -1;
            var v = new Validator();
            var rule = new Mock<IValidatorRule>();
            rule.Setup(s => s.IsValid(myField)).Returns(true);
            v.AddRule(nameof(myField), rule.Object);

            var actual = v.IsValid(nameof(myField), myField);

            Assert.That(actual, Is.True);
        }

        [Test]
        public void AddRuleShouldThrowExceptionIfFieldNameIsInvalid()
        {
            var v = new Validator();
            var rule = new Mock<IValidatorRule>();

            TestDelegate del = () => v.AddRule(null, rule.Object);

            Assert.Throws<ArgumentNullException>(del);
        }

        [Test]
        public void IsValidShouldCheckRuleOneTime()
        {
            var myField = -1;
            var v = new Validator();
            var rule = new Mock<IValidatorRule>();
            v.AddRule(nameof(myField), rule.Object);

            v.IsValid(nameof(myField), myField);

            rule.Verify(s => s.IsValid(It.IsAny<object>()), Times.Once);
        }

        [Test]
        public void IsValidShouldReturnUpdatedValueAfterChangeInFieldValue()
        {
            var myField = 1;
            var v = new Validator();
            var rule = new Mock<IValidatorRule>();
            rule.SetupSequence(s => s.IsValid(myField))
                .Returns(true)
                .Returns(false);
            v.AddRule(nameof(myField), rule.Object);

            var actual1 = v.IsValid(nameof(myField), myField);

            // Let's assume value in field changed here. 
            // We don't really have to do this, because we changed return value in mock on the second call.
            var actual2 = v.IsValid(nameof(myField), myField);

            Assert.That(actual1, Is.True);
            Assert.That(actual2, Is.False);
        }

        [Test]
        public void IsValidatingShouldBeSetWhenRuleRaisesIsValidatingEvent()
        {
            var myField = 1;
            var v = new Validator();
            var rule = new Mock<IValidatorRule>();
            rule.Setup(s => s.IsValid(myField)).Returns(true).Raises(e => e.IsValidating += null, rule.Object, true);
            v.AddRule(nameof(myField), rule.Object);

            v.IsValid(nameof(myField), myField);

            Assert.That(v.IsValidating, Is.True);
        }
    }
}
