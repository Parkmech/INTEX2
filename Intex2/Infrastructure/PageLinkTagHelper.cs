using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Intex2.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Intex2.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PageLinkTagHelper : TagHelper
    {
        
        private IUrlHelperFactory urlHelperFactory;

        //constructor
        public PageLinkTagHelper(IUrlHelperFactory hp)
        {
            urlHelperFactory = hp;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public PagingInfo PageModel { get; set; }
        public string PageAction { get; set; }
        public bool PageClassesEnabled { get; set; }
        public string PageClass { get; set; }
        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }

        //public Fil

        //Overriding 
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);

            TagBuilder result = new TagBuilder("div");


            //page 1 here
            TagBuilder firstTag = new TagBuilder("a");
            firstTag.Attributes["href"] = urlHelper.Action(PageAction, new { pageNum = 1 });

            if (PageClassesEnabled)
            {
                firstTag.AddCssClass(PageClass);
                //shorthand if statement to highlight the selected page
                firstTag.AddCssClass(1 == PageModel.CurrentPage ? PageClassSelected : PageClassNormal);
            }

            firstTag.InnerHtml.Append("First");

            result.InnerHtml.AppendHtml(firstTag);

            //next three pages
            //two if statements

            if(PageModel.CurrentPage == 1)
            {
                int curPage = PageModel.CurrentPage + 1;
                TagBuilder secondTag = new TagBuilder("a");
                secondTag.Attributes["href"] = urlHelper.Action(PageAction, new { pageNum = curPage});
                secondTag.Attributes["name"] = "pagingInfo";

                if (PageClassesEnabled)
                {
                    secondTag.AddCssClass(PageClass);
                    //shorthand if statement to highlight the selected page
                    secondTag.AddCssClass(PageClassNormal);
                }

                secondTag.InnerHtml.Append("2");

                result.InnerHtml.AppendHtml(secondTag);

                int thirdPage = PageModel.CurrentPage + 2;
                TagBuilder thirdTag = new TagBuilder("a");
                thirdTag.Attributes["href"] = urlHelper.Action(PageAction, new { pageNum = thirdPage });
                thirdTag.Attributes["name"] = "pagingInfo";

                if (PageClassesEnabled)
                {
                    thirdTag.AddCssClass(PageClass);
                    //shorthand if statement to highlight the selected page
                    thirdTag.AddCssClass(PageClassNormal);
                }

                thirdTag.InnerHtml.Append("3");

                result.InnerHtml.AppendHtml(thirdTag);
                result.InnerHtml.AppendHtml(".....");
            }

           else if(PageModel.CurrentPage == 2)
            {
                int curPage = PageModel.CurrentPage;
                TagBuilder secondTag = new TagBuilder("a");
                secondTag.Attributes["href"] = urlHelper.Action(PageAction, new { pageNum = curPage });
                secondTag.Attributes["name"] = "pagingInfo";

                if (PageClassesEnabled)
                {
                    secondTag.AddCssClass(PageClass);
                    //shorthand if statement to highlight the selected page
                    secondTag.AddCssClass(PageClassSelected);
                }

                secondTag.InnerHtml.Append("2");

                result.InnerHtml.AppendHtml(secondTag);

                int thirdPage = PageModel.CurrentPage + 1;
                TagBuilder thirdTag = new TagBuilder("a");
                thirdTag.Attributes["href"] = urlHelper.Action(PageAction, new { pageNum = thirdPage });

                if (PageClassesEnabled)
                {
                    thirdTag.AddCssClass(PageClass);
                    //shorthand if statement to highlight the selected page
                    thirdTag.AddCssClass(PageClassNormal);
                }

                thirdTag.InnerHtml.Append("3");

                result.InnerHtml.AppendHtml(thirdTag);

                result.InnerHtml.AppendHtml("......");

            }
            else if(PageModel.CurrentPage == PageModel.TotalPages - 1)
            {
                result.InnerHtml.AppendHtml(".......");

                int curPage = PageModel.CurrentPage - 1;
                TagBuilder secondTag = new TagBuilder("a");
                secondTag.Attributes["href"] = urlHelper.Action(PageAction, new { pageNum = curPage});
                secondTag.Attributes["name"] = "pagingInfo";

                if (PageClassesEnabled)
                {
                    secondTag.AddCssClass(PageClass);
                    //shorthand if statement to highlight the selected page
                    secondTag.AddCssClass(PageClassNormal);
                }

                secondTag.InnerHtml.Append(curPage.ToString());

                result.InnerHtml.AppendHtml(secondTag);

                int thirdPage = PageModel.CurrentPage;
                TagBuilder thirdTag = new TagBuilder("a");
                thirdTag.Attributes["href"] = urlHelper.Action(PageAction, new { pageNum = thirdPage });

                if (PageClassesEnabled)
                {
                    thirdTag.AddCssClass(PageClass);
                    //shorthand if statement to highlight the selected page
                    thirdTag.AddCssClass(PageClassSelected);
                }

                thirdTag.InnerHtml.Append(thirdPage.ToString());

                result.InnerHtml.AppendHtml(thirdTag);
            }
            else if(PageModel.CurrentPage == PageModel.TotalPages)
            {
                result.InnerHtml.AppendHtml(".......");
                int curPage = PageModel.CurrentPage -2;
                TagBuilder secondTag = new TagBuilder("a");
                secondTag.Attributes["href"] = urlHelper.Action(PageAction, new { pageNum = curPage});
                secondTag.Attributes["name"] = "pagingInfo";

                if (PageClassesEnabled)
                {
                    secondTag.AddCssClass(PageClass);
                    //shorthand if statement to highlight the selected page
                    secondTag.AddCssClass(PageClassNormal);
                }

                secondTag.InnerHtml.Append((curPage).ToString());

                result.InnerHtml.AppendHtml(secondTag);

                int thirdPage = PageModel.CurrentPage - 1;
                TagBuilder thirdTag = new TagBuilder("a");
                thirdTag.Attributes["href"] = urlHelper.Action(PageAction, new { pageNum = thirdPage });

                if (PageClassesEnabled)
                {
                    thirdTag.AddCssClass(PageClass);
                    //shorthand if statement to highlight the selected page
                    thirdTag.AddCssClass(PageClassNormal);
                }

                thirdTag.InnerHtml.Append(thirdPage.ToString());

                result.InnerHtml.AppendHtml(thirdTag);
            }
            else
            {
                result.InnerHtml.AppendHtml(".......");
                //Create a tags for three current pages
                for (int i = PageModel.CurrentPage - 1; i <= PageModel.CurrentPage + 1; i++)
                {
                    TagBuilder tag = new TagBuilder("a");
                    tag.Attributes["href"] = urlHelper.Action(PageAction, new { pageNum = i });

                    if (PageClassesEnabled)
                    {
                        tag.AddCssClass(PageClass);
                        //shorthand if statement to highlight the selected page
                        tag.AddCssClass(i == PageModel.CurrentPage ? PageClassSelected : PageClassNormal);
                    }

                    tag.InnerHtml.Append(i.ToString());

                    result.InnerHtml.AppendHtml(tag);

                }
                result.InnerHtml.AppendHtml("......");
            }

            int lastPage = PageModel.TotalPages;
            TagBuilder lastTag = new TagBuilder("a");
            lastTag.Attributes["href"] = urlHelper.Action(PageAction, new { pageNum = lastPage });

            if (PageClassesEnabled)
            {
                lastTag.AddCssClass(PageClass);
                //shorthand if statement to highlight the selected page
                lastTag.AddCssClass(PageModel.TotalPages == PageModel.CurrentPage ? PageClassSelected : PageClassNormal);
            }

            lastTag.InnerHtml.Append("Last (" + PageModel.TotalPages.ToString() + ")");

            result.InnerHtml.AppendHtml(lastTag);

            output.Content.AppendHtml(result.InnerHtml);
        }

    }
}
