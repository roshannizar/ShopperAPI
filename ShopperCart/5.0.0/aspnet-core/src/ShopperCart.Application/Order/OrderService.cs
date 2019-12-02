﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopperCart.Order.Dto;
using ShopperCart.Product;

namespace ShopperCart.Order
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Models.Order> orderRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<Models.OrderLine> orderItemRepository;
        private readonly ProductService productService;
        private readonly IMapper mapper;

        public OrderService(IRepository<Models.Order> orderRepository, IRepository<Models.Product> productRepository
            , IUnitOfWork unitOfWork, IRepository<Models.OrderLine> orderItemRepository, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.unitOfWork = unitOfWork;
            this.orderItemRepository = orderItemRepository;
            this.mapper = mapper;
            productService = new ProductService(productRepository, mapper, unitOfWork);
        }

        public async Task CreateOrder(OrderDto orderDto)
        {
            try
            {
                foreach (var item in orderDto.OrderItems)
                {
                    //Updating the product
                    await productService.Update(item.ProductId, -(item.Quantity));
                }

                OrderDto orderDtoTemp = new OrderDto(orderDto.CustomerId, orderDto.Date, orderDto.OrderItems, orderDto.Status);
                var order = mapper.Map<Models.Order>(orderDtoTemp);
                //This method will add orderlines as well, since this entity has the orderline list
                await orderRepository.InsertAsync(order);
                await unitOfWork.SaveChangesAsync();
            }
            catch (OrderNotFoundException ex)
            {
                throw ex;
            }
        }

        public async Task DeleteOrder(int id)
        {
            try
            {
                var orderDto = await orderRepository.GetAsync(id);

                if (orderDto == null)
                    throw new OrderNotFoundException();

                var order = mapper.Map<Models.Order>(orderDto);
                //Checks the status type
                if (order.Status == Models.StatusType.Completed)
                {
                    //product quantity will not be update if the status is confirmed
                    await orderRepository.DeleteAsync(order);
                }
                else
                {
                    //Retrieving the orderLine from the database, so that can get the quantity
                    var orderBOTemp = GetOrderById(id);

                    foreach (var temp in orderBOTemp.OrderItems)
                    {
                        //updates the quantity
                        await productService.Update(temp.ProductId, temp.Quantity);
                    }

                    await orderRepository.DeleteAsync(order);
                }
                //Records will be saved after checking the status type
                await unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateOrder(List<OrderLineDto> orderLineBOs)
        {
            try
            {

                foreach (var item in orderLineBOs)
                {
                    //Retrieving the orderline as temporary to check the database quantity
                    var tempOrderLine = orderItemRepository.Get(item.Id);

                    //Identifying the difference between the updated orderline and database quantity
                    var tempDifference = tempOrderLine.Quantity - item.Quantity;

                    //setting the quantity
                    tempOrderLine.Quantity = item.Quantity;

                    if (item.Quantity == 0)
                    {
                        //If the quantity is zero the order item is deleted
                        await DeleteOrderLine(tempOrderLine);
                    }
                    else
                    {
                        //updates the orderline
                        var order = mapper.Map<Models.OrderLine>(tempOrderLine);
                        await orderItemRepository.UpdateAsync(order);
                        await unitOfWork.SaveChangesAsync();
                    }

                    //updates the difference quantity
                    await productService.Update(item.ProductId, tempDifference);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task DeleteOrderLine(Models.OrderLine orderLine)
        {
            if (orderLine == null)
                throw new OrderLineNotFoundException();
            await orderItemRepository.DeleteAsync(orderLine);
            await unitOfWork.SaveChangesAsync();
        }

        public OrderDto GetOrderById(int id)
        {
            try
            {
                var orders = orderRepository.GetAllIncluding().Include(i => i.OrderItems).ThenInclude(i => i.Products).Include(i => i.Customers).FirstOrDefault(o => o.Id == id);
                var query = mapper.Map<OrderDto>(orders);
                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<OrderDto> GetOrders()
        {
            try
            {
                var orders = orderRepository.GetAllIncluding(c => c.Customers, o => o.OrderItems).ToList();

                if (orders != null)
                {
                    var query = mapper.Map<IEnumerable<OrderDto>>(orders);
                    return query;
                }
                else
                {
                    throw new OrderNotFoundException();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}