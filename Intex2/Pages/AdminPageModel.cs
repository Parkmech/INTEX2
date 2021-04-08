using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
namespace Intex2.Pages
{
    [Authorize(Roles = "Administrator")]
    public class AdminPageModel : PageModel
    {
    }
}