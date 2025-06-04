namespace CheckedComboBoxDemo
{
    partial class checkcombobox
    {
        /// <summary> 
        /// 設計工具所需的變數。
        private TextBox displayBox;
        private Button dropDownButton;
        private CheckedListBox checkedListBox;
        private Form popupForm;
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            displayBox = new TextBox();
            dropDownButton = new Button();
            SuspendLayout();
            // 
            // displayBox
            // 
            displayBox.Dock = DockStyle.Fill;
            displayBox.Location = new Point(0, 0);
            displayBox.Name = "displayBox";
            displayBox.ReadOnly = true;
            displayBox.Size = new Size(151, 23);
            displayBox.TabIndex = 0;
            // 
            // dropDownButton
            // 
            dropDownButton.Dock = DockStyle.Right;
            dropDownButton.Location = new Point(151, 0);
            dropDownButton.Name = "dropDownButton";
            dropDownButton.Size = new Size(23, 23);
            dropDownButton.TabIndex = 1;
            dropDownButton.Text = "▼";
            dropDownButton.Click += DropDownButton_Click;
            // 
            // checkcombobox
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(displayBox);
            Controls.Add(dropDownButton);
            Name = "checkcombobox";
            Size = new Size(174, 23);
            ResumeLayout(false);
            PerformLayout();
        }
        private void InitializePopup()
        {
            this.checkedListBox = new CheckedListBox();
            this.checkedListBox.CheckOnClick = true;
            //this.checkedListBox.ItemCheck += (s, e) => {
            //    this.BeginInvoke((Action)(() => UpdateDisplayText()));
            //};
            checkedListBox.ItemCheck += (s, e) =>
            {
                if (this.IsHandleCreated)
                {
                    this.BeginInvoke((Action)(() => UpdateDisplayText()));
                }
            };

            this.popupForm = new Form
            {
                FormBorderStyle = FormBorderStyle.None,
                ShowInTaskbar = false,
                StartPosition = FormStartPosition.Manual,
                TopMost = true,
                Size = new Size(this.Width, 150)
            };
            this.popupForm.Controls.Add(checkedListBox);
            this.popupForm.Deactivate += (s, e) => popupForm.Hide();
        }

        private void DropDownButton_Click(object sender, EventArgs e)
        {
            if (!popupForm.Visible)
            {
                Point location = this.PointToScreen(new Point(0, this.Height));
                popupForm.Location = location;
                popupForm.Width = this.Width;
                checkedListBox.Width = this.Width;
                popupForm.Show();
            }
            else
            {
                popupForm.Hide();
            }
        }

        private void UpdateDisplayText()
        {
            var items = checkedListBox.CheckedItems.Cast<string>();

            displayBox.Text = string.Join(", ", items);

            // 觸發事件通知外部
            SelectedItemsUpdated?.Invoke(this, EventArgs.Empty);
        }

        // 公開方法：加入選項
        public void AddItem(string item, bool isChecked = false)
        {
            checkedListBox.Items.Add(item, isChecked);
            UpdateDisplayText();
        }
        public void Clear()
        {
            checkedListBox.Items.Clear();    // 清除所有項目
            displayBox.Clear();              // 清空上方顯示框文字
        }
        public event EventHandler? SelectedItemsUpdated;
       
        // 取回所有勾選項目
        public string[] GetCheckedItems()
        {
            return checkedListBox.CheckedItems.Cast<string>().ToArray();
            //return checkedListBox.CheckedItems.Cast<object>().Select(i => i.ToString()).ToArray();
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            if (popupForm != null)
            {
                popupForm.Width = this.Width;
                checkedListBox.Width = this.Width;
            }
        }
        #endregion
    }
}
