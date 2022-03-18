namespace Slice.Web.Extensions;
public static class ClaimsPrincipleExtensions
{
    public static string GetUserId(this ClaimsPrincipal user)
        => user.FindFirst(ClaimTypes.NameIdentifier).Value;
}