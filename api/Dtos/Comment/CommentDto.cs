using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Comment
{
    public class CommentDto
    {
        public int Id { get; set; }

        [Required]
        [MinLength(5, ErrorMessage ="Content must be 5 Characters ")]
        [MaxLength(280, ErrorMessage ="Content cannot be over 250 characters")]
        public string Title  { get; set; } = string.Empty;

        [Required]
        [MinLength(5, ErrorMessage ="Content must be 5 Characters ")]
        [MaxLength(280, ErrorMessage ="Content cannot be over 250 characters")]
        public string Content { get; set; } = string.Empty;

        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int? StockId { get; set; }
        
    }
}