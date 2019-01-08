namespace QLDT
{
    partial class SHOW_FORM
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
            this.dataGridShowForm = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridShowForm)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridShowForm
            // 
            this.dataGridShowForm.AllowUserToDeleteRows = false;
            this.dataGridShowForm.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridShowForm.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridShowForm.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridShowForm.Location = new System.Drawing.Point(-1, 0);
            this.dataGridShowForm.Name = "dataGridShowForm";
            this.dataGridShowForm.Size = new System.Drawing.Size(1283, 582);
            this.dataGridShowForm.TabIndex = 0;
            // 
            // SHOW_FORM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 580);
            this.Controls.Add(this.dataGridShowForm);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SHOW_FORM";
            this.Text = "SHOW_FORM";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridShowForm)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridShowForm;
    }
}