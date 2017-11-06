using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models.ViewModels
{
    public class OrdemViewModel
    {
        public Dictionary<int, string> SelectItens = new Dictionary<int, string>();
        public OrdemViewModel()
        {
            SelectItens.Add(0, "Menor preço");
            SelectItens.Add(1, "Maior preço");
            SelectItens.Add(2, "Novos");
        }
    }
}
