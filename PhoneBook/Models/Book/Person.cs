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
        [Display(Name = "First Name")]
        public string Name { get; set; }
        [Required]
        [MaxLength(80)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        [MaxLength(80)]
        [Display(Name = "E-mail")]
        public string EMail { get; set; }
        [Display(Name = "Phone number")]
        public long Phone { get; set; }
    }
}