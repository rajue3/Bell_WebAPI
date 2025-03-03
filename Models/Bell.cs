using System;

namespace ZionAPI
{
    //open AI, assistance
    public class searchPayLoad
    {
        public string reportType { get; set; }
        public string area { get; set; }
        public string date1 { get; set; }
        public string date2 { get; set; }
        public string shop { get; set; }
        public string itemname { get; set; }
    }
    public class tblUsers
    {
        public int id { get; set; }
        public string username { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string password { get; set; }
        public string usertype { get; set; }
        public string status { get; set; }
        public string ActionDate { get; set; }
    }
    public class tblItemDetails
    {
        public int ID { get; set; }
        public string ITEMNAME { get; set; }
        public string MRP { get; set; }
        public string RATE { get; set; }
        public string IMAGEURL { get; set; }
        public string DESCRIPTION { get; set; }
        public string TOTALITEMSINPACK { get; set; }
        public string TOTALITEMSINCARTON { get; set; }
        public string CATEGORY { get; set; }
        public string PACKINGTYPE { get; set; }
    }
    public class tblItemMaster
    {
        public int ID { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string MRP { get; set; }
        public string PRate { get; set; }  //PURCHASE RATE ADDED ON 11-JAN-25
        public string Rate { get; set; }
        public string Qty { get; set; }
        public string Amount { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public string TOTALITEMSINPACK { get; set; }
        public string TOTALITEMSINCARTON { get; set; }
        public string CATEGORY { get; set; }
        public string Manufacture { get; set; }
        public string PACKINGTYPE { get; set; }
        //public string Status { get; set; }
        public long STOCK { get; set; } //Total PACKETS / final Stock Out
        public long Cartons { get; set; } //Cartons
        public long Packets { get; set; } //loos packets
        public string USERNAME { get; set; }
        public int MinOrderAlert { get; set; }        
        public string ActionDate { get; set; }
        public long TOTAL_PACKS { get; set; }
        public long RETURN_PACKS { get; set; } 
        public long DAMAGE_PACKS { get; set; }
        public string LINE { get; set; }

    }
    public class tblCustomers
    {
        public int ID { get; set; }
        public string AreaName { get; set; }
        public string SalesMan { get; set; }
        public string Customername { get; set; }
        public string Shopname { get; set; }
        public string mobile { get; set; }
        public string AdminMobile { get; set; }
        public string AdminMobile2 { get; set; }
        public string IsAuth { get; set; }
        public string Status { get; set; }
        public string LineName { get; set; }
        public string LandMark { get; set; }
        public string SubRoute { get; set; }
    }
    public class tblBellCustomers
    {
        public int ID { get; set; }
        public string Area { get; set; }
        public string ShopName { get; set; }
        public string CustomerName { get; set; }
        public string Mobile { get; set; }
        public string SalesMan { get; set; }
        public string LAT { get; set; }
        public string LNG { get; set; }
        public string LANDMARK { get; set; }
    }
    public class tblBellCustomersNew
    {
        public int SNO { get; set; }
        public string Line { get; set; }
        public string Area { get; set; }
        public string ShopName { get; set; }
        public string CustomerName { get; set; }
        public string SalesMan { get; set; }
        public string Mobile { get; set; }
    }
    public class tblSalesReportNew
    {
        public string Line { get; set; }
        public string Area { get; set; }
        public string BillDate { get; set; }
        //public string ActionDate { get; set; }
        public string Purchase_Amount { get; set; }
        public string Amount { get; set; } //sale_amount
        public string Profit_Amount { get; set; }
        public string Profit_Percent { get; set; }
        public int TotalBills { get; set; }
        public string UserName { get; set; }
    }
    //not using, can cleaned
    public class tblSalesReport
    {
        public int CustID { get; set; }
        public string Area { get; set; }
        public string ShopName { get; set; }
        public string CustomerName { get; set; }
        public string Mobile { get; set; }
        public string ItemName { get; set; }
        public string SalesMan { get; set; }
        public string Week1 { get; set; }
        public string Week2 { get; set; }
        public string Week3 { get; set; }
        public string Week4 { get; set; }
        public string Week5 { get; set; }
        public string TotalAmount { get; set; }
    }
    public class tblSalesReport2
    {
        public string Area { get; set; }
        public string ShopName { get; set; }
        public string ItemName { get; set; }
        public string Qty { get; set; }
    }
    public class ItemMaster
    {
        public int ID { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public int Rate { get; set; }
        public string Qty { get; set; }
        public int Amount
        {
            get { return Convert.ToInt16(Rate) * Convert.ToInt16(Qty); }
        }
    }
    public class LSItems
    {
        public string ItemName { get; set; }
        public int Rate { get; set; }
        public string Qty { get; set; }
        public DateTime ActionDate
        {
            get { return DateTime.Today; }
            set { }
        }
        public string Area { get; set; }
        public int Amount
        {
            get { return Convert.ToInt16(Rate) * Convert.ToInt16(Qty); }
        }
    }
    //using this model as orders and orderitems
    public class tblBell_Orders
    {
        public int ID { get; set; }
        public string ItemName { get; set; }
        public int BillNo { get; set; }
        public string BillDate
        {
            get; set;
        }
        public string Area { get; set; }
        public string ShopName { get; set; }
        public string Customer { get; set; }
        public string Mobile { get; set; }
        public string TotalAmount { get; set; }
        public string TotalItems { get; set; }
    }
    //using this model as orders and orderitems
    public class tblBills
    {
        public int ID { get; set; }
        public string ItemName { get; set; }
        public string Rate { get; set; }
        public string Qty { get; set; }
        public DateTime BillDate
        {
            get { return DateTime.Now; }
            set { }
        }
        public string Area { get; set; }
        public string Salesman { get; set; }
        public string Customer { get; set; }
        public string Mobile { get; set; }
        public string TotalAmount { get; set; }
        public string Status { get; set; }
        public string Amount
        {
            //get { return Convert.ToInt16(Rate) * Convert.ToInt16(Qty); }
            //get { return decimal.Round(Convert.ToDecimal(Rate) * Convert.ToInt16(Qty)); }
            get { return (Convert.ToDecimal(Rate) * Convert.ToInt16(Qty)).ToString("0.00"); }
        }
    }
    public class tblBills_Bell
    {
        public int ID { get; set; }
        public string ItemName { get; set; }
        public string Rate { get; set; }
        public string Qty { get; set; }
        public string Ret_Qty { get; set; }
        public int BillNo { get; set; }
        public string BillDate
        {
            get;set;
            //get { return DateTime.Now; }   set { }
        }
        public string Area { get; set; }
        public string Salesman { get; set; }
        public string ShopName { get; set; }
        public string Customer { get; set; }
        public string Mobile { get; set; }
        public string TotalAmount { get; set; }
        public string Status { get; set; }
        public string TotalItems { get; set; }
        public string Amount
        {
            get; set;
            ////get { return Convert.ToInt16(Rate) * Convert.ToInt16(Qty); }
            ////get { return decimal.Round(Convert.ToDecimal(Rate) * Convert.ToInt16(Qty)); }
            //get { return (Convert.ToDecimal(Rate) * Convert.ToInt16(Qty)).ToString("0.00"); }
        }
    }
    public class BellAreaMaster
    {
        public string Line { get; set; }
        public string Area { get; set; }
        public string Shop { get; set; }
        public string Customer { get; set; }
    }
    public class AreaMaster
    {
        public string AreaName { get; set; }
        public string Salesman { get; set; }
    }
    public class tblOrders
    {
        public int OrderID { get; set; }
        public string CustomerName { get; set; }
        public string Mobile { get; set; }
        public string Area { get; set; }
        public string OrderDate { get; set; }
        public string TotalItems { get; set; }
        public string TotalAmount { get; set; }
        public string Status { get; set; }
    }
    public class tblOrderItems
    {
        //public int SNO { get; set; }
        public int ID { get; set; }
        public int OrderID { get; set; }
        public string ItemName { get; set; }
        public int Rate { get; set; }
        public string Qty { get; set; }
        public DateTime ActionDate
        {
            get { return DateTime.Today; }
            set { }
        }
        public int Amount
        {
            get { return Convert.ToInt16(Rate) * Convert.ToInt16(Qty); }
        }
    }

}