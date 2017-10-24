using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using StoreEP.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models
{
    public class SessionCart : Carrinho
    {
        public static Carrinho GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            SessionCart Carrinho = session?.GetJson<SessionCart>("Carrinho") ?? new SessionCart();
            Carrinho.Session = session;
            return Carrinho;
        }
        [JsonIgnore]
        public ISession Session { get; set; }
        public override void AddItem(Produto produto, int quantidade)
        {
            base.AddItem(produto, quantidade);
            Session.SetJson("Carrinho", this);
        }
        public override void RemoveLine(Produto produto)
        {
            base.RemoveLine(produto);
            Session.SetJson("Carrinho", this);
        }
        public override void Limpar()
        {
            base.Limpar();
            Session.Remove("Carrinho");
        }
    }
}
