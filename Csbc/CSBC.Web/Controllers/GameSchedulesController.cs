using System;
using System.Collections.Generic;
using System.Linq;
using Breeze.ContextProvider;
using Breeze.WebApi2;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using CSBC.Core.Repositories;
using CSBC.Core.Data;
using CSBC.Core.Models;

namespace CSBC.Web.Controllers
{
    [BreezeController]
    public class GameSchedulesController : ApiController
    {

        readonly ScheduleGameRepository _repository = new ScheduleGameRepository(new CSBCDbContext());

        // GET api/<controller>
        public List<ScheduleGame> Get()
        {
            using (var db = new CSBCDbContext())
            {
                var rep = new ScheduleGameRepository(db);
                return rep.GetAll().ToList();
            }
        }

        // GET api/<controller>/5
        public IEnumerable<ScheduleGame> Get(int id)
        {
           
            return _repository.GetSeasonGames(757);
           
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