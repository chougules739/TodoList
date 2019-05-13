using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    /// <summary>
    /// 'Strategy' abstract class
    /// </summary>
    public abstract class ToDoTaskStrategy
    {
        /// <summary>  
        /// To Insert/Update either of type AgileTodoTask or NormalTodoTask in Redis DB  
        /// </summary>  
        /// <param name="host">Redis Host Name</param>  
        /// <param name="todoTask">Either of AgileTodoTask or NormalTodoTask model to insert</param>  
        /// <returns>TodoTaskModel</returns>
        public abstract TodoTaskModel Save(string host, TodoTaskModel todoTaskModel);

        /// <summary>  
        /// To delete either of type AgileTodoTask or NormalTodoTask
        /// </summary>  
        /// <param name="host">Redis Host Name</param>  
        /// <param name="taskId">Either of AgileTodoTask or NormalTodoTask Task id to delete</param>  
        /// <returns>TodoTaskModel</returns>
        public abstract TodoTaskModel DeleteById(string host, Guid taskId);

        /// <summary>  
        /// To delete AgileTodoTask from AgileTodoTask
        /// </summary>  
        /// <param name="host">Redis Host Name</param>  
        /// <param name="taskIda">Delete specific taska of AgileTodoTask by Task id</param>  
        /// <returns>IEnumerable<TodoTaskModel></returns>
        public abstract IEnumerable<TodoTaskModel> DeleteByIds(string host, List<Guid> taskIds);

        /// <summary>  
        /// To delete all either from AgileTodoTask or NormalTodoTask
        /// </summary>  
        /// <param name="host">Redis Host Name</param>  
        /// <returns></returns>
        public abstract void DeleteAll(string host);

        /// <summary>  
        /// To returns either AgileTodoTask or NormalTodoTask model
        /// </summary>  
        /// <param name="host">Redis Host Name</param>  
        /// <returns></returns>
        public abstract TodoTaskModel GetById(string host, Guid taskId);

        /// <summary>  
        /// To get all either from AgileTodoTask or NormalTodoTask
        /// </summary>  
        /// <param name="host">Redis Host Name</param>  
        /// <returns></returns>
        public abstract IEnumerable<TodoTaskModel> GetAll(string host);
    }

    /// <summary>
    /// 'Concrete agile task strategy'
    /// </summary>
    public class AgileTask : ToDoTaskStrategy
    {
        /// <summary>  
        /// To Insert/Update task of type AgileTodoTask in Redis DB  
        /// </summary>  
        /// <param name="host">Redis Host Name</param>  
        /// <param name="todoTask">AgileTodoTask model to insert</param>  
        /// <returns></returns>
        public override TodoTaskModel Save(string host, TodoTaskModel todoTaskModel)
        {
            using (var _objRedisClient = new RedisClient(host))
            {
                _objRedisClient.As<AgileTodoTaskModel>().StoreAll(new List<AgileTodoTaskModel> { todoTaskModel as AgileTodoTaskModel });
                return _objRedisClient.As<AgileTodoTaskModel>().GetById(todoTaskModel.Id);
            }
        }

        /// <summary>  
        /// To delete AgileTodoTask from AgileTodoTask
        /// </summary>  
        /// <param name="host">Redis Host Name</param>  
        /// <param name="taskId">Delete specific task of AgileTodoTask by Task id</param>  
        /// <returns>TodoTaskModel</returns>
        public override TodoTaskModel DeleteById(string host, Guid taskId)
        {
            using (var _objRedisClient = new RedisClient(host))
            {
                _objRedisClient.As<AgileTodoTaskModel>().DeleteById(taskId);
                return _objRedisClient.As<AgileTodoTaskModel>().GetById(taskId);
            }
        }

        /// <summary>  
        /// To delete AgileTodoTask from AgileTodoTask
        /// </summary>  
        /// <param name="host">Redis Host Name</param>  
        /// <param name="taskId">Delete specific task of AgileTodoTask by Task id</param>  
        /// <returns>IEnumerable<TodoTaskModel></returns>
        public override IEnumerable<TodoTaskModel> DeleteByIds(string host, List<Guid> taskIds)
        {
            using (var _objRedisClient = new RedisClient(host))
            {
                _objRedisClient.As<AgileTodoTaskModel>().DeleteByIds(taskIds);
                return _objRedisClient.As<AgileTodoTaskModel>().GetByIds(taskIds);
            }
        }

        /// <summary>  
        /// To delete all from AgileTodoTask
        /// </summary>  
        /// <param name="host">Redis Host Name</param>  
        /// <returns></returns>
        public override void DeleteAll(string host)
        {
            using (var _objRedisClient = new RedisClient(host))
            {
                _objRedisClient.As<AgileTodoTaskModel>().DeleteAll();
            }
        }

        /// <summary>  
        /// To returns AgileTodoTask model
        /// </summary>  
        /// <param name="host">Redis Host Name</param>  
        /// <param name="taskId">=AgileTodoTask Task id to delete</param>  
        /// <returns></returns>
        public override TodoTaskModel GetById(string host, Guid taskId)
        {
            using (var _objRedisClient = new RedisClient(host))
            {
                return _objRedisClient.As<AgileTodoTaskModel>().GetById(taskId);
            }
        }

        /// <summary>  
        /// To get all from AgileTodoTask
        /// </summary>  
        /// <param name="host">Redis Host Name</param>  
        /// <returns></returns>
        public override IEnumerable<TodoTaskModel> GetAll(string host)
        {
            using (var _objRedisClient = new RedisClient(host))
            {
                return _objRedisClient.As<AgileTodoTaskModel>().GetAll();
            }
        }
    }

    /// <summary>
    /// 'Concrete normal task strategy'
    /// </summary>
    public class NormalTask : ToDoTaskStrategy
    {
        /// <summary>  
        /// To Insert/Update task of type NormalTodoTask in Redis DB  
        /// </summary>  
        /// <param name="host">Redis Host Name</param>  
        /// <param name="todoTask">NormalTodoTask model to insert</param>  
        /// <returns></returns>
        public override TodoTaskModel Save(string host, TodoTaskModel todoTaskModel)
        {
            using (var _objRedisClient = new RedisClient(host))
            {
                _objRedisClient.As<NormalTodoTaskModel>().StoreAll(new List<NormalTodoTaskModel> { todoTaskModel as NormalTodoTaskModel });
                return _objRedisClient.As<NormalTodoTaskModel>().GetById(todoTaskModel.Id);
            }
        }

        /// <summary>  
        /// To delete NormalTask
        /// </summary>  
        /// <param name="host">Redis Host Name</param>  
        /// <param name="taskId">Delete specific task of NormalTodoTask by Task id</param>  
        /// <returns></returns>
        public override TodoTaskModel DeleteById(string host, Guid taskId)
        {
            using (var _objRedisClient = new RedisClient(host))
            {
                _objRedisClient.As<NormalTodoTaskModel>().DeleteById(taskId);
                return _objRedisClient.As<NormalTodoTaskModel>().GetById(taskId);
            }
        }

        /// <summary>  
        /// To delete AgileTodoTask from AgileTodoTask
        /// </summary>  
        /// <param name="host">Redis Host Name</param>  
        /// <param name="taskIds">Delete specific task of AgileTodoTask by Task ids</param>  
        /// <returns>IEnumerable<TodoTaskModel></returns>
        public override IEnumerable<TodoTaskModel> DeleteByIds(string host, List<Guid> taskIds)
        {
            using (var _objRedisClient = new RedisClient(host))
            {
                _objRedisClient.As<NormalTodoTaskModel>().DeleteByIds(taskIds);
                return _objRedisClient.As<NormalTodoTaskModel>().GetByIds(taskIds);
            }
        }

        /// <summary>  
        /// To delete all NormalTodoTask
        /// </summary>  
        /// <param name="host">Redis Host Name</param>  
        /// <returns></returns>
        public override void DeleteAll(string host)
        {
            using (var _objRedisClient = new RedisClient(host))
            {
                _objRedisClient.As<NormalTodoTaskModel>().DeleteAll();
            }
        }

        /// <summary>  
        /// To returns NormalTodoList model
        /// </summary>  
        /// <param name="host">Redis Host Name</param>  
        /// <param name="taskId">NormalTodoList Task id to delete</param>  
        /// <returns>TodoTaskModel</returns>
        public override TodoTaskModel GetById(string host, Guid taskId)
        {
            using (var _objRedisClient = new RedisClient(host))
            {
                IRedisTypedClient<NormalTodoTaskModel> users = _objRedisClient.As<NormalTodoTaskModel>();
                return users.GetById(taskId);
                //return _objRedisClient.As<NormalTodoTaskModel>().GetById(taskId);
            }
        }

        /// <summary>  
        /// To get all from AgileTodoTask
        /// </summary>  
        /// <param name="host">Redis Host Name</param>  
        /// <returns>List<TodoTaskModel></returns>
        public override IEnumerable<TodoTaskModel> GetAll(string host)
        {
            using (var _objRedisClient = new RedisClient(host))
            {
                return _objRedisClient.As<NormalTodoTaskModel>().GetAll();
            }
        }
    }

    /// <summary>
    /// 'Context' component of strategy
    /// </summary>
    public class ToDoTask
    {
        private ToDoTaskStrategy _toDoTaskStrategy;
        string _server = ConfigurationManager.AppSettings["RedisServer"].ToString();
        Logger _logger = new Logger();

        /// <summary>  
        /// Decides further stgategy or behaviour of an object, either
        /// it would be AgileTodoTask or NormalTodoTask 
        /// </summary>  
        /// <param name="toDoTaskStrategy">Either AgileTodoTask or NormalTodoTask </param>  
        /// <returns></returns>
        public void SetToDoTaskStrategy(ToDoTaskStrategy toDoTaskStrategy)
        {
            this._toDoTaskStrategy = toDoTaskStrategy;
        }

        /// <summary>  
        /// To Insert/Update either of type AgileTodoTask or NormalTodoTask in Redis DB  
        /// </summary>  
        /// <param name="todoTaskModel">Model of Agile or Normal task</param>  
        /// <returns></returns>
        public TodoTaskModel Save(TodoTaskModel todoTaskModel)
        {
            Notify(DateTime.Now.ToString() + ": New task inserting.");
            return _toDoTaskStrategy.Save(_server, todoTaskModel);
        }

        /// <summary>  
        /// To delete either of type AgileTodoTask or NormalTodoTask
        /// </summary>  
        /// <param name="host">Redis Host Name</param>  
        /// <param name="taskId">Either of AgileTodoTask or NormalTodoTask Task id to delete</param>  
        /// <returns></returns>
        public TodoTaskModel DeleteById(Guid taskId)
        {
            Notify(DateTime.Now.ToString() + ": Task (" + taskId + ") deleting");
            return _toDoTaskStrategy.DeleteById(_server, taskId);
        }

        /// <summary>  
        /// To delete either of type AgileTodoTask or NormalTodoTask
        /// </summary>  
        /// <param name="host">Redis Host Name</param>  
        /// <param name="taskIds">Either of AgileTodoTask or NormalTodoTask Task ids to delete</param>  
        /// <returns></returns>
        public IEnumerable<TodoTaskModel> DeleteByIds(List<Guid> taskIds)
        {
            for (int i = 0; i < taskIds.Count; i++)
            {
                Notify(DateTime.Now.ToString() + ": Task (" + taskIds[i] + ") deleting");
            }

            return _toDoTaskStrategy.DeleteByIds(_server, taskIds);
        }

        /// <summary>  
        /// To delete all either from AgileTodoTask or NormalTodoTask
        /// </summary>  
        /// <returns></returns>
        public void DeleteAll()
        {
            Notify(DateTime.Now.ToString() + ": All deleting");
            _toDoTaskStrategy.DeleteAll(_server);
        }

        /// <summary>  
        /// To returns either AgileTodoTask or NormalTodoTask model
        /// </summary>  
        /// <param name="taskId">Task id to get the model.</param>  
        /// <returns></returns>
        public TodoTaskModel GetById(Guid taskId)
        {
            return _toDoTaskStrategy.GetById(_server, taskId);
        }

        /// <summary>  
        /// To get all either from AgileTodoTask or NormalTodoTask
        /// </summary>  
        /// <returns></returns>
        public IEnumerable<TodoTaskModel> GetAll(Guid projectId)
        {
            return _toDoTaskStrategy.GetAll(_server).Where(x => x.ProjectId == projectId);
        }

        public void Notify(string message)
        {
            _logger.LogInfo(message);
        }
    }

    /// <summary>
    /// The 'Observer' interface
    /// </summary>
    interface ILogger
    {
        void LogInfo(string message);
    }

    /// <summary>
    /// The 'ConcreteObserver' class
    /// </summary>
    public class Logger : ILogger
    {
        public void LogInfo(string message)
        {
            //Log message logic
        }
    }
}
