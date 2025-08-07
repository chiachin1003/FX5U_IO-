using CheckedComboBoxDemo;
using CsvHelper;
using FX5U_IOMonitor;
using FX5U_IOMonitor.Data;
using FX5U_IOMonitor.DatabaseProvider;
using FX5U_IOMonitor.Login;
using FX5U_IOMonitor.Models;
using FX5U_IOMonitor.Resources;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Windows.Forms;
using static FX5U_IOMonitor.Resources.Element_Settings;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;


namespace FX5U_IO元件監控
{

    public partial class Alarm_Notify : Form
    {
        private string datatable = "";
        public Alarm_Notify()
        {

            InitializeComponent();
            BuildTree();

            int mode = DBfunction.Get_alarm_classnumber();
            UpdateRadioButtonColor(mode);

            switch (mode)
            {
                case 1:
                    radioButton_alluser.Checked = true;
                    treeView1.CheckBoxes = false;

                    break;
                case 2:
                    checkcombobox_special.Visible = true;
                    radioButton_special.Checked = true;

                    checkcombobox_special.Clear();
                    _ = Add_NotifyUser(checkcombobox_special, NotifyUserMode.FromAlarm);
                    checkcombobox_special.SelectedItemsUpdated += (s, e) =>
                    {
                        selectedUsers = checkcombobox_special.GetCheckedItems();
                    };
                    treeView1.CheckBoxes = false;

                    break;
                case 3:
                    radioButton_DesignatedUser.Checked = true;
                    treeView1.CheckBoxes = true;

                    break;
                default:
                    // 預設選項（必要時）
                    radioButton_alluser.Checked = true;
                    break;
            }

            radioButton_alluser.CheckedChanged += radioButton_CheckedChanged;
            radioButton_special.CheckedChanged += radioButton_CheckedChanged;
            radioButton_DesignatedUser.CheckedChanged += radioButton_CheckedChanged;
            treeView1.AfterCheck += treeView1_AfterCheck_ChildSync;  // 父節點同步子節點

            string lang = FX5U_IOMonitor.Properties.Settings.Default.LanguageSetting;
            LanguageManager.LoadLanguageFromDatabase(lang);
            LanguageManager.LanguageChanged += OnLanguageChanged;
            SwitchLanguage();

        }
        private void OnLanguageChanged(string cultureName)
        {
            SwitchLanguage();
        }


        //private void control_choose_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    datatable = control_choose.Text;
        //    BuildTree(datatable);
        //    int mode = DBfunction.Get_alarm_classnumber();
        //    switch (mode)
        //    {
        //        case 1:
        //            radioButton_alluser.Checked = true;
        //            treeView1.CheckBoxes = false;

        //            break;
        //        case 2:
        //            radioButton_special.Checked = true;

        //            treeView1.CheckBoxes = false;

        //            break;
        //        case 3:
        //            radioButton_DesignatedUser.Checked = true;
        //            treeView1.CheckBoxes = true;

        //            break;
        //        default:
        //            // 預設選項（必要時）
        //            radioButton_alluser.Checked = true;
        //            break;
        //    }
        //}
        private void UpdateRadioButtonColor(int mode)
        {
            var normalColor = Color.Gray;
            var selectedColor = Color.Black;

            radioButton_alluser.ForeColor = (mode == 1) ? selectedColor : normalColor;
            radioButton_special.ForeColor = (mode == 2) ? selectedColor : normalColor;
            radioButton_DesignatedUser.ForeColor = (mode == 3) ? selectedColor : normalColor;
        }

        private void BuildTree()
        {
            treeView1.Nodes.Clear();

            List<string> classList = DBfunction.Get_alarm_class()
                                    .Distinct()
                                    .OrderBy(c => c)
                                    .ToList();
            using var context = new ApplicationDB();
            var allAlarms = context.alarm.ToList();

            foreach (var className in classList)
            {
                // 翻譯分類名稱
                string translationKey = "AlarmNotify_ClassTag_" + className;
                string localizedClassName = LanguageManager.Translate(translationKey);

                TreeNode classNode = new TreeNode(localizedClassName)
                {
                    Tag = className  // ➤ 保留原始分類代碼
                };

                var alarms = allAlarms
                    .Where(a => a.classTag == className)
                    .ToList();

                foreach (var alarm in alarms)
                {
                    string displayError = DBfunction.Get_Error_ByAddress(alarm.address);

                    TreeNode errorNode = new TreeNode(displayError)
                    {
                        Tag = alarm.Id
                    };
                    classNode.Nodes.Add(errorNode);
                }

                treeView1.Nodes.Add(classNode);

                // 自動勾選已設定通知的節點
                bool anyChildHasUser = alarms.Any(a => !string.IsNullOrWhiteSpace(a.AlarmNotifyuser));
                classNode.Checked = anyChildHasUser;

                foreach (TreeNode child in classNode.Nodes)
                {
                    if (child.Tag is int alarmId)
                    {
                        var alarm = alarms.FirstOrDefault(a => a.Id == alarmId);
                        child.Checked = alarm != null && !string.IsNullOrWhiteSpace(alarm.AlarmNotifyuser);
                    }
                }
            }




            //foreach (var className in classList)
            //{
            //    TreeNode classNode = new TreeNode(className)
            //    {
            //        Tag = className // ➤ 儲存 classTag 名稱
            //    };

            //    var alarms = allAlarms
            //        .Where(a => NormalizeClassTag(a.classTag) == className)
            //        .ToList();

            //    foreach (var alarm in alarms)
            //    {
            //        string displayError = DBfunction.Get_Error_ByAddress(alarm.address);

            //        TreeNode errorNode = new TreeNode(displayError)
            //        {
            //            Tag = alarm.Id // ➤ 子節點直接儲存 Alarm Id
            //        };
            //        classNode.Nodes.Add(errorNode);
            //    }

            //    treeView1.Nodes.Add(classNode);

            //    // ✅ 根據資料庫資料自動判斷勾選狀態（如果任一子節點有設通知人）
            //    bool anyChildHasUser = alarms.Any(a => !string.IsNullOrWhiteSpace(a.AlarmNotifyuser));
            //    classNode.Checked = anyChildHasUser;
            //    foreach (TreeNode child in classNode.Nodes)
            //    {
            //        if (child.Tag is int alarmId)
            //        {
            //            var alarm = alarms.FirstOrDefault(a => a.Id == alarmId);
            //            child.Checked = alarm != null && !string.IsNullOrWhiteSpace(alarm.AlarmNotifyuser);
            //        }
            //    }
            //}
        }


        private async void btn_update_Click(object sender, EventArgs e)
        {

            Update_AlarmNotifyClass();

            if (radioButton_alluser.Checked)
            {
                //using var userService = new UserService<ApplicationDB>();
                using var userService = LocalDbProvider.GetUserService();

                var userNameList = userService.GetAllUser();
                _ = All_NotifyUser(userNameList);
                MessageBox.Show(LanguageManager.Translate("Alarm_Notify_UpdateSuccess"));

            }
            else if (radioButton_special.Checked)
            {

                checkcombobox_special.Visible = true;



                if (selectedUsers == null || selectedUsers.Length == 0)
                {
                    MessageBox.Show(LanguageManager.Translate("Alarm_Notify_errormessage_selectUser"),
                        LanguageManager.Translate("Alarm_Notify_errormessage_NoUser"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                All_NotifyUser(selectedUsers, datatable);

                treeView1.CheckBoxes = false;
                MessageBox.Show(LanguageManager.Translate("Alarm_Notify_UpdateSuccess"));


            }
            else if (radioButton_DesignatedUser.Checked)
            {
                checkcombobox_special.Visible = false;
                MessageBox.Show(LanguageManager.Translate("Alarm_Notify_UpdateSuccess"));
                treeView1.CheckBoxes = true;

                // ✅ 根據資料庫自動打勾「已設定的分類」
                using var db = new ApplicationDB();
                foreach (TreeNode node in treeView1.Nodes)
                {
                    string category = node.Tag.ToString();
                    var hasUser = db.alarm
                        .Where(a => a.classTag == category)
                        .Any(a => !string.IsNullOrWhiteSpace(a.AlarmNotifyuser));

                    node.Checked = hasUser;

                    if (hasUser)
                    {
                        // 若分類節點已設定，則其子節點也一併打勾
                        foreach (TreeNode child in node.Nodes)
                        {
                            child.Checked = true;
                        }
                    }
                }
            }
            int mode = DBfunction.Get_alarm_classnumber();
            UpdateRadioButtonColor(mode);
        }


        private async Task Add_NotifyUser(checkcombobox combo, NotifyUserMode mode = NotifyUserMode.All)
        {
            using var userService = LocalDbProvider.GetUserService();
            var allUsers = userService.GetAllUser();

            List<string> filterUserNames;

            if (mode == NotifyUserMode.All)
            {
                filterUserNames = allUsers.Select(u => u.UserName).ToList();
            }
            else // FromAlarm
            {
                // 從 alarm 資料取得已被指定的使用者
                using var db = new ApplicationDB();
                var notifyUserStrs = db.alarm
                    .Where(a => !string.IsNullOrWhiteSpace(a.AlarmNotifyuser))
                    .Select(a => a.AlarmNotifyuser)
                    .ToList();

                filterUserNames = notifyUserStrs
                    .SelectMany(u => u.Split(',', StringSplitOptions.RemoveEmptyEntries))
                    .Select(u => u.Trim())
                    .Distinct()
                    .ToList();
            }

            combo.Clear();

            foreach (var user in allUsers)
            {
                bool shouldAdd = filterUserNames.Contains(user.UserName);
                if (mode == NotifyUserMode.All || shouldAdd)
                {
                    combo.AddItem(user.UserName, isChecked: shouldAdd);
                }
            }

            // 同步 global selectedUsers
            selectedUsers = combo.GetCheckedItems();
        }
        /// <summary>
        /// 取得所有使用者
        /// </summary>
        /// <returns></returns>
        private async Task All_NotifyUser(List<ApplicationUser> userNameList)
        {
            //using var userService = new UserService<ApplicationDB>();
            //var userNameList = await userService.GetAllUser();
            // 將 UserName 用逗號串接
            string userNames = string.Join(",", userNameList.Select(u => u.UserName));


            using (var db = new ApplicationDB())
            {
                var allAlarms = db.alarm.ToList();
                foreach (var alarm in allAlarms)
                {
                    alarm.AlarmNotifyuser = userNames;
                }
                await db.SaveChangesAsync();

            }
            ;

        }
        private static void All_NotifyUser(string[] userNameList, string datatable)
        {

            string userNames = string.Join(",", userNameList);

            using (var db = new ApplicationDB())
            {
                var allAlarms = db.alarm.ToList();
                foreach (var alarm in allAlarms)
                {
                    alarm.AlarmNotifyuser = userNames;
                }
                db.SaveChanges();
            }
            ;

        }


        private async void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            // 使用者點選一律無效，取消打勾
            if (e.Action == TreeViewAction.ByMouse)
            {
                e.Node.Checked = !e.Node.Checked; // 還原勾選
                return;
            }

            if (e.Action != TreeViewAction.ByMouse)
                return;
        }

        /// <summary>
        /// 當父節點被勾選/取消時，讓所有子節點狀態同步
        /// </summary>
        private void treeView1_AfterCheck_ChildSync(object sender, TreeViewEventArgs e)
        {
            //// 如果是由程式或手動打勾，才同步
            if (e.Action != TreeViewAction.ByMouse)
                return;

            // 只有父節點才同步子節點
            if (e.Node.Parent == null)
            {
                foreach (TreeNode child in e.Node.Nodes)
                {
                    child.Checked = e.Node.Checked;
                }
            }

        }


        private static List<string> GetCheckedCategories(TreeNodeCollection nodes)
        {
            var result = new List<string>();
            foreach (TreeNode node in nodes)
            {
                if (node.Checked)
                    result.Add(node.Tag.ToString());

                if (node.Nodes.Count > 0)
                    result.AddRange(GetCheckedCategories(node.Nodes));
            }
            return result;
        }
        string[] selectedUsers;

        private async void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_alluser.Checked)
            {
                checkcombobox_special.Visible = false;
                treeView1.CheckBoxes = false;


            }
            else if (radioButton_special.Checked)
            {
                checkcombobox_special.Visible = true;
                checkcombobox_special.Clear();
                _ = Add_NotifyUser(checkcombobox_special, NotifyUserMode.FromAlarm);
                treeView1.CheckBoxes = false;
                checkcombobox_special.SelectedItemsUpdated += (s, e) =>
                {
                    selectedUsers = checkcombobox_special.GetCheckedItems();
                };


            }
            else if (radioButton_DesignatedUser.Checked)
            {
                int mode = DBfunction.Get_alarm_classnumber();

                if (mode != 3)
                {
                    return;
                }

                checkcombobox_special.Visible = false;
                treeView1.CheckBoxes = true;
                UpdateTreeNodeCheckStates();
            }

        }

        private void treeView1_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Action == TreeViewAction.ByMouse)
                e.Cancel = true; // ✅ 阻止使用者手動勾選
        }
        /// <summary>
        /// 確定當前資料庫讀取的警告發送標記
        /// </summary>
        private void Update_AlarmNotifyClass()
        {
            string machineName = control_choose.Text;
            try
            {
                if (radioButton_alluser == null || radioButton_special == null || radioButton_DesignatedUser == null)
                {
                    MessageBox.Show("RadioButton" + LanguageManager.Translate("Alarm_Notify_errormessage_NoInitial"));
                    return;
                }

                int notifyClass = radioButton_alluser.Checked ? 1 :
                                  radioButton_special.Checked ? 2 :
                                  radioButton_DesignatedUser.Checked ? 3 : 0;

                if (notifyClass == 0)
                {
                    MessageBox.Show(LanguageManager.Translate("Alarm_Notify_notification_method"));
                    return;
                }

                using (var db = new ApplicationDB())
                {
                    var alarm = db.alarm.ToList();

                    if (alarm == null)
                    {
                        MessageBox.Show(LanguageManager.Translate("Alarm_Notify_errormessage_NoGroup"));
                        return;
                    }

                    foreach (var alarm_io in alarm)
                    {
                        alarm_io.AlarmNotifyClass = notifyClass;
                    }

                    db.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageManager.Translate("Message_Error") + $"：{ex.Message}");
            }


        }



        private List<TreeNode> GetCheckedNodes(TreeNodeCollection nodes)
        {
            var result = new List<TreeNode>();
            foreach (TreeNode node in nodes)
            {
                if (node.Checked)
                    result.Add(node);

                if (node.Nodes.Count > 0)
                    result.AddRange(GetCheckedNodes(node.Nodes));
            }
            return result;
        }


        private async void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {

            if (!radioButton_DesignatedUser.Checked)
            {
                return;
            }
            int mode = DBfunction.Get_alarm_classnumber();

            if (mode != 3)
            {
                MessageBox.Show(LanguageManager.Translate("Message_Error"));
                return;
            }

            var hit = treeView1.HitTest(e.Location);

            if (hit.Location != TreeViewHitTestLocations.Label)
                return; // 不處理勾勾或空白
            using var userService = LocalDbProvider.GetUserService();
            var allUsers = userService.GetAllUser();
            var db = new ApplicationDB();

            string alarmKey;
            List<Alarm> targetAlarms;
            string formTitle;

            if (e.Node.Parent == null)
            {
                // ✅ 分類節點：以 classTag 做設定
                string category = (string)e.Node.Tag;
                alarmKey = category;
                var allMatching = db.alarm.ToList(); // ➤ 強制先取回所有 alarm
                targetAlarms = allMatching.Where(a => a.classTag == category).ToList();

                formTitle = LanguageManager.Translate("Alarm_Notify_Setting_group") + $"：{category}";
            }
            else
            {
                // ✅ 子節點
                int alarmId = (int)e.Node.Tag;
                var alarm = db.alarm.FirstOrDefault(a => a.Id == alarmId);
                if (alarm == null) return;

                alarmKey = alarm.Error;
                targetAlarms = new List<Alarm> { alarm };
                formTitle = LanguageManager.Translate("Alarm_Notify_Setting_error") + $"：{alarmKey}";
            }

            var currentUsersRaw = targetAlarms.FirstOrDefault()?.AlarmNotifyuser;
            var currentUserList = string.IsNullOrWhiteSpace(currentUsersRaw)
                ? Array.Empty<string>()
                : currentUsersRaw.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToArray();

            var form = new Form
            {
                Width = 300,
                Height = 250,
                Text = formTitle,
                StartPosition = FormStartPosition.CenterScreen // ➤ 顯示在螢幕正中央
            };
            var combo = new checkcombobox { Dock = DockStyle.Top };

            foreach (var user in allUsers)
            {
                bool isChecked = currentUserList.Contains(user.UserName);
                combo.AddItem(user.UserName, isChecked);
            }

            var btnOk = new Button { Text = "確認", Dock = DockStyle.Bottom };
            btnOk.Click += async (s, ev) =>
            {
                var selected = combo.GetCheckedItems();
                var users = string.Join(",", selected);

                foreach (var alarm in targetAlarms)
                {
                    alarm.AlarmNotifyuser = users;
                }

                await db.SaveChangesAsync();
                // ✅ 根據是否有設定使用者來變更 TreeView 勾選狀態
                foreach (TreeNode parent in treeView1.Nodes)
                {
                    foreach (TreeNode child in parent.Nodes)
                    {
                        if (child.Tag is int alarmId)
                        {
                            if (targetAlarms.Any(a => a.Id == alarmId))
                            {
                                // 找到剛剛更新過的 alarm
                                var alarm = targetAlarms.First(a => a.Id == alarmId);
                                child.Checked = !string.IsNullOrWhiteSpace(alarm.AlarmNotifyuser);
                            }
                        }
                    }

                    // ✅ 更新父節點勾選狀態（僅當所有子節點都勾選時）
                    parent.Checked = parent.Nodes.Cast<TreeNode>().All(n => n.Checked);
                }

                MessageBox.Show($"✅ {alarmKey} 已設定 {selected.Length} 位使用者！");
                form.Close();
            };

            form.Controls.Add(combo);
            form.Controls.Add(btnOk);
            form.ShowDialog();
        }

        private void UpdateTreeNodeCheckStates()
        {
            using var db = new ApplicationDB();
            foreach (TreeNode classNode in treeView1.Nodes)
            {
                string classTag = (string)classNode.Tag;

                var alarms = db.alarm.Where(a => a.classTag == classTag)
                                    .ToList();

                // 子節點自動勾選
                foreach (TreeNode child in classNode.Nodes)
                {
                    if (child.Tag is int alarmId)
                    {
                        var alarm = alarms.FirstOrDefault(a => a.Id == alarmId);
                        child.Checked = alarm != null && !string.IsNullOrWhiteSpace(alarm.AlarmNotifyuser);
                    }
                }

                // 父節點：只要有任一子節點勾選，就勾選
                classNode.Checked = classNode.Nodes.Cast<TreeNode>().Any(n => n.Checked);
            }
        }

        public enum NotifyUserMode
        {
            All,        // 所有使用者
            FromAlarm   // 只加入當前 alarm 通知使用者
        }

        private void SwitchLanguage()
        {
            this.Text = LanguageManager.Translate("Alarm_Notify_title");
            radioButton_alluser.Text = LanguageManager.Translate("Alarm_Notify_alluser");
            radioButton_special.Text = LanguageManager.Translate("Alarm_Notify_special");
            radioButton_DesignatedUser.Text = LanguageManager.Translate("Alarm_Notify_DesignatedUser");
            lab_alarm_notify.Text = LanguageManager.Translate("Alarm_Notify_type");
            lab_machine.Text = LanguageManager.Translate("Alarm_Notify_machine");
            btn_apply.Text = LanguageManager.Translate("Alarm_Notify_btn_apply");
            btn_update.Text = LanguageManager.Translate("Alarm_Notify_btn_update");
        }

        private void btn_apply_Click(object sender, EventArgs e)
        {

        }

        private void radioButton_special_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}



