using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string NickName { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime ModifyTime { get; set; }
        public bool IsDelete { get; set; }
    }
}
