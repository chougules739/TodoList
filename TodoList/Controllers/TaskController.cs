using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using BAL;
using System.Linq;
using TodoList.Models;
using System.Net;

namespace TodoList.Controllers
{
    public class TaskController : Controller
    {
        [Authorize]
        //[Authorize(Roles = "Manager, Developer, Tester")]
        public ActionResult AllTasks()
        {
            ProjectManager _projectManager = new ProjectManager();

            //To populate hard caded project data
            //_projectManager.SaveProject();

            ToDoTaskComplexType _toDoTaskComplexType = new ToDoTaskComplexType();

            //Loads normal/waterfall model's project only.
            _toDoTaskComplexType.Project = _projectManager.GetProjects().Where(x => x.Type == (int)ProjectType.Normal).FirstOrDefault();

            ToDoTask _toDoTask = new ToDoTask();

            if (_toDoTaskComplexType.Project.Type == (int)ProjectType.Agile)
                _toDoTask.SetToDoTaskStrategy(new AgileTask());
            else if (_toDoTaskComplexType.Project.Type == (int)ProjectType.Normal)
                _toDoTask.SetToDoTaskStrategy(new NormalTask());

            _toDoTaskComplexType.TodoTasks = _toDoTask.GetAll(_toDoTaskComplexType.Project.Id).Where(x => x.UserId == ((Models.User)Session["User"]).Id);

            _toDoTaskComplexType.TaskStatuses = from TaskStatus status in Enum.GetValues(typeof(TaskStatus))
                                                select new SelectListItem { Value = Convert.ToInt32(status).ToString(), Text = status.ToString() };

            _toDoTaskComplexType.TaskPriorities = from TaskPriority priority in Enum.GetValues(typeof(TaskPriority))
                                                  select new SelectListItem { Value = Convert.ToInt32(priority).ToString(), Text = priority.ToString() };

            return View(_toDoTaskComplexType);
        }

        public JsonResult PopulateTask(string projectType, string prjectId, string taskId)
        {
            ToDoTask toDoTask = new ToDoTask();

            if (Convert.ToInt32(projectType) == (int)ProjectType.Agile)
                toDoTask.SetToDoTaskStrategy(new AgileTask());
            else if (Convert.ToInt32(projectType) == (int)ProjectType.Normal)
                toDoTask.SetToDoTaskStrategy(new NormalTask());

            TodoTaskModel todoTaskModel = toDoTask.GetById(new Guid(taskId));

            return Json(todoTaskModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //[Authorize]
        //[ValidateAntiForgeryToken]
        public JsonResult InsertNormalTask(NormalTodoTaskModel todoTaskModel)
        {
            todoTaskModel.Id = Guid.NewGuid();
            todoTaskModel.IsActive = true;
            todoTaskModel.CreatedDate = DateTime.Now;

            todoTaskModel.CreatedBy = ((Models.User)Session["User"]).Id;
            todoTaskModel.UserId = ((Models.User)Session["User"]).Id;

            ToDoTask toDoTask = new ToDoTask();

            toDoTask.SetToDoTaskStrategy(new NormalTask());

            TodoTaskModel _normalTodoTaskModel = toDoTask.Save(todoTaskModel);

            return Json(_normalTodoTaskModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //[Authorize]
        //[ValidateAntiForgeryToken]
        public JsonResult UpdateNormalTask(NormalTodoTaskModel todoTaskModel)
        {
            todoTaskModel.IsActive = true;
            todoTaskModel.UpdatedDate = DateTime.Now;

            //Need to change
            todoTaskModel.UpdatedBy = ((Models.User)Session["User"]).Id;
            todoTaskModel.UserId = ((Models.User)Session["User"]).Id;

            ToDoTask toDoTask = new ToDoTask();

            toDoTask.SetToDoTaskStrategy(new NormalTask());

            TodoTaskModel _normalTodoTaskModel = toDoTask.Save(todoTaskModel);

            return Json(_normalTodoTaskModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //[Authorize]
        //[ValidateAntiForgeryToken]
        public HttpStatusCodeResult DeleteNormalTask(string taskId)
        {
            ToDoTask toDoTask = new ToDoTask();

            toDoTask.SetToDoTaskStrategy(new NormalTask());

            TodoTaskModel _normalTodoTaskModel = toDoTask.DeleteById(new Guid(taskId));

            return _normalTodoTaskModel == null ? new HttpStatusCodeResult(HttpStatusCode.OK, "Record deleted") :
                new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Bad Request");
        }

        [HttpPost]
        //[Authorize]
        //[ValidateAntiForgeryToken]
        public HttpStatusCodeResult DeleteTasks(List<Guid> taskIds)
        {
            ToDoTask toDoTask = new ToDoTask();

            toDoTask.SetToDoTaskStrategy(new NormalTask());

            IEnumerable<TodoTaskModel> _normalTodoTaskModels = toDoTask.DeleteByIds(taskIds);

            return _normalTodoTaskModels.Count() == 0 ? new HttpStatusCodeResult(HttpStatusCode.OK, "Record deleted") :
                new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Bad Request");
        }

    }
}
