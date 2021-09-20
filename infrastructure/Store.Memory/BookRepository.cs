using System;
using System.Linq;

namespace Store.Memory
{
    public class BookRepository : IBookRepository
    {
        private readonly Book[] books = new Book[] {
            new Book(1, "C# 9 and .NET 5 – Modern Cross-Platform Development: Build intelligent apps, websites, and services with Blazor, ASP.NET Core, and Entity Framework Core using Visual Studio Code, 5th Edition"),
            new Book(2, "Hands-On Unity 2021 Game Development: Create, customize, and optimize your own professional games from scratch with Unity 2021, 2nd Edition 2nd ed. Edition"),
            new Book(3, "Professional C# and .NET 2021st Edition")
        };
        public Book[] GetAllBookByTitle(string titlePart)
        {
            return books.Where(book => book.Title.Contains(titlePart))
                        .ToArray();
        }

        public Book GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
