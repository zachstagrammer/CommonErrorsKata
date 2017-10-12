using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CommonErrorsKata.Shared;

namespace CommonErrors.Test
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
            for (int i = 0; i < 11; i++)
            {
                stack.Push(new TrueFalseAnswer(true));
            }

            //Assert
            var result = stack.Count == 10;
            Assert.IsTrue(result);
        }
    }
}
