using BusinessDataModel;
using Examen.Models;
using ModelView;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Examen.Controllers
{
    public class SheetController : BaseController
    {
        [HttpPost]
        public mvReply vna([FromBody] mvSecurity model)
        {
            mvReply oR = new mvReply();
            oR.result = 0;

            if (!Verify(model.token))
            {
                oR.message = "No Autorizado";
                return oR;
            }
            try
            {
                using (ExamenEntities db = new ExamenEntities())
                {
                    string ruta = "C:\\Users\\danie\\Desktop\\EjercicioDictum.xlsx";
                    SLDocument sl = new SLDocument(ruta);
                    SpreadSheet oSpreadSheet = new SpreadSheet(sl.GetCurrentWorksheetName());
                    List<mvInputs> oModel = new List<mvInputs>();
                    int iCol = 1;
                    const int rowHead = 1;
                    const int row = 2;
                    while (sl.GetCellValueAsString(rowHead, iCol) != "TASA")
                    {
                        oModel.Add(new mvInputs()
                        {
                            Name = sl.GetCellValueAsString(rowHead, iCol),
                            Valor = sl.GetCellValueAsInt32(row, iCol)
                        });
                        iCol++;
                    }
                    oSpreadSheet.set(oModel);
                    oSpreadSheet.calculate();
                    oR.data = oSpreadSheet.get();
                    oR.message = "resultado:" + oSpreadSheet.vna();
                    oR.result = 1;
                }
            }
            catch (Exception ex)
            {
                oR.message = "Ocurrio un error en el servidor, inteta más tarde";
            }
            return oR;
        }
    }
}
  