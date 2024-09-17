using System.ComponentModel.DataAnnotations;

namespace AsiaYo_Test.Models
{
    public class OrderReq
    {
        [Required(ErrorMessage = "ID is required")]
        public string Id { get; set; } = string.Empty;
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Address is required")]
        public Address Address { get; set; } = new Address();
        [Required(ErrorMessage = "Price is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Price must be a valid number")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Currency is required")]
        public string Currency { get; set; } = string.Empty;
    }

    public class Address
    {
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; } = string.Empty;
        [Required(ErrorMessage = "District is required")]
        public string District { get; set; } = string.Empty;
        [Required(ErrorMessage = "Street is required")]
        public string Street { get; set; } = string.Empty;
    }
}
