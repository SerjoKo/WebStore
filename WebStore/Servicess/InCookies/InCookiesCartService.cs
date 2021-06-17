using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Servicess.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Servicess.InCookies
{
    public class InCookiesCartService : ICartService
    {
        private readonly IHttpContextAccessor _HttpContextAccesor;
        private readonly IProductData _ProductData;
        private readonly string _CartName;

        public InCookiesCartService(IHttpContextAccessor httpContextAccesor, IProductData ProductData)
        {
            _HttpContextAccesor = httpContextAccesor;
            _ProductData = ProductData;

            var user = _HttpContextAccesor.HttpContext!.User;
            var user_name = user.Identity!.IsAuthenticated ? $"-{user.Identity.Name}" : null;

            _CartName = $"WS.Cart{user_name}";
        }

        public void Add(int Id)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void Decrement(int Id)
        {
            throw new NotImplementedException();
        }

        public CartViewModel GetCartViewModel()
        {
            throw new NotImplementedException();
        }

        public void Remove(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
