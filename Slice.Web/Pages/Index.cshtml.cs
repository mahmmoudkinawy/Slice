namespace Slice.Web.Pages;
public class IndexModel : PageModel
{
    public RedirectToPageResult OnGet()
    {
        //I know it's a bad idea to redirect and I'm trying fix it
        return RedirectToPage("/Customer/Home/Index");
    }
}