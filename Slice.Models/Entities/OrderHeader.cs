namespace Slice.Models.Entities;
public class OrderHeader
{
    public int Id { get; set; }

    [Required]
    public DateTime OrderDate { get; set; }

    [Required]
    [Display(Name = "Order Total")]
    public double OrderTotal { get; set; }

    [Required]
    [Display(Name = "Pick Up Time")]
    public DateTime PickUpTime { get; set; }

    [Required]
    [NotMapped]
    public DateTime PickUpDate { get; set; }

    public string Status { get; set; }

    public string? Comments { get; set; }

    public string? SessionId { get; set; }
    public string? PaymentIntentId { get; set; }

    [Display(Name = "Pick Up Name")]
    public string PickUpName { get; set; }

    [Display(Name = "Phone Number")]
    [RegularExpression(@"^01[0-2]\d{1,8}$", ErrorMessage = "Please enter a valid Egyption phone number")]
    public string PhoneNumber { get; set; }

    [Required]
    public string AppUserId { get; set; }

    [ForeignKey(nameof(AppUserId))]
    public AppUser AppUser { get; set; }
}
