using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
//using System.Web.Mvc;
using System.Web.Http;
using System.Web.Http.Cors;
using ZionAPI;
using ZionAPI.DataAdapaters;

namespace ZionWebAPI.Controllers
{
    [EnableCors(origins: "http://localhost:4200,https://ebps.in,https://myorders.zionwellmark.in,https://zionwellmark.in,https://bellbrandbhavanikhara.in", headers: "*", methods: "*")]

    [Route("[controller]")]
    public class AdminController : ApiController
    {
        ILogger _Logger;
        private readonly DAL objDAL;
        private readonly DAL_Bell objDAL_Bell;
        public new IConfiguration Configuration { get; }

        public AdminController()
        {
            objDAL = new DAL(Configuration);
            objDAL_Bell = new DAL_Bell(Configuration);
        }
        [Route("bell/GetAllItems/{area}/{shop}/{date1}/{date2}")]
        [HttpGet]
        public tblBills_Bell[] GetAllItems(string area, string shop, string date1, string date2)
        {
            return objDAL_Bell.GetAllItems(area, shop, date1, date2);
        }

        //[Route("bell/GetWeeklySales/{area}/{date1}")]
        //[HttpGet]
        //public tblSalesReport[] GetWeeklySales(string area, string date1)
        //{
        //    return objDAL_Bell.GetWeeklySales(area, date1);
        //}
        [Route("bell/GetInactiveShops/{area}/{shop}/{date1}/{date2}")]
        [HttpGet]
        public string GetInactiveShops(string area, string shop, string date1, string date2)
        {
            return objDAL_Bell.GetInactiveShops(area, shop, date1, date2);
        }
        [Route("bell/GetSalebyShopsBillDate/{reportType}/{area}/{shop}/{date1}/{date2}")]
        [HttpGet]
        public string GetSalebyShopsBillDate(string reportType, string area, string shop, string date1, string date2)
        {
            return objDAL_Bell.GetSalebyShopsBillDate(reportType, area, shop, date1, date2);
        }
        //[Route("bell/GetSaleItemsbyBillDate/{area}/{shop}/{date1}/{date2}")]
        //[HttpGet]
        //public string GetSaleItemsbyBillDate(string area, string shop, string date1, string date2)
        //{
        //    return objDAL_Bell.GetSaleItemsbyBillDate(area, shop, date1, date2);
        //}
        [Route("bell/GetTotalSalesByShop/{area}/{shop}/{date1}/{date2}/{totalamount}")]
        [HttpGet]
        public string GetTotalSalesByShop(string area, string shop, string date1, string date2, int totalamount)
        {
            return objDAL_Bell.GetTotalSalesByShop(area, shop, date1, date2, totalamount);
        }
        [Route("bell/GetWeeklySalesByItems/{type}/{area}/{shop}/{item}/{date1}/{date2}")]
        [HttpGet]
        public string GetWeeklySalesByItems(string type, string area, string shop, string item, string date1, string date2)
        {
            return objDAL_Bell.GetWeeklySalesByItems(type, area, shop, item, date1, date2);
        }
        [Route("bell/GetWeeklySalesByItems")]
        [HttpPost]
        public string GetWeeklySalesByItems(searchPayLoad objRequest)
        {
            return objDAL_Bell.GetWeeklySalesByItems(objRequest);
        }
        [Route("bell/GetLSCustomersAll")]
        [HttpGet]
        public tblBills_Bell[] GetAllLSCustomersAll()
        {
            return objDAL_Bell.GetAllLSCustomers();
        }
        [Route("bell/Bell_GetAreaList/{type}/{date1}/{date2}")]
        [HttpGet]
        public BellAreaMaster[] Bell_GetAreaList(string type, string date1, string date2) //GetAllLSItems
        {
            return objDAL_Bell.Bell_GetAreaList(type, date1, date2);
        }
        //[Route("bell/Bell_GetAllCategories")]
        //[HttpGet]
        //public BellAreaMaster[] Bell_GetAreaList(string type, string date1, string date2) //GetAllLSItems
        //{
        //    return objDAL_Bell.Bell_GetAreaList(type, date1, date2);
        //}
        [Route("bell/GetLSCustomersByAreaShop/{area}/{shop}")]
        [HttpGet]
        public tblBell_Orders[] GetLSCustomersByAreaShop(string area, string shop)
        {
            //not using for now.
            return objDAL_Bell.GetLSCustomersByAreaShop(area, shop);
        }
        [Route("bell/GetLSTotalSalesByArea/{area}/{date1}/{date2}")]
        [HttpGet]
        public tblSalesReportNew[] GetLSTotalSalesByArea(string area, string date1, string date2)
        {
            //not using for now.
            return objDAL_Bell.GetLSTotalSalesByArea(area, date1, date2);
        }
        [Route("bell/GetLSTotalSalesByArea_New/{area}/{date1}/{searchtype}")]
        [HttpGet]
        public tblSalesReportNew[] GetLSTotalSalesByArea_New(string area, string date1, string searchtype)
        {
            //not using for now.
            return objDAL_Bell.GetLSTotalSalesByArea_New(area, date1, searchtype);
        }
        [Route("bell/GetLSItemsByAreaDate/{area}/{billdate}")]
        [HttpGet]
        public tblBills_Bell[] GetLSItemsByAreaDate(string area, string billdate)
        {
            //not using for now.
            return objDAL_Bell.GetLSItemsByAreaDate(area, billdate);
        }
        [Route("bell/GetLSItemsByMonth/{custid}/{area}/{shop}/{date1}")]
        [HttpGet]
        public tblBills_Bell[] GetLSItemsByMonth(string custid, string area, string shop, string date1)
        {
            //not using for now.
            return objDAL_Bell.GetLSItemsByMonth(custid, area, shop, date1);
        }
        [Route("bell/Bell_GetAllCustomers/{line}/{area}/{shop}")]
        [HttpGet]
        public tblBellCustomersNew[] Bell_GetAllCustomers(string line, string area, string shop)
        {
            return objDAL_Bell.Bell_GetAllCustomers(line, area, shop);
        }

        [Route("bell/GetLSCustomerDetails/{ID}")]
        [HttpGet]
        public tblBellCustomers GetLSCustomerDetails(int ID)
        {
            //THIS IS USING TO USER NAVIGATION LOCATIONS.
            return objDAL_Bell.GetLSCustomerDetails(ID);
        }
        [Route("bell/GetLSCustomersAreaDates/{area}/{date1}/{date2}")]
        [HttpGet]
        public tblBills_Bell[] GetAllLSCustomers(string area, string date1, string date2)
        {
            return objDAL_Bell.GetAllLSCustomers(area, date1, date2);
        }

        [Route("bell/GetLSItems/{area}/{shopname}")]
        [HttpGet]
        public tblBills_Bell[] GetAllLSItems(string area, string shopname)
        {
            return objDAL_Bell.GetAllLSItems(area, shopname);
        }
        [Route("SaveCustLocation")]
        [HttpPost]
        public string SaveCustLocation(tblBellCustomers objCustDetails)
        {
            string returnData = objDAL_Bell.SaveCustLocation(objCustDetails);
            return returnData;
        }
        [Route("bell/getallusers/{id}")]
        [HttpGet]
        public tblUsers[] getallusers(int id)
        {
            return objDAL_Bell.getallusers(id);
        }
        //this is to authenticate for only admin credentials. for user validation use validation method.
        [Route("bell/authenticate/{username}/{password}/{usertype}")]
        [HttpGet]
        public tblUsers authenticate(string username, string password, string usertype)
        {
            return objDAL_Bell.authenticate(username, password, usertype);
        }
        //not sure using it or not.
        [Route("bell/validateuser")]
        [HttpPost]
        public tblUsers validateuser(tblUsers userinfo)
        {
            return objDAL_Bell.validateuser(userinfo);
        }
        [Route("bell/saveuserdetails")]
        [HttpPost]
        public tblUsers saveuserdetails(tblUsers userinfo)
        {
            return objDAL_Bell.saveuserdetails(userinfo);
        }
        [Route("bell/deleteuser/{id}")]
        [HttpDelete]
        public string deleteuser(int id)
        {
            return objDAL_Bell.deleteuser(id);
        }

        [HttpPut]
        public string updatedUserDetails(tblUsers user)
        {
            return objDAL_Bell.updatedUserDetails(user);
        }

        [HttpPost]
        [Route("bell/UpdatedPurchareRateMinOrder")]
        public string UpdatedPurchareRateMinOrder(tblItemMaster item)
        {
            return objDAL_Bell.UpdatedPurchareRateMinOrder(item);
        }
        //End of Bell methods.

        [Route("admin/GetAllItems/{id}")]
        [HttpGet]
        public tblItemDetails GetItemByID(int id)
        {
            return objDAL.GetItemByID(id);
        }

        [Route("admin/SaveItemDetailsByPut")]
        [HttpPut]
        public string SaveItemDetailsByPut(tblItemDetails obj)
        {
            return objDAL.SaveItemDetails(obj);
        }
        [Route("admin/SaveItemDetails")]
        [HttpPost]
        public string SaveItemDetails(tblItemDetails obj)
        {
            return objDAL.SaveItemDetails(obj);
        }
        [Route("admin/DeleteItem")]
        [HttpGet]
        public string DeleteItem(int id)
        {
            return objDAL.DeleteItem(id);
        }

        private static Random random = new Random();
        public static string GetRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        [Route("admin/uploadimage/{filename}")]
        [HttpPost]
        public string UploadImage(string filename)
        {
            //This is using FTP to upload images
            //HttpPostedFileBase file
            string strServerImagesPath = ConfigurationManager.AppSettings["UploadImagePath"];
            var file = HttpContext.Current.Request.Files.Count > 0 ? HttpContext.Current.Request.Files[0] : null;
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    string extension = Path.GetExtension(file.FileName);
                    if (filename == "new")
                    {
                        filename = filename + GetRandomString(8);
                    }
                    //var fileName = Path.GetFileName(file.FileName);
                    //String uploadUrl = String.Format("{0}/{1}/{2}", "zionwellmark.in", "/myorders.zionwellmark.in/zionimages", fileName);
                    //FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uploadUrl);
                    //FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://103.211.202.60:21" + "/myorders.zionwellmark.in/zionimages/" + fileName);
                    //string hostServer = ConfigurationManager.AppSettings["FtpHost"] + ConfigurationManager.AppSettings["FtpFolderPath"] + filename + ".jpeg";
                    string hostServer = ConfigurationManager.AppSettings["FtpHost"] + "/" + filename + ".jpeg";
                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(hostServer);
                    request.Method = WebRequestMethods.Ftp.UploadFile;
                    request.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["FTP_Uname"], ConfigurationManager.AppSettings["FTP_dwp"]);
                    //request.Method = WebRequestMethods.Ftp.ListDirectory;
                    //request.EnableSsl = true;
                    //request.Proxy = null;
                    //request.KeepAlive = true;
                    //request.UseBinary = true;

                    Stream requestStream = request.GetRequestStream();

                    var sourceStream = file.InputStream;
                    request.ContentLength = sourceStream.Length;
                    byte[] buffer = new byte[1026];
                    int bytesRead = sourceStream.Read(buffer, 0, 1026);
                    do
                    {
                        requestStream.Write(buffer, 0, bytesRead);
                        bytesRead = sourceStream.Read(buffer, 0, 1026);
                    } while (bytesRead > 0);
                    sourceStream.Close();
                    requestStream.Close();
                    FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                    //Update new file in DB

                    response.Close();
                    return "File upload successful.";
                }
                else
                {
                    return "Invalid file format";
                }
            }
            catch (WebException ex)
            {
                return ex.Message;
            }
        }
        [Route("admin/UploadImage2")]
        [HttpPost]
        public string UploadImage2()
        {
            //this is working only for App_Data, for other folders we do not have write permissions.
            //But unable to read or use the image copied under App_Data due to restrictions in IIS level. Hence FTP is better.
            string strServerImagesPath = ConfigurationManager.AppSettings["UploadImagePath"];
            var file = HttpContext.Current.Request.Files.Count > 0 ?
            HttpContext.Current.Request.Files[0] : null;

            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    //var path = Path.Combine(HttpContext.Current.Server.MapPath("~/zionimages"),fileName );
                    var path = Path.Combine(HttpContext.Current.Server.MapPath("~/App_Data"), fileName);
                    file.SaveAs(path);

                    //file.SaveAs("https://zionwellmark.in/ZionImages/" + fileName);

                    //var inputStream = file.InputStream;
                    //using (var fileStream = System.IO.File.Create(path))
                    //{
                    //    inputStream.CopyTo(fileStream);
                    //}
                    ////WebClient wclient = new WebClient();
                    ////wclient.UploadFile(strServerImagesPath, path);
                    return file != null ? strServerImagesPath + file.FileName : null;
                }
                else
                {
                    return "Invalid file format";
                }
            }
            catch (WebException ex)
            {
                return ex.Message;
            }
        }
        [Route("admin/SaveAllCartItems")]
        [HttpPost]
        public string SaveAllCartItems(List<tblBills> objAllCartItems)
        {
            //https://rajuebps.bsite.net/BellBrand/SaveAllCartItems/OBJALLCARTITEMS
            objDAL.SaveAllCartItems(objAllCartItems);
            return JsonConvert.SerializeObject("Cart items saved Successfully");
        }
        [Route("bell/GetStockDetails/{type}/{category}")]
        [HttpGet]
        public tblItemMaster[] GetStockDetails(string type, string category)
        {
            return objDAL_Bell.GetStockDetails(type, category);
        }
        [Route("bell/GetStockTransactions/{category}/{transtype}/{date1}/{date2}/{username}")]
        [HttpGet]
        public string GetStockTransactions(string category, string transtype, string date1, string date2, string username)
        {
            return objDAL_Bell.GetStockTransactions(category, transtype, date1, date2, username);
        }
    }
}
