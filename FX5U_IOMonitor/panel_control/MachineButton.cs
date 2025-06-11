using FX5U_IOMonitor.Data;
using FX5U_IOMonitor.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static FX5U_IOMonitor.Models.UI_Display;

namespace FX5U_IOMonitor.panel_control
{
    public partial class MachineButton
    {
        public static Button CreateMachineButton(string indexName, Panel targetPanel)
        {
            var btn = new Button
            {
                Name = $"Mainform_{indexName}",
                Height = 50,
                Dock = DockStyle.Top,
                Font = new Font("微軟正黑體", 9F, FontStyle.Regular),
                FlatStyle = FlatStyle.Flat,
                Tag = new ButtonTag { IndexName = indexName, TargetPanel = targetPanel }
            };

            btn.Click += OnMachineButtonClick;
            return btn;
        }

        // 封裝點擊參數
        private class ButtonTag
        {
            public string IndexName { get; set; }
            public Panel TargetPanel { get; set; }
        }

        // 事件觸發：顯示對應子畫面
        private static void OnMachineButtonClick(object? sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag is ButtonTag tag)
            {
                var form = Machine_main.GetInstance(tag.IndexName);
                form.TopLevel = false;
                form.FormBorderStyle = FormBorderStyle.None;
                form.Dock = DockStyle.Fill;

                tag.TargetPanel.Controls.Clear();
                tag.TargetPanel.Controls.Add(form);
                form.Show();
            }
        }
        ///原始參考程式
        //private void btn_Drill_Click(object sender, EventArgs e)
        //{

        //    string machine = "Drill";
        //    var form = Machine_main.GetInstance(machine); // ✅ 呼叫單例

        //    form.TopLevel = false;
        //    form.FormBorderStyle = FormBorderStyle.None;
        //    form.Dock = DockStyle.Fill;

        //    panel_main.Controls.Clear();
        //    panel_main.Controls.Add(form);
        //    form.Show();
        //}
        ///
        /// <summary>
        /// 新增機台時新增旁邊按鈕
        /// </summary>
        /// <param name="container"></param>
        /// <param name="anchor"></param>
        /// <param name="targetPanel"></param>
        public static void UpdateMachineButtons(Panel container, Control anchor, Panel targetPanel)
        {
            // 從資料庫取得最新機台清單
            List<Machine_number> machineList = DBfunction.GetMachineIndexes();
            HashSet<string> currentIndexNames = machineList.Select(m => m.Name).ToHashSet();

            // 快取已存在的機台按鈕（符合命名規則的）
            var existingButtonNames = container.Controls
                .OfType<Button>()
                .Where(b => b.Name.StartsWith("Mainform_"))
                .ToDictionary(b => b.Name.Replace("Mainform_", ""), b => b);

            // 找出「最後一顆已存在的機台按鈕」的 index（以插在它後面）
            int baseInsertIndex = container.Controls
                .OfType<Button>()
                .Where(b => b.Name.StartsWith("Mainform_") && currentIndexNames.Contains(b.Name.Replace("Mainform_", "")))
                .Select(b => container.Controls.GetChildIndex(b))
                .DefaultIfEmpty(container.Controls.GetChildIndex(anchor)) // 如果沒有機台按鈕，就插在 btn_Main 之後
                .Min();
           
            // ✅ 1. 刪除不再存在的機台按鈕
            foreach (var pair in existingButtonNames)
            {
                string indexName = pair.Key;
                Button btn = pair.Value;

                if (!currentIndexNames.Contains(indexName))
                {
                    container.Controls.Remove(btn);
                }
            }
            // ✅ 2. 插入尚未存在的機台按鈕
            int insertOffset = 0;
            for (int i = 0; i < machineList.Count; i++)
            {
                string indexName = machineList[i].Name;
                string buttonName = $"Mainform_{indexName}";

                if (existingButtonNames.ContainsKey(indexName))
                    continue; // 已存在就跳過

                string displayName = LanguageManager.Translate($"Mainform_{indexName}") ?? indexName;

                Button btn = MachineButton.CreateMachineButton(indexName, targetPanel);
                btn.Name = buttonName;

                container.Controls.Add(btn);
                container.Controls.SetChildIndex(btn, baseInsertIndex);
                Main.Instance.machineButtons.Add(btn);

                string displayname = LanguageManager.Translate(btn.Name);
                MachinePanelGroup.UpdateButtonLabel(btn, displayname);
            }
           
        }
    }
}
