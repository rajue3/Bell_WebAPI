//using BellBrandAPI.Controllers;
using BellBrandAPI.Models;
//using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Data;
using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Specialized;
using System.Net;
using System.Web;
using System.Collections.Generic;
//using System.Text.Json
using Newtonsoft.Json;
using System.Configuration;
using System.Linq;

namespace ZionAPI.DataAdapaters
{
    public class DAL
    {
        //private readonly ILogger<BellController> _logger;
        ILogger _Logger;
        string strDBConnectionString;
        private readonly IConfiguration _configuration;
        
        public DAL(IConfiguration configuration)
        {
            _configuration = configuration;
            //strDBConnectionString = ConfigurationManager.GetConnectionString("BellBrandDBConn");
            strDBConnectionString = ConfigurationManager.ConnectionStrings["ZionDBConnection"].ConnectionString;
        }
        public tblCustomers[] GetAllCustomers(string id)
        {
            DataTable table = new DataTable();
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
            {
                SqlCommand cmd = new SqlCommand("ZION_GET_ALL_CUSTOMERS", myCon);
                cmd.CommandType = CommandType.StoredProcedure;                
                cmd.Parameters.AddWithValue("@ID", id);
                myCon.Open();
                myReader = cmd.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }
            List<tblCustomers> objBillItems = new List<tblCustomers>();
            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow dr in table.Rows)
                {
                    tblCustomers oItem = new tblCustomers();
                    oItem.ID = Convert.ToInt16(dr["ID"]);
                    oItem.Customername = Convert.ToString(dr["CustomerName"]);
                    oItem.Shopname = Convert.ToString(dr["ShopName"]);
                    oItem.AreaName = Convert.ToString(dr["AreaName"]);
                    //oItem.LineName = Convert.ToString(dr["LineName"]);
                    oItem.LandMark = Convert.ToString(dr["LandMark"]);
                    oItem.mobile = Convert.ToString(dr["Mobile"]);
                    oItem.IsAuth = Convert.ToString(dr["IsAuth"]);
                    oItem.Status = Convert.ToString(dr["Status"]);
                    oItem.SubRoute = Convert.ToString(dr["SubRoute"]);
                    objBillItems.Add(oItem);
                }
            }
            return objBillItems.ToArray();
        }
        public string DeleteCustomer(int id)
        {
            DataTable table = new DataTable();
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
            {
                SqlCommand cmd = new SqlCommand("ZION_DELETE_CUSTOMER", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", id);
                myCon.Open();
                myReader = cmd.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }
            return "User deleted successfull!";
        }
        public string UpsertCustomer(tblCustomers users)
        {
            DataTable table = new DataTable();
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
            {
                SqlCommand cmd = new SqlCommand("ZION_INS_UPD_CUSTOMER", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", users.ID);
                cmd.Parameters.AddWithValue("@CUSTOMERNAME", users.Customername);
                cmd.Parameters.AddWithValue("@SHOPNAME", users.Shopname);
                cmd.Parameters.AddWithValue("@AREANAME", users.AreaName);
                cmd.Parameters.AddWithValue("@MOBILE", users.mobile);
                cmd.Parameters.AddWithValue("@LANDMARK", users.LandMark);
                cmd.Parameters.AddWithValue("@ISAUTH", users.IsAuth);
                cmd.Parameters.AddWithValue("@STATUS", users.Status);
                cmd.Parameters.AddWithValue("@SUBROUTE", users.SubRoute);
                myCon.Open();
                myReader = cmd.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }
            return "Update successfull!";
        }
        public tblCustomers[] GetCustomersByMobile(string strMobile)
        {
            List<tblCustomers> objItems = new List<tblCustomers>();
            //string query = @"Select Areaname,salesman,customername,shopname,mobile from dbo.tblCustomers Where status='Active' And Areaname='"+ strArea +"' ";
            DataTable table = new DataTable();
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
            {
                SqlCommand cmd = new SqlCommand("USP_GET_CUSTOMER_INFOBY_MOBILE", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MOBILE", strMobile);
                myCon.Open();
                myReader = cmd.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }
            //convert datatable to object 
            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow dr in table.Rows)
                {
                    tblCustomers oItem = new tblCustomers();
                    oItem.ID = Convert.ToInt16(dr["ID"]);
                    oItem.AreaName = Convert.ToString(dr["AreaName"]);
                    oItem.SalesMan = Convert.ToString(dr["SalesMan"]);
                    oItem.Customername = Convert.ToString(dr["Customername"]);
                    oItem.Shopname = Convert.ToString(dr["Shopname"]);
                    oItem.mobile = Convert.ToString(dr["mobile"]);
                    oItem.AdminMobile = Convert.ToString(dr["AdminMobile"]);
                    oItem.AdminMobile2 = Convert.ToString(dr["AdminMobile2"]);//just to add my mobile#
                    oItem.IsAuth = Convert.ToString(dr["IsAuth"]);
                    objItems.Add(oItem);
                }
                //return new JsonResult(table);
            }
            //return JsonConvert.SerializeObject(table); 
            return objItems.ToArray();
        }
        public tblItemMaster[] GetAllItems()
        {
            List<tblItemMaster> objItems = new List<tblItemMaster>();
            //TODO: change to SP
            //string query = @"Select ItemID,ItemName,Rate,PACKINGTYPE,TOTALITEMSINPACK,CATEGORY from dbo.tblItemMaster";
            DataTable table = new DataTable();
            //string sqlDataSource = _configuration.GetConnectionString("BellBrandDBConn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
            {
                SqlCommand cmd = new SqlCommand("USP_GET_ALLITEMS_Refresh", myCon); //USP_GET_ALLITEMS
                cmd.CommandType = CommandType.StoredProcedure;
                myCon.Open();
                myReader = cmd.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }
            //convert datatable to object 
            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow dr in table.Rows)
                {
                    tblItemMaster oItem = new tblItemMaster();
                    oItem.ID = Convert.ToInt16(dr["ID"]);
                    oItem.ItemName = Convert.ToString(dr["ItemName"]);
                    oItem.MRP = Convert.ToString(dr["MRP"]);
                    oItem.Rate = Convert.ToString(dr["Rate"]);
                    oItem.PACKINGTYPE = Convert.ToString(dr["PACKINGTYPE"]);
                    oItem.ImageUrl = Convert.ToString(dr["ImageUrl"]);
                    oItem.CATEGORY = Convert.ToString(dr["CATEGORY"]);
                    oItem.TOTALITEMSINPACK = Convert.ToString(dr["TOTALITEMSINPACK"]);
                    oItem.Description = Convert.ToString(dr["Description"]);
                    oItem.TOTALITEMSINCARTON = Convert.ToString(dr["TOTALITEMSINCARTON"]);
                    objItems.Add(oItem);
                }
                //return new JsonResult(table);
            }
            //return JsonConvert.SerializeObject(table); 
            return objItems.ToArray();
            //return JsonConvert.SerializeObject(table);
        }        
        public tblItemDetails GetItemByID(int id)
            {
            DataTable table = new DataTable();
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
            {
                SqlCommand cmd = new SqlCommand("USP_GET_AllItemsById", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", id);
                myCon.Open();
                myReader = cmd.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }
            //convert datatable to object 
            tblItemDetails oItem = new tblItemDetails();
            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow dr in table.Rows)
                {
                    oItem.ID = Convert.ToInt16(dr["ID"]);
                    oItem.ITEMNAME = Convert.ToString(dr["ItemName"]);
                    oItem.MRP = Convert.ToString(dr["MRP"]);
                    oItem.RATE = Convert.ToString(dr["Rate"]);
                    oItem.PACKINGTYPE = Convert.ToString(dr["PACKINGTYPE"]);
                    oItem.IMAGEURL = Convert.ToString(dr["ImageUrl"]);
                    oItem.CATEGORY = Convert.ToString(dr["CATEGORY"]);
                    oItem.TOTALITEMSINPACK = Convert.ToString(dr["TOTALITEMSINPACK"]);
                    oItem.DESCRIPTION = Convert.ToString(dr["Description"]);
                    oItem.TOTALITEMSINCARTON = Convert.ToString(dr["TOTALITEMSINCARTON"]);
                }
            }
            return oItem;
        }
        public tblOrders[] GetAllOrdersByStatus(string strStatus)
        {
            List<tblOrders> objOrders = new List<tblOrders>();
            DataTable table = new DataTable();
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
            {
                SqlCommand cmd = new SqlCommand("USP_GET_ALL_OrdersByStatus", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@STATUS", strStatus);
                myCon.Open();
                myReader = cmd.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }
            //convert datatable to object 
            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow dr in table.Rows)
                {
                    tblOrders oItem = new tblOrders();
                    oItem.OrderID = Convert.ToInt16(dr["OrderID"]);
                    oItem.CustomerName = Convert.ToString(dr["CustomerName"]);
                    oItem.Mobile = Convert.ToString(dr["Mobile"]);
                    oItem.Area = Convert.ToString(dr["Area"]);
                    oItem.TotalAmount = Convert.ToString(dr["Amount"]);
                    oItem.OrderDate = Convert.ToString(dr["OrderDate"]);
                    oItem.TotalItems = Convert.ToString(dr["TOTALITEMS"]);
                    oItem.Status = Convert.ToString(dr["Status"]);
                    objOrders.Add(oItem);
                }
            }
            //return JsonConvert.SerializeObject(table);
            return objOrders.ToArray();
        }
        public tblOrders[] GetAllOrdersByMobile(string strMobile)
        {
            List<tblOrders> objOrders = new List<tblOrders>();
            DataTable table = new DataTable();
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
            {
                SqlCommand cmd = new SqlCommand("USP_Get_OrdersByMobile", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MOBILE", strMobile);
                myCon.Open();
                myReader = cmd.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }
            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow dr in table.Rows)
                {
                    tblOrders oItem = new tblOrders();
                    oItem.OrderID = Convert.ToInt16(dr["OrderID"]);
                    oItem.CustomerName = Convert.ToString(dr["CustomerName"]);
                    oItem.Mobile = Convert.ToString(dr["Mobile"]);
                    oItem.Area = Convert.ToString(dr["Area"]);
                    oItem.TotalAmount = Convert.ToString(dr["Amount"]);
                    oItem.OrderDate = Convert.ToString(dr["OrderDate"]);
                    oItem.TotalItems = Convert.ToString(dr["TOTALITEMS"]);
                    oItem.Status = Convert.ToString(dr["Status"]);
                    objOrders.Add(oItem);
                }
            }
            //return JsonConvert.SerializeObject(table);
            return objOrders.ToArray();
            //return JsonConvert.SerializeObject(table);
        }
        public tblOrderItems[] GetAllOrderItemsByID(int orderid)
        {
            List<tblOrderItems> objList = new List<tblOrderItems>();
            DataTable table = new DataTable();
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
            {
                SqlCommand cmd = new SqlCommand("USP_Get_OrderItemsByOrderID", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ORDERID", orderid);
                myCon.Open();
                myReader = cmd.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }
            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow dr in table.Rows)
                {
                    tblOrderItems oItem = new tblOrderItems();
                    oItem.ID = Convert.ToInt16(dr["SNO"]);  //to display serial no. in grid view
                    oItem.OrderID = Convert.ToInt16(dr["OrderID"]);
                    oItem.ItemName = Convert.ToString(dr["ItemName"]);
                    oItem.Rate = Convert.ToInt16(dr["Rate"]);
                    oItem.Qty = Convert.ToString(dr["Qty"]);
                    objList.Add(oItem);
                }
            }
            return objList.ToArray();
            //return JsonConvert.SerializeObject(table);
        }
        public string SaveMobileAuthentication(string strMobile)
        {
            DataTable table = new DataTable();
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
            {
                SqlCommand cmd = new SqlCommand("USP_UpdateMobileAuth", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MOBILE", strMobile);
                myCon.Open();
                myReader = cmd.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }
            return JsonConvert.SerializeObject(table);
        }       

        public string GetAreaList()
        {
            //string query = @"Select AreaName,Salesman from dbo.AreaMaster Where status='Active' ";
            DataTable table = new DataTable();
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
            {
                SqlCommand cmd = new SqlCommand("USP_GET_AREALIST", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@AREA", strArea);
                myCon.Open();
                //cmd.ExecuteNonQuery();
                myReader = cmd.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
                //myCon.Open();
                //using (SqlCommand myCommand = new SqlCommand(query, myCon))
                //{
                //    myReader = myCommand.ExecuteReader();
                //    table.Load(myReader);

                //    myReader.Close();
                //    myCon.Close();
                //}
            }

            return JsonConvert.SerializeObject(table);
        }
        public string GetOrderSheetByArea(string strArea, string strSalesman, string strLSdate)
        {
            //string query = @"Select ID,ItemName,Rate,T_B as Stock,BillDate,Area from dbo.LS where area='" + strArea + "' ";
            DataTable table = new DataTable();
            //string sqlDataSource = _configuration.GetConnectionString("BellBrandDBConn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
            {
                SqlCommand cmd = new SqlCommand("USP_GET_ORDERITEMSBYAREA", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AREA", strArea);
                cmd.Parameters.AddWithValue("@LSDATE", strLSdate);
                cmd.Parameters.AddWithValue("@SALESMAN", strSalesman);
                myCon.Open();
                //cmd.ExecuteNonQuery();
                myReader = cmd.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();

                //using (SqlCommand myCommand = new SqlCommand(query, myCon))
                //{
                //    myReader = myCommand.ExecuteReader();
                //    table.Load(myReader);

                //    myReader.Close();
                //    myCon.Close();
                //}
            }

            return JsonConvert.SerializeObject(table);
        }
        //public JsonResult GetCustomersByMobile(string strMobile)
        //{
        //    //string query = @"Select Areaname,salesman,customername,shopname,mobile from dbo.tblCustomers Where status='Active' And Areaname='"+ strArea +"' ";
        //    DataTable table = new DataTable();
        //    SqlDataReader myReader;
        //    using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand("USP_GET_CUSTOMERSBYMOBILE", myCon);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@MOBILE", strMobile);
        //        myCon.Open();
        //        myReader = cmd.ExecuteReader();
        //        table.Load(myReader);
        //        myReader.Close();
        //        myCon.Close();
        //    }
        //    return new JsonResult(table);
        //}
        public string GetCustomersByArea(string strArea)
        {
            //string query = @"Select Areaname,salesman,customername,shopname,mobile from dbo.tblCustomers Where status='Active' And Areaname='"+ strArea +"' ";
            DataTable table = new DataTable();
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
            {
                SqlCommand cmd = new SqlCommand("USP_GET_CUSTOMERSBYAREA", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AREA", strArea);
                myCon.Open();
                myReader = cmd.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }
            return JsonConvert.SerializeObject(table);
        }
        //public JsonResult GetAllOrdersByPageWise(int skip,int take)
        //{
        //    DataTable dtData = new DataTable();
        //    SqlDataReader myReader;
        //    using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand("USP_GET_ALL_OrdersByStatus", myCon);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@STATUS", "ALL");
        //        myCon.Open();
        //        myReader = cmd.ExecuteReader();
        //        dtData.Load(myReader);
        //        myReader.Close();
        //        myCon.Close();
        //    }
        //    List<tblBills> objItems = new List<tblBills>();
        //    tblBills objOrder;
        //    foreach (DataRow dr in dtData.Rows)
        //    {
        //        objOrder = new tblBills();
        //        //objOrder.ID = dr("ID");
        //        objItems.Add(objOrder);
        //    }
        //    var customers = objOrder.
        //                        .OrderBy(c => c.LastName)
        //                        .Include(c => c.State)
        //                        .Include(c => c.Orders)
        //                        .Skip(skip)
        //                        .Take(take)
        //                        .ToListAsync();
        //    return new JsonResult(dtData);

        //}        
        
        public string SaveAllCartItems(List<tblBills> objItems)
        {
            DataTable table = new DataTable();
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
            {
                myCon.Open();
                
                SqlCommand cmd = new SqlCommand("USP_SAVE_ORDER_DETAILS", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CUSTOMER", objItems[0].Customer);
                cmd.Parameters.AddWithValue("@AREA", objItems[0].Area);
                cmd.Parameters.AddWithValue("@MOBILE", objItems[0].Mobile);
                cmd.Parameters.AddWithValue("@AMOUNT", objItems[objItems.Count - 1].TotalAmount);  //as the actual Total amt is adding to the last record.
                cmd.Parameters.AddWithValue("@ORDERDATE", objItems[0].BillDate);
                myReader = cmd.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                
                int OrderID = Convert.ToInt32(table.Rows[0][0]);

                foreach (tblBills p in objItems)
                {
                    cmd = new SqlCommand("USP_SAVE_ORDER_ITEMDETAILS", myCon);
                    cmd.CommandType = CommandType.StoredProcedure;                    
                    cmd.Parameters.AddWithValue("@ITEMNAME", p.ItemName);
                    cmd.Parameters.AddWithValue("@RATE", p.Rate);
                    cmd.Parameters.AddWithValue("@QTY", p.Qty);
                    cmd.Parameters.AddWithValue("@ORDERDATE", p.BillDate);
                    cmd.Parameters.AddWithValue("@ORDERID", OrderID);
                    cmd.ExecuteNonQuery();
                }
                myCon.Close();
            }
            return "OK";
        }
        public string SaveBillNew(tblBills p)
        {
            //string query = @"Insert into dbo.Bills(ItemName,Rate,Packets,Amount,BillDate,Area,Salesman) values ('" + p.ItemName + "'," + p.Rate + "," + p.Qty + "," + p.Amount + ",'" + p.BillDate + "','" + p.Area + "','" + p.Salesman + "'); \n";
            //DataTable table = new DataTable();
            //string sqlDataSource = _configuration.GetConnectionString("BellBrandDBConn");
            using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
            {
                //myCon.Open();
                //using (SqlCommand myCommand = new SqlCommand(query, myCon))
                //{
                //    myCommand.ExecuteNonQuery();
                //    myCon.Close();
                //}

                SqlCommand cmd = new SqlCommand("USP_SAVE_BILLS", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ITEMNAME", p.ItemName);
                cmd.Parameters.AddWithValue("@RATE", p.Rate);
                cmd.Parameters.AddWithValue("@QTY", p.Qty);
                cmd.Parameters.AddWithValue("@AMOUNT", p.Amount);
                cmd.Parameters.AddWithValue("@BILLDATE", p.BillDate);
                cmd.Parameters.AddWithValue("@AREA", p.Area);
                cmd.Parameters.AddWithValue("@SALESMAN", p.Salesman);
                cmd.Parameters.AddWithValue("@CUSTOMER", p.Customer);
                myCon.Open();
                cmd.ExecuteNonQuery();
                myCon.Close();
            }
            return "Bill details saved Successfully";

        }
        public string UpdateOrderDetails(tblOrders p)
        {
            using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
            {
                SqlCommand cmd = new SqlCommand("USP_UPDATE_ORDER_DETAILS", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CUSTOMER", p.CustomerName);
                cmd.Parameters.AddWithValue("@ORDERID", p.OrderID);
                cmd.Parameters.AddWithValue("@STATUS", p.Status);
                myCon.Open();
                cmd.ExecuteNonQuery();
                myCon.Close();
            }
            return "Order status updated Successfully";
        }

        //not using this
        public string GetLoading_Items(string strArea,string strSalesman, string strBilldate)
        {
            strBilldate = DateTime.ParseExact(strBilldate,"d",null).ToString();
            //DateTime myDate;
            //if (!DateTime.TryParse(dateString, out myDate))
            //{
            //    // handle parse failure
            //}

            string query = @"Select ID,ItemName,Rate,T_B as Qty,BillDate from dbo.LS where area='" + strArea + "' and Salesman='" + strSalesman + "' and Billdate='" + strBilldate + "'  ";
            DataTable table = new DataTable();
            //string sqlDataSource = _configuration.GetConnectionString("BellBrandDBConn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return JsonConvert.SerializeObject(table);
        }
        

        //NOT USING
        public string GetSalesManList()
        {
            string query = @"Select ItemValue from dbo.MasterItems where ItemType='salesman' ";
            DataTable table = new DataTable();
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return JsonConvert.SerializeObject(table);
        }
        
        public string SaveBill(string strArea)  //tblBills p
        {           
            //var request = HttpContext.Current.Request;
            
            //string vals = request.Form.GetValues("medicineOrder").First();

            //NameValueCollection vals2 = HttpUtility.ParseQueryString(vals);

            ////example values:
            //string value1 = vals2.GetValues("username").First();
            //string value2 = vals2.GetValues("FullName").First();

            //int count = vals2.Count; // 11

            

            //string query = @"Insert into dbo.Bills(ItemName,Rate,Packets,BillDate,Area) values ('" + p.ItemName + "'," + p.Rate + "," + p.Qty + ",'" + p.BillDate + "','" + p.Area + "'); \n";
            ////query = query + @"Update dbo.LS set T_B=T_B - " + p.Qty + " where ItemName='" + p.ItemName + "' and Area='" + p.Area + "' ";
            //// and BillDate='" + p.BillDate + "'  //billdate cannot be considered incase if sales man work more than a day

            //DataTable table = new DataTable();
            ////string sqlDataSource = _configuration.GetConnectionString("BellBrandDBConn");
            //using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
            //{
            //    myCon.Open();
            //    using (SqlCommand myCommand = new SqlCommand(query, myCon))
            //    {
            //        myCommand.ExecuteNonQuery();
            //        myCon.Close();
            //    }
            //}
            
            return "Bill details saved Successfully";
        }

        #region Admin section
        
        public string SaveNewItem(tblItemMaster objItem)
        {
            DataTable table = new DataTable();
            //SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
            {
                myCon.Open();

                SqlCommand cmd = new SqlCommand("USP_SAVE_UPDATE_ITEM_DETAILS", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", objItem.ID);  //IF ID > = 0 THEN UPDATE
                cmd.Parameters.AddWithValue("@IMAGEURL", objItem.ImageUrl);
                cmd.Parameters.AddWithValue("@ITEMNAME", objItem.ItemName);
                cmd.Parameters.AddWithValue("@MRP", objItem.MRP);
                cmd.Parameters.AddWithValue("@RATE", objItem.Rate);
                cmd.Parameters.AddWithValue("@QTY", objItem.Qty);
                cmd.Parameters.AddWithValue("@CATEGORY", objItem.CATEGORY);
                cmd.Parameters.AddWithValue("@PACKINGTYPE", objItem.PACKINGTYPE);
                cmd.Parameters.AddWithValue("@TOTALITEMSINCARTON", objItem.TOTALITEMSINCARTON);
                cmd.Parameters.AddWithValue("@TOTALITEMSINPACK", objItem.TOTALITEMSINPACK);
                cmd.Parameters.AddWithValue("@DESCRIPTION", objItem.Description);

                cmd.ExecuteNonQuery();
                myCon.Close();
            }
            if (objItem.ID > 0) { 
               return "Item details updated successfully";
            }
            else { 
                return "New Item added successfully"; }
        }

        public string SaveItemDetails(tblItemDetails objItem)
        {
            using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
            {
                myCon.Open();

                SqlCommand cmd = new SqlCommand("USP_Update_ItemDetails", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", objItem.ID);
                cmd.Parameters.AddWithValue("@ITEMNAME", objItem.ITEMNAME);
                cmd.Parameters.AddWithValue("@RATE", objItem.RATE);
                cmd.Parameters.AddWithValue("@MRP", objItem.MRP);
                cmd.Parameters.AddWithValue("@PACKINGTYPE", objItem.PACKINGTYPE);
                cmd.Parameters.AddWithValue("@TOTALITEMSINPACK", objItem.TOTALITEMSINPACK);
                cmd.Parameters.AddWithValue("@TOTALITEMSINCARTON", objItem.TOTALITEMSINCARTON);
                cmd.Parameters.AddWithValue("@CATEGORY", objItem.CATEGORY);
                cmd.Parameters.AddWithValue("@DESCRIPTION", objItem.DESCRIPTION);

                if (objItem.ID == 0)
                {                    
                    string imageName = String.Concat(objItem.ITEMNAME.Where(c => !Char.IsWhiteSpace(c)));
                    cmd.Parameters.AddWithValue("@IMAGEURL", imageName + GetRandomString(4)+".jpeg");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@IMAGEURL", objItem.IMAGEURL);
                }

                cmd.ExecuteNonQuery();
                myCon.Close();
            }
            return "Item details saved Successfully";
        }

        private static Random rndm = new Random();
        public static string GetRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[rndm.Next(s.Length)]).ToArray());            
        }
        public string DeleteItem(int ID)
        {
            using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
            {
                myCon.Open();

                SqlCommand cmd = new SqlCommand("USP_DELETE_ITEM", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID",ID);
                cmd.ExecuteNonQuery();
                myCon.Close();
            }
            return "Item details DELETED Successfully";
        }

        #endregion
    }
}
