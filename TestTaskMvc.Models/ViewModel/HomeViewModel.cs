namespace TestTaskMvc.Models.ViewModel
{
    public class HomeViewModel
    {
        public IEnumerable<Order> Order { get; set; }
        public IEnumerable<OrderItem> OrderItem { get; set; }
        public FilterSelectListViewModel FilterSelectList { get; set; }
    }
}
