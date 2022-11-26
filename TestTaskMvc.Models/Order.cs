using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestTaskMvc.Models
{
    [Table("Order")]
    public class Order
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите номер заказа")]
        public string? Number { get; set; }
        [Required(ErrorMessage = "Введите дату")]
        public DateTime Date { get; set; }

        public int ProviderId { get; set; }
        public virtual Provider? Provider { get; set; }
    }
}
