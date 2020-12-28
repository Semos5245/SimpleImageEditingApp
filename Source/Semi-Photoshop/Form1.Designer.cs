namespace Semi_Photoshop
{
    partial class Form1
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.informationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toGrayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toRedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toGreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toBlueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toBetterGrayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addColorLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mergeImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.limitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtLimit = new System.Windows.Forms.ToolStripTextBox();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rotateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtAngle = new System.Windows.Forms.ToolStripTextBox();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mergeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cbMergeMode = new System.Windows.Forms.ToolStripComboBox();
            this.startToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.cropToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.drawToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.circleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rectangleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flipToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cbFlipMode = new System.Windows.Forms.ToolStripComboBox();
            this.doFlipToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mainPicture = new System.Windows.Forms.PictureBox();
            this.colorPicture = new System.Windows.Forms.PictureBox();
            this.btnBrighten = new System.Windows.Forms.Button();
            this.btnDarken = new System.Windows.Forms.Button();
            this.progress = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.hColor = new System.Windows.Forms.Label();
            this.lColor = new System.Windows.Forms.Label();
            this.sColor = new System.Windows.Forms.Label();
            this.lstImages1 = new System.Windows.Forms.ListView();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.editToolStripMenuItem,
            this.actionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1428, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.saveToolStripMenuItem1});
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.openToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.saveToolStripMenuItem.Text = "Open";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem1
            // 
            this.saveToolStripMenuItem1.Name = "saveToolStripMenuItem1";
            this.saveToolStripMenuItem1.Size = new System.Drawing.Size(103, 22);
            this.saveToolStripMenuItem1.Text = "Save";
            this.saveToolStripMenuItem1.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.informationToolStripMenuItem,
            this.convertToolStripMenuItem,
            this.addColorLayerToolStripMenuItem,
            this.mergeImageToolStripMenuItem,
            this.limitToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // informationToolStripMenuItem
            // 
            this.informationToolStripMenuItem.Name = "informationToolStripMenuItem";
            this.informationToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.informationToolStripMenuItem.Text = "Information";
            this.informationToolStripMenuItem.Click += new System.EventHandler(this.informationToolStripMenuItem_Click);
            // 
            // convertToolStripMenuItem
            // 
            this.convertToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toGrayToolStripMenuItem,
            this.toRedToolStripMenuItem,
            this.toGreenToolStripMenuItem,
            this.toBlueToolStripMenuItem,
            this.toBetterGrayToolStripMenuItem});
            this.convertToolStripMenuItem.Name = "convertToolStripMenuItem";
            this.convertToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.convertToolStripMenuItem.Text = "Convert";
            // 
            // toGrayToolStripMenuItem
            // 
            this.toGrayToolStripMenuItem.Name = "toGrayToolStripMenuItem";
            this.toGrayToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.toGrayToolStripMenuItem.Text = "ToGray";
            this.toGrayToolStripMenuItem.Click += new System.EventHandler(this.toGrayToolStripMenuItem_Click);
            // 
            // toRedToolStripMenuItem
            // 
            this.toRedToolStripMenuItem.Name = "toRedToolStripMenuItem";
            this.toRedToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.toRedToolStripMenuItem.Text = "ToRed";
            this.toRedToolStripMenuItem.Click += new System.EventHandler(this.toRedToolStripMenuItem_Click);
            // 
            // toGreenToolStripMenuItem
            // 
            this.toGreenToolStripMenuItem.Name = "toGreenToolStripMenuItem";
            this.toGreenToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.toGreenToolStripMenuItem.Text = "ToGreen";
            this.toGreenToolStripMenuItem.Click += new System.EventHandler(this.toGreenToolStripMenuItem_Click);
            // 
            // toBlueToolStripMenuItem
            // 
            this.toBlueToolStripMenuItem.Name = "toBlueToolStripMenuItem";
            this.toBlueToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.toBlueToolStripMenuItem.Text = "ToBlue";
            this.toBlueToolStripMenuItem.Click += new System.EventHandler(this.toBlueToolStripMenuItem_Click);
            // 
            // toBetterGrayToolStripMenuItem
            // 
            this.toBetterGrayToolStripMenuItem.Name = "toBetterGrayToolStripMenuItem";
            this.toBetterGrayToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.toBetterGrayToolStripMenuItem.Text = "ToBetterGray";
            this.toBetterGrayToolStripMenuItem.Click += new System.EventHandler(this.toBetterGrayToolStripMenuItem_Click);
            // 
            // addColorLayerToolStripMenuItem
            // 
            this.addColorLayerToolStripMenuItem.Name = "addColorLayerToolStripMenuItem";
            this.addColorLayerToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.addColorLayerToolStripMenuItem.Text = "Add Color Layer";
            this.addColorLayerToolStripMenuItem.Click += new System.EventHandler(this.addColorLayerToolStripMenuItem_Click);
            // 
            // mergeImageToolStripMenuItem
            // 
            this.mergeImageToolStripMenuItem.Name = "mergeImageToolStripMenuItem";
            this.mergeImageToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.mergeImageToolStripMenuItem.Text = "Blend Image";
            this.mergeImageToolStripMenuItem.Click += new System.EventHandler(this.blendImageToolStripMenuItem_Click);
            // 
            // limitToolStripMenuItem
            // 
            this.limitToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtLimit});
            this.limitToolStripMenuItem.Name = "limitToolStripMenuItem";
            this.limitToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.limitToolStripMenuItem.Text = "Limit";
            // 
            // txtLimit
            // 
            this.txtLimit.Name = "txtLimit";
            this.txtLimit.Size = new System.Drawing.Size(100, 23);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.onCopyClicked);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.onPasteClicked);
            // 
            // actionsToolStripMenuItem
            // 
            this.actionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rotateToolStripMenuItem,
            this.mergeToolStripMenuItem,
            this.cropToolStripMenuItem,
            this.drawToolStripMenuItem,
            this.flipToolStripMenuItem});
            this.actionsToolStripMenuItem.Name = "actionsToolStripMenuItem";
            this.actionsToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.actionsToolStripMenuItem.Text = "Actions";
            // 
            // rotateToolStripMenuItem
            // 
            this.rotateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtAngle,
            this.startToolStripMenuItem});
            this.rotateToolStripMenuItem.Name = "rotateToolStripMenuItem";
            this.rotateToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.rotateToolStripMenuItem.Text = "Rotate";
            // 
            // txtAngle
            // 
            this.txtAngle.Name = "txtAngle";
            this.txtAngle.Size = new System.Drawing.Size(100, 23);
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.rotateStartClicked);
            // 
            // mergeToolStripMenuItem
            // 
            this.mergeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cbMergeMode,
            this.startToolStripMenuItem1});
            this.mergeToolStripMenuItem.Name = "mergeToolStripMenuItem";
            this.mergeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.mergeToolStripMenuItem.Text = "Merge";
            // 
            // cbMergeMode
            // 
            this.cbMergeMode.Items.AddRange(new object[] {
            "Horizontal",
            "Vertical"});
            this.cbMergeMode.Name = "cbMergeMode";
            this.cbMergeMode.Size = new System.Drawing.Size(121, 23);
            this.cbMergeMode.Text = "Select mode";
            // 
            // startToolStripMenuItem1
            // 
            this.startToolStripMenuItem1.Name = "startToolStripMenuItem1";
            this.startToolStripMenuItem1.Size = new System.Drawing.Size(181, 22);
            this.startToolStripMenuItem1.Text = "Start";
            this.startToolStripMenuItem1.Click += new System.EventHandler(this.mergeStartClicked);
            // 
            // cropToolStripMenuItem
            // 
            this.cropToolStripMenuItem.Name = "cropToolStripMenuItem";
            this.cropToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.cropToolStripMenuItem.Text = "Crop";
            this.cropToolStripMenuItem.Click += new System.EventHandler(this.cropToolStripMenuItem_Click);
            // 
            // drawToolStripMenuItem
            // 
            this.drawToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lineToolStripMenuItem,
            this.circleToolStripMenuItem,
            this.rectangleToolStripMenuItem});
            this.drawToolStripMenuItem.Name = "drawToolStripMenuItem";
            this.drawToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.drawToolStripMenuItem.Text = "Draw";
            // 
            // lineToolStripMenuItem
            // 
            this.lineToolStripMenuItem.Name = "lineToolStripMenuItem";
            this.lineToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.lineToolStripMenuItem.Text = "Line";
            this.lineToolStripMenuItem.Click += new System.EventHandler(this.drawLineClicked);
            // 
            // circleToolStripMenuItem
            // 
            this.circleToolStripMenuItem.Name = "circleToolStripMenuItem";
            this.circleToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.circleToolStripMenuItem.Text = "Circle";
            this.circleToolStripMenuItem.Click += new System.EventHandler(this.drawCircleClicked);
            // 
            // rectangleToolStripMenuItem
            // 
            this.rectangleToolStripMenuItem.Name = "rectangleToolStripMenuItem";
            this.rectangleToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.rectangleToolStripMenuItem.Text = "Rectangle";
            this.rectangleToolStripMenuItem.Click += new System.EventHandler(this.drawRectangleClicked);
            // 
            // flipToolStripMenuItem
            // 
            this.flipToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cbFlipMode,
            this.doFlipToolStripMenuItem});
            this.flipToolStripMenuItem.Name = "flipToolStripMenuItem";
            this.flipToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.flipToolStripMenuItem.Text = "Flip";
            // 
            // cbFlipMode
            // 
            this.cbFlipMode.Items.AddRange(new object[] {
            "Horizontal",
            "Vertical"});
            this.cbFlipMode.Name = "cbFlipMode";
            this.cbFlipMode.Size = new System.Drawing.Size(121, 23);
            this.cbFlipMode.Text = "Select mode";
            // 
            // doFlipToolStripMenuItem
            // 
            this.doFlipToolStripMenuItem.Name = "doFlipToolStripMenuItem";
            this.doFlipToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.doFlipToolStripMenuItem.Text = "Do Flip";
            this.doFlipToolStripMenuItem.Click += new System.EventHandler(this.onDoFlipClicked);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.mainPicture);
            this.panel1.Location = new System.Drawing.Point(12, 104);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1386, 699);
            this.panel1.TabIndex = 1;
            // 
            // mainPicture
            // 
            this.mainPicture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mainPicture.Location = new System.Drawing.Point(3, 3);
            this.mainPicture.Name = "mainPicture";
            this.mainPicture.Size = new System.Drawing.Size(770, 330);
            this.mainPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.mainPicture.TabIndex = 0;
            this.mainPicture.TabStop = false;
            this.mainPicture.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mainPictureMouseDown);
            this.mainPicture.MouseMove += new System.Windows.Forms.MouseEventHandler(this.onMouseMove);
            this.mainPicture.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mainPictureMouseUp);
            // 
            // colorPicture
            // 
            this.colorPicture.Location = new System.Drawing.Point(740, 57);
            this.colorPicture.Name = "colorPicture";
            this.colorPicture.Size = new System.Drawing.Size(46, 33);
            this.colorPicture.TabIndex = 2;
            this.colorPicture.TabStop = false;
            // 
            // btnBrighten
            // 
            this.btnBrighten.Location = new System.Drawing.Point(14, 57);
            this.btnBrighten.Name = "btnBrighten";
            this.btnBrighten.Size = new System.Drawing.Size(75, 33);
            this.btnBrighten.TabIndex = 4;
            this.btnBrighten.Text = "Brighten";
            this.btnBrighten.UseVisualStyleBackColor = true;
            this.btnBrighten.Click += new System.EventHandler(this.btnBrighten_Click);
            // 
            // btnDarken
            // 
            this.btnDarken.Location = new System.Drawing.Point(95, 57);
            this.btnDarken.Name = "btnDarken";
            this.btnDarken.Size = new System.Drawing.Size(75, 33);
            this.btnDarken.TabIndex = 5;
            this.btnDarken.Text = "Darken";
            this.btnDarken.UseVisualStyleBackColor = true;
            this.btnDarken.Click += new System.EventHandler(this.btnDarken_Click);
            // 
            // progress
            // 
            this.progress.ForeColor = System.Drawing.Color.Lime;
            this.progress.Location = new System.Drawing.Point(287, 62);
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(160, 23);
            this.progress.TabIndex = 9;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(333, 88);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 24);
            this.lblStatus.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(535, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "HSL Color:";
            // 
            // hColor
            // 
            this.hColor.AutoSize = true;
            this.hColor.Location = new System.Drawing.Point(641, 54);
            this.hColor.Name = "hColor";
            this.hColor.Size = new System.Drawing.Size(0, 13);
            this.hColor.TabIndex = 11;
            // 
            // lColor
            // 
            this.lColor.AutoSize = true;
            this.lColor.Location = new System.Drawing.Point(641, 89);
            this.lColor.Name = "lColor";
            this.lColor.Size = new System.Drawing.Size(0, 13);
            this.lColor.TabIndex = 12;
            // 
            // sColor
            // 
            this.sColor.AutoSize = true;
            this.sColor.Location = new System.Drawing.Point(641, 72);
            this.sColor.Name = "sColor";
            this.sColor.Size = new System.Drawing.Size(0, 13);
            this.sColor.TabIndex = 13;
            // 
            // lstImages1
            // 
            this.lstImages1.GridLines = true;
            this.lstImages1.HideSelection = false;
            this.lstImages1.Location = new System.Drawing.Point(12, 23);
            this.lstImages1.Name = "lstImages1";
            this.lstImages1.Scrollable = false;
            this.lstImages1.Size = new System.Drawing.Size(790, 25);
            this.lstImages1.TabIndex = 20;
            this.lstImages1.UseCompatibleStateImageBehavior = false;
            this.lstImages1.SelectedIndexChanged += new System.EventHandler(this.lstImages1_SelectedIndexChanged);
            this.lstImages1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.onListImageDoubleClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1428, 865);
            this.Controls.Add(this.lstImages1);
            this.Controls.Add(this.sColor);
            this.Controls.Add(this.lColor);
            this.Controls.Add(this.hColor);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.progress);
            this.Controls.Add(this.btnDarken);
            this.Controls.Add(this.btnBrighten);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.colorPicture);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem informationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem convertToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toGrayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toRedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toGreenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toBlueToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox mainPicture;
        private System.Windows.Forms.PictureBox colorPicture;
        private System.Windows.Forms.Button btnBrighten;
        private System.Windows.Forms.Button btnDarken;
        private System.Windows.Forms.ProgressBar progress;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ToolStripMenuItem toBetterGrayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addColorLayerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mergeImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem actionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rotateToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox txtAngle;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mergeToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox cbMergeMode;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem cropToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem limitToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox txtLimit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label hColor;
        private System.Windows.Forms.Label lColor;
        private System.Windows.Forms.Label sColor;
        private System.Windows.Forms.ToolStripMenuItem drawToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem circleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rectangleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem flipToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox cbFlipMode;
        private System.Windows.Forms.ToolStripMenuItem doFlipToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ListView lstImages1;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
    }
}

