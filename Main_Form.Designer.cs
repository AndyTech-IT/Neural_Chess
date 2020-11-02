namespace Neural_Chess
{
    partial class Main_Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main_Form));
            this.Chess_Field1 = new Chess_Field(@"C:\Users\Евгения\Desktop\Andy\Neural_Chess\Default_Images");
            this.SuspendLayout();
            // 
            // Chess_Field1
            // 
            this.Chess_Field1.BlackCell_Image = ((System.Drawing.Image)(resources.GetObject("Chess_Field1.BlackCell_Image")));
            this.Chess_Field1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Chess_Field1.Location = new System.Drawing.Point(89, 25);
            this.Chess_Field1.Name = "Chess_Field1";
            this.Chess_Field1.Size = new System.Drawing.Size(418, 418);
            this.Chess_Field1.TabIndex = 0;
            this.Chess_Field1.WhiteCell_Image = ((System.Drawing.Image)(resources.GetObject("Chess_Field1.WhiteCell_Image")));
            // 
            // Main_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 471);
            this.Controls.Add(this.Chess_Field1);
            this.Name = "Main_Form";
            this.Text = "Main_Form";
            this.ResumeLayout(false);

        }

        #endregion

        Chess_Field Chess_Field1;
    }
}