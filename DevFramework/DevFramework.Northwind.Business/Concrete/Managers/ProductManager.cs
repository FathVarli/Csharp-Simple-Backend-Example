using DevFramework.Core.CrossCuttingConcerns.Validation.FluentValidation;
using DevFramework.Northwind.Business.Abstract;
using DevFramework.Northwind.Business.ValidationRules.FluentValidation;
using DevFramework.Northwind.DataAccess.Abstract;
using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevFramework.Core.Aspects.Postsharp;
using DevFramework.Core.Aspects.Postsharp.TransactionAspects;
using DevFramework.Core.Aspects.Postsharp.ValidationAspects;
using DevFramework.Core.CrossCuttingConcerns.Caching.Microsoft;
using DevFramework.Core.Aspects.Postsharp.CacheAspect;
using DevFramework.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using DevFramework.Core.Aspects.Postsharp.LogAspects;
using DevFramework.Core.Aspects.Postsharp.PerformanceAspects;
using System.Threading;
using DevFramework.Core.Aspects.Postsharp.AuthorizationAspects;
using AutoMapper;
using DevFramework.Core.Utilities.Mappings;

namespace DevFramework.Northwind.Business.Concrete.Managers
{
    [LogAspect(typeof(FileLogger))]
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;
        private IMapper _mapper;
        public ProductManager(IProductDal productDal,IMapper mapper)
        {
            _productDal = productDal;
            _mapper = mapper;
        }

        [FluentValidationAspect(typeof(ProductValidatior))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public Product Add(Product product)
        {
           // ValidatorTool.FluentValidate(new ProductValidatior(), product);
            return _productDal.Add(product);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [PerformanceCounterAspect(2)]
        [SecuredOperation(Roles="Admin,Editor,Student")]
        public List<Product> GetAll()
        {
            //Thread.Sleep(3000);
            //Manuel Mapping
            /* return _productDal.GetList().Select(p=>new Product
             {
                 CategoryId=p.CategoryId,
                 ProductId=p.ProductId,
                 ProductName=p.ProductName,
                 QuantityPerUnit=p.QuantityPerUnit,
                 UnitPrice=p.UnitPrice
             }).ToList();
             */

            // var products = AutoMapperHelper.MapToSameTypeList(_productDal.GetList());

            var products = _mapper.Map<List<Product>>(_productDal.GetList());

            return products;
        }

        public Product GetById(int id)
        {
            return _productDal.Get(p => p.ProductId == id);
        }

        [TransactionScopeAspect]
        [FluentValidationAspect(typeof(ProductValidatior))]
        public void TransactionalOperation(Product product1,Product product2)
        {
            _productDal.Add(product1);
            //BusinessCode
            _productDal.Update(product2);
        }

        [FluentValidationAspect(typeof(ProductValidatior))]
        public Product Update(Product product)
        {
            return _productDal.Update(product);
        }
    }
}
