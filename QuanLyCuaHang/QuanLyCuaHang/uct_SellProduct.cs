﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyCuaHang.BS_layer;

namespace QuanLyCuaHang
{
    public partial class uct_SellProduct : UserControl
    {
        private BS_SellProduct bS_SellProduct;
        private int totalPrice = 0;
        private string idCustomer;
        private List<string> listProductSelled;

        public uct_SellProduct()
        {
            InitializeComponent();
            bS_SellProduct = new BS_SellProduct();
            listProductSelled = new List<string>();
            loadCustomer();
            loadProducts();
        }
        // add
        private void btn_Add_Click(object sender, EventArgs e)
        {
            frm_AddCustomer temp = new frm_AddCustomer();
            temp.ShowDialog();
        }
        // sell
        private void btn_Sell_Click(object sender, EventArgs e)
        {
            if (txt_customer_added.Text != "" && lvw_ProductSell.Items.Count > 0)
            {
                string discount = "";
                if (txt_idDiscount.Text == "" || bS_SellProduct.isDiscount(txt_idDiscount.Text, ref discount))
                {
                    MessageBox.Show("Cell Success", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    new frm_Sell(idCustomer, totalPrice, loadDateSell(), txt_idDiscount.Text, discount).ShowDialog();
                    deleteSelled();
                    clear();
                }
                else
                    MessageBox.Show("id discount not true","Error",MessageBoxButtons.OK,MessageBoxIcon.None);
            }
            else
                MessageBox.Show("Do not empty values before confirm","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
        }
        // delete values after selled
        public void deleteSelled()
        {
            string error = "";
            if (!bS_SellProduct.deleteSelled(listProductSelled, txt_idDiscount.Text, ref error))
                MessageBox.Show(error);
            else
                loadProducts();
        }   
        // load date sell
        public string loadDateSell()
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            return dateTime.ToString("dd/MM/yyyy");
        }
        // clear
        private void btn_Clear_Click(object sender, EventArgs e)
        {
            clear();
        }
        // clear
        private void clear()
        {
            lvw_ProductSell.Clear();
            listProductSelled.Clear();
            totalPrice = 0;
        }
        // load customer
        private void loadCustomer()
        {
            dataGid_Customer.DataSource = bS_SellProduct.loadCustomer();
        }
        // load Products
        private void loadProducts()
        {
            dataGid_Products.DataSource = bS_SellProduct.loadProducts();
        }

        private void dataGid_Customer_MouseDown(object sender, MouseEventArgs e)
        {
            if (frm_AddCustomer.isUpdate)
            {
                loadCustomer();
                frm_AddCustomer.isUpdate = false;
            }
        }
        // search
        private void txt_SearchCustomer_OnValueChanged(object sender, EventArgs e)
        {
            if (txt_SearchCustomer.Text != "")
            {
                dataGid_Customer.DataSource = bS_SellProduct.searchCustomer(txt_SearchCustomer.Text.Trim().ToLower());
            }
            else
            {
                loadCustomer();
            }
        }

        private void txt_SearchProducts_OnValueChanged(object sender, EventArgs e)
        {
            if (txt_SearchProducts.Text != "")
            {
                dataGid_Products.DataSource = bS_SellProduct.searchProducts(txt_SearchProducts.Text.Trim().ToLower());
            }
            else
            {
                loadProducts();
            }
        }

        private void dataGid_Customer_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txt_customer_added.Text = idCustomer = dataGid_Customer.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void dataGid_Products_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            lvw_ProductSell.Items.Add(dataGid_Products.Rows[e.RowIndex].Cells[1].Value.ToString());
            totalPrice += int.Parse(dataGid_Products.Rows[e.RowIndex].Cells[2].Value.ToString());
            listProductSelled.Add(dataGid_Products.Rows[e.RowIndex].Cells[0].Value.ToString());

        }
    }
}
