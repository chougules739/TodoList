using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BAL
{
    public class TodoTaskModel
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public Guid UserId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Guid UpdatedBy { get; set; }
    }

    public class AgileTodoTaskModel : TodoTaskModel
    {
        public int Efforts { get; set; }
        public int StoryPoints { get; set; }
        public float BurnedHours { get; set; }
    }

    public class NormalTodoTaskModel : TodoTaskModel
    {
        public int Priority  { get; set; }
        public DateTime EstimatedCompletionDate { get; set; }
    }

    public enum ProjectType
    {
        Agile = 1,
        Normal = 2
    }

    public enum TaskStatus
    {
        ToDo = 1,
        InProgress = 2,
        Completed = 3
    }

    public enum TaskPriority
    {
        Low = 1,
        Moderate = 2,
        High = 3
    }
}
