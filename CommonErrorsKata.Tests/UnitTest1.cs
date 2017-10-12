using System;
using CommonErrorsKata.Shared;
using NUnit.Framework;

namespace CommonErrorsKata.Tests
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void ShouldOnlyAllowTenAnswers()
        {

            //Arrange
            var size = 10;
            var stack = new AnswerStack<TrueFalseAnswer>(size);

            //Act
            for (int i = 0; i < ++size; i++)
                stack.Push(new TrueFalseAnswer(true));

            //Assert
            Assert.IsTrue(stack.Count < ++size);
        }
    }
}
