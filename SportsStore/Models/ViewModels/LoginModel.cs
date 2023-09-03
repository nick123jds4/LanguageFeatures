using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.ViewModels
{
    public class LoginModel
    {
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Свойство Password декорировано атрибутом UIHint, поэтому в случае
        //применения атрибута asp-for внутри элемента input представления Razoг, предназначенного
        //для входа, вспомогательная функция дескриптора установит атрибут
       //type в password; таким образом, вводимый пользователем текст не будет виден на экране.
        /// </summary>
        [Required]
        [UIHint("password")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; } = "/";
    }
}
