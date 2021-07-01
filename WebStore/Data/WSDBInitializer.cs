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

            // Секции        
            Logger.LogInformation("Инициализации таблицы секций");

            using (db.Database.BeginTransaction())
            {
                db.Sections.AddRange(TestData.Sections);

                db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Sections] ON");
                db.SaveChanges();
                db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Sections] OFF");

                db.Database.CommitTransaction();
            }

            Logger.LogInformation("Инициализации таблицы секций завершена");

            // Бренды        
            Logger.LogInformation("Инициализации таблицы брендов");

            using (db.Database.BeginTransaction())
            {
                db.Brands.AddRange(TestData.Brands);

                db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Brands] ON");
                db.SaveChanges();
                db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Brands] OFF");

                db.Database.CommitTransaction();
            }

            Logger.LogInformation("Инициализации таблицы брендов завершена");

            // Товары
            Logger.LogInformation("Инициализации таблицы товаров");

            using (db.Database.BeginTransaction())
            {
                db.Products.AddRange(TestData.Products);

                db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Products] ON");
                db.SaveChanges();
                db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Products] OFF");

                db.Database.CommitTransaction();
            }

            Logger.LogInformation("Инициализации таблицы товаров завершена");
        }
    }
}
