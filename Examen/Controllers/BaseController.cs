using Examen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Examen.Controllers
{
    public class BaseController : ApiController
    {
        //verificar el tocken
        public bool Verify(string token) {
            using (ExamenEntities db = new ExamenEntities()) 
            {
                if (db.Users.Where(d => d.token == token && d.idEstatus == 1).Count() > 0)
                    return true;
            }
            return false;
        }
    }
}
