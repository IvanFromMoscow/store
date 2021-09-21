using System;
using Xunit;

namespace Store.Tests
{
    public class BookTests
    {
        [Fact]
        public void IsIsbn_WithNull_ReturnFalse()
        {
            bool actual = Book.IsIsbn(null);
            Assert.False(actual);
        }

        [Fact]
        public void IsIsbn_WithBlankString_ReturnFalse()
        {
            bool actual = Book.IsIsbn("    ");
            Assert.False(actual);
        }

        [Fact]
        public void IsIsbn_WithInvalidIsbn_ReturnFalse()
        {
            bool actual = Book.IsIsbn("ISBN 2345");
            Assert.False(actual);
        }

        [Fact]
        public void IsIsbn_WithCorrectIsbn_ReturnTrue()
        {
            bool actual = Book.IsIsbn("IsbN 123-456-700 0");
            Assert.True(actual);
        }

        [Fact]
        public void IsIsbn_WithCrashIsbn_ReturnFalse()
        {
            bool actual = Book.IsIsbn("xxx IsbN 123-456-700 0 yyyy");
            Assert.False(actual);
        }
    }
}
