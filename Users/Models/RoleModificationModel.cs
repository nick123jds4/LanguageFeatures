using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Users.Models
{
    /// <summary>
    /// Набор изменений роли
    /// </summary>
    public class RoleModificationModel
    {
        /// <summary>
        /// Идентификатор роли
        /// </summary>
        public string RoleId { get; set; }
        /// <summary>
        /// Название роли
        /// </summary>
        [Required]
        public string RoleName { get; set; }
        public string[] IdsToAdd { get; set; }
        public string[] IdsToDelete { get; set; }
    }
}
