using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entitys;
using WebStore.Servicess.Interfaces;

namespace WebStore.Servicess
{
    public class InMemoryProductData : IProductData
    {
        public IEnumerable<Brand> GetBrands()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Section> GetSections()
        {
            throw new NotImplementedException();
        }
    }
}
