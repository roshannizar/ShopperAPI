using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using AutoMapper;
using ShopperCart.Customer;
using ShopperCart.Product.Dto;


namespace ShopperCart.Product
{
    public class ProductService : ShopperCartAppServiceBase, IProductService
    {
        private readonly IRepository<Models.Product> repository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public ProductService(IRepository<Models.Product> repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task Create(ProductDto input)
        {
            try
            {
                if (input != null)
                {
                    var product = mapper.Map<Models.Product>(input);
                    await repository.InsertAsync(product);
                    await unitOfWork.SaveChangesAsync();
                }
                else
                {
                    throw new ProductNotFoundException();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProductDto GetProduct(int id)
        {
            try
            {
                var query = repository.Get(id);
                return mapper.Map<ProductDto>(query);
            }
            catch (ProductNotFoundException)
            {
                throw new ProductNotFoundException();
            }
        }

        public IEnumerable<ProductDto> GetProducts()
        {
            var product = repository.GetAll();
            var query = mapper.Map<IEnumerable<ProductDto>>(product);
            return query;
        }

        public async Task Update(int productId, int quantity)
        {
            try
            {
                var productBO = repository.Get(productId);

                if (productBO != null)
                {
                    productBO.Quantity = productBO.Quantity + quantity;
                }
                else
                {
                    throw new ProductNotFoundException();
                }

                var product = mapper.Map<Models.Product>(productBO);
                await repository.UpdateAsync(product);
                await unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
