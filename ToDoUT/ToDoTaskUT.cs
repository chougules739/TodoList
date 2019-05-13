using BAL;
using NUnit.Framework;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Tests
{
    public class ToDoTaskUT
    {
        List<AgileTodoTaskModel> _agileTodoTaskModel = new List<AgileTodoTaskModel>();
        List<NormalTodoTaskModel> _normalTodoTaskModel = new List<NormalTodoTaskModel>();
        string _server = "localhost";

        [Test]
        public void InsertAgileTask()
        {
            using (var _objRedisClient = new RedisClient(_server))
            {
                IRedisTypedClient<AgileTodoTaskModel> _agileTodoList = _objRedisClient.As<AgileTodoTaskModel>();

                _agileTodoTaskModel.Add(new AgileTodoTaskModel
                {
                    Id = new Guid("a444b67a-85ad-480a-8dc8-577155587356"),
                    ProjectId = new Guid("f67a9154-117b-4fc8-951b-86312a0d11ec"),
                    Name = "Demo task1",
                    Description = "Demo Description1",
                    Efforts = 5,
                    StoryPoints = 5,
                    BurnedHours = 0,
                    Status = 1,
                    CreatedDate = DateTime.Now
                });
                _agileTodoTaskModel.Add(new AgileTodoTaskModel
                {
                    Id = new Guid("b444b67b-85ad-480a-8dc8-577155587356"),
                    ProjectId = new Guid("f67a9154-117b-4fc8-951b-86312a0d11ec"),
                    Name = "Demo task2",
                    Description = "Demo Description2",
                    Efforts = 5,
                    StoryPoints = 5,
                    BurnedHours = 0,
                    Status = 1,
                    CreatedDate = DateTime.Now
                });

                _agileTodoList.StoreAll(_agileTodoTaskModel);

                List<Guid> storedIds = new List<Guid>() { new Guid("a444b67a-85ad-480a-8dc8-577155587356"), new Guid("b444b67b-85ad-480a-8dc8-577155587356") };
                int _normalTaskCount = _objRedisClient.As<NormalTodoTaskModel>().GetByIds(storedIds).Count;

                Assert.AreEqual(2, _normalTaskCount);
            }
            Assert.Pass();
        }

        [Test]
        public void InsertNormalTask()
        {
            using (var _objRedisClient = new RedisClient(_server))
            {
                IRedisTypedClient<NormalTodoTaskModel> _normalTodoList = _objRedisClient.As<NormalTodoTaskModel>();

                _normalTodoTaskModel.Add(new NormalTodoTaskModel { Id = new Guid("p99a9154-117b-4fc8-951b-86312a0d11ec"), ProjectId = new Guid("c444b67a-85ad-480a-8dc8-577155587356"), Name = "Demo task3", Description = "Demo Description3", Priority = 1, EstimatedCompletionDate = DateTime.Now, Status = 1, CreatedDate = DateTime.Now });
                _normalTodoTaskModel.Add(new NormalTodoTaskModel
                {
                    Id = new Guid("s99b9154-117b-4fc8-951b-86312a0d11ec"),
                    ProjectId = new Guid("c444b67a-85ad-480a-8dc8-577155587356"),
                    Name = "Demo task4",
                    Description = "Demo Description4",
                    Priority = 1,
                    EstimatedCompletionDate = DateTime.Now,
                    Status = 1,
                    CreatedDate = DateTime.Now
                });

                List<Guid> storedIds = new List<Guid>() { new Guid("p99a9154-117b-4fc8-951b-86312a0d11ec"), new Guid("s99b9154-117b-4fc8-951b-86312a0d11ec") };
                int _normalTaskCount = _objRedisClient.As<NormalTodoTaskModel>().GetByIds(storedIds).Count;

                Assert.AreEqual(2, _normalTaskCount);
            }
        }

        [Test]
        public void UpdateAgileTask()
        {
            _agileTodoTaskModel[0].Description = "Updated";

            ToDoTask _toDoTask = new ToDoTask();
            TodoTaskModel _todoTaskModel = _toDoTask.Save(_agileTodoTaskModel[0]);

            Assert.AreNotEqual(null, _todoTaskModel);
        }


        [Test]
        public void UpdateNormalTask()
        {
            _normalTodoTaskModel[0].Description = "Updated";

            ToDoTask _toDoTask = new ToDoTask();
            TodoTaskModel _todoTaskModel = _toDoTask.Save(_normalTodoTaskModel[0]);

            Assert.AreNotEqual(null, _todoTaskModel);
        }

        [Test]
        public void DeleteAgileTask()
        {
            ToDoTask _toDoTask = new ToDoTask();
            List<Guid> storedIds = new List<Guid>() { new Guid("a444b67a-85ad-480a-8dc8-577155587356"), new Guid("b444b67b-85ad-480a-8dc8-577155587356") };
            IEnumerable<TodoTaskModel> _agileTodoTaskModel = _toDoTask.DeleteByIds(storedIds);

            Assert.AreEqual(0, _agileTodoTaskModel.Count());
        }

        [Test]
        public void DeleteNormalTask()
        {
            ToDoTask _toDoTask = new ToDoTask();
            List<Guid> storedIds = new List<Guid>() { new Guid("p99a9154-117b-4fc8-951b-86312a0d11ec"), new Guid("s99b9154-117b-4fc8-951b-86312a0d11ec") };
            IEnumerable<TodoTaskModel> _normalTodoTaskModel = _toDoTask.DeleteByIds(storedIds);

            Assert.AreEqual(0, _agileTodoTaskModel);
        }
    }
}