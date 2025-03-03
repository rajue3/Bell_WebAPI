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
using System.Text;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web.Services.Description;
using System.Web.UI.WebControls;

namespace ZionAPI.DataAdapaters
{
    public class DAL_Bell
    {
        ILogger _Logger;
        string strDBConnectionString;
        private readonly IConfiguration _configuration;

        public DAL_Bell(IConfiguration configuration)
        {
            _configuration = configuration;
            //strDBConnectionString = ConfigurationManager.GetConnectionString("BellBrandDBConn");
            strDBConnectionString = ConfigurationManager.ConnectionStrings["ZionDBConnection"].ConnectionString;
        }
        public tblUsers[] getallusers(int id)
        {
            DataTable table = new DataTable();
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
            {
                SqlCommand cmd = new SqlCommand("USP_GET_ALL_USERS", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@USERID", id);
                myCon.Open();
                myReader = cmd.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }
            List<tblUsers> objBillItems = new List<tblUsers>();
            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow dr in table.Rows)
                {
                    tblUsers oItem = new tblUsers();
                    oItem.id = Convert.ToInt16(dr["ID"]);
                    oItem.username = Convert.ToString(dr["USERNAME"]);
                    oItem.password = Convert.ToString(dr["PASSWORD"]);
                    oItem.firstname = Convert.ToString(dr["FIRSTNAME"]);
                    oItem.lastname = Convert.ToString(dr["LASTNAME"]);
                    oItem.status = Convert.ToString(dr["STATUS"]);
                    oItem.ActionDate = Convert.ToString(dr["ACTIONDATE"]);
                    objBillItems.Add(oItem);
                }
            }
            return objBillItems.ToArray();
        }
        public tblUsers validateuser(tblUsers users)
        {
            DataTable table = new DataTable();
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
            {
                SqlCommand cmd = new SqlCommand("USP_VALIDATE_USER", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@USERNAME", users.username);
                cmd.Parameters.AddWithValue("@PASSWORD", users.password);
                cmd.Parameters.AddWithValue("@USERTYPE", users.usertype);
                myCon.Open();
                myReader = cmd.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }
            tblUsers oItem = new tblUsers();
            if (table != null && table.Rows.Count > 0)
            {
                DataRow dr = table.Rows[0];
                oItem.id = Convert.ToInt16(dr["ID"]);
                oItem.username = Convert.ToString(dr["USERNAME"]);
                oItem.firstname = Convert.ToString(dr["FIRSTNAME"]);
                oItem.lastname = Convert.ToString(dr["LASTNAME"]);
                oItem.usertype = Convert.ToString(dr["USERTYPE"]);
                oItem.status = Convert.ToString(dr["STATUS"]);
            }
            return oItem;
        }
        public tblUsers authenticate(string username, string password, string usertype)
        {
            DataTable table = new DataTable();
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
            {
                SqlCommand cmd = new SqlCommand("USP_VALIDATE_USER", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@USERNAME", username);
                cmd.Parameters.AddWithValue("@PASSWORD", password);
                cmd.Parameters.AddWithValue("@USERTYPE", usertype);
                myCon.Open();
                myReader = cmd.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }
            tblUsers oItem = new tblUsers();
            if (table != null && table.Rows.Count > 0)
            {
                DataRow dr = table.Rows[0];
                oItem.id = Convert.ToInt16(dr["ID"]);
                oItem.username = Convert.ToString(dr["USERNAME"]);
                oItem.firstname = Convert.ToString(dr["FIRSTNAME"]);
                oItem.lastname = Convert.ToString(dr["LASTNAME"]);
                oItem.usertype = Convert.ToString(dr["USERTYPE"]);
                oItem.status = Convert.ToString(dr["STATUS"]);
            }
            return oItem;
        }
        public tblItemMaster[] GetStockDetails(string strType, string category)
        {
            DataTable table = new DataTable();
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
            {
                SqlCommand cmd = new SqlCommand("BELL_STOCK_DETAILS", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Type", strType);
                cmd.Parameters.AddWithValue("@CATEGORY", category);
                //cmd.Parameters.AddWithValue("@FROMDATE", Convert.ToDateTime(date1));
                //cmd.Parameters.AddWithValue("@TODATE", Convert.ToDateTime(date2));
                myCon.Open();
                myReader = cmd.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }
            List<tblItemMaster> objBillItems = new List<tblItemMaster>();
            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow dr in table.Rows)
                {
                    tblItemMaster oItem = new tblItemMaster();
                    oItem.ItemName = Convert.ToString(dr["ITEMNAME"]);
                    oItem.ItemCode = Convert.ToString(dr["ITEMCODE"]);
                    oItem.PRate = Convert.ToDecimal(dr["PRATE"]).ToString("0.00");
                    oItem.Rate = Convert.ToDecimal(dr["Rate"]).ToString("0.00");
                    oItem.MRP = Convert.ToDecimal(dr["MRP"]).ToString("0.00");
                    oItem.CATEGORY = Convert.ToString(dr["CATEGORY"]);
                    oItem.Qty = Convert.ToString(dr["QTY"]);
                    oItem.Manufacture = Convert.ToString(dr["Manufacture"]);
                    oItem.PACKINGTYPE = Convert.ToString(dr["PACKINGTYPE"]);
                    oItem.Description = Convert.ToString(dr["Description"]);
                    oItem.TOTALITEMSINPACK = Convert.ToString(dr["TOTALITEMSINPACK"]);
                    oItem.MinOrderAlert = Convert.ToInt32(dr["MinOrderAlert"]);
                    oItem.STOCK = Convert.ToInt64(dr["STOCK"]);
                    oItem.Cartons = Convert.ToInt64(dr["Cartons"]);
                    oItem.Packets = Convert.ToInt64(dr["Packets"]);                    
                    objBillItems.Add(oItem);
                }
            }
            return objBillItems.ToArray();
        }
        public tblItemMaster[] GetPendingOrders(string strType, string strGroupBy)
        {
            DataTable table = new DataTable();
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
            {
                SqlCommand cmd = new SqlCommand("BELL_GET_PENDING_ORDERS", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Type", strType);
                cmd.Parameters.AddWithValue("@GROUPBYLINE", strGroupBy);
                myCon.Open();
                myReader = cmd.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }
            List<tblItemMaster> objBillItems = new List<tblItemMaster>();
            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow dr in table.Rows)
                {
                    tblItemMaster oItem = new tblItemMaster();
                    oItem.ItemCode = Convert.ToString(dr["ITEMCODE"]);
                    oItem.ItemName = Convert.ToString(dr["ITEMNAME"]);
                    //oItem.TOTALITEMSINPACK = Convert.ToString(dr["TOTALITEMSINPACK"]);
                    oItem.TOTAL_PACKS = Convert.ToInt64(dr["TOTAL_PACKS"]);
                    oItem.RETURN_PACKS = Convert.ToInt64(dr["RETURN_PACKS"]);
                    oItem.DAMAGE_PACKS = Convert.ToInt64(dr["DAMAGE_PACKS"]);
                    //actual stock out
                    oItem.STOCK = Convert.ToInt64(dr["TOTAL_PACKS"]) - Convert.ToInt64(dr["RETURN_PACKS"]) - Convert.ToInt64(dr["DAMAGE_PACKS"]);
                    oItem.ActionDate = Convert.ToString(dr["BILLDATE"]);
                    oItem.USERNAME = Convert.ToString(dr["USERNAME"]);
                    oItem.LINE = Convert.ToString(dr["LINE"]);

                    objBillItems.Add(oItem);
                }
            }
            return objBillItems.ToArray();
        }
        public string GetStockTransactions(string category, string transtype, string date1, string date2, string strUserName)
        {
            DataTable table = new DataTable();
            SqlDataReader myReader;
            string strProcedure = "BELL_GET_STOCK_TRANSACTIONS";
            using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
            {
                SqlCommand cmd = new SqlCommand(strProcedure, myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CATEGORY", category);
                cmd.Parameters.AddWithValue("@TRANS_TYPE", transtype);
                cmd.Parameters.AddWithValue("@FROMDATE", date1);
                cmd.Parameters.AddWithValue("@TODATE", date2);
                cmd.Parameters.AddWithValue("@USERNAME", strUserName);

                myCon.Open();
                myReader = cmd.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }
            //return ConvertDataTableToJson(table);
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            int varCounter = 1;
            foreach (DataRow dr in table.Rows)
            {
                row = new Dictionary<string, object>();
                row.Add("SNO.", varCounter++);
                foreach (DataColumn col in table.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
        }
        public tblUsers saveuserdetails(tblUsers users)
        {
            DataTable table = new DataTable();
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
            {
                SqlCommand cmd = new SqlCommand("USP_SAVE_USER_DETAILS", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", users.id);
                cmd.Parameters.AddWithValue("@USERNAME", users.username);
                cmd.Parameters.AddWithValue("@PASSWORD", users.password);
                cmd.Parameters.AddWithValue("@FIRSTNAME", users.firstname);
                cmd.Parameters.AddWithValue("@LASTNAME", users.lastname);
                myCon.Open();
                myReader = cmd.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }
            tblUsers oItem = new tblUsers();
            if (table != null && table.Rows.Count > 0)
            {
                DataRow dr = table.Rows[0];
                oItem.id = Convert.ToInt16(dr["ID"]);
                oItem.username = Convert.ToString(dr["USERNAME"]);
                oItem.usertype = Convert.ToString(dr["USERTYPE"]);
            }
            return oItem;
        }
        public string deleteuser(int id)
        {
            DataTable table = new DataTable();
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
            {
                SqlCommand cmd = new SqlCommand("USP_DELETE_USER", myCon);
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
        public string updatedUserDetails(tblUsers users)
        {
            DataTable table = new DataTable();
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
            {
                SqlCommand cmd = new SqlCommand("USP_SAVE_USER_DETAILS", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", users.id);
                cmd.Parameters.AddWithValue("@USERNAME", users.username);
                cmd.Parameters.AddWithValue("@PASSWORD", users.password);
                cmd.Parameters.AddWithValue("@FIRSTNAME", users.firstname);
                cmd.Parameters.AddWithValue("@LASTNAME", users.lastname);
                myCon.Open();
                myReader = cmd.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }
            return "Update successfull!";
        }
        public string UpdatedPurchareRateMinOrder(tblItemMaster item)
        {
            DataTable table = new DataTable();
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
            {
                SqlCommand cmd = new SqlCommand("BELL_UPD_ITEMS_PRATE_MINORDER", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ITEMCODE", item.ItemCode);
                cmd.Parameters.AddWithValue("@PRATE", item.PRate);
                cmd.Parameters.AddWithValue("@MINORDERALERT", item.MinOrderAlert);
                cmd.Parameters.AddWithValue("@STOCK", item.STOCK);
                cmd.Parameters.AddWithValue("@USERNAME", item.USERNAME);
                myCon.Open();
                myReader = cmd.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }
            return "Update successfull!";
        }
        public BellAreaMaster[] Bell_GetAreaList(string strType, string date1, string date2)
        {
            DataTable table = new DataTable();
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
            {
                SqlCommand cmd = new SqlCommand("USP_GET_AREALIST", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Type", strType);
                if (date1 != "n")
                    cmd.Parameters.AddWithValue("@FROMDATE", Convert.ToDateTime(date1));
                if (date2 != "n")
                    cmd.Parameters.AddWithValue("@TODATE", Convert.ToDateTime(date2));
                myCon.Open();
                myReader = cmd.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }
            List<BellAreaMaster> objBillItems = new List<BellAreaMaster>();
            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow dr in table.Rows)
                {
                    BellAreaMaster oItem = new BellAreaMaster();
                    oItem.Line = Convert.ToString(dr["LINE"]);
                    oItem.Area = Convert.ToString(dr["AREA"]);
                    oItem.Shop = Convert.ToString(dr["ShopName"]);
                    //oItem.Customer = Convert.ToString(dr["CustomerName"]);
                    objBillItems.Add(oItem);
                }
            }
            return objBillItems.ToArray();
        }

        //Code not completed, currenlty using GetAllLSItems to get ItemNames -09-06-24
        public tblBills_Bell[] GetAllItems(string area, string shopname, string date1, string date2)
        {
            DataTable table = new DataTable();
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
            {
                SqlCommand cmd = new SqlCommand("BELL_GET_All_Items", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AREA", area);
                cmd.Parameters.AddWithValue("@SHOPNAME", shopname);
                cmd.Parameters.AddWithValue("@FROMDATE", Convert.ToDateTime(date1));
                cmd.Parameters.AddWithValue("@TODATE", Convert.ToDateTime(date2));
                myCon.Open();
                myReader = cmd.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }
            List<tblBills_Bell> objBillItems = new List<tblBills_Bell>();
            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow dr in table.Rows)
                {
                    tblBills_Bell oItem = new tblBills_Bell();
                    oItem.ItemName = Convert.ToString(dr["ITEMNAME"]);
                    objBillItems.Add(oItem);
                }
            }
            return objBillItems.ToArray();
        }
        public tblBills_Bell[] GetAllLSItems(string area, string shopname)
        {
            DataTable table = new DataTable();
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
            {
                SqlCommand cmd = new SqlCommand("BELL_SP_GET_All_LS_Items", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AREA", area);
                cmd.Parameters.AddWithValue("@SHOPNAME", shopname);
                myCon.Open();
                myReader = cmd.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }
            List<tblBills_Bell> objBillItems = new List<tblBills_Bell>();
            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow dr in table.Rows)
                {
                    tblBills_Bell oItem = new tblBills_Bell();
                    oItem.ID = Convert.ToInt16(dr["BILLID"]);
                    oItem.ItemName = Convert.ToString(dr["ITEMNAME"]);
                    oItem.Qty = Convert.ToString(dr["QTY"]);
                    oItem.Rate = Convert.ToDecimal(dr["Rate"]).ToString("0.00");
                    oItem.Amount = Convert.ToDecimal(dr["Amount"]).ToString("0.00");
                    oItem.BillDate = Convert.ToString(dr["BILLDATE"]);
                    oItem.BillNo = Convert.ToInt16(dr["BILLNUMBER"]);
                    objBillItems.Add(oItem);
                }
            }
            return objBillItems.ToArray();
        }
        public tblBills_Bell[] GetAllLSCustomers()
        {
            DataTable table = new DataTable();
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
            {
                SqlCommand cmd = new SqlCommand("BELL_SP_GET_All_LS_Customers", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Area", "");
                //cmd.Parameters.AddWithValue("@FROMDATE", null);
                //cmd.Parameters.AddWithValue("@TODATE", null);
                myCon.Open();
                myReader = cmd.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }
            List<tblBills_Bell> objBillItems = new List<tblBills_Bell>();
            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow dr in table.Rows)
                {
                    tblBills_Bell oItem = new tblBills_Bell();
                    oItem.Area = Convert.ToString(dr["AREA"]);
                    oItem.ShopName = Convert.ToString(dr["ShopName"]);
                    oItem.TotalItems = Convert.ToString(dr["TotalItems"]);
                    oItem.TotalAmount = Convert.ToDecimal(dr["TotalAmount"]).ToString("0,00");
                    objBillItems.Add(oItem);
                }
            }
            return objBillItems.ToArray();
        }
        public tblBellCustomers GetLSCustomerDetails(int ID)
        {
            DataTable table = new DataTable();
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
            {
                SqlCommand cmd = new SqlCommand("Bell_GET_CustomerDetails", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ID);
                myCon.Open();
                myReader = cmd.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }
            tblBellCustomers objDetails = new tblBellCustomers();
            if (table != null && table.Rows.Count > 0)
            {
                DataRow dr = table.Rows[0];
                objDetails.Area = Convert.ToString(dr["AREA"]);
                objDetails.ShopName = Convert.ToString(dr["ShopName"]);
                objDetails.CustomerName = Convert.ToString(dr["CustomerName"]);
                objDetails.SalesMan = Convert.ToString(dr["SalesMan"]);
                objDetails.Mobile = Convert.ToString(dr["Mobile"]);
                objDetails.LAT = Convert.ToString(dr["LAT"]);
                objDetails.LNG = Convert.ToString(dr["LNG"]);
            }
            return objDetails;
        }
        public tblBell_Orders[] GetLSCustomersByAreaShop(string strArea, string strShop)
        {
            DataTable table = new DataTable();
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
            {
                SqlCommand cmd = new SqlCommand("BELL_SP_GET_All_Customer_BILL_DETAILS", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Area", strArea);
                cmd.Parameters.AddWithValue("@Shop", strShop.Replace('@', '/').Replace('$', '&'));
                //cmd.Parameters.AddWithValue("@FROMDATE", null);
                //cmd.Parameters.AddWithValue("@TODATE", null);
                myCon.Open();
                myReader = cmd.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }
            List<tblBell_Orders> objBillItems = new List<tblBell_Orders>();
            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow dr in table.Rows)
                {
                    tblBell_Orders oItem = new tblBell_Orders();
                    oItem.Area = Convert.ToString(dr["AREA"]);
                    oItem.ShopName = Convert.ToString(dr["ShopName"]);
                    oItem.Customer = Convert.ToString(dr["CustomerName"]);
                    oItem.TotalItems = Convert.ToString(dr["TotalItems"]);
                    oItem.TotalAmount = Convert.ToDecimal(dr["TotalAmount"]).ToString("0,00");
                    //oItem.TotalAmount = Convert.ToDecimal(dr["TotalAmount"]);
                    //oItem.TotalAmount = decimal.Round(oItem.TotalAmount, 2, MidpointRounding.AwayFromZero);
                    objBillItems.Add(oItem);
                }
            }
            return objBillItems.ToArray();
        }
        //public tblBills_Bell[] GetLSItemsByDate(string strArea, string date1, string date2)
        //{
        //    DataTable table = new DataTable();
        //    SqlDataReader myReader;
        //    using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand("BELL_GET_LS_ItemsByDate", myCon);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@Area", strArea);
        //        cmd.Parameters.AddWithValue("@FROMDATE", Convert.ToDateTime(date1));
        //        cmd.Parameters.AddWithValue("@TODATE", Convert.ToDateTime(date2));
        //        myCon.Open();
        //        myReader = cmd.ExecuteReader();
        //        table.Load(myReader);
        //        myReader.Close();
        //        myCon.Close();
        //    }
        //    List<tblBills_Bell> objBillItems = new List<tblBills_Bell>();
        //    if (table != null && table.Rows.Count > 0)
        //    {
        //        foreach (DataRow dr in table.Rows)
        //        {
        //            tblBills_Bell oItem = new tblBills_Bell();
        //            oItem.Area = Convert.ToString(dr["AREA"]);
        //            oItem.ShopName = Convert.ToString(dr["ShopName"]);
        //            oItem.BillDate = Convert.ToString(dr["BillDate"]);
        //            oItem.ItemName = Convert.ToString(dr["ItemName"]);
        //            //oItem.Rate = Convert.ToDecimal(dr["Rate"]).ToString();
        //            oItem.Rate = Convert.ToDecimal(dr["Rate"]).ToString("0.00");
        //            oItem.Qty = Convert.ToString(dr["Qty"]);
        //            oItem.Amount = Convert.ToDecimal(dr["Amount"]).ToString("0.00");
        //            //oItem.Amount = decimal.Round(oItem.Amount, 2, MidpointRounding.AwayFromZero);
        //            objBillItems.Add(oItem);
        //        }
        //    }
        //    return objBillItems.ToArray();
        //}
        public tblSalesReportNew[] GetLSTotalSalesByArea_New(string strArea, string date1, string searchtype)
        {
            DataTable table = new DataTable();
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
            {
                SqlCommand cmd = new SqlCommand("BELL_GET_TOTAL_SALES_BY_AREA_NEW", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Area", strArea);
                cmd.Parameters.AddWithValue("@FROMDATE", Convert.ToDateTime(date1));
                cmd.Parameters.AddWithValue("@SEARCHBY", searchtype);
                myCon.Open();
                myReader = cmd.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }
            List<tblSalesReportNew> objBillItems = new List<tblSalesReportNew>();
            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow dr in table.Rows)
                {
                    tblSalesReportNew oItem = new tblSalesReportNew();
                    oItem.TotalBills = Convert.ToInt32(dr["TotalBills"]);
                    oItem.Area = Convert.ToString(dr["AREA"]);
                    oItem.BillDate = Convert.ToString(dr["BillDate"]);
                    //oItem.ActionDate = Convert.ToString(dr["ActionDate"]);
                    oItem.Purchase_Amount = Convert.ToDecimal(dr["Purchase_Amount"]).ToString("0.00");
                    oItem.Amount = Convert.ToDecimal(dr["Amount"]).ToString("0.00"); //sales amount
                    oItem.Profit_Amount = Convert.ToDecimal(dr["Profit_Amount"]).ToString("0.00");
                    oItem.Profit_Percent = Convert.ToDecimal(dr["Profit_Percent"]).ToString("0.00") + "%";
                    oItem.UserName = Convert.ToString(dr["USERNAME"]);
                    objBillItems.Add(oItem);
                }
            }
            return objBillItems.ToArray();
        }
        public string GetTotalSalesByShop(string strArea, string strAreaLine, string strShop, string date1, string date2, int totalAmount)
        {
            DataTable table = new DataTable();
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
            {
                SqlCommand cmd = new SqlCommand("BELL_SHOP_WISE_TOTAL_SALES", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Area", strArea);
                cmd.Parameters.AddWithValue("@Area_Line", strAreaLine);
                cmd.Parameters.AddWithValue("@SHOP", strShop.Replace('@', '/').Replace('$', '&'));
                cmd.Parameters.AddWithValue("@BILLDATE1", Convert.ToDateTime(date1));
                cmd.Parameters.AddWithValue("@BILLDATE2", Convert.ToDateTime(date2));
                cmd.Parameters.AddWithValue("@AMOUNT", totalAmount);
                myCon.Open();
                myReader = cmd.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            int varCounter = 1;
            foreach (DataRow dr in table.Rows)
            {
                row = new Dictionary<string, object>();
                row.Add("SNO.", varCounter++);
                foreach (DataColumn col in table.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
        }
        public tblSalesReportNew[] GetLSTotalSalesByArea(string strArea, string date1, string date2)
        {
            DataTable table = new DataTable();
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
            {
                SqlCommand cmd = new SqlCommand("BELL_GET_TOTAL_SALES_BY_AREA", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Area", strArea);
                cmd.Parameters.AddWithValue("@FROMDATE", Convert.ToDateTime(date1));
                cmd.Parameters.AddWithValue("@TODATE", Convert.ToDateTime(date2));
                myCon.Open();
                myReader = cmd.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }
            List<tblSalesReportNew> objBillItems = new List<tblSalesReportNew>();
            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow dr in table.Rows)
                {
                    tblSalesReportNew oItem = new tblSalesReportNew();
                    oItem.TotalBills = Convert.ToInt32(dr["TotalBills"]);
                    oItem.Area = Convert.ToString(dr["AREA"]);
                    oItem.BillDate = Convert.ToString(dr["BillDate"]);
                    oItem.Amount = Convert.ToDecimal(dr["Amount"]).ToString("0.00");
                    objBillItems.Add(oItem);
                }
            }
            return objBillItems.ToArray();
        }
        public tblBills_Bell[] GetLSItemsByAreaDate(string strReportName, string strArea, string strShop, string billDate)
        {
            DataTable table = new DataTable();
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
            {
                SqlCommand cmd = new SqlCommand("BELL_GET_LS_ItemsByArea_Date", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ReportName", strReportName);
                cmd.Parameters.AddWithValue("@Area", strArea);
                cmd.Parameters.AddWithValue("@Shop", strShop.Replace('@', '/').Replace('$', '&'));
                cmd.Parameters.AddWithValue("@BILLDATE", Convert.ToDateTime(billDate));
                myCon.Open();
                myReader = cmd.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }
            List<tblBills_Bell> objBillItems = new List<tblBills_Bell>();
            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow dr in table.Rows)
                {
                    tblBills_Bell oItem = new tblBills_Bell();
                    //oItem.ID = Convert.ToInt32(dr["CUSTID"]);
                    oItem.Area = Convert.ToString(dr["AREA"]);
                    oItem.ItemName = Convert.ToString(dr["ItemName"]);
                    //oItem.Rate = Convert.ToDecimal(dr["Rate"]).ToString("0.00");
                    oItem.Qty = Convert.ToString(dr["Qty"]);
                    oItem.Ret_Qty = Convert.ToString(dr["Ret_Qty"]);
                    oItem.Amount = Convert.ToDecimal(dr["Amount"]).ToString("0.00");
                    //oItem.ShopName = Convert.ToString(dr["ShopName"]);
                    //oItem.BillNo = Convert.ToInt16(dr["BILLNUMBER"]);
                    //oItem.BillDate = Convert.ToString(dr["BillDate"]);

                    //oItem.Amount = decimal.Round(oItem.Amount, 2, MidpointRounding.AwayFromZero);
                    objBillItems.Add(oItem);
                }
            }
            return objBillItems.ToArray();
        }
        public tblBills_Bell[] GetLSItemsByMonth(string custid, string area, string shop, string date1)
        {
            DataTable table = new DataTable();
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
            {
                SqlCommand cmd = new SqlCommand("BELL_GET_ItemsByMonth", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CUSTID", custid);
                cmd.Parameters.AddWithValue("@AREA", area);
                cmd.Parameters.AddWithValue("@SHOP", shop.Replace('@', '/').Replace('$', '&'));
                cmd.Parameters.AddWithValue("@ORDERDATE", Convert.ToDateTime(date1));
                myCon.Open();
                myReader = cmd.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }
            List<tblBills_Bell> objBillItems = new List<tblBills_Bell>();
            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow dr in table.Rows)
                {
                    tblBills_Bell oItem = new tblBills_Bell();
                    oItem.ItemName = Convert.ToString(dr["ItemName"]);
                    oItem.Qty = Convert.ToString(dr["Qty"]);
                    oItem.Rate = Convert.ToDecimal(dr["Rate"]).ToString("0.00");
                    oItem.Amount = Convert.ToDecimal(dr["Amount"]).ToString("0.00");
                    //oItem.Amount = decimal.Round(oItem.Amount, 2, MidpointRounding.AwayFromZero);
                    objBillItems.Add(oItem);
                }
            }
            return objBillItems.ToArray();
        }
        public tblBellCustomersNew[] Bell_GetAllCustomers(string strLine, string strArea, string strShop)
        {
            DataTable table = new DataTable();
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
            {
                SqlCommand cmd = new SqlCommand("BELL_SP_GET_All_LS_Customers", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Line", strLine);
                cmd.Parameters.AddWithValue("@Area", strArea);
                cmd.Parameters.AddWithValue("@Shop", strShop.Replace('@', '/').Replace('$', '&'));
                myCon.Open();
                myReader = cmd.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }
            List<tblBellCustomersNew> objBillItems = new List<tblBellCustomersNew>();
            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow dr in table.Rows)
                {
                    tblBellCustomersNew oItem = new tblBellCustomersNew();
                    oItem.SNO = Convert.ToInt16(dr["SNO"]);
                    oItem.Line = Convert.ToString(dr["Line"]);
                    oItem.Area = Convert.ToString(dr["AREA"]);
                    oItem.ShopName = Convert.ToString(dr["ShopName"]);
                    oItem.CustomerName = Convert.ToString(dr["CustomerName"]);
                    oItem.SalesMan = Convert.ToString(dr["SalesMan"]);
                    oItem.Mobile = Convert.ToString(dr["Mobile"]);
                    objBillItems.Add(oItem);
                }
            }
            return objBillItems.ToArray();
        }
        public tblBills_Bell[] GetAllLSCustomers(string area, string date1, string date2)
        {
            DataTable table = new DataTable();
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
            {
                SqlCommand cmd = new SqlCommand("BELL_SP_GET_All_LS_Customers", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AREA", area);
                cmd.Parameters.AddWithValue("@FROMDATE", date1);
                cmd.Parameters.AddWithValue("@TODATE", date2);
                myCon.Open();
                myReader = cmd.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }
            List<tblBills_Bell> objBillItems = new List<tblBills_Bell>();
            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow dr in table.Rows)
                {
                    tblBills_Bell oItem = new tblBills_Bell();
                    oItem.Area = Convert.ToString(dr["AREA"]);
                    oItem.ShopName = Convert.ToString(dr["ShopName"]);
                    oItem.TotalItems = Convert.ToString(dr["TotalItems"]);
                    oItem.TotalAmount = Convert.ToString(dr["TotalAmount"]);
                    objBillItems.Add(oItem);
                }
            }
            return objBillItems.ToArray();
        }
        public string SaveCustLocation(tblBellCustomers obj)
        {
            using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
            {
                SqlCommand cmd = new SqlCommand("Bell_SAVE_CustomerLocation", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", obj.ID);
                cmd.Parameters.AddWithValue("@LAT", obj.LAT);
                cmd.Parameters.AddWithValue("@LNG", obj.LNG);
                cmd.Parameters.AddWithValue("@LANDMARK", obj.LANDMARK);
                myCon.Open();
                cmd.ExecuteNonQuery();
                myCon.Close();
            }
            return "Location details saved Successfully";
        }

        //public tblSalesReport[] GetWeeklySales(string strArea, string date1)
        //{
        //    DataTable table = new DataTable();
        //    SqlDataReader myReader;
        //    using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand("USP_WEEKLY_SALES_REPORT", myCon);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@AREA", strArea);
        //        cmd.Parameters.AddWithValue("@BILLDATE", date1);

        //        myCon.Open();
        //        myReader = cmd.ExecuteReader();
        //        table.Load(myReader);
        //        myReader.Close();
        //        myCon.Close();
        //    }
        //    List<tblSalesReport> objBillItems = new List<tblSalesReport>();
        //    if (table != null && table.Rows.Count > 0)
        //    {
        //        foreach (DataRow dr in table.Rows)
        //        {
        //            tblSalesReport oItem = new tblSalesReport();
        //            oItem.CustID = Convert.ToInt16(dr["CustID"]);
        //            oItem.Area = Convert.ToString(dr["AREA"]);
        //            oItem.ShopName = Convert.ToString(dr["ShopName"]);
        //            oItem.CustomerName = Convert.ToString(dr["CustomerName"]);
        //            //oItem.Mobile = Convert.ToString(dr["Mobile"]);
        //            //oItem.SalesMan = Convert.ToString(dr["SalesMan"]);
        //            //oItem.Week1 = Convert.ToString(dr["Week1"]);
        //            //oItem.Week2 = Convert.ToString(dr["Week2"]);
        //            //oItem.Week3 = Convert.ToString(dr["Week3"]);
        //            //oItem.Week4 = Convert.ToString(dr["Week4"]);
        //            oItem.Week1 = Convert.ToDecimal(dr["Week1"]).ToString("0.00");
        //            oItem.Week2 = Convert.ToDecimal(dr["Week2"]).ToString("0.00");
        //            oItem.Week3 = Convert.ToDecimal(dr["Week3"]).ToString("0.00");
        //            oItem.Week4 = Convert.ToDecimal(dr["Week4"]).ToString("0.00");
        //            //oItem.Week5 = Convert.ToDecimal(dr["Week5"]).ToString("0.00");
        //            oItem.TotalAmount = Convert.ToDecimal(dr["TotalAmount"]).ToString("0.00");
        //            objBillItems.Add(oItem);
        //        }
        //    }
        //    return objBillItems.ToArray();
        //}
        public string GetInactiveShops(string strArea, string strShop, string date1, string date2)
        {
            DataTable table = new DataTable();
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
            {
                SqlCommand cmd = new SqlCommand("BELL_GET_INACTIVE_SHOPS", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AREA", strArea);
                //cmd.Parameters.AddWithValue("@SHOP", strShop.Replace('@', '/').Replace('$', '&'));
                cmd.Parameters.AddWithValue("@BILLDATE1", date1);
                cmd.Parameters.AddWithValue("@BILLDATE2", date2);

                myCon.Open();
                myReader = cmd.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }
            //return ConvertDataTableToJson(table);
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in table.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in table.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
        }
        //not using. refer GetSalebyShopsBillDate
        //public string GetSaleItemsbyBillDate(string strArea, string strShop, string date1, string date2)
        //{
        //    DataTable table = new DataTable();
        //    SqlDataReader myReader;
        //    using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand("USP_ITEMS_WISE_SALES_COUNT_BY_BILLDATE", myCon);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@AREA", strArea);
        //        cmd.Parameters.AddWithValue("@SHOP", strShop.Replace('@', '/'));
        //        cmd.Parameters.AddWithValue("@BILLDATE1", date1);
        //        cmd.Parameters.AddWithValue("@BILLDATE2", date2);

        //        myCon.Open();
        //        myReader = cmd.ExecuteReader();
        //        table.Load(myReader);
        //        myReader.Close();
        //        myCon.Close();
        //    }
        //    //return ConvertDataTableToJson(table);
        //    System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        //    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        //    Dictionary<string, object> row;
        //    foreach (DataRow dr in table.Rows)
        //    {
        //        row = new Dictionary<string, object>();
        //        foreach (DataColumn col in table.Columns)
        //        {
        //            row.Add(col.ColumnName, dr[col]);
        //        }
        //        rows.Add(row);
        //    }
        //    return serializer.Serialize(rows);
        //}
        public string GetOperatorName(string strArea, string date1)
        {
            DataTable table = new DataTable();
            SqlDataReader myReader;
            string operatorName = "";
            using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
            {
                SqlCommand cmd = new SqlCommand("BELL_GET_OPERATOR_NAME", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AREA", strArea);
                cmd.Parameters.AddWithValue("@BILLDATE1", date1);

                myCon.Open();
                myReader = cmd.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
                if (table != null && table.Rows.Count > 0)
                {
                    operatorName = table.Rows[0][0].ToString();
                }
            }
            return operatorName;
        }
        public string GetWeekDaysLinesCount(string BillDate, string strArea)
        {
            DataTable table = new DataTable();
            SqlDataReader myReader;
            string strProcedure = "USP_GetWeekDaysLineCount";
            using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
            {
                SqlCommand cmd = new SqlCommand(strProcedure, myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@WeekDate", BillDate);
                cmd.Parameters.AddWithValue("@AREA", strArea);
                myCon.Open();
                myReader = cmd.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }
            if (table != null && table.Rows.Count > 0)
                return table.Rows[0][0].ToString();
            else
                return "0";
        }
        public string GetSalebyShopsBillDate(string reportType, string strArea,string strAreaLine,string strShop, string date1, string date2)
        {
            DataTable table = new DataTable();
            SqlDataReader myReader;
            //if (reportType == "ITEMWISE")
            //{
            //    return GetItemWiseSales_New(reportType,strArea,strShop,date1,date2);
            //}
            string strProcedure = "USP_SHOP_WISE_SALES_COUNT_BY_BILLDATE_26JAN25";
            using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
            {
                if (reportType == "BILLWISE")
                {
                    strProcedure = "BELL_SHOP_WISE_SALES_BY_BILLNUMBER";
                }
                SqlCommand cmd = new SqlCommand(strProcedure, myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@HEADER", reportType);
                cmd.Parameters.AddWithValue("@AREA", strArea);
                cmd.Parameters.AddWithValue("@AREA_LINE", strAreaLine);
                cmd.Parameters.AddWithValue("@SHOP", strShop.Replace('@', '/').Replace('$', '&'));
                cmd.Parameters.AddWithValue("@BILLDATE1", date1);
                cmd.Parameters.AddWithValue("@BILLDATE2", date2);

                myCon.Open();
                myReader = cmd.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }
            DataTable dtNew = new DataTable();
            dtNew = table;
            decimal ColumnTotal;
            decimal varTotal;
            string operatorName = "";
            string strLineCount = "";
            foreach (DataColumn col in table.Columns)
            {
                ColumnTotal = 0;
                varTotal = 0;
                if (col.ColumnName != "BILLNUMBER" && col.ColumnName != "SHOPNAME" && col.ColumnName != "NAME" && col.ColumnName != "ITEMCODE" && col.ColumnName.IndexOf("QTY") == -1)
                {
                    foreach (DataRow dr in table.Rows)
                    {
                        //if (dr[col] == null) varTotal = 0;
                        if (!(dr[col] is DBNull)) varTotal = Convert.ToDecimal(dr[col]);
                        else varTotal = varTotal = 0;

                        ColumnTotal = ColumnTotal + varTotal;
                    }
                    if (reportType == "ITEMWISE")
                    {
                        if (col.ColumnName != "Profit_Percent" && col.ColumnName != "Profit_Amt" && col.ColumnName != "Sales_Amt")
                        {                            
                            strLineCount = GetWeekDaysLinesCount(dtNew.Columns[col.ColumnName].ColumnName, strArea);
                            dtNew.Columns[col.ColumnName].ColumnName = dtNew.Columns[col.ColumnName].ColumnName + " - Tot:" + ColumnTotal.ToString() + " Lines:" + strLineCount;
                        }
                        else if (col.ColumnName == "Profit_Amt" || col.ColumnName == "Sales_Amt")
                        {
                            dtNew.Columns[col.ColumnName].ColumnName = dtNew.Columns[col.ColumnName].ColumnName + " - Tot:" + ColumnTotal.ToString();
                        }
                    }
                    else
                    {
                        //if (col.ColumnName != "Profit_Percent" && col.ColumnName != "Profit_Amt" && col.ColumnName != "Sales_Amt")
                        //{
                        //    operatorName = GetOperatorName(strArea, col.ColumnName);
                        //}
                        operatorName = GetOperatorName(strArea, col.ColumnName);
                        dtNew.Columns[col.ColumnName].ColumnName = dtNew.Columns[col.ColumnName].ColumnName + " - Tot: " + ColumnTotal.ToString() + " - " + operatorName;
                    }
                }
                //row.Add(col.ColumnName, dr[col]);
            }

            //return ConvertDataTableToJson(table);
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            int varCounter = 1;
            foreach (DataRow dr in table.Rows)
            {
                row = new Dictionary<string, object>();
                if (reportType != "BILLWISE") { row.Add("SNO.", varCounter++); }
                foreach (DataColumn col in table.Columns)
                {
                    //if (col.ColumnName == "NAME")
                    //{
                    //    col.ColumnName = "Name (Operator : Raju)"; //working but not using
                    //}
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
        }

        //not using...
        public string GetItemWiseSales_New(string reportType, string strArea, string strShop, string date1, string date2)
        {
            DataTable table = new DataTable();
            DataSet ds = new DataSet();
            SqlDataReader myReader;
            string strProcedure = "USP_SHOP_WISE_SALES_COUNT_BY_BILLDATE";
            using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
            {
                SqlCommand cmd = new SqlCommand(strProcedure, myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@HEADER", reportType);
                cmd.Parameters.AddWithValue("@AREA", strArea);
                cmd.Parameters.AddWithValue("@SHOP", strShop.Replace('@', '/').Replace('$', '&'));
                cmd.Parameters.AddWithValue("@BILLDATE1", date1);
                cmd.Parameters.AddWithValue("@BILLDATE2", date2);

                myCon.Open();
                SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                myDA.Fill(ds);
                myCon.Close();
            }
            DataTable dtNew = new DataTable();
            table = ds.Tables[0];
            dtNew = ds.Tables[0];

            long ColumnTotal;
            long varTotal;
            foreach (DataColumn col in table.Columns)
            {
                ColumnTotal = 0;
                varTotal = 0;
                if (col.ColumnName != "BILLNUMBER" && col.ColumnName != "NAME" && col.ColumnName != "ITEMCODE")
                {
                    foreach (DataRow dr in table.Rows)
                    {
                        //if (dr[col] == null) varTotal = 0;
                        if (!(dr[col] is DBNull)) varTotal = Convert.ToInt64(dr[col]);
                        else varTotal = varTotal = 0;

                        ColumnTotal = ColumnTotal + varTotal;
                    }
                    dtNew.Columns[col.ColumnName].ColumnName = dtNew.Columns[col.ColumnName].ColumnName + " (Total: " + ColumnTotal.ToString() + ")";
                }
                //row.Add(col.ColumnName, dr[col]);
            }

            //return ConvertDataTableToJson(table);
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> objRows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in table.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in table.Columns)
                {
                    //if (col.ColumnName == "NAME")
                    //{
                    //    col.ColumnName = "Name (Operator : Raju)"; //working but not using
                    //}
                    row.Add(col.ColumnName, dr[col]);
                }
                objRows.Add(row);
            }

            //for Second table 
            table = ds.Tables[1];
            //dtNew = ds.Tables[1];
            foreach (DataColumn col in ds.Tables[1].Columns)
            {
                ColumnTotal = 0;
                varTotal = 0;
                if (col.ColumnName != "BILLNUMBER" && col.ColumnName != "NAME" && col.ColumnName != "ITEMCODE")
                {
                    table.Columns.Add(table.Columns[col.ColumnName].ColumnName + " (QTY)");
                }
            }

            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in table.Columns)
                {
                    //if (col.ColumnName == "NAME")
                    //{
                    //    col.ColumnName = "Name (Operator : Raju)"; //working but not using
                    //}
                    row.Add(col.ColumnName, dr[col]);
                }
                objRows.Add(row);
            }
            return serializer.Serialize(objRows);
        }
        public string GetWeeklySalesByShop(string strArea, string strShop, string date1, string date2)
        {
            DataTable table = new DataTable();
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
            {
                SqlCommand cmd = new SqlCommand("USP_ITEMS_WISE_SALES_COUNT_BY_SHOP", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AREA", strArea);
                cmd.Parameters.AddWithValue("@SHOP", strShop.Replace('@', '/').Replace('$', '&'));
                cmd.Parameters.AddWithValue("@BILLDATE1", date1);
                cmd.Parameters.AddWithValue("@BILLDATE2", date2);

                myCon.Open();
                myReader = cmd.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }
            //return ConvertDataTableToJson(table);
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in table.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in table.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
        }
        public string GetWeeklySalesByItems(searchPayLoad objRequest)
        {
            DataTable table = new DataTable();
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
            {
                SqlCommand cmd = new SqlCommand("USP_ITEMS_WISE_SALES_COUNT_BY_ITEMNAME", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TYPE", objRequest.reportType);
                cmd.Parameters.AddWithValue("@AREA", objRequest.area);
                cmd.Parameters.AddWithValue("@SHOP", objRequest.shop.Replace('@', '/').Replace('$', '&'));
                cmd.Parameters.AddWithValue("@ITEMNAME", objRequest.itemname.Replace('@', '/'));
                cmd.Parameters.AddWithValue("@BILLDATE1", objRequest.date1);
                cmd.Parameters.AddWithValue("@BILLDATE2", objRequest.date2);

                myCon.Open();
                myReader = cmd.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }
            //return ConvertDataTableToJson(table);
            if (table != null && table.Rows.Count > 0)
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row;
                int varCounter = 1;
                foreach (DataRow dr in table.Rows)
                {
                    row = new Dictionary<string, object>();
                    row.Add("SNO.", varCounter++);
                    foreach (DataColumn col in table.Columns)
                    {
                        row.Add(col.ColumnName, dr[col]);
                    }
                    rows.Add(row);
                }
                return serializer.Serialize(rows);
            }
            else { return ""; }
        }
        public string GetWeeklySalesByItems(string strType, string strArea, string strShop, string strItem, string date1, string date2)
        {
            DataTable table = new DataTable();
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(strDBConnectionString))
            {
                SqlCommand cmd = new SqlCommand("USP_ITEMS_WISE_SALES_COUNT_BY_ITEMNAME", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TYPE", strType);
                cmd.Parameters.AddWithValue("@AREA", strArea);
                cmd.Parameters.AddWithValue("@SHOP", strShop.Replace('@', '/').Replace('$', '&'));
                cmd.Parameters.AddWithValue("@ITEMNAME", strItem.Replace('@', '/'));
                cmd.Parameters.AddWithValue("@BILLDATE1", date1);
                cmd.Parameters.AddWithValue("@BILLDATE2", date2);

                myCon.Open();
                myReader = cmd.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }
            //return ConvertDataTableToJson(table);
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            int varCounter = 1;
            foreach (DataRow dr in table.Rows)
            {
                row = new Dictionary<string, object>();
                row.Add("SNO.", varCounter++);
                foreach (DataColumn col in table.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);


        }
        private string ConvertDataTableToJson(DataTable dataTable)
        {
            string jsonString = string.Empty;
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                jsonString = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
            }
            return jsonString;
        }

        //End of Bell methods.

        public string DataTableToJSONWithStrBuilder(DataTable table)
        {
            var JSONString = new StringBuilder();
            if (table.Rows.Count > 0)
            {
                JSONString.Append("[");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    JSONString.Append("{");
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        if (j < table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\",");
                        }
                        else if (j == table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\"");
                        }
                    }
                    if (i == table.Rows.Count - 1)
                    {
                        JSONString.Append("}");
                    }
                    else
                    {
                        JSONString.Append("},");
                    }
                }
                JSONString.Append("]");
            }
            return JSONString.ToString();
        }
    }
}