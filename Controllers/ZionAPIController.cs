using System.Net;
using System.Net.Http;
using System.Web.Http;
using BellBrandAPI.DataAdapaters;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
//using System.Web.Http.Results;
using Newtonsoft.Json;
//using Newtonsoft.Json.Serialization;
using ZionAPI.DataAdapaters;
using Microsoft.Extensions.Configuration;
using System.Web.Http.Cors;

namespace ZionAPI.Controllers
{
    //Access to XMLHttpRequest at from origin 'http://localhost:4200' has been blocked by CORS policy: No 'Access-Control-Allow-Origin' header is present on the requested resource.
    //[EnableCors(origins: "http://localhost:4200,https://localhost:44328", headers: "*", methods: "*")]
    [EnableCors(origins: "http://localhost:4200,http://localhost:8000,https://ebps.in,https://myorders.zionwellmark.in,https://zionwellmark.in", headers: "*", methods: "*")]
    [Route("[controller]")]
    
    public class ZionAPIController : ApiController
    {
        ILogger _Logger;
        private readonly DAL objDAL;
        public new IConfiguration Configuration { get; }

        public ZionAPIController()
        {
            objDAL = new DAL(Configuration);
        }
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [Route("GetAllCustomers/{id}")]
        [HttpGet]
        public tblCustomers[] GetAllCustomers(string id)
        {
            return objDAL.GetAllCustomers(id);
        }
        [Route("UpsertCustomer")]
        [HttpPost]
        public string UpsertCustomer(tblCustomers objCust)
        {
            return objDAL.UpsertCustomer(objCust);
        }        
        
        [Route("DeleteCustomer")]
        [HttpPost]
        public string DeleteCustomer(int id)
        {
            return objDAL.DeleteCustomer(id);
        }

        //[Route("[action]/{mobile}")]
        [Route("GetCustomersByMobile/{mobile}")]
        [HttpGet]
        public tblCustomers[] GetCustomersByMobile(string mobile)
        {
            return objDAL.GetCustomersByMobile(mobile);
        }

        //[Route("TestGet")]
        //[HttpGet]
        //public string TestGet()
        //{
        //    return "TestGet method is working fine.";
        //}
        [Route("TestGetByStatus/{status}")]
        [HttpGet]
        public string TestGetByStatus(string status) //TestGet
        {
            return "You selected stats = " + status;
        }

        [Route("GetAllOrdersByStatus/{status}")]
        [HttpGet]
        public tblOrders[] GetAllOrdersByStatus(string status)
        {
            return objDAL.GetAllOrdersByStatus(status);
        }

        [Route("UpdateMobileAuthentication")]
        [HttpPost]
        public string UpdateMobileAuthentication(string mobile)
        {
            //objDAL.SaveMobileAuthentication(mobile);
            string objAllItems = objDAL.SaveMobileAuthentication(mobile);
            return objAllItems;
        }
        [Route("GetAllItems")]
        [HttpGet]
        public tblItemMaster[] GetAllItems()
        {
            return objDAL.GetAllItems();
            //return new JsonResult("Added Successfully");
        }
        [Route("SaveAllCartItems")]
        [HttpPost]
        public string SaveAllCartItems(List<tblBills> objAllCartItems)
        {            
            objDAL.SaveAllCartItems(objAllCartItems);
            return JsonConvert.SerializeObject("OK");
        }
        [Route("SaveAllCartItemsNew")]
        [HttpPost]
        public async Task<string> SaveAllCartItemsNew(StringContent strCartContent)
        {
            //List<tblBills> objAllCartItems
            //https://rajuebps.bsite.net/BellBrand/SaveAllCartItems/OBJALLCARTITEMS
            
            var myJson = await strCartContent.ReadAsStringAsync().ConfigureAwait(false);
            List<tblBills> objAllCartItems = JsonConvert.DeserializeObject<List<tblBills>>(myJson);
            //var result = _service.AddAsync(object).ConfigureAwait(false);
            objDAL.SaveAllCartItems(objAllCartItems);
            return JsonConvert.SerializeObject("OK");            
        }
        [Route("SaveAllCartItemsByContent")]
        [HttpPost]
        public string SaveAllCartItemsByContent([FromBody] List<tblBills> objAllCartItems)
        {
            objDAL.SaveAllCartItems(objAllCartItems);
            return JsonConvert.SerializeObject("OK");
        }
        [Route("SaveBillsNew")]
        [HttpPost]
        public string SaveBillsNew(tblBills objbills)
        {
            objDAL.SaveBillNew(objbills);
            return JsonConvert.SerializeObject("Bill details saved Successfully");
        }

        [Route("UpdateOrderStatus")]
        [HttpPost]
        public string UpdateOrderStatus(tblOrders objOrders)
        {
            string strResponse = objDAL.UpdateOrderDetails(objOrders);
            return JsonConvert.SerializeObject(strResponse);
        }
        [Route("GetAllOrderItemsByID/{orderid}")]
        [HttpGet]
        public tblOrderItems[] GetAllOrderItemsByID(int orderid)
        {
            return objDAL.GetAllOrderItemsByID(orderid);            
        }
        [Route("UpdateOrderDetails")]
        [HttpPost]
        public string UpdateOrderDetails(tblOrders objOrders)
        {
            string strResponse = objDAL.UpdateOrderDetails(objOrders);
            return JsonConvert.SerializeObject(strResponse);
        }
        
        [Route("GetAllOrdersByMobile/{mobile}")]
        [HttpGet]
        public tblOrders[] GetAllOrdersByMobile(string mobile)
        {
            return objDAL.GetAllOrdersByMobile(mobile);
        }

        [Route("GetOrderSheetByArea/{area}/{salesman}/{billdate}")]
        [HttpGet]
        public string GetOrderSheetByArea(string area, string salesman, string billdate)
        {                       
            string objAllItems = objDAL.GetOrderSheetByArea(area, salesman, billdate);
            return objAllItems;
        }
        //[HttpGet("[action]/{area}/{salesman}/{billdate}")]  //is also working.
        //[HttpGet("{id}")]

        [Route("GetAreaList")]
        [HttpGet]
        public string GetAreaList()
        {
            string objItems = objDAL.GetAreaList();
            return objItems;
        }

        [Route("GetCustomersByArea/{area}")]
        [HttpGet]
        public string GetCustomersByArea(string area)
        {
            string objAllItems = objDAL.GetCustomersByArea(area);
            return objAllItems;
        }

        //// GET api/customers/page/10/10
        //[HttpGet("page/{skip}/{take}")]
        ////[NoCache]
        ////[ProducesResponseType(typeof(List<Customer>), 200)]
        ////[ProducesResponseType(typeof(ApiResponse), 400)]
        //public JsonResult GetAllOrdersByPageWise(int skip, int take)
        //{
        //    try
        //    {
        //        JsonResult objAllItems = objDAL.GetAllOrdersByPageWise(skip, take);
        //        //Response.Headers.Add("X-InlineCount", pagingResult.TotalRecords.ToString()); //this is for pagewise data
        //        //return Ok(pagingResult.Records);
        //        return objAllItems; 
        //    }
        //    catch (Exception exp)
        //    {
        //        _Logger.LogError(exp.Message);
        //        //return BadRequest(new ApiResponse { Status = false });
        //        return new JsonResult(exp.Message);
        //    }
        //}        

        //[HttpGet("[action]/{mobile}")]
        //public JsonResult GetAllOrdersByMobile(string mobile)
        //{
        //    JsonResult objAllItems = objDAL.GetAllOrdersByMobile(mobile);
        //    return objAllItems;
        //}
        //[HttpGet]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    var rng = new Random();
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateTime.Now.AddDays(index),
        //        TemperatureC = rng.Next(-20, 55),
        //        Summary = Summaries[rng.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}       
        //[HttpGet("{city}/{country}")]
        //public IActionResult Get(string city, string country)
        //{
        //}
        //not required
        //[HttpGet("[action]")]
        //public JsonResult GetSalesManList()
        //{
        //    JsonResult objItems = objDAL.GetSalesManList();
        //    return objItems;
        //}


        [Route("SaveBillsNew2")]
        [HttpPost]
        public string SaveBillsNew2(tblBills objbills2)
        {
            objDAL.SaveBillNew(objbills2);
            return JsonConvert.SerializeObject("Bill details saved Successfully");
        }

        ////[Route("api/Values/SaveBills")]
        //[Route("action")]
        //[HttpPost]
        //public string SaveBills(string area)
        //{
        //    //if (!Request.Content.IsMimeMultipartContent("form-data"))
        //    //    return Request.CreateResponse(HttpStatusCode.InternalServerError);

        //    //objDAL.SaveBill(area);
        //    return "success";
        //}      

        //if (!Request.Content.IsMimeMultipartContent("form-data"))
        //      return Request.CreateResponse(HttpStatusCode.InternalServerError);

        //[HttpGet("action")]
        //public MaserItems GetMaserItems()
        //{
        //    return ReadJsonFile();
        //}

        //public MaserItems ReadJsonFile()
        //{
        //    MaserItems allitems = new MaserItems();
        //    //ItemMaster objitem = new ItemMaster();
        //    string jsontext = System.IO.File.ReadAllText(@"./masteritems.json");
        //    //string json = System.IO.File.ReadAllText(Server.MapPath("~/files/myfile.json"));
        //    //var items = JsonSerializer.Deserialize<ItemMaster>(jsontext);

        //    allitems = Newtonsoft.Json.JsonConvert.DeserializeObject<MaserItems>(jsontext) ?? allitems; 

        //    //allitems = items;
        //    return allitems;
        //    //Console.WriteLine($"First name: {person.FirstName}");
        //    //Console.WriteLine($"Last name: {person.LastName}");
        //    //Console.WriteLine($"Job title: {person.JobTitle}");
        //}
    }
}
