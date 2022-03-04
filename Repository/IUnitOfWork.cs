using ShopApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.WebUI.Repository
{
    public interface IUnitOfWork :IDisposable
    {
        IProductDal ProductDal { get; }
        ICategoryDal CategoryDal { get; }
        int Save();
    }
}
