using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNice.Web.Helpers
{
    public static class Routes
    {
        public const string BaseUrl = "http://localhost:4000/";
        //Room management
        public const string RoomUrl = "api/room";
        public const string ProductUrl = "api/products";
        public const string BookingServiceUrl = "api/orders";
        public const string EmployeeUrl = "api/employees";
        public const string WorkShiftUrl = "api/workshifts";
        public const string AddToCart = "api/ShoppingCarts/AddToCart";
        public const string RemoveFromCart = "api/ShoppingCarts/RemoveFromCart";
        public const string ShoppingCart = "api/ShoppingCarts";
        public const string EmptyToCart = "api/ShoppingCarts/EmptyCart";

    }
}
