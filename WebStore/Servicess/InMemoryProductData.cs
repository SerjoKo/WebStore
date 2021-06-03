using System.Collections.Generic;
using WebStore.Data;
using WebStore.Domain.Entitys;
using WebStore.Servicess.Interfaces;

namespace WebStore.Servicess
{
    public class InMemoryProductData : IProductData
    {
        public IEnumerable<Brand> GetBrands()
        {
            return TestData.Brands;
        }

        public IEnumerable<Section> GetSections()
        {
            return TestData.Sections;
        }
    }
}
