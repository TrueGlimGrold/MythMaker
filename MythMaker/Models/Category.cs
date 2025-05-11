using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MythMaker.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Category Name")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Display Order")]
        public int DisplayOrder { get; set; }
        [DisplayName("Image Path")]
        [ValidateNever]
        public string ImagePath { get; set; }
        [ValidateNever]
        public ICollection<Product> Products { get; set; }
    }
}
