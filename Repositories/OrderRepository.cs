﻿using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class OrderRepository : IOrderRepository
    {
        ShopApiContext _shopContext;
        public OrderRepository(ShopApiContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<Order> getOrderById(int id)
        {
            Order order = await _shopContext.Orders.FirstOrDefaultAsync(e => e.OrderId == id);
            return order;


        }
        public async Task<Order> addOrder(Order order)
        {
            await _shopContext.Orders.AddAsync(order);
            await _shopContext.SaveChangesAsync(); //you need the new order id so res= AddAsync, order.Id= res.id....
            return (order);
        }
    }
}
