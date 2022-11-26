using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestTaskMvc.Models
{
    [Table("OrderItem")]
    public class OrderItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите имя товара")]
        public string? Name { get; set; }

        [Column(TypeName = "decimal(18, 3)")]
        [Range(0, 999999999999999999.999, ErrorMessage ="Число должно быть не более 18 цифр перед запятой и 3 после запятой")]
        public decimal Quantity { get; set; }

        [Required(ErrorMessage = "Введите Unit")]
        public string? Unit { get; set; }
            
        public int OrderId { get; set; }
        public virtual Order? Order { get; set; }
    }
}
