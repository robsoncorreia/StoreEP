using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using StoreEP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Infrastructure
{
    [HtmlTargetElement("select", Attributes = "combobox-categorias-model")]
    public class ComboboxCategoriasTagHelper:TagHelper
    {
        private IUrlHelperFactory urlHelperFactory;

        public ComboboxCategoriasTagHelper(IUrlHelperFactory helperFactory)
        {
            urlHelperFactory = helperFactory;
        }
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public IEnumerable<string> ComboboxCategoriasModel { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (ComboboxCategoriasModel == null)
            {
                return;
            }
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            TagBuilder div = new TagBuilder("div");
            foreach (var item in ComboboxCategoriasModel)
            {
                TagBuilder option = new TagBuilder("option");
                option.InnerHtml.AppendHtml(item);
                div.InnerHtml.AppendHtml(option);
            }
            output.Content.AppendHtml(div.InnerHtml);
        }
    }
}
