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
    public class ProjectManager
    {
        string _server = ConfigurationManager.AppSettings["RedisServer"].ToString();

        /// <summary>  
        /// To get value from Redis DB  
        /// </summary>  
        /// <param name="host">Redis Host Name</param>  
        /// <param name="key">Key as string</param>  
        /// <returns></returns>  
        public IEnumerable<Project> GetProjects()
        {
            using (var objRedisClient = new RedisClient(_server))
            {
                IRedisTypedClient<Project> projects = objRedisClient.As<Project>();
                return projects.GetAll();

            }
        }

        /// <summary>  
        /// To Save Key Value Pair in Redis DB  
        /// </summary>  
        /// <param name="host">Redis Host Name</param>  
        /// <param name="key">Key as string</param>  
        /// <param name="value">Value as string</param>  
        /// <returns></returns>  
        public void SaveProject()
        {
            using (var objRedisClient = new RedisClient(_server))
            {
                IRedisTypedClient<Project> projectList = objRedisClient.As<Project>();

                List<Project> project = new List<Project>();
                project.Add(new Project { Id = Guid.NewGuid(), Type = 1, Name = "Proj 1", IsActive = true });
                project.Add(new Project { Id = Guid.NewGuid(), Type = 2, Name = "Proj 2", IsActive = true });

                //projectList.DeleteAll();
                projectList.StoreAll(project);
            }
        }
    }
}
