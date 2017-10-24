using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models.ViewModels
{
    public class FiltroViewModel
    {
        public string Filtro { get; set; }
        public List<SelectListItem> ComboboxFiltros = new List<SelectListItem>();
        public FiltroViewModel()
        {
            ComboboxFiltros.Add(new SelectListItem { Text = "Menor preço", Value = "menor_preco", Selected = true });
            ComboboxFiltros.Add(new SelectListItem { Text = "Maior preço", Value = "maior_preco" });
            ComboboxFiltros.Add(new SelectListItem { Text = "Novos", Value = "novos" });
            ComboboxFiltros.Add(new SelectListItem { Text = "Recomendados", Value = "recomendados" ,Disabled = true });
        }
    }
}
