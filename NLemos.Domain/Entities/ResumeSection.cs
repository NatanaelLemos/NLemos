using System;
using System.Collections.Generic;
using System.Text;

namespace NLemos.Domain.Entities
{
    public class ResumeSection
    {
        public string SectionName { get; set; }

        public List<ResumeItem> Items { get; set; }

        public ResumeSection()
        {
            Items = new List<ResumeItem>();
        }
    }
}
