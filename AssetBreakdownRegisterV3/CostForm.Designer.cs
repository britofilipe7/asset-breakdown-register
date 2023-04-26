namespace AssetBreakdownRegisterV2
{
    partial class CostForm
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
            this.lblCost = new System.Windows.Forms.Label();
            this.txtCost = new System.Windows.Forms.TextBox();
            this.btnCostSave = new System.Windows.Forms.Button();
            this.btnCostCancel = new System.Windows.Forms.Button();
            this.lblObservations = new System.Windows.Forms.Label();
            this.txtObservations = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblCost
            // 
            this.lblCost.AutoSize = true;
            this.lblCost.Location = new System.Drawing.Point(52, 13);
            this.lblCost.Name = "lblCost";
            this.lblCost.Size = new System.Drawing.Size(114, 13);
            this.lblCost.TabIndex = 0;
            this.lblCost.Text = "Custo da manutenção:";
            // 
            // txtCost
            // 
            this.txtCost.Location = new System.Drawing.Point(166, 10);
            this.txtCost.Name = "txtCost";
            this.txtCost.Size = new System.Drawing.Size(86, 20);
            this.txtCost.TabIndex = 1;
            // 
            // btnCostSave
            // 
            this.btnCostSave.Location = new System.Drawing.Point(55, 154);
            this.btnCostSave.Name = "btnCostSave";
            this.btnCostSave.Size = new System.Drawing.Size(64, 20);
            this.btnCostSave.TabIndex = 2;
            this.btnCostSave.Text = "Confirmar";
            this.btnCostSave.UseVisualStyleBackColor = true;
            this.btnCostSave.Click += new System.EventHandler(this.btnCostSave_Click);
            // 
            // btnCostCancel
            // 
            this.btnCostCancel.Location = new System.Drawing.Point(188, 154);
            this.btnCostCancel.Name = "btnCostCancel";
            this.btnCostCancel.Size = new System.Drawing.Size(64, 20);
            this.btnCostCancel.TabIndex = 3;
            this.btnCostCancel.Text = "Cancelar";
            this.btnCostCancel.UseVisualStyleBackColor = true;
            this.btnCostCancel.Click += new System.EventHandler(this.btnCostCancel_Click);
            // 
            // lblObservations
            // 
            this.lblObservations.AutoSize = true;
            this.lblObservations.Location = new System.Drawing.Point(52, 48);
            this.lblObservations.Name = "lblObservations";
            this.lblObservations.Size = new System.Drawing.Size(73, 13);
            this.lblObservations.TabIndex = 4;
            this.lblObservations.Text = "Observações:";
            // 
            // txtObservations
            // 
            this.txtObservations.Location = new System.Drawing.Point(55, 64);
            this.txtObservations.Multiline = true;
            this.txtObservations.Name = "txtObservations";
            this.txtObservations.Size = new System.Drawing.Size(197, 84);
            this.txtObservations.TabIndex = 5;
            // 
            // CostForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 195);
            this.Controls.Add(this.txtObservations);
            this.Controls.Add(this.lblObservations);
            this.Controls.Add(this.btnCostCancel);
            this.Controls.Add(this.btnCostSave);
            this.Controls.Add(this.txtCost);
            this.Controls.Add(this.lblCost);
            this.Name = "CostForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lblCost;
        private TextBox txtCost;
        private Button btnCostSave;
        private Button btnCostCancel;
        private Label lblObservations;
        private TextBox txtObservations;
    }
}