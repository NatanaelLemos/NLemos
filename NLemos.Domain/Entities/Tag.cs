using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NLemos.Domain.Entities
{
    public class Tag
    {
        public string HashName { get; set; }

        [Required(ErrorMessage = "Tag is required")]
        [MinLength(1, ErrorMessage = "Tag name should not be blank")]
        [MaxLength(20, ErrorMessage = "Tag name should contain less than 20 characters")]
        public string Name { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public Tag()
        {
            Posts = new List<Post>();
        }
    }
}