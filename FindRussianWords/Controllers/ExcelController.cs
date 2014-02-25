using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.POIFS.FileSystem;


namespace FindRussianWords.Controllers
{
    public class ExcelController : Controller
    {
        //
        // GET: /Excel/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Creates a new Excel spreadsheet based on a template using the ExcelPackage library.
        ///  A new file is created on the server based on a template. 
        /// </summary> 
        ///<returns>Excel report</returns>

        
        public ActionResult ExcelPackageCreate()
        {
            return View();
        }

        public void ExportClientsListToExcel()
        {
            var grid = new System.Web.UI.WebControls.GridView();
            string[] ClientsList={"mike","jonh","vladimit"};
            grid.DataSource = /*from d in dbContext.diners
                              where d.user_diners.All(m => m.user_id == userID) && d.active == true */
                              from d in ClientsList
                              select new
                              {
                                  FirstName = d
                              };

            grid.DataBind();

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=Exported_Diners.xls");
            Response.ContentType = "application/excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            Response.Write(sw.ToString());

            Response.End();

        }
    }
}
