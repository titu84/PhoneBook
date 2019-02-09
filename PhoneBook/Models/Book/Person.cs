using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhoneBook.Models.Book
{
    public class Person
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(80)]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        [MaxLength(80)]
        public string EMail { get; set; }
               
        public long Phone { get; set; }
    }
}