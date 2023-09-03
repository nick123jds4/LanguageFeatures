using SportsStore.Models.Data;

namespace SportsStore.Models.ViewModels
{
    /// <summary>
    /// Представлению, которое будет отображать содержимое корзины, 
    /// необходимо передать две порции информации: объект Cart и URL для отображения в случае, 
    /// если пользователь щелкнет на кнопке Continue shopping(Продолжить покупку).
    /// </summary>
    public class CartIndexViewModel
    {
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }
    }
}
