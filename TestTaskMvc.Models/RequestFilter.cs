namespace TestTaskMvc.Models
{
    public class RequestFilter
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public IEnumerable<string> OrderNumberFilter { get; set; }
        public IEnumerable<string> OrderItemNameFilter { get; set; }
        public IEnumerable<string> OrderItemUnitFilter { get; set; }
        public IEnumerable<int> ProviderId { get; set; }

    }
}
