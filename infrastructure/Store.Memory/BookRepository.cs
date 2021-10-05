using System;
using System.Collections.Generic;
using System.Linq;

namespace Store.Memory
{
    public class BookRepository : IBookRepository
    {
        private readonly Book[] books = new Book[] {
            new Book(1, "ISBN 1234567000", "Jhon A"
                      , "C# 9 and .NET 5 – Modern Cross-Platform Development: Build intelligent apps, websites, and services with Blazor, ASP.NET Core, and Entity Framework Core using Visual Studio Code, 5th Edition"
                      , "Dive into C# and create apps, user interfaces, games, and more using this fun and highly visual introduction to C#, .NET Core, and Visual Studio. With this completely updated guide, which covers C# 8.0 and Visual Studio 2019, beginning programmers like you will build a fully functional game in the opening chapter. Then you'll learn how to use classes and object-oriented programming, create 3D games in Unity, and query data with LINQ. And you'll do it all by solving puzzles, doing hands-on exercises, and building real-world applications. By the time you're done, you'll be a solid C# programmer--and you'll have a great time along the way!"
                      , 120.0m),
            new Book(2, "ISBN 1234567001", "Jhon B"
                      , "Hands-On Unity 2021 Game Development: Create, customize, and optimize your own professional games from scratch with Unity 2021, 2nd Edition 2nd ed. Edition"
                      , "Книга C# ДЛЯ ПРОФЕССИОНАЛОВ. ТОНКОСТИ ПРОГРАММИРОВАНИЯ (C# in Depth) является обновлением предыдущего издания, ставшего бестселлером, с целью раскрытия новых средств языка C# 5, включая решение проблем, которые связаны с написанием сопровождаемого асинхронного кода. Она предлагает уникальные сведения о сложных областях и темных закоулках языка, которые может предоставить только эксперт Джон Скит."
                      , 100.0m),
            new Book(3, "ISBN 1234567002", "Jhon C"
                      , "Professional C# and .NET 2021st Edition"
                      , "В новом издании бестселлера читателю предлагаются все необходимые ответы на разнообразные вопросы по языку C# 9.0 и .NET 5. C# - это язык с замечательной гибкостью и широким размахом, но такое его непрекращающееся развитие означает, что по-прежнему есть многие вещи, которые предстоит изучить. В соответствии с традициями справочников O'Reilly это основательно обновленное издание будет наилучшим однотомным источником сведений o языке C#, из доступных на сегодняшний день.Организованное вокруг концепций и сценариев исполь- зования, новое издание книги снабдит программистов средней и высокой квалификации лаконичным планом получения глубоких знаний по C# и .NET."
                      , 50.34m)
        };
        public Book[] GetAllByAuthorOrTitle(string query)
        {
            return books.Where(book => book.Author.Contains(query) || 
                                       book.Title.Contains(query))
                        .ToArray();
        }

        public Book[] GetAllByIds(IEnumerable<int> bookIds)
        {
            var foundBooks = from book in books
                            join bookId in bookIds on book.Id equals bookId
                            select book;
            return foundBooks.ToArray();
        }

        public Book[] GetAllByIsbn(string isbn)
        {
            return books.Where(book => book.Isbn == isbn)
                        .ToArray();
        }

        public Book GetById(int id)
        {
            return books.Single(book => book.Id == id);
        }
    }
}
