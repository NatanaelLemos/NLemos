using System;
using System.Collections.Generic;
using System.Text;

namespace NLemos.Domain.Entities
{
    public class Social
    {
        public string Url { get; set; }
        public SocialType Type { get; set; }
    }

    public enum SocialType
    {
        Facebook,
        GitHub,
        LinkedIn,
        Instagram,
        Email
    }
}
