using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Store.Web.Models;

namespace Store.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IBookRepository bookRepository;
        private readonly IOrderRepository orderRepository;

        public OrderController(IBookRepository bookRepository, IOrderRepository orderRepository)
        {
            this.bookRepository = bookRepository;
            this.orderRepository = orderRepository;
        }

        private OrderModel Map(Order order)
        {
            var bookIds = order.Items.Select(item => item.BookId);
            var books = bookRepository.GetAllByIds(bookIds);
            var itemsModel = from item in order.Items
                             join book in books on item.BookId equals book.Id
                             select new OrderItemModel
                             {
                                 BookId = book.Id,
                                 Title = book.Title,
                                 Author = book.Author,
                                 Price = item.Price,
                                 Count = item.Count

                             };
            return new OrderModel
            {
                Id = order.Id,
                Items = itemsModel.ToArray(),
                TotalCount = order.TotalCount,
                TotalPrice = order.TotalPrice
            };
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.TryGetCart(out Cart cart))
            {
                var order = orderRepository.GetById(cart.OrderId);
                OrderModel model = Map(order);

                return View(model);
            }
            return View("Empty");
        }

        public IActionResult AddItem(int bookId, int count)
        {
            (Order order, Cart cart) = GetOrCreateOrderAndCart();
            var book = bookRepository.GetById(bookId);
            order.AddOrUpdateItem(book, count);
            SaveOrderAndCart(order, cart);
            return RedirectToAction("Index", "Book", new { bookId });
        }

        public IActionResult RemoveItem(int id)
        {
            (Order order, Cart cart) = GetOrCreateOrderAndCart();
            order.RemoveItem(id);
            SaveOrderAndCart(order, cart);
            return RedirectToAction("Index", "Book", new { id });
        }

        private void SaveOrderAndCart(Order order, Cart cart)
        {
            orderRepository.Update(order);

            cart.TotalCount = order.TotalCount;
            cart.TotalPrice = order.TotalPrice;

            HttpContext.Session.Set(cart);
        }

        [HttpPost]
        public IActionResult UpdateItem(int bookId, int count)
        {
            (Order order, Cart cart) = GetOrCreateOrderAndCart();
            order.GetItem(bookId).Count = count;
            SaveOrderAndCart(order, cart);
            return RedirectToAction("Index", "Book", new { bookId });
        }

        IActionResult RemoveBook(int id)
        {
            (Order order, Cart cart) = GetOrCreateOrderAndCart();
            order.GetItem(id).Count--;
            SaveOrderAndCart(order, cart);
            return RedirectToAction("Index", "Book", new { id });
        }
        private (Order order, Cart cart) GetOrCreateOrderAndCart()
        {
            Order order;
            if (!HttpContext.Session.TryGetCart(out Cart cart))
            {
                order = orderRepository.Create();
                cart = new Cart(order.Id);
            }
            else
            {
                order = orderRepository.GetById(cart.OrderId);
            }
            return (order, cart);
        }
    }
} 