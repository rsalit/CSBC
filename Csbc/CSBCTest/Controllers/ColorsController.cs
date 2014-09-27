using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CSBC.Core.Interfaces;
using CSBC.Core.Models;

namespace CSBC.Admin.Web.Controllers
{
    public class ColorsController : ApiController
    {
        protected ICsbcUow Uow { get; set; }

        public ColorsController(ICsbcUow uow)
        {
            Uow = uow;
        }
        // GET: api/Color
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Color/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Color
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Color/5
        public HttpResponseMessage Put(Color color)
        {
            Uow.Colors.Insert(color);
            Uow.Commit();
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }

        // DELETE: api/Color/5
        public void Delete(int id)
        {
        }
    }
}
