using System.Collections.Generic;
using System.Linq;
using WebStore.DAL.Context.WebStore.DAL.Context;
using WebStore.Domain;
using WebStore.Domain.Entitys;
using WebStore.Servicess.Interfaces;

namespace WebStore.Servicess.InSQL
{
    public class SqlProductData : IProductData
    {
        private readonly WebStoreDB _db;

        public SqlProductData(WebStoreDB db) => _db = db;

        public IEnumerable<Brand> GetBrands() => _db.Brands;
        
        public IEnumerable<Section> GetSections() => _db.Sections;
        
        public IEnumerable<Product> GetProducts(ProductFilter Filter = null)
        {
            IQueryable<Product> query = _db.Products;

            if (Filter?.SectionId is { } section_id)
                query = query.Where(product => product.SectionId == section_id);

            if (Filter?.BrandId is { } brand_id)
                query = query.Where(product => product.SectionId == brand_id);

            return query;
        }        
    }
}
