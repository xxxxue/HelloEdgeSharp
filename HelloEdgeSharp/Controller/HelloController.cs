using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EdgeSharp.Core.Network;

namespace HelloEdgeSharp.Controller
{
    [ActionController(Name = "HelloController", Description = "测试控制器")]
    public class HelloController : ActionController
    {

        public class UserInfo
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Version { get; set; }
            public string Url { get; set; }
        }
        public class Result<T>
        {
            public bool Succeeded { get; set; }

            public T Data { get; set; }
            public string Errors { get; set; }
        }


        [ActionRoute(Path = "/getFrameWorks")]
        public Result<object> GetFrameWorks(int index, int size)
        {
            var list = new List<UserInfo>
            {
                new UserInfo()
                {
                    Id = 1,
                    Name = "React",
                    Version = "v1",
                    Url = "http://www.react.com",
                },
                new UserInfo()
                {
                    Id = 2,
                    Name = "Vue",
                    Version="v2",
                    Url = "http://www.vue.com",
                }
            };


            return new Result<object>
            {
                Succeeded = true,
                Data = new
                {
                    Items = list,
                    TotalCount = 100,
                }
            };
        }

    }
}
