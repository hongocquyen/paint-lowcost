
namespace SharpGL
{
    partial class fMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fMain));
            this.openGLControl = new SharpGL.OpenGLControl();
            this.bt_Line = new System.Windows.Forms.Button();
            this.bt_Circle = new System.Windows.Forms.Button();
            this.bt_Color = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.tx_coordstart = new System.Windows.Forms.Label();
            this.tx_coordend = new System.Windows.Forms.Label();
            this.bt_clear = new System.Windows.Forms.Button();
            this.bt_Rectangle = new System.Windows.Forms.Button();
            this.bt_Ellipse = new System.Windows.Forms.Button();
            this.tx_drawtime = new System.Windows.Forms.Label();
            this.bt_Triangle = new System.Windows.Forms.Button();
            this.cb_size = new System.Windows.Forms.ComboBox();
            this.bt_Pentagon = new System.Windows.Forms.Button();
            this.bt_Polygon = new System.Windows.Forms.Button();
            this.bt_Bfill = new System.Windows.Forms.Button();
            this.bt_FillColor = new System.Windows.Forms.Button();
            this.colorDialog2 = new System.Windows.Forms.ColorDialog();
            this.bt_Select = new System.Windows.Forms.Button();
            this.bt_Move = new System.Windows.Forms.Button();
            this.bt_Scale = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).BeginInit();
            this.SuspendLayout();
            // 
            // openGLControl
            // 
            this.openGLControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.openGLControl.BackColor = System.Drawing.SystemColors.Control;
            this.openGLControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.openGLControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.openGLControl.DrawFPS = false;
            this.openGLControl.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.openGLControl.Location = new System.Drawing.Point(28, 80);
            this.openGLControl.Margin = new System.Windows.Forms.Padding(5);
            this.openGLControl.Name = "openGLControl";
            this.openGLControl.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.openGLControl.RenderContextType = SharpGL.RenderContextType.DIBSection;
            this.openGLControl.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.openGLControl.Size = new System.Drawing.Size(575, 492);
            this.openGLControl.TabIndex = 0;
            this.openGLControl.OpenGLInitialized += new System.EventHandler(this.openGLControl_OpenGLInitialized);
            this.openGLControl.OpenGLDraw += new SharpGL.RenderEventHandler(this.openGLControl_OpenGLDraw);
            this.openGLControl.Resized += new System.EventHandler(this.openGLControl_Resized);
            this.openGLControl.Load += new System.EventHandler(this.openGLControl_Load);
            this.openGLControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.openGLControl_MouseDown);
            this.openGLControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.openGLControl_MouseMove);
            this.openGLControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.openGLControl_MouseUp);
            // 
            // bt_Line
            // 
            this.bt_Line.Location = new System.Drawing.Point(27, 14);
            this.bt_Line.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bt_Line.Name = "bt_Line";
            this.bt_Line.Size = new System.Drawing.Size(59, 46);
            this.bt_Line.TabIndex = 1;
            this.bt_Line.Text = "Line";
            this.bt_Line.UseVisualStyleBackColor = true;
            this.bt_Line.Click += new System.EventHandler(this.bt_Line_Click);
            // 
            // bt_Circle
            // 
            this.bt_Circle.Location = new System.Drawing.Point(92, 14);
            this.bt_Circle.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bt_Circle.Name = "bt_Circle";
            this.bt_Circle.Size = new System.Drawing.Size(58, 46);
            this.bt_Circle.TabIndex = 2;
            this.bt_Circle.Text = "Circle";
            this.bt_Circle.UseVisualStyleBackColor = true;
            this.bt_Circle.Click += new System.EventHandler(this.bt_Circle_Click);
            // 
            // bt_Color
            // 
            this.bt_Color.Location = new System.Drawing.Point(631, 143);
            this.bt_Color.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bt_Color.Name = "bt_Color";
            this.bt_Color.Size = new System.Drawing.Size(91, 46);
            this.bt_Color.TabIndex = 8;
            this.bt_Color.Text = "Draw Color";
            this.bt_Color.UseVisualStyleBackColor = true;
            this.bt_Color.Click += new System.EventHandler(this.bt_Color_Click);
            // 
            // tx_coordstart
            // 
            this.tx_coordstart.AutoSize = true;
            this.tx_coordstart.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tx_coordstart.Location = new System.Drawing.Point(611, 9);
            this.tx_coordstart.Name = "tx_coordstart";
            this.tx_coordstart.Size = new System.Drawing.Size(111, 25);
            this.tx_coordstart.TabIndex = 4;
            this.tx_coordstart.Text = "Start: (X;Y)";
            // 
            // tx_coordend
            // 
            this.tx_coordend.AutoSize = true;
            this.tx_coordend.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tx_coordend.Location = new System.Drawing.Point(611, 33);
            this.tx_coordend.Name = "tx_coordend";
            this.tx_coordend.Size = new System.Drawing.Size(110, 25);
            this.tx_coordend.TabIndex = 5;
            this.tx_coordend.Text = "End:  (X;Y)";
            // 
            // bt_clear
            // 
            this.bt_clear.Location = new System.Drawing.Point(631, 80);
            this.bt_clear.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bt_clear.Name = "bt_clear";
            this.bt_clear.Size = new System.Drawing.Size(91, 46);
            this.bt_clear.TabIndex = 7;
            this.bt_clear.Text = "Clear";
            this.bt_clear.UseVisualStyleBackColor = true;
            this.bt_clear.Click += new System.EventHandler(this.bt_clear_Click);
            // 
            // bt_Rectangle
            // 
            this.bt_Rectangle.Location = new System.Drawing.Point(156, 14);
            this.bt_Rectangle.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bt_Rectangle.Name = "bt_Rectangle";
            this.bt_Rectangle.Size = new System.Drawing.Size(87, 46);
            this.bt_Rectangle.TabIndex = 3;
            this.bt_Rectangle.Text = "Rectangle";
            this.bt_Rectangle.UseVisualStyleBackColor = true;
            this.bt_Rectangle.Click += new System.EventHandler(this.bt_Rectangle_Click);
            // 
            // bt_Ellipse
            // 
            this.bt_Ellipse.Location = new System.Drawing.Point(249, 14);
            this.bt_Ellipse.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bt_Ellipse.Name = "bt_Ellipse";
            this.bt_Ellipse.Size = new System.Drawing.Size(94, 46);
            this.bt_Ellipse.TabIndex = 4;
            this.bt_Ellipse.Text = "Ellipse";
            this.bt_Ellipse.UseVisualStyleBackColor = true;
            this.bt_Ellipse.Click += new System.EventHandler(this.bt_Ellipse_Click);
            // 
            // tx_drawtime
            // 
            this.tx_drawtime.AutoSize = true;
            this.tx_drawtime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tx_drawtime.Location = new System.Drawing.Point(313, 577);
            this.tx_drawtime.Name = "tx_drawtime";
            this.tx_drawtime.Size = new System.Drawing.Size(109, 25);
            this.tx_drawtime.TabIndex = 9;
            this.tx_drawtime.Text = "Draw time: ";
            // 
            // bt_Triangle
            // 
            this.bt_Triangle.Location = new System.Drawing.Point(349, 14);
            this.bt_Triangle.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bt_Triangle.Name = "bt_Triangle";
            this.bt_Triangle.Size = new System.Drawing.Size(73, 46);
            this.bt_Triangle.TabIndex = 5;
            this.bt_Triangle.Text = "Triangle";
            this.bt_Triangle.UseVisualStyleBackColor = true;
            this.bt_Triangle.Click += new System.EventHandler(this.bt_Triangle_Click);
            // 
            // cb_size
            // 
            this.cb_size.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cb_size.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cb_size.FormattingEnabled = true;
            this.cb_size.Items.AddRange(new object[] {
            "1px",
            "3px",
            "5px",
            "8px"});
            this.cb_size.Location = new System.Drawing.Point(631, 209);
            this.cb_size.Name = "cb_size";
            this.cb_size.Size = new System.Drawing.Size(91, 24);
            this.cb_size.TabIndex = 11;
            this.cb_size.Text = "1px";
            this.cb_size.SelectedIndexChanged += new System.EventHandler(this.cb_size_SelectedIndexChanged);
            // 
            // bt_Pentagon
            // 
            this.bt_Pentagon.Location = new System.Drawing.Point(432, 14);
            this.bt_Pentagon.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bt_Pentagon.Name = "bt_Pentagon";
            this.bt_Pentagon.Size = new System.Drawing.Size(82, 46);
            this.bt_Pentagon.TabIndex = 6;
            this.bt_Pentagon.Text = "Pentagon";
            this.bt_Pentagon.UseVisualStyleBackColor = true;
            this.bt_Pentagon.Click += new System.EventHandler(this.bt_Pentagon_Click);
            // 
            // bt_Polygon
            // 
            this.bt_Polygon.Location = new System.Drawing.Point(530, 14);
            this.bt_Polygon.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bt_Polygon.Name = "bt_Polygon";
            this.bt_Polygon.Size = new System.Drawing.Size(73, 46);
            this.bt_Polygon.TabIndex = 12;
            this.bt_Polygon.Text = "Polygon";
            this.bt_Polygon.UseVisualStyleBackColor = true;
            this.bt_Polygon.Click += new System.EventHandler(this.bt_Polygon_Click);
            // 
            // bt_Bfill
            // 
            this.bt_Bfill.Location = new System.Drawing.Point(631, 259);
            this.bt_Bfill.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bt_Bfill.Name = "bt_Bfill";
            this.bt_Bfill.Size = new System.Drawing.Size(90, 46);
            this.bt_Bfill.TabIndex = 13;
            this.bt_Bfill.Text = "Boundary Fill";
            this.bt_Bfill.UseVisualStyleBackColor = true;
            this.bt_Bfill.Click += new System.EventHandler(this.bt_Bfill_Click);
            // 
            // bt_FillColor
            // 
            this.bt_FillColor.Location = new System.Drawing.Point(630, 320);
            this.bt_FillColor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bt_FillColor.Name = "bt_FillColor";
            this.bt_FillColor.Size = new System.Drawing.Size(91, 46);
            this.bt_FillColor.TabIndex = 14;
            this.bt_FillColor.Text = "Fill Color";
            this.bt_FillColor.UseVisualStyleBackColor = true;
            this.bt_FillColor.Click += new System.EventHandler(this.bt_FillColor_Click);
            // 
            // bt_Select
            // 
            this.bt_Select.Location = new System.Drawing.Point(631, 382);
            this.bt_Select.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bt_Select.Name = "bt_Select";
            this.bt_Select.Size = new System.Drawing.Size(91, 46);
            this.bt_Select.TabIndex = 15;
            this.bt_Select.Text = "Select";
            this.bt_Select.UseVisualStyleBackColor = true;
            this.bt_Select.Click += new System.EventHandler(this.bt_Select_Click);
            // 
            // bt_Move
            // 
            this.bt_Move.Location = new System.Drawing.Point(631, 450);
            this.bt_Move.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bt_Move.Name = "bt_Move";
            this.bt_Move.Size = new System.Drawing.Size(91, 46);
            this.bt_Move.TabIndex = 16;
            this.bt_Move.Text = "Move";
            this.bt_Move.UseVisualStyleBackColor = true;
            this.bt_Move.Click += new System.EventHandler(this.bt_Move_Click);
            // 
            // bt_Scale
            // 
            this.bt_Scale.Location = new System.Drawing.Point(631, 526);
            this.bt_Scale.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bt_Scale.Name = "bt_Scale";
            this.bt_Scale.Size = new System.Drawing.Size(91, 46);
            this.bt_Scale.TabIndex = 17;
            this.bt_Scale.Text = "Scale";
            this.bt_Scale.UseVisualStyleBackColor = true;
            this.bt_Scale.Click += new System.EventHandler(this.bt_Scale_Click);
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(773, 600);
            this.Controls.Add(this.bt_Scale);
            this.Controls.Add(this.bt_Move);
            this.Controls.Add(this.bt_Select);
            this.Controls.Add(this.bt_FillColor);
            this.Controls.Add(this.bt_Bfill);
            this.Controls.Add(this.bt_Polygon);
            this.Controls.Add(this.bt_Pentagon);
            this.Controls.Add(this.cb_size);
            this.Controls.Add(this.bt_Triangle);
            this.Controls.Add(this.tx_drawtime);
            this.Controls.Add(this.bt_Ellipse);
            this.Controls.Add(this.bt_Rectangle);
            this.Controls.Add(this.bt_clear);
            this.Controls.Add(this.tx_coordend);
            this.Controls.Add(this.tx_coordstart);
            this.Controls.Add(this.bt_Color);
            this.Controls.Add(this.bt_Circle);
            this.Controls.Add(this.bt_Line);
            this.Controls.Add(this.openGLControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "fMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Draw Shape";
            this.Load += new System.EventHandler(this.fMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OpenGLControl openGLControl;
        private System.Windows.Forms.Button bt_Line;
        private System.Windows.Forms.Button bt_Circle;
        private System.Windows.Forms.Button bt_Color;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Label tx_coordstart;
        private System.Windows.Forms.Label tx_coordend;
        private System.Windows.Forms.Button bt_clear;
        private System.Windows.Forms.Button bt_Rectangle;
        private System.Windows.Forms.Button bt_Ellipse;
        private System.Windows.Forms.Label tx_drawtime;
        private System.Windows.Forms.Button bt_Triangle;
        private System.Windows.Forms.ComboBox cb_size;
        private System.Windows.Forms.Button bt_Pentagon;
        private System.Windows.Forms.Button bt_Polygon;
        private System.Windows.Forms.Button bt_Bfill;
        private System.Windows.Forms.Button bt_FillColor;
        private System.Windows.Forms.ColorDialog colorDialog2;
        private System.Windows.Forms.Button bt_Select;
        private System.Windows.Forms.Button bt_Move;
        private System.Windows.Forms.Button bt_Scale;
    }
}

