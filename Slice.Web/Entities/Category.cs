namespace Slice.Web.Entities;
public class Category
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Display(Name = "Display Order")]
    [Range(1, 100, ErrorMessage = "Display Order must be in range 1-100")]
    public int DisplayOrder { get; set; }
}