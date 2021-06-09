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

            Logger.LogInformation("Инициализации таблицы товаров");


            Logger.LogInformation("Инициализации таблицы товаров завершена");

        }
    }
}
