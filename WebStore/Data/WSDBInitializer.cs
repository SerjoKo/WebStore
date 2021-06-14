using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context.WebStore.DAL.Context;
using WebStore.Domain.Entitys.Identity;

namespace WebStore.Data
{
    public class WSDBInitializer
    {
        private readonly WebStoreDB _db;
        private readonly UserManager<User> _UserManager;
        private readonly RoleManager<Role> _RoleManager;
        private readonly ILogger<WSDBInitializer> _Logger;

        public WSDBInitializer(
            WebStoreDB db, 
            UserManager<User> UserManager,
            RoleManager<Role> RoleManager,
            ILogger<WSDBInitializer> Logger)
        {
            _db = db;
            _UserManager = UserManager;
            _RoleManager = RoleManager;
            _Logger = Logger;
        }

        public void Initialize()
        {
            _Logger.LogInformation("Инициализация БД");

            if (_db.Database.GetAppliedMigrations().Any())
            {
                _Logger.LogInformation("Миграция БД");

                _db.Database.Migrate();

                _Logger.LogInformation("Завершение миграции БД");
            }
            else
                _Logger.LogInformation("Миграция не требуется");
            
            #region Инициализация товаров
            try
            {
                InitializeProduct();
            }
            catch (Exception e)
            {
                _Logger.LogError(e, "Ошибка инициализации товаров");
            }
            #endregion


            try
            {
                InitializeIdentityAcync().GetAwaiter().GetResult();
            }
            catch (Exception e)
            {
                _Logger.LogError(e, "Ошибка при инициализации данных в БД ситемы identity");
            }

            _Logger.LogInformation("Завершение инициализации");
        }

        private void InitializeProduct()
        {
            if (_db.Products.Any())
            {
                _Logger.LogInformation("Инициализации таблицы товаров не требуется");
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

            using (_db.Database.BeginTransaction())
            {
                _db.Sections.AddRange(TestData.Sections);
                _db.Brands.AddRange(TestData.Brands);

                _db.Products.AddRange(TestData.Products);

                //db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Products] ON");
                _db.SaveChanges();
                //db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Products] OFF");

                _db.Database.CommitTransaction();
            }

            _Logger.LogInformation("Инициализации таблицы товаров завершена");
        }

        private async Task InitializeIdentityAcync()
        {
            _Logger.LogInformation("Инициализация БД системы identuty");
            
            var time = Stopwatch.StartNew();

            async Task CheckRole(string RoleName)
            {
                if (!await _RoleManager.RoleExistsAsync(RoleName))
                {
                    _Logger.LogInformation("Роль {0} отсутсвует. Создаю...", RoleName);
                    await _RoleManager.CreateAsync(new Role { Name = Role.Administrators });
                    _Logger.LogInformation("Роль {0} создана", RoleName);
                }
            }

            await CheckRole(Role.Administrators);
            await CheckRole(Role.Users);

            if (await _UserManager.FindByNameAsync(User.Administrator) is null)
            {
                _Logger.LogInformation("Пользователь {0} отсутствует. Создаю...", User.Administrator);

                var admin = new User
                {
                    UserName = User.Administrator
                };

                var creat_result = await _UserManager.CreateAsync(admin, User.AdmPass);
                
                if(creat_result.Succeeded)
                {
                    _Logger.LogInformation("Пользователь {0} сщздан.", User.Administrator);

                    await _UserManager.AddToRoleAsync(admin, Role.Administrators);

                    _Logger.LogInformation("Пользователь {0} наделён ролью {1}", User.Administrator, Role.Administrators);
                }
                else
                {
                    var errors = creat_result.Errors.Select(e => e.Description).ToArray();
                    _Logger.LogError("Учётная запись администратора не создана по причине: {0}",
                        string.Join(",", errors));

                    throw new InvalidOperationException($"Ошибка при создании пользователя " +
                        $"{User.Administrator}:{string.Join(",", errors)}");
                }
            }

                //if (!await _RoleManager.RoleExistsAsync(Role.Administrators))
                //    await _RoleManager.CreateAsync(new Role { Name = Role.Administrators });

                _Logger.LogInformation("Инициализация БД системы identuty завершена {0} c", time.Elapsed.TotalSeconds);
        }
    }
}