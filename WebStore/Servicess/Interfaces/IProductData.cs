using System.Collections.Generic;
using WebStore.Domain.Entitys;

namespace WebStore.Servicess.Interfaces
{
    public interface IProductData
    {
        IEnumerable<Section> GetSections();

        IEnumerable<Brand> GetBrands();
    }
}
