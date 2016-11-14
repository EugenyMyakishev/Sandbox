using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreFirst.Models
{
    public class Author
    {
        public int Id { get; set; }
        [Display(Name="Возраст")]
        [Required(ErrorMessage = "Поле возраста обязательно")]
        [Range(15, 100, ErrorMessage = "Возраст должен быть от 15-100")]
        public byte Age { get; set; }
        [Display(Name="Имя")]
        [Required(ErrorMessage = "Имя обязательно для заполнения")]
        [MaxLength(100)]
        public string Name { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}
