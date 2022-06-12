using Microsoft.Office.Interop.Excel;
using OfficeOpenXml;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace WebScenarioAccounting.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult UserActivity()
        {
            Microsoft.Office.Interop.Excel.Application app =
        new Microsoft.Office.Interop.Excel.Application();

            app.Visible = true;

            Workbook wb = app.Workbooks.Add();
            Worksheet ws = wb.Worksheets[1];
             string cs =  @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Semyon\WebScenarioAccounting.mdf;Integrated Security=True;";


        SqlConnection conn = new SqlConnection(cs);
            conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT Surname AS Фамилия, UserName AS Имя, Patronymic AS Отчество, TypeName AS [Тип пользователя], Count(Scenario.id) AS [Количество сценариев] FROM 
                                            (SELECT id, Surname, Name AS UserName, Patronymic, 
                                            ClassifierUserType.TypeName AS TypeName FROM SysUser
                                            RIGHT JOIN ClassifierUserType ON UserType = ClassifierUserType.TypeID
                                            WHERE TerminationDate IS NULL) As UserInfo
                                            LEFT JOIN Scenario ON Scenario.Author = UserInfo.id
                                            GROUP BY Surname, UserName, Patronymic,TypeName
                                            ORDER BY [Количество сценариев] DESC", conn);

            SqlDataReader reader = cmd.ExecuteReader();

            ws.Cells[1, 1].Value = reader.GetName(0);
            ws.Cells[1, 2].Value = reader.GetName(1);
            ws.Cells[1, 3].Value = reader.GetName(2);
            ws.Cells[1, 4].Value = reader.GetName(3);
            ws.Cells[1, 5].Value = reader.GetName(4);

            int i = 2;
            while (reader.Read())
            {
                ws.Cells[i, 1].Value = reader[0];
                ws.Cells[i, 2].Value = reader[1];
                ws.Cells[i, 3].Value = reader[2];
                ws.Cells[i, 4].Value = reader[3];
                ws.Cells[i, 5].Value = reader[4];
                i++;
            }

            reader.Close();
            conn.Close();

            return RedirectToAction("Index");

        }

        public ActionResult RoomStats()
        {
            Microsoft.Office.Interop.Excel.Application app =
        new Microsoft.Office.Interop.Excel.Application();

            app.Visible = true;

            Workbook wb = app.Workbooks.Add();
            Worksheet ws = wb.Worksheets[1];
            string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Semyon\WebScenarioAccounting.mdf;Integrated Security=True;";


            SqlConnection conn = new SqlConnection(cs);
            conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT RoomInfo.Name AS Помещение, Floor AS Этаж, Area AS Площадь, 
                    COUNT(Area) AS [Количество приборов], COUNT(Area)/Area AS [Приборов на м2]  FROM
                    (SELECT Name, Floor, Area,id FROM Room WHERE TerminationDate IS NULL) AS RoomInfo
                    INNER JOIN Actuator ON RoomInfo.id = Actuator.Room
                    GROUP BY RoomInfo.Name, Floor, Area", conn);

            SqlDataReader reader = cmd.ExecuteReader();

            ws.Cells[1, 1].Value = reader.GetName(0);
            ws.Cells[1, 2].Value = reader.GetName(1);
            ws.Cells[1, 3].Value = reader.GetName(2);
            ws.Cells[1, 4].Value = reader.GetName(3);
            ws.Cells[1, 5].Value = reader.GetName(4);

            int i = 2;
            while (reader.Read())
            {
                ws.Cells[i, 1].Value = reader[0];
                ws.Cells[i, 2].Value = reader[1];
                ws.Cells[i, 3].Value = reader[2];
                ws.Cells[i, 4].Value = reader[3];
                ws.Cells[i, 5].Value = reader[4];
                i++;
            }

            reader.Close();
            conn.Close();

            return RedirectToAction("Index");

        }


    }
}