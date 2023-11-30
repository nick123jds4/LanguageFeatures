using Microsoft.AspNetCore.Identity;

namespace Users.Models
{
    /// <summary>
    /// Класс пользователя
    /// </summary>
    public class AppUser : IdentityUser
    {
        public Cities City { get; set; }
        /// <summary>
        /// Уровень квалификации
        /// </summary>
        public QualificationLevels Qualifications { get; set; }
    }
}
