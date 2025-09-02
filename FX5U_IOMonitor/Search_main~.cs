using CsvHelper;
using System.Globalization;
using static FX5U_IOMonitor.Main;
using FX5U_IOMonitor.Data;
using Newtonsoft.Json;
using System.Text;
using System.Data;
using FX5U_IOMonitor.Models;


namespace FX5U_IOMonitor
{

    public partial class Search_main : Form
    {
        public Search_main()
        {

            InitializeComponent();
            update_interface();

        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 避免點到標題欄或錯誤行
            if (e.RowIndex < 0) return;

            var selectedRow = dataGridView1.Rows[e.RowIndex];
            var addressValue = selectedRow.Cells["地址"].Value?.ToString(); // ✅ 中文欄位名稱對應你 Select 時的名稱

            if (!string.IsNullOrEmpty(addressValue))
            {
                // 直接開啟詳細頁面，從 DB 查
                alarm_setting detailForm = new alarm_setting(addressValue);
                detailForm.ShowDialog();
            }


            update_interface();
        }
        private void update_interface()
        {

            using var context = new ApplicationDB();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var data = context.alarm
            .Select(d => new
            {
                地址 = d.address,
                位置 = d.classTag,
                料件 = d.Description,
                錯誤信息 = d.Error,
                可能原因 = d.Possible,
                維護步驟 = d.Repair_steps
            })
            .ToList();

            dataGridView1.DataSource = data;

        }
    }

}



