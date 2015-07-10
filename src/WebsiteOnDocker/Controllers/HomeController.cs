using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ServiceStack.Redis;

namespace WebsiteOnDocker.Controllers
{
    public class HomeController : Controller
    {
        string key = "hits";
        string redisPort = "REDIS_PORT_6379_TCP_ADDR";
        public IActionResult Index()
        {
            ViewBag.Message = this.GetHitsFromRedis();

            //String db = Environment.GetEnvironmentVariable(redisPort);

            var environmentVariables = Environment.GetEnvironmentVariables();
            return View(environmentVariables);
        }

        public int GetHitsFromRedis(string db)
        {
            using (RedisClient client = new RedisClient(db))
            {
                client.Get<Int32>(key);
            }
            return 0;
        }

        public int GetHitsFromRedis()
        {
            int hits = 0;

            BasicRedisClientManager basicRedisClientManager = new BasicRedisClientManager("sayhellodocker.cloudapp.net:6379");
            if (basicRedisClientManager != null)
            {
                using (var redisClient = (RedisClient)basicRedisClientManager.GetClient())
                {
                    if (redisClient != null)
                    {
                        try
                        {
                            hits = redisClient.Get<Int32>("hits");
                            return hits == int.MinValue ? 0 : hits;
                        }
                        catch (Exception ex)
                        {
                            return -1;
                        }
                    }
                }
            }
            return hits;
        }

        public IActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }
    }
}
