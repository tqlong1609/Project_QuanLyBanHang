﻿using QuanLyCuaHang.DB_layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHang.BS_layer
{
    class BS_SellProduct
    {
        private DBMain dBMain;

        public BS_SellProduct()
        {
            dBMain = new DBMain();
        }

        // load data customer
        public DataTable loadCustomer()
        {
            return dBMain.ExecuteQueryDataSet("select IDKhachHang, Name from KHACHHANG", CommandType.Text);
        }

        // load data products
        public DataTable loadProducts()
        {
            return dBMain.ExecuteQueryDataSet("select IDSanPham, Name, Price from SANPHAM", CommandType.Text);
        }
        // search customer
        public DataTable searchCustomer(string _search)
        {
            string sqlString = "select IDKhachHang, Name from KHACHHANG where IDKhachHang like '" + _search + "%' " +
                "or Name like N'" + _search + "%'";
            return dBMain.ExecuteQueryDataSet(sqlString, CommandType.Text);
        }
        // search product
        public DataTable searchProducts(string _search)
        {
            string sqlString = "select IDSanPham, Name, Price from SANPHAM where IDSanPham like '"+_search+"%' " +
                "or Name like N'"+_search+"%'";
            return dBMain.ExecuteQueryDataSet(sqlString, CommandType.Text);
        }
        // load id bill
        public string loadId()
        {
            DataTable dataTable = dBMain.ExecuteQueryDataSet("select IDHoaDon from HOADON", CommandType.Text);
            int num = 0;
            foreach (DataRow dataRow in dataTable.Rows)
            {
                if (!dataRow["IDHoaDon"].ToString().Contains(num.ToString()))
                    return "HD" + num;
                num++;
            }
            return "HD" + num;
        }
        // check id discount
        public bool isDiscount(string id, ref string discount)
        {
            bool t = false;
            string sqlString = "select IDKhuyenMai, GiamGia from KHUYENMAI";
            DataTable dataTable = dBMain.ExecuteQueryDataSet(sqlString, CommandType.Text);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                if (id.ToUpper().Equals(dataRow["IDKhuyenMai"].ToString().Trim()))
                {
                    discount = dataRow["GiamGia"].ToString().Trim();
                    t = true;
                }
            }
            return t;
        }
    }
}