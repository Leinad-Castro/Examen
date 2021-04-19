using Examen.Models;
using ModelView;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Examen.Controllers
{
    public class AccessController : ApiController
    {

        [HttpPost]
        public mvReply Login([FromBody] mvAccess model) {
            mvReply oR = new mvReply();
            try
            {
                using (ExamenEntities db = new ExamenEntities())
                {
                    var list = db.Users.Where(i => i.email == model.email && i.password == model.password && i.idEstatus == 1);
                    if (list.Count() > 0)
                    {
                        oR.result = 1;
                        oR.data = Guid.NewGuid().ToString();

                        Users oUser = list.First();
                        oUser.token = (string)oR.data;
                        db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    else 
                    {
                        oR.message = "Datos incorrectos";
                    }
                }
            }
            catch (Exception ex)
            {
                oR.result = 0;
                oR.message = "Ocurrio un error, estamos corrigiendolo";
                
            }
            return oR;
        }
    }
}
