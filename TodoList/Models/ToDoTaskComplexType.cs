using BAL;
using System.Collections.Generic;
using System.Web.Mvc;

namespace TodoList.Models
{
    public class ToDoTaskComplexType
    {
        public Project Project { get; set; }
        public IEnumerable<TodoTaskModel> TodoTasks { get; set; }
        public IEnumerable<SelectListItem> TaskStatuses { get; set; }
        public IEnumerable<SelectListItem> TaskPriorities { get; set; }
    }
}
