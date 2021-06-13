using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context.WebStore.DAL.Context;

namespace WebStore.Data
{
    public class WSDBInitializer
    {
        private readonly WebStoreDB db;
        private readonly ILogger<WSDBInitializer> Logger;

        public WSDBInitializer(WebStoreDB db, ILogger<WSDBInitializer> Logger)
        {
            this.db = db;
            this.Logger = Logger;
        }

        public void Initialize()
        {
            Logger.LogInformation("Инициализация БД");
            
            if (db.Database.GetAppliedMigrations().Any())
            {
                Logger.LogInformation("Миграция БД");

                db.Database.Migrate();

                Logger.LogInformation("Завершение миграции БД");
            }
            else
                Logger.LogInformation("Миграция не требуется");

            try
            {
                InitializeProduct();
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Ошибка инициализации товаров");
            }

            Logger.LogInformation("Завершение инициализации");
        }

        private void InitializeProduct()
        {
            if (db.Products.Any())
            {
                Logger.LogInformation("Инициализации таблицы товаров не требуется");
            }

            #region Грохнул
            // Секции        
            //Logger.LogInformation("Инициализации таблицы секций");

            //using (db.Database.BeginTransaction())
            //{
            //    db.Sections.AddRange(TestData.Sections);

            //    db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Sections] ON");
            //    db.SaveChanges();
            //    db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Sections] OFF");

            //    db.Database.CommitTransaction();
            //}

            //Logger.LogInformation("Инициализации таблицы секций завершена");

            //// Бренды        
            //Logger.LogInformation("Инициализации таблицы брендов");

            //using (db.Database.BeginTransaction())
            //{
            //    db.Brands.AddRange(TestData.Brands);

            //    db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Brands] ON");
            //    db.SaveChanges();
            //    db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Brands] OFF");

            //    db.Database.CommitTransaction();
            //}

            //Logger.LogInformation("Инициализации таблицы брендов завершена");

            //// Товары
            //Logger.LogInformation("Инициализации таблицы товаров");
            #endregion

            var section_pool = TestData.Sections.ToDictionary(section => section.Id);
            var brand_pool = TestData.Brands.ToDictionary(brand => brand.Id);

            foreach (var section in TestData.Sections.Where(s => s.ParentId != null))
            {
                section.Parent = section_pool[(int)section.ParentId!];
            }

            foreach (var product in TestData.Products)
            {
                product.Section = section_pool[product.SectionId];
                if (product.BrandId is { } brand_id)
                {
                    product.Brand = brand_pool[brand_id];
                }

                product.Id = 0;
                product.SectionId = 0;
                product.BrandId = null;
            }

            foreach (var section in TestData.Sections)
            {
                section.Id = 0;
                section.ParentId = null;
            }

            foreach (var brand in TestData.Brands)
            {
                brand.Id = 0;

            }

            using (db.Database.BeginTransaction())
            {
                db.Sections.AddRange(TestData.Sections);
                db.Brands.AddRange(TestData.Brands);

                db.Products.AddRange(TestData.Products);

                //db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Products] ON");
                db.SaveChanges();
                //db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Products] OFF");

                db.Database.CommitTransaction();
            }

            Logger.LogInformation("Инициализации таблицы товаров завершена");
        }
    }
}
