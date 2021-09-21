using System;
using System.Linq;

namespace Store.Memory
{
    public class BookRepository : IBookRepository
    {
        private readonly Book[] books = new Book[] {
            new Book(1, "ISBN 1234567000", "Jhon A", "C# 9 and .NET 5 – Modern Cross-Platform Development: Build intelligent apps, websites, and services with Blazor, ASP.NET Core, and Entity Framework Core using Visual Studio Code, 5th Edition"),
            new Book(2, "ISBN 1234567001", "Jhon B", "Hands-On Unity 2021 Game Development: Create, customize, and optimize your own professional games from scratch with Unity 2021, 2nd Edition 2nd ed. Edition"),
            new Book(3, "ISBN 1234567002", "Jhon C","Professional C# and .NET 2021st Edition")
        };
        public Book[] GetAllByAuthorOrTitle(string query)
        {
            return books.Where(book => book.Author.Contains(query) || 
                                       book.Title.Contains(query))
                        .ToArray();
        }

        public Book[] GetAllByIsbn(string isbn)
        {
            return books.Where(book => book.Isbn == isbn)
                        .ToArray();
        }
    }
}
