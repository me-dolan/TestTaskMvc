using Microsoft.AspNetCore.Mvc.Rendering;

namespace TestTaskMvc.Models.ViewModel
{
    public class FilterSelectListViewModel
    {
        public IEnumerable<SelectListItem> OrderSelectList { get; set; }
        public IEnumerable<SelectListItem> OrderItemNameSelectList { get; set; }
        public IEnumerable<SelectListItem> OrderItemUnitSelectList { get; set; }
        public IEnumerable<SelectListItem> ProviderSelectList { get; set; }

    }
}
