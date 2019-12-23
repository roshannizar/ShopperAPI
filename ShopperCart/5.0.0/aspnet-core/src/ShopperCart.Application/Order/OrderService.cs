using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopperCart.Order.Dto;
using ShopperCart.Product;

namespace ShopperCart.Order
{

    public class OrderService: ShopperCartAppServiceBase, IOrderService
    {
        private readonly IRepository<Models.Order> orderRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ProductService productService;
        private readonly IMapper mapper;

        public OrderService(IRepository<Models.Order> orderRepository, IRepository<Models.Product> productRepository
            , IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.unitOfWork = unitOfWork;
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

                //OrderDto orderDtoTemp = new OrderDto(orderDto.CustomerId, orderDto.Date, orderDto.OrderItems, orderDto.Status);
                var order = mapper.Map<Models.Order>(orderDto);
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

        public async Task UpdateOrder(OrderDto orderDto)
        {
            try
            {
                var orderTemp = orderRepository.GetAllIncluding().Include(i => i.OrderItems).First(o => o.Id == orderDto.Id);

                var order = mapper.Map<Models.Order>(orderDto);

                foreach (var item in order.OrderItems.ToList())
                {
                    if (item.Id != 0)
                    {
                        var orderItemQuantity = orderTemp.OrderItems.FirstOrDefault(o => o.Id == item.Id).Quantity;
                        var difference = orderItemQuantity - item.Quantity;
                        orderTemp.OrderItems.FirstOrDefault(o => o.Id == item.Id).Quantity = item.Quantity;

                        if (item.IsDeleted)
                        {
                            //Remove the orderline
                            var orderItem = orderTemp.OrderItems.FirstOrDefault(o => o.Id == item.Id);
                            orderTemp.OrderItems.Remove(orderItem);
                        }

                        //updates the difference quantity
                        await productService.Update(item.ProductId, difference);
                    }
                    else
                    {
                        await productService.Update(item.ProductId, -(item.Quantity));
                        orderTemp.OrderItems.Add(item);
                    }

                    //updates the order
                    await orderRepository.UpdateAsync(orderTemp);
                    await unitOfWork.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public OrderDto GetOrderById(int id)
        {
            try
            {
                var orders = orderRepository.GetAllIncluding().Include(i => i.OrderItems).ThenInclude(i => i.Products)
                            .Include(i => i.Customers).FirstOrDefault(o => o.Id == id);
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
                var orders = orderRepository.GetAll().Include(i => i.OrderItems).ThenInclude(i => i.Products)
                            .Include(i => i.Customers);
                if (orders != null)
                {
                    var query = mapper.Map<IEnumerable<OrderDto>>(orders.ToList());
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
