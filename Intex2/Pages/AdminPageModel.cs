using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
namespace Intex2.Pages
{
    // Inherit this model to make it so only admins can access pages where this model is inherited. lit.

    [Authorize(Roles = "Admins")]
    public class AdminPageModel : PageModel
    {
    }
}