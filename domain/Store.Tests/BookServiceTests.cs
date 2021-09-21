using Moq;
using System.Diagnostics;
using Xunit;

namespace Store.Tests
{
    public class BookServiceTests
    {
        [Fact]
        public void GetAllByQuery_WithIsbn_CallsGetAllByIsbn()
        {
            var bookRepositoryStub = new Mock<IBookRepository>();
            bookRepositoryStub.Setup(x => x.GetAllByIsbn(It.IsAny<string>()))
                              .Returns(new[] { new Book(1, "", "", "") });
            bookRepositoryStub.Setup(x => x.GetAllByAuthorOrTitle(It.IsAny<string>()))
                              .Returns(new[] { new Book(2, "", "", "") });

            var bookService = new BookService(bookRepositoryStub.Object);
            var actual = bookService.GetAllByQuery("ISBN 1234567000");
            Assert.Collection(actual, book => Assert.Equal(1, book.Id));
        }

        [Fact]
        public void GetAllByQuery_WithInvlidIsbn_CallsGetAllByIsbn()
        {
            var bookRepositoryStub = new Mock<IBookRepository>();
            bookRepositoryStub.Setup(x => x.GetAllByIsbn(It.IsAny<string>()))
                              .Returns(new[] { new Book(1, "", "", "") });
            bookRepositoryStub.Setup(x => x.GetAllByAuthorOrTitle(It.IsAny<string>()))
                              .Returns(new[] { new Book(2, "", "", "") });

            var bookService = new BookService(bookRepositoryStub.Object);
            var actual = bookService.GetAllByQuery("123456700");
            Assert.Collection(actual, book => Assert.Equal(2, book.Id));
        }

        [Fact]
        public void GetAllByQuery_WithAuthorOrTitle_CallsGetAllByAuthorOrTitle()
        {
            var bookRepositoryStub = new Mock<IBookRepository>();
            bookRepositoryStub.Setup(x => x.GetAllByIsbn(It.IsAny<string>()))
                              .Returns(new[] { new Book(1, "", "", "") });
            bookRepositoryStub.Setup(x => x.GetAllByAuthorOrTitle(It.IsAny<string>()))
                              .Returns(new[] { new Book(2, "", "", "") });

            var bookService = new BookService(bookRepositoryStub.Object);
            var actual = bookService.GetAllByQuery("Jhon A");
            Assert.Collection(actual, book => Assert.Equal(2, book.Id));
        }
    }
}
