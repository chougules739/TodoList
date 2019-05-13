using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class ProjectManager
    {
        /// <summary>  
        /// To get value from Redis DB  
        /// </summary>  
        /// <param name="host">Redis Host Name</param>  
        /// <param name="key">Key as string</param>  
        /// <returns></returns>  
        public static IEnumerable<Project> GetProjects()
        {
            using (var objRedisClient = new RedisClient("localhost"))
            {
                IRedisTypedClient<Project> projects = objRedisClient.As<Project>();
                return projects.GetAll();

            }
        }
    }
}
