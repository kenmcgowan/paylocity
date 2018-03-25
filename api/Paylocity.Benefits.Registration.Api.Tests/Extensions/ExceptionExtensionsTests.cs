using System;
using Xunit;

namespace Paylocity.Benefits.Registration.Api.Extensions.Tests
{
    public class ExceptionExtensionsTests
    {
        private class TestException : Exception
        {
            public TestException(string message) :
                base(message)
            {
            }

            public TestException(string message, Exception innerException) :
                base(message, innerException)
            {
            }

            public override string ToString()
            {
                return Message;
            }
        }

        [Fact]
        public void ToStringRecursive_ExceptionIsNull_ReturnsString()
        {
            var result = ((Exception)null).ToStringRecursive();

            Assert.NotNull(result);
        }

        [Fact]
        public void ToStringRecursive_NonNestedException_ReturnsExceptionToString()
        {
            var message = "This is a test exception";
            var expectedString = $"{message}\r\n\r\n";

            var actualString = (new TestException(message)).ToStringRecursive();

            Assert.Equal(expectedString, actualString);
        }

        [Fact]
        public void ToStringRecursive_NestedException_ReturnsAllExceptionsToString()
        {
            var message1 = "First exception thrown";
            var message2 = "Second exception thrown";
            var message3 = "Third exception thrown";
            var expectedString = $"{message3}\r\n\r\n{message2}\r\n\r\n{message1}\r\n\r\n";

            var actualString = new TestException(message3, new TestException(message2, new TestException(message1))).ToStringRecursive();

            Assert.Equal(expectedString, actualString);
        }
    }
}
