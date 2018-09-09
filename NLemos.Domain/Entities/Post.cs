using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NLemos.Domain.Entities
{
    public class Post
    {
        public string HashName { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [MinLength(1, ErrorMessage = "Title should not be blank")]
        [MaxLength(100, ErrorMessage = "Title should contain less than 100 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Summary is required")]
        [MinLength(1, ErrorMessage = "Summary should not be blank")]
        [MaxLength(1000, ErrorMessage = "Summary should contain less than 1000 characters")]
        public string Summary { get; set; }

        [Required(ErrorMessage = "Full post is required")]
        [MinLength(1, ErrorMessage = "Full post should not be blank")]
        [MaxLength(1048576, ErrorMessage = "Full post should contain less than 1048576 characters")]
        public string FullPost { get; set; }

        public DateTime PostDate { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public Post()
        {
            Tags = new List<Tag>();
            PostDate = DateTime.Now;
        }
    }
}