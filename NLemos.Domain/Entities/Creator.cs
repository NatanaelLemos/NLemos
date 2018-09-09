using System;
using System.Collections.Generic;
using System.Text;

namespace NLemos.Domain.Entities
{
    public class Creator
    {
        public string Name { get; set; }
        public string Key { get; set; }
        public string JobTitle { get; set; }
        public string Summary { get; set; }
        public string Image { get; set; }

        public ICollection<Social> Social { get; set; }

        public Creator()
        {
            Social = new List<Social>();
            Image = "launcher-icon-2x.png";
            JobTitle = "Creator of the blog";
            Name = "Creator";
            Summary = "This is the person who created this blog";
            Key = "12345";
        }
    }
}
