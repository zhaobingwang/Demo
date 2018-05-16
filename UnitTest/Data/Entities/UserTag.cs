using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    public class UserTag
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Tag { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
