namespace ViewStateValidator
{
	partial class MainForm
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
			this.label1 = new System.Windows.Forms.Label();
			this.txtEventValidation = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtViewState = new System.Windows.Forms.TextBox();
			this.btnParseEventValidation = new System.Windows.Forms.Button();
			this.grdEventFields = new System.Windows.Forms.DataGridView();
			this.btnParseViewState = new System.Windows.Forms.Button();
			this.txtViewStateHash = new System.Windows.Forms.TextBox();
			this.btnClose = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.grdEventFields)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(122, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "__EVENTVALIDATION:";
			// 
			// txtEventValidation
			// 
			this.txtEventValidation.Location = new System.Drawing.Point(13, 30);
			this.txtEventValidation.MaxLength = 1000000;
			this.txtEventValidation.Multiline = true;
			this.txtEventValidation.Name = "txtEventValidation";
			this.txtEventValidation.Size = new System.Drawing.Size(277, 95);
			this.txtEventValidation.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 136);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(85, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "__VIEWSTATE:";
			// 
			// txtViewState
			// 
			this.txtViewState.Location = new System.Drawing.Point(12, 153);
			this.txtViewState.MaxLength = 1000000;
			this.txtViewState.Multiline = true;
			this.txtViewState.Name = "txtViewState";
			this.txtViewState.Size = new System.Drawing.Size(277, 95);
			this.txtViewState.TabIndex = 1;
			// 
			// btnParseEventValidation
			// 
			this.btnParseEventValidation.Location = new System.Drawing.Point(297, 30);
			this.btnParseEventValidation.Name = "btnParseEventValidation";
			this.btnParseEventValidation.Size = new System.Drawing.Size(24, 25);
			this.btnParseEventValidation.TabIndex = 2;
			this.btnParseEventValidation.Text = ">";
			this.btnParseEventValidation.UseVisualStyleBackColor = true;
			this.btnParseEventValidation.Click += new System.EventHandler(this.btnParseEventValidation_Click);
			// 
			// grdEventFields
			// 
			this.grdEventFields.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.grdEventFields.Location = new System.Drawing.Point(328, 30);
			this.grdEventFields.Name = "grdEventFields";
			this.grdEventFields.Size = new System.Drawing.Size(159, 95);
			this.grdEventFields.TabIndex = 3;
			// 
			// btnParseViewState
			// 
			this.btnParseViewState.Location = new System.Drawing.Point(295, 153);
			this.btnParseViewState.Name = "btnParseViewState";
			this.btnParseViewState.Size = new System.Drawing.Size(24, 25);
			this.btnParseViewState.TabIndex = 2;
			this.btnParseViewState.Text = ">";
			this.btnParseViewState.UseVisualStyleBackColor = true;
			// 
			// txtViewStateHash
			// 
			this.txtViewStateHash.Location = new System.Drawing.Point(328, 153);
			this.txtViewStateHash.Name = "txtViewStateHash";
			this.txtViewStateHash.ReadOnly = true;
			this.txtViewStateHash.Size = new System.Drawing.Size(159, 20);
			this.txtViewStateHash.TabIndex = 5;
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(411, 224);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 6;
			this.btnClose.Text = "&Close";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(499, 262);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.txtViewStateHash);
			this.Controls.Add(this.grdEventFields);
			this.Controls.Add(this.btnParseViewState);
			this.Controls.Add(this.btnParseEventValidation);
			this.Controls.Add(this.txtViewState);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtEventValidation);
			this.Controls.Add(this.label1);
			this.Name = "MainForm";
			this.Text = "View State Validator";
			((System.ComponentModel.ISupportInitialize)(this.grdEventFields)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtEventValidation;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtViewState;
		private System.Windows.Forms.Button btnParseEventValidation;
		private System.Windows.Forms.DataGridView grdEventFields;
		private System.Windows.Forms.Button btnParseViewState;
		private System.Windows.Forms.TextBox txtViewStateHash;
		private System.Windows.Forms.Button btnClose;
	}
}

