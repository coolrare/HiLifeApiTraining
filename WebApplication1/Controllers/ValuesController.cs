using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get所有資料()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get取得打招呼文字(int id)
        {
            var name = "Will";
            return "Hello " + name + " (" + id + ")";
        }

        // POST api/values
        public string Post([FromBody] string value)
        {
            return value;
        }

        // PUT api/values/5
        public string Put(int id, [FromBody] string value)
        {
            return value;
        }

        // DELETE api/values/5
        public int Delete(int id)
        {
            return id;
        }
    }
}
