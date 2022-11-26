using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTaskMvc.Models.ViewModel;
using TestTaskMvc.Models;

namespace TestTaskMvc.Services.Interface
{
    public interface IOrderFilterService
    {
        HomeViewModel Filter(RequestFilter request);
        FilterSelectListViewModel GetFilterSelectListView();
    }
}
