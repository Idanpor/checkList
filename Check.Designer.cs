namespace CheckListTool
{
    partial class Check
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.AlreadyCoveredrb = new System.Windows.Forms.RadioButton();
            this.Helpfulrb = new System.Windows.Forms.RadioButton();
            this.NotRelevantrb = new System.Windows.Forms.RadioButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // AlreadyCoveredrb
            // 
            this.AlreadyCoveredrb.AutoSize = true;
            this.AlreadyCoveredrb.Location = new System.Drawing.Point(422, 3);
            this.AlreadyCoveredrb.Name = "AlreadyCoveredrb";
            this.AlreadyCoveredrb.Size = new System.Drawing.Size(103, 17);
            this.AlreadyCoveredrb.TabIndex = 1;
            this.AlreadyCoveredrb.TabStop = true;
            this.AlreadyCoveredrb.Text = "Already Covered";
            this.AlreadyCoveredrb.UseVisualStyleBackColor = true;
            // 
            // Helpfulrb
            // 
            this.Helpfulrb.AutoSize = true;
            this.Helpfulrb.Location = new System.Drawing.Point(317, 3);
            this.Helpfulrb.Name = "Helpfulrb";
            this.Helpfulrb.Size = new System.Drawing.Size(58, 17);
            this.Helpfulrb.TabIndex = 2;
            this.Helpfulrb.TabStop = true;
            this.Helpfulrb.Text = "Helpful";
            this.Helpfulrb.UseVisualStyleBackColor = true;
            // 
            // NotRelevantrb
            // 
            this.NotRelevantrb.AutoSize = true;
            this.NotRelevantrb.Location = new System.Drawing.Point(549, 3);
            this.NotRelevantrb.Name = "NotRelevantrb";
            this.NotRelevantrb.Size = new System.Drawing.Size(88, 17);
            this.NotRelevantrb.TabIndex = 3;
            this.NotRelevantrb.TabStop = true;
            this.NotRelevantrb.Text = "Not Relevant";
            this.NotRelevantrb.UseVisualStyleBackColor = true;
            // 
            // Check
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.NotRelevantrb);
            this.Controls.Add(this.Helpfulrb);
            this.Controls.Add(this.AlreadyCoveredrb);
            this.Controls.Add(this.label1);
            this.Name = "Check";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Size = new System.Drawing.Size(640, 26);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton AlreadyCoveredrb;
        private System.Windows.Forms.RadioButton Helpfulrb;
        private System.Windows.Forms.RadioButton NotRelevantrb;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
