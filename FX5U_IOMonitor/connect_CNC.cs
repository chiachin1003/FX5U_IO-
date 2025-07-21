
using FX5U_IOMonitor.Models;
using SLMP;
using EZNCAUTLib;




namespace FX5U_IOMonitor
{
    public partial class Connect_CNC : Form
    {

        private DispEZNcCommunication EZNcCom;
        long IRet = 0;
        private CancellationTokenSource? _cts;


        public Connect_CNC()
        {
            InitializeComponent();
            
            string lang = Properties.Settings.Default.LanguageSetting;
            LanguageManager.LoadLanguageFromDatabase(lang);
            SwitchLanguage();
        
            ApplyAutoFontShrinkToTableLabels(tableLayoutPanel1);



            LanguageManager.LanguageChanged += OnLanguageChanged;

            connect_choose.SelectedIndexChanged += connect_choose_SelectedIndexChanged;
            control_choose.SelectedIndexChanged += control_choose_SelectedIndexChanged;

        }
        private void OnLanguageChanged(string cultureName)
        {
            SwitchLanguage();
          
            ApplyAutoFontShrinkToTableLabels(tableLayoutPanel1);

        }

        private void connect_choose_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (connect_choose.SelectedIndex == 0)
            {
                panel_Ethernet.Visible = true;
                panel_RS485.Visible = false;
                button_FILE.Visible = false;

            }
            if (connect_choose.SelectedIndex == 1)
            {
                panel_RS485.Visible = true;
                panel_Ethernet.Visible = false;
                button_FILE.Visible = true;
                combobox_text_center();


            }
            if (connect_choose.SelectedIndex == 2)
            {
                panel_RS485.Visible = true;
                panel_Ethernet.Visible = false;
                button_FILE.Visible = true;
                combobox_text_center();

            }
          

        }



        private void btn_connect_ethernet_Click(object sender, EventArgs e)
        {
            if (EZNcCom != null)
            {
                EZNcCom.Close();
            }
            EZNcCom = new DispEZNcCommunication();
            IRet = EZNcCom.SetTCPIPProtocol(txb_IP.Text, int.Parse(txb_port.Text)); //三菱模擬器連線
            if (IRet == 0)
            {
                string selected = control_choose.SelectedItem.ToString();
                int systemType = -1;
                switch (selected)
                {
                    case "M700-L":
                        systemType = 5;
                        break;
                    case "M700-M":
                        systemType = 6;
                        break;
                    case "M800-L":
                        systemType = 8;
                        break;
                    case "M800-M":
                        systemType = 9;
                        break;
                    default:
                        MessageBox.Show("未定義的模式！");
                        break;
                }
                IRet = EZNcCom.Open2(systemType, 1, 10, "EZNC_LOCALHOST");
                if (IRet != 0)
                {
                    MessageBox.Show("❌ CNC 連線失敗，請確認控制器型號");
                    return;
                }
                IRet = EZNcCom.SetHead(1);
                if (IRet != 0)
                {
                    MessageBox.Show("⚠️ 無法設置 Head，請確認 CNC 連線狀態！");
                    return;
                }
                _ = StartMonitoringAsync(); // ✅ 啟動背景監控


            }
            else 
            {
                MessageBox.Show("連線失敗、請確認IP及Port是否有誤");
            }

        }

        private void btn_disconnect_ethernet_Click(object sender, EventArgs e)
        {
            StopMonitoring();
            EZNcCom.Close();
            lab_X.Text = "";
            lab_Y.Text = "";
            lab_Z.Text = "";
        }

        private void btn_disconnect_RS485_Click(object sender, EventArgs e)
        {
            if (EZNcCom != null)
            {
                EZNcCom.Close();
            }
            lab_Error.Text = "";

            EZNcCom = new DispEZNcCommunication();
            IRet = EZNcCom.SetTCPIPProtocol(txb_IP.Text, int.Parse(txb_port.Text)); //三菱模擬器連線
            if (IRet != 0)
            {
                AbortWithError("❌ TCP/IP 連線設定失敗");
                return;
            }
            string selected = control_choose.SelectedItem.ToString();
            int systemType = selected switch
            {
                "M700-L" => 5,
                "M700-M" => 6,
                "M800-L" => 8,
                "M800-M" => 9,
                _ => -1
            };
            if (systemType == -1)
            {
                AbortWithError("❌ 未定義的控制器型號");
                return;
            }

            IRet = EZNcCom.Open2(systemType, 1, 10, "EZNC_LOCALHOST");
            if (IRet != 0)
            {
                AbortWithError("❌ CNC 連線失敗，請確認控制器型號");
                return;
            }

            IRet = EZNcCom.SetHead(1);
            if (IRet != 0)
            {
                AbortWithError("⚠️ 無法設置 Head，請確認 CNC 連線狀態！");
                return;
            }

            string fullPath = txb_file_address.Text + txb_file_name.Text;
            string FileInfo = "";

            IRet = EZNcCom.File_FindDir2(fullPath, 1, out FileInfo);
            IRet = EZNcCom.File_ResetDir();
            if (IRet != 0)
            {
                AbortWithError("⚠️ 無法重置檔案資料夾");
                return;
            }

            IRet = EZNcCom.File_Delete(fullPath);
            if (IRet != 0)
            {
                AbortWithError("⚠️ 無法刪除檔案，請確認是否有權限或路徑正確");
                return;
            }

            EZNcCom.Close();
            lab_Error.Text = "✅ 檔案已成功刪除";
            lab_Error.ForeColor = Color.Green;
        }


        private void control_choose_SelectedIndexChanged(object sender, EventArgs e)
        {
            combobox_text_center();

            if (control_choose.SelectedIndex == 0)
            {

                panel1.Visible = true;
            }
            if (control_choose.SelectedIndex == 1)
            {
                panel1.Visible = true;
            }
            if (control_choose.SelectedIndex == 2)
            {
                panel1.Visible = false;
            }
            if (control_choose.SelectedIndex == 2 && connect_choose.SelectedIndex == 2)
            {
                panel1.Visible = false;
                button_FILE.Visible = true;

            }

        }

        private void button_FILE_Click(object sender, EventArgs e)
        {
            if (EZNcCom != null)
            {
                EZNcCom.Close();
            }
            lab_Error.Text = "";
            EZNcCom = new DispEZNcCommunication();

            IRet = EZNcCom.SetTCPIPProtocol(txb_IP.Text, int.Parse(txb_port.Text));
            if (IRet != 0) { AbortWithError("❌ TCP/IP 連線設定失敗"); return; }

            string selected = control_choose.SelectedItem.ToString();
            int systemType = selected switch
            {
                "M700-L" => 5,
                "M700-M" => 6,
                "M800-L" => 8,
                "M800-M" => 9,
                _ => -1
            };

            if (systemType == -1) { AbortWithError("❌ 未定義的控制器型號"); return; }

            IRet = EZNcCom.Open2(systemType, 1, 10, "EZNC_LOCALHOST");
            if (IRet != 0) { AbortWithError("❌ CNC 連線失敗，請確認控制器型號"); return; }

            IRet = EZNcCom.SetHead(1);
            if (IRet != 0) { AbortWithError("⚠️ 無法設置 Head，請確認 CNC 連線狀態！"); return; }

            IRet = EZNcCom.File_OpenNCFile2(txb_file_address.Text + txb_file_name.Text, 3);
            if (IRet != 0) { AbortWithError("⚠️ 檔案路徑有誤，請確認檔案路徑是否正確！"); return; }

            byte[] ncBytes = File.ReadAllBytes(txb_file_name.Text);
            IRet = EZNcCom.File_WriteNCFile(ncBytes);
            if (IRet != 0) { AbortWithError("❌ 檔案寫入失敗"); return; }

            IRet = EZNcCom.File_CloseNCFile2();
            if (IRet != 0) { AbortWithError("⚠️ 檔案未正確關閉"); return; }

            // 最後再主動關閉連線
            EZNcCom.Close();
            lab_Error.Text = "✅ 程式已成功傳送至 CNC ";
            lab_Error.ForeColor=Color.Green;

        }
        private void AbortWithError(string message)
        {
            lab_Error.Text = message;
            lab_Error.ForeColor = Color.Red;
            EZNcCom?.Close();
            return;
        }




        private void SwitchLanguage()
        {
           
            lab_MachineType.Text = LanguageManager.Translate("Connect_MachineType");
            lab_Type.Text = LanguageManager.Translate("Connect_Type");
            label_IP.Text = LanguageManager.Translate("Connect_Enthernetaddress");
            lab_Enthernetport.Text = LanguageManager.Translate("Connect_Enthernetport");
            btn_connect_ethernet.Text = LanguageManager.Translate("Connect_Connect");
            btn_disconnect_ethernet.Text = LanguageManager.Translate("Connect_Disconnect");
          
            btn_disconnect_RS485.Text = LanguageManager.Translate("Connect_Filedelete");
            button_FILE.Text = LanguageManager.Translate("Connect_Fileinput");

        }
        private void combobox_text_center()
        {


        }
        private void ApplyAutoFontShrinkToTableLabels(TableLayoutPanel panel)
        {
            foreach (Control ctrl in panel.Controls)
            {
                if (ctrl is Label lbl)
                {
                    lbl.Dock = DockStyle.Fill;
                    lbl.AutoSize = false;
                    lbl.TextAlign = ContentAlignment.MiddleLeft;
                    Text_design.FitFontToLabel(lbl);
                }
            }
        }

        private void btn_mishubishi_Click(object sender, EventArgs e)
        {
            StopMonitoring();
            if (EZNcCom != null)
            {
                EZNcCom.Close();
            }
            var main = Main.Instance;

            if (main == null) return;

            // 清空 panel，再顯示 plcForm
            main.panel_main.Controls.Clear();

            var plcForm = new Connect_PLC(main);

            plcForm.TopLevel = false; // 禁止作為獨立窗口
            plcForm.FormBorderStyle = FormBorderStyle.None; // 移除邊框
            plcForm.Dock = DockStyle.Fill; // 填滿 Panel

            // 將子窗體添加到 Panel 並顯示
            main.panel_main.Controls.Clear(); // 清空 Panel
            main.panel_main.Controls.Add(plcForm); // 添加子窗體
            plcForm.Show(); // 顯示子窗體
           

        }
        private async Task StartMonitoringAsync()
        {
            _cts = new CancellationTokenSource();
            while (!_cts.IsCancellationRequested)
            {
                try
                {
                    GetMachinePositionAndDisplay(); // 呼叫更新
                    await Task.Delay(500, _cts.Token); // 每 500ms
                }
                catch (TaskCanceledException)
                {
                    break; // 正常中止
                }
                catch (Exception ex)
                {
                    Console.WriteLine("背景更新錯誤：" + ex.Message);
                }
            }
        }
        private void StopMonitoring()
        {
            _cts?.Cancel();
            _cts = null;
        }
        private void GetMachinePositionAndDisplay()
        {
            double MXPosition = 0.0, MYPosition = 0.0, MZPosition = 0.0;

            IRet = EZNcCom.Position_GetMachinePosition2(1, out MXPosition, 0);
            if (IRet != 0) { MXPosition = 0.0; }

            IRet = EZNcCom.Position_GetMachinePosition2(2, out MYPosition, 0);
            if (IRet != 0) { MYPosition = 0.0; }

            IRet = EZNcCom.Position_GetMachinePosition2(3, out MZPosition, 0);
            if (IRet != 0) { MZPosition = 0.0; }

            // 執行緒安全更新 UI
            if (lab_X.InvokeRequired)
            {
                lab_X.Invoke(new Action(() =>
                {
                    lab_X.Text = MXPosition.ToString("F3");
                    lab_Y.Text = MYPosition.ToString("F3");
                    lab_Z.Text = MZPosition.ToString("F3");
                }));
            }
            else
            {
                lab_X.Text = MXPosition.ToString("F3");
                lab_Y.Text = MYPosition.ToString("F3");
                lab_Z.Text = MZPosition.ToString("F3");
            }
        }
    }
}



