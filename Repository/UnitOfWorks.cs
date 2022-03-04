using ShopApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.WebUI.Repository
{
    public class UnitOfWorks : IUnitOfWork
    {
        private IProductDal _productDal;
        private ICategoryDal _categoryDal;
        private ShopDbContext _dbContext;

        public UnitOfWorks(ShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ICategoryDal CategoryDal
        {
            get
            {
                return _categoryDal ?? (_categoryDal = new CategoryDal());
            }
        }
        public IProductDal ProductDal
        {
            get
            {
                return _productDal ?? (_productDal = new ProductDal());
            }

        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public int Save()
        {
           return _dbContext.SaveChanges();
        }
    }
}
