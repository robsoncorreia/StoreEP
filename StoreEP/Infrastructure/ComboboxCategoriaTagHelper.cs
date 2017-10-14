using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using StoreEP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Infrastructure
{
    [HtmlTargetElement(tag: "div", Attributes = "combobox-model")]
    public class ComboboxCategoriaTagHelper : TagHelper
    {
        private IUrlHelperFactory _urlHelperFactory;
        private readonly IProdutoRepositorio _produtoRepositorio;
        public ViewContext ViewContext { get; set; }
        private IEnumerable<Produto> ComboboxModel { get; set; } 
        public ComboboxCategoriaTagHelper(IUrlHelperFactory urlHelperFactory, IProdutoRepositorio produtoRepositorio)
        {
            _urlHelperFactory = urlHelperFactory;
            _produtoRepositorio = produtoRepositorio;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);
            TagBuilder combobox = new TagBuilder("select");
            foreach (var value in ComboboxModel.Select(c => c.Categoria))
            {
                TagBuilder option = new TagBuilder("option");
                option.Attributes["value"] = value;
                option.InnerHtml.AppendHtml(value);
                combobox.InnerHtml.AppendHtml(option);
            }
            output.Content.AppendHtml(combobox);
        }
    }
}
