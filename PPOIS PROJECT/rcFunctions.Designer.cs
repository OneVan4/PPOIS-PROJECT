namespace PPOIS_PROJECT
{
    partial class rcFunctions
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button13 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.checkedListBox713 = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(72, 258);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(105, 50);
            this.button13.TabIndex = 5;
            this.button13.Text = "Выбрать всё";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // button14
            // 
            this.button14.Location = new System.Drawing.Point(274, 258);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(107, 50);
            this.button14.TabIndex = 4;
            this.button14.Text = "Добавить";
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.button14_Click);
            // 
            // checkedListBox713
            // 
            this.checkedListBox713.FormattingEnabled = true;
            this.checkedListBox713.Location = new System.Drawing.Point(0, 10);
            this.checkedListBox713.Name = "checkedListBox713";
            this.checkedListBox713.Size = new System.Drawing.Size(477, 242);
            this.checkedListBox713.TabIndex = 3;
            this.checkedListBox713.SelectedIndexChanged += new System.EventHandler(this.checkedListBox713_SelectedIndexChanged);
            // 
            // rcFunctions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 318);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.button14);
            this.Controls.Add(this.checkedListBox713);
            this.Name = "rcFunctions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "rcFunctions";
            this.Load += new System.EventHandler(this.rcFunctions_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button14;
        public System.Windows.Forms.CheckedListBox checkedListBox713;
    }
}