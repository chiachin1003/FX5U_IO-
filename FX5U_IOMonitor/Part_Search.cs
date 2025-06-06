using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using CsvHelper;
using System.Globalization;
using System.IO;
using System.Linq;
using FX5U_IOMonitor.Models;
using FX5U_IOMonitor.Data;
using FX5U_IOMonitor;
using System.Text;
using System.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static System.Runtime.InteropServices.JavaScript.JSType;
using FX5U_IOMonitor.Resources;
using static FX5U_IOMonitor.Resources.Element_Settings;


namespace FX5U_IO元件監控
{

    public partial class Part_Search : Form
    {
        private string datatable = "";
        public Part_Search(ShowPage page)
        {

            InitializeComponent();

            switch (page)
            {
                case ShowPage.Drill:
                    datatable = "Drill";
                    update_combobox(datatable);
                    break;
                case ShowPage.Sawing:
                    datatable = "Sawing";
                    update_combobox(datatable);
                    break;
            }

        }
        private void update_combobox(string datatable)
        {
            // 取得所有 `ClassTag` 並去重複
            var classTags = DBfunction.GetAllClassTags(datatable);

            // 更新 comb_class 下拉選單
            comb_class.Items.Clear(); // 清空舊選項
            classTags.Insert(0, "Total");
            comb_class.Items.AddRange(classTags.ToArray()); // 新增 ClassTag 選項
            if (comb_class.Items.Count > 0)
            {
                comb_class.SelectedIndex = 0; // 預設選擇第一個項目
            }

        }

        public enum ShowPage
        {
            Drill,
            Sawing
        }

        private object Get_IO_Data(string datatable)
        {
            using var context = new ApplicationDB();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var data = context.Machine_IO
            .Where(a => a.Machine_name == datatable)
            .Select(d => new
            {
                地址 = d.address,
                料件 = d.Description,
                最大壽命 = d.MaxLife,
                紅燈 = d.Setting_red,
                黃燈 = d.Setting_yellow,

            })
            .ToList();
            return data;
        }
        private object Get_IO_Class(string classTag, string datatable)
        {
            using var context = new ApplicationDB();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var data = context.Machine_IO
                .Where(d => d.ClassTag != null && d.Machine_name == datatable &&
                d.ClassTag.ToLower().Contains(classTag.ToLower())) // 模糊+忽略大小寫
            .Select(d => new
            {
                地址 = d.address,
                料件 = d.Description,
                最大壽命 = d.MaxLife,
                紅燈 = d.Setting_red,
                黃燈 = d.Setting_yellow,
                綠燈 = d.Setting_green

            })
            .ToList();
            return data;
        }
        


        private void comb_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            object data;

            data = Get_IO_Data(datatable);

            if (comb_class.SelectedIndex > 0)
            {
                string selectedClass = comb_class.SelectedItem?.ToString() ?? "";
                data = Get_IO_Class(selectedClass, datatable);
            }


            dataGridView1.DataSource = data;

        }

        private void txB_search_TextChanged(object sender, EventArgs e)
        {
            string searchText = txB_search.Text.Trim();
            var data = SearchDataFromDB(datatable, searchText);
            dataGridView1.DataSource = data;
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // 避免點到欄位標題列
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;


            var selectedRow = dataGridView1.Rows[e.RowIndex];
            var addressValue = selectedRow.Cells["地址"].Value?.ToString(); // ✅ 中文欄位名稱對應 Select 時的名稱

            if (!string.IsNullOrEmpty(addressValue))
            {
                // 直接開啟詳細頁面，從 DB 查
                ShowDetail detailForm = new ShowDetail(datatable, addressValue, ShowDetailPage.LifeSetting);
                detailForm.ShowDialog();
            }

        }


        private object SearchDataFromDB(string datatable, string searchText)
        {
            using var context = new ApplicationDB();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            searchText = searchText.ToLower(); // ✅ 強制轉小寫以比對


            var result = context.Machine_IO
                .Where(a => a.Machine_name == datatable)
                .ToList()
                .Where(d =>
                    (!string.IsNullOrEmpty(d.address) && d.address.ToLower().Contains(searchText)) ||
                    (!string.IsNullOrEmpty(d.ClassTag) && d.ClassTag.ToLower().Contains(searchText)) ||
                    (!string.IsNullOrEmpty(d.Description) && d.Description.ToLower().Contains(searchText))
                )
                .Select(d => new
                {
                    地址 = d.address,
                    料件 = d.Description,
                    最大壽命 = d.MaxLife,
                    紅燈 = d.Setting_red,
                    黃燈 = d.Setting_yellow,
                    綠燈 = d.Setting_green
                })
                .ToList();

            return result;

        }

        private void btn_add_element_Click(object sender, EventArgs e)
        {
            string? addressValue = null;

            // ✅ 有選取列才進入編輯模式
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView1.SelectedRows[0];
                addressValue = selectedRow.Cells["地址"].Value?.ToString();
            }

            // 如果有選到資料，進入編輯或檢視模式
            if (!string.IsNullOrWhiteSpace(addressValue))
            {
                using (var context = new ApplicationDB())
                {
                   
                    var item = context.Machine_IO.FirstOrDefault(d => d.address == addressValue && d.Machine_name == datatable);
                    if (item != null)
                    {
                        using (var form = new Element_Settings(ElementMode.ViewOnly, datatable, item.address))
                        {
                            form.StartPosition = FormStartPosition.CenterParent;

                            form.OnDataUpdated = () =>
                            {
                                RefreshData();
                            };
                            form.ShowDialog(this);
                        }
                        return;
                    }
                  
                    MessageBox.Show("⚠️ 找不到資料，無法開啟詳細資料。", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // ✅ 未選取任何資料，進入新增模式
                using (var form = new Element_Settings(ElementMode.Add, datatable))
                {
                    form.StartPosition = FormStartPosition.CenterParent;
                    form.OnDataUpdated = () =>
                    {
                        RefreshData();
                    };
                    form.ShowDialog(this);
                }
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            // 檢查有沒有選到列
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("⚠️ 請先選取要刪除的資料列！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 取得選取列的 "地址"（address）
            var selectedRow = dataGridView1.SelectedRows[0];
            var addressValue = selectedRow.Cells["地址"].Value?.ToString();

            if (string.IsNullOrEmpty(addressValue))
            {
                MessageBox.Show("⚠️ 選取的資料沒有有效的地址！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 二次確認
            var result = MessageBox.Show($"確定要刪除地址為 {addressValue} 的資料嗎？", "確認刪除", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                return;

            // 開始刪除
            using (var context = new ApplicationDB())
            {

                var item = context.Machine_IO.FirstOrDefault(d => d.address == addressValue && d.Machine_name == datatable);
                if (item != null)
                {
                    context.Machine_IO.Remove(item);
                    context.SaveChanges();
                    MessageBox.Show("✅ 資料刪除成功！");
                }
                else
                {
                    MessageBox.Show("⚠️ 找不到資料，刪除失敗！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

            RefreshData();
        }

        private void RefreshData()
        {

            dataGridView1.DataSource = Get_IO_Data(datatable);

            dataGridView1.Refresh();          // ✅ 強制刷新顯示
            dataGridView1.ClearSelection();   // ✅ 清除舊選擇（避免看起來沒變）
        }


    }
}



