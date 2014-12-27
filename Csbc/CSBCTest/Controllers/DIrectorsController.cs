using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CSBC.Core.Data;
using CSBC.Core.Repositories;

namespace CSBC.Admin.Web.Controllers
{
    public class DirectorsController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<object> Get()
        {
            using (var db = new CSBCDbContext())
            {
                var repo = new DirectorRepository(db);
                var data = repo.GetAll(1);
                return data;
            }
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}