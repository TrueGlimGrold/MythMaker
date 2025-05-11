using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MythMaker.Models
{
	public class Product
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public int DisplayOrder { get; set; }
		[Required]
		public double Price { get; set; }
		[Required]
		public string Description { get; set; }
        [ValidateNever]
        public string ImagePath { get; set; }
        [DisplayName("Category Id")]
		[ValidateNever]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }
	}
}
