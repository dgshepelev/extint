namespace AliensGame
{
    partial class mainForm
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
            this.lblScore = new System.Windows.Forms.Label();
            this.lblLevel = new System.Windows.Forms.Label();
            this.lblHealth = new System.Windows.Forms.Label();
            this.lblLives = new System.Windows.Forms.Label();
            this.btStart = new System.Windows.Forms.Button();
            this.btSets = new System.Windows.Forms.Button();
            this.btQuit = new System.Windows.Forms.Button();
            this.btPlus0 = new System.Windows.Forms.Button();
            this.edt0 = new System.Windows.Forms.TextBox();
            this.btMin0 = new System.Windows.Forms.Button();
            this.sLbl0 = new System.Windows.Forms.Label();
            this.sLbl4 = new System.Windows.Forms.Label();
            this.btMin4 = new System.Windows.Forms.Button();
            this.edt4 = new System.Windows.Forms.TextBox();
            this.btPlus4 = new System.Windows.Forms.Button();
            this.sLbl3 = new System.Windows.Forms.Label();
            this.btMin3 = new System.Windows.Forms.Button();
            this.edt3 = new System.Windows.Forms.TextBox();
            this.btPlus3 = new System.Windows.Forms.Button();
            this.sLbl2 = new System.Windows.Forms.Label();
            this.btMin2 = new System.Windows.Forms.Button();
            this.edt2 = new System.Windows.Forms.TextBox();
            this.btPlus2 = new System.Windows.Forms.Button();
            this.sLbl1 = new System.Windows.Forms.Label();
            this.btMin1 = new System.Windows.Forms.Button();
            this.edt1 = new System.Windows.Forms.TextBox();
            this.btPlus1 = new System.Windows.Forms.Button();
            this.sLbl8 = new System.Windows.Forms.Label();
            this.btMin8 = new System.Windows.Forms.Button();
            this.edt8 = new System.Windows.Forms.TextBox();
            this.btPlus8 = new System.Windows.Forms.Button();
            this.sLbl6 = new System.Windows.Forms.Label();
            this.btMin6 = new System.Windows.Forms.Button();
            this.edt6 = new System.Windows.Forms.TextBox();
            this.btPlus6 = new System.Windows.Forms.Button();
            this.sLblA = new System.Windows.Forms.Label();
            this.sLblC = new System.Windows.Forms.Label();
            this.btRes = new System.Windows.Forms.Button();
            this.btBack = new System.Windows.Forms.Button();
            this.logoPic = new System.Windows.Forms.PictureBox();
            this.plrPic = new System.Windows.Forms.PictureBox();
            this.lblTime = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.logoPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.plrPic)).BeginInit();
            this.SuspendLayout();
            // 
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblScore.Location = new System.Drawing.Point(519, 9);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(38, 13);
            this.lblScore.TabIndex = 0;
            this.lblScore.Text = "Score:";
            // 
            // lblLevel
            // 
            this.lblLevel.AutoSize = true;
            this.lblLevel.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblLevel.Location = new System.Drawing.Point(33, 9);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(36, 13);
            this.lblLevel.TabIndex = 1;
            this.lblLevel.Text = "Level:";
            // 
            // lblHealth
            // 
            this.lblHealth.AutoSize = true;
            this.lblHealth.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblHealth.Location = new System.Drawing.Point(716, 9);
            this.lblHealth.Name = "lblHealth";
            this.lblHealth.Size = new System.Drawing.Size(41, 13);
            this.lblHealth.TabIndex = 7;
            this.lblHealth.Text = "Health:";
            // 
            // lblLives
            // 
            this.lblLives.AutoSize = true;
            this.lblLives.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblLives.Location = new System.Drawing.Point(900, 9);
            this.lblLives.Name = "lblLives";
            this.lblLives.Size = new System.Drawing.Size(12, 13);
            this.lblLives.TabIndex = 8;
            this.lblLives.Text = "x";
            // 
            // btStart
            // 
            this.btStart.Location = new System.Drawing.Point(424, 369);
            this.btStart.Name = "btStart";
            this.btStart.Size = new System.Drawing.Size(155, 49);
            this.btStart.TabIndex = 10;
            this.btStart.Text = "Start Game";
            this.btStart.UseVisualStyleBackColor = true;
            this.btStart.Click += new System.EventHandler(this.btStart_Click);
            // 
            // btSets
            // 
            this.btSets.Location = new System.Drawing.Point(424, 424);
            this.btSets.Name = "btSets";
            this.btSets.Size = new System.Drawing.Size(155, 49);
            this.btSets.TabIndex = 11;
            this.btSets.Text = "Settings";
            this.btSets.UseVisualStyleBackColor = true;
            this.btSets.Click += new System.EventHandler(this.btSets_Click);
            // 
            // btQuit
            // 
            this.btQuit.Location = new System.Drawing.Point(424, 479);
            this.btQuit.Name = "btQuit";
            this.btQuit.Size = new System.Drawing.Size(155, 49);
            this.btQuit.TabIndex = 12;
            this.btQuit.Text = "Quit";
            this.btQuit.UseVisualStyleBackColor = true;
            this.btQuit.Click += new System.EventHandler(this.btQuit_Click);
            // 
            // btPlus0
            // 
            this.btPlus0.Location = new System.Drawing.Point(594, 145);
            this.btPlus0.Name = "btPlus0";
            this.btPlus0.Size = new System.Drawing.Size(50, 20);
            this.btPlus0.TabIndex = 13;
            this.btPlus0.Text = ">>";
            this.btPlus0.UseVisualStyleBackColor = true;
            this.btPlus0.Click += new System.EventHandler(this.btPlus0_Click);
            // 
            // edt0
            // 
            this.edt0.Location = new System.Drawing.Point(539, 145);
            this.edt0.Name = "edt0";
            this.edt0.ReadOnly = true;
            this.edt0.Size = new System.Drawing.Size(50, 20);
            this.edt0.TabIndex = 14;
            this.edt0.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btMin0
            // 
            this.btMin0.Location = new System.Drawing.Point(483, 145);
            this.btMin0.Name = "btMin0";
            this.btMin0.Size = new System.Drawing.Size(50, 20);
            this.btMin0.TabIndex = 15;
            this.btMin0.Text = "<<";
            this.btMin0.UseVisualStyleBackColor = true;
            this.btMin0.Click += new System.EventHandler(this.btMin0_Click);
            // 
            // sLbl0
            // 
            this.sLbl0.AutoSize = true;
            this.sLbl0.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.sLbl0.Location = new System.Drawing.Point(331, 148);
            this.sLbl0.Name = "sLbl0";
            this.sLbl0.Size = new System.Drawing.Size(116, 13);
            this.sLbl0.TabIndex = 16;
            this.sLbl0.Text = "Game Timer Delay (ms)";
            // 
            // sLbl4
            // 
            this.sLbl4.AutoSize = true;
            this.sLbl4.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.sLbl4.Location = new System.Drawing.Point(331, 357);
            this.sLbl4.Name = "sLbl4";
            this.sLbl4.Size = new System.Drawing.Size(103, 13);
            this.sLbl4.TabIndex = 20;
            this.sLbl4.Text = "Alien Movement (px)";
            // 
            // btMin4
            // 
            this.btMin4.Location = new System.Drawing.Point(483, 354);
            this.btMin4.Name = "btMin4";
            this.btMin4.Size = new System.Drawing.Size(50, 20);
            this.btMin4.TabIndex = 19;
            this.btMin4.Text = "<<";
            this.btMin4.UseVisualStyleBackColor = true;
            this.btMin4.Click += new System.EventHandler(this.btMin4_Click);
            // 
            // edt4
            // 
            this.edt4.Location = new System.Drawing.Point(539, 354);
            this.edt4.Name = "edt4";
            this.edt4.ReadOnly = true;
            this.edt4.Size = new System.Drawing.Size(50, 20);
            this.edt4.TabIndex = 18;
            this.edt4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btPlus4
            // 
            this.btPlus4.Location = new System.Drawing.Point(594, 354);
            this.btPlus4.Name = "btPlus4";
            this.btPlus4.Size = new System.Drawing.Size(50, 20);
            this.btPlus4.TabIndex = 17;
            this.btPlus4.Text = ">>";
            this.btPlus4.UseVisualStyleBackColor = true;
            this.btPlus4.Click += new System.EventHandler(this.btPlus4_Click);
            // 
            // sLbl3
            // 
            this.sLbl3.AutoSize = true;
            this.sLbl3.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.sLbl3.Location = new System.Drawing.Point(331, 262);
            this.sLbl3.Name = "sLbl3";
            this.sLbl3.Size = new System.Drawing.Size(32, 13);
            this.sLbl3.TabIndex = 36;
            this.sLbl3.Text = "Lives";
            // 
            // btMin3
            // 
            this.btMin3.Location = new System.Drawing.Point(483, 259);
            this.btMin3.Name = "btMin3";
            this.btMin3.Size = new System.Drawing.Size(50, 20);
            this.btMin3.TabIndex = 35;
            this.btMin3.Text = "<<";
            this.btMin3.UseVisualStyleBackColor = true;
            this.btMin3.Click += new System.EventHandler(this.btMin3_Click);
            // 
            // edt3
            // 
            this.edt3.Location = new System.Drawing.Point(539, 259);
            this.edt3.Name = "edt3";
            this.edt3.ReadOnly = true;
            this.edt3.Size = new System.Drawing.Size(50, 20);
            this.edt3.TabIndex = 34;
            this.edt3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btPlus3
            // 
            this.btPlus3.Location = new System.Drawing.Point(594, 259);
            this.btPlus3.Name = "btPlus3";
            this.btPlus3.Size = new System.Drawing.Size(50, 20);
            this.btPlus3.TabIndex = 33;
            this.btPlus3.Text = ">>";
            this.btPlus3.UseVisualStyleBackColor = true;
            this.btPlus3.Click += new System.EventHandler(this.btPlus3_Click);
            // 
            // sLbl2
            // 
            this.sLbl2.AutoSize = true;
            this.sLbl2.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.sLbl2.Location = new System.Drawing.Point(331, 174);
            this.sLbl2.Name = "sLbl2";
            this.sLbl2.Size = new System.Drawing.Size(102, 13);
            this.sLbl2.TabIndex = 32;
            this.sLbl2.Text = "Shot Movement (px)";
            // 
            // btMin2
            // 
            this.btMin2.Location = new System.Drawing.Point(483, 171);
            this.btMin2.Name = "btMin2";
            this.btMin2.Size = new System.Drawing.Size(50, 20);
            this.btMin2.TabIndex = 31;
            this.btMin2.Text = "<<";
            this.btMin2.UseVisualStyleBackColor = true;
            this.btMin2.Click += new System.EventHandler(this.btMin2_Click);
            // 
            // edt2
            // 
            this.edt2.Location = new System.Drawing.Point(539, 171);
            this.edt2.Name = "edt2";
            this.edt2.ReadOnly = true;
            this.edt2.Size = new System.Drawing.Size(50, 20);
            this.edt2.TabIndex = 30;
            this.edt2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btPlus2
            // 
            this.btPlus2.Location = new System.Drawing.Point(594, 171);
            this.btPlus2.Name = "btPlus2";
            this.btPlus2.Size = new System.Drawing.Size(50, 20);
            this.btPlus2.TabIndex = 29;
            this.btPlus2.Text = ">>";
            this.btPlus2.UseVisualStyleBackColor = true;
            this.btPlus2.Click += new System.EventHandler(this.btPlus2_Click);
            // 
            // sLbl1
            // 
            this.sLbl1.AutoSize = true;
            this.sLbl1.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.sLbl1.Location = new System.Drawing.Point(331, 236);
            this.sLbl1.Name = "sLbl1";
            this.sLbl1.Size = new System.Drawing.Size(77, 13);
            this.sLbl1.TabIndex = 28;
            this.sLbl1.Text = "Movement (px)";
            // 
            // btMin1
            // 
            this.btMin1.Location = new System.Drawing.Point(483, 233);
            this.btMin1.Name = "btMin1";
            this.btMin1.Size = new System.Drawing.Size(50, 20);
            this.btMin1.TabIndex = 27;
            this.btMin1.Text = "<<";
            this.btMin1.UseVisualStyleBackColor = true;
            this.btMin1.Click += new System.EventHandler(this.btMin1_Click);
            // 
            // edt1
            // 
            this.edt1.Location = new System.Drawing.Point(539, 233);
            this.edt1.Name = "edt1";
            this.edt1.ReadOnly = true;
            this.edt1.Size = new System.Drawing.Size(50, 20);
            this.edt1.TabIndex = 26;
            this.edt1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btPlus1
            // 
            this.btPlus1.Location = new System.Drawing.Point(594, 233);
            this.btPlus1.Name = "btPlus1";
            this.btPlus1.Size = new System.Drawing.Size(50, 20);
            this.btPlus1.TabIndex = 25;
            this.btPlus1.Text = ">>";
            this.btPlus1.UseVisualStyleBackColor = true;
            this.btPlus1.Click += new System.EventHandler(this.btPlus1_Click);
            // 
            // sLbl8
            // 
            this.sLbl8.AutoSize = true;
            this.sLbl8.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.sLbl8.Location = new System.Drawing.Point(331, 383);
            this.sLbl8.Name = "sLbl8";
            this.sLbl8.Size = new System.Drawing.Size(147, 13);
            this.sLbl8.TabIndex = 48;
            this.sLbl8.Text = "Alien Respawn Delay (cycles)";
            // 
            // btMin8
            // 
            this.btMin8.Location = new System.Drawing.Point(483, 380);
            this.btMin8.Name = "btMin8";
            this.btMin8.Size = new System.Drawing.Size(50, 20);
            this.btMin8.TabIndex = 47;
            this.btMin8.Text = "<<";
            this.btMin8.UseVisualStyleBackColor = true;
            this.btMin8.Click += new System.EventHandler(this.btMin8_Click);
            // 
            // edt8
            // 
            this.edt8.Location = new System.Drawing.Point(539, 380);
            this.edt8.Name = "edt8";
            this.edt8.ReadOnly = true;
            this.edt8.Size = new System.Drawing.Size(50, 20);
            this.edt8.TabIndex = 46;
            this.edt8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btPlus8
            // 
            this.btPlus8.Location = new System.Drawing.Point(594, 380);
            this.btPlus8.Name = "btPlus8";
            this.btPlus8.Size = new System.Drawing.Size(50, 20);
            this.btPlus8.TabIndex = 45;
            this.btPlus8.Text = ">>";
            this.btPlus8.UseVisualStyleBackColor = true;
            this.btPlus8.Click += new System.EventHandler(this.btPlus8_Click);
            // 
            // sLbl6
            // 
            this.sLbl6.AutoSize = true;
            this.sLbl6.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.sLbl6.Location = new System.Drawing.Point(331, 331);
            this.sLbl6.Name = "sLbl6";
            this.sLbl6.Size = new System.Drawing.Size(102, 13);
            this.sLbl6.TabIndex = 40;
            this.sLbl6.Text = "Total Aliens in Level";
            // 
            // btMin6
            // 
            this.btMin6.Location = new System.Drawing.Point(483, 328);
            this.btMin6.Name = "btMin6";
            this.btMin6.Size = new System.Drawing.Size(50, 20);
            this.btMin6.TabIndex = 39;
            this.btMin6.Text = "<<";
            this.btMin6.UseVisualStyleBackColor = true;
            this.btMin6.Click += new System.EventHandler(this.btMin6_Click);
            // 
            // edt6
            // 
            this.edt6.Location = new System.Drawing.Point(539, 328);
            this.edt6.Name = "edt6";
            this.edt6.ReadOnly = true;
            this.edt6.Size = new System.Drawing.Size(50, 20);
            this.edt6.TabIndex = 38;
            this.edt6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btPlus6
            // 
            this.btPlus6.Location = new System.Drawing.Point(594, 328);
            this.btPlus6.Name = "btPlus6";
            this.btPlus6.Size = new System.Drawing.Size(50, 20);
            this.btPlus6.TabIndex = 37;
            this.btPlus6.Text = ">>";
            this.btPlus6.UseVisualStyleBackColor = true;
            this.btPlus6.Click += new System.EventHandler(this.btPlus6_Click);
            // 
            // sLblA
            // 
            this.sLblA.AutoSize = true;
            this.sLblA.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.sLblA.Location = new System.Drawing.Point(421, 205);
            this.sLblA.Name = "sLblA";
            this.sLblA.Size = new System.Drawing.Size(39, 13);
            this.sLblA.TabIndex = 49;
            this.sLblA.Text = "Player:";
            // 
            // sLblC
            // 
            this.sLblC.AutoSize = true;
            this.sLblC.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.sLblC.Location = new System.Drawing.Point(421, 299);
            this.sLblC.Name = "sLblC";
            this.sLblC.Size = new System.Drawing.Size(53, 13);
            this.sLblC.TabIndex = 51;
            this.sLblC.Text = "1st Level:";
            // 
            // btRes
            // 
            this.btRes.Location = new System.Drawing.Point(424, 314);
            this.btRes.Name = "btRes";
            this.btRes.Size = new System.Drawing.Size(155, 49);
            this.btRes.TabIndex = 52;
            this.btRes.Text = "Resume";
            this.btRes.UseVisualStyleBackColor = true;
            this.btRes.Click += new System.EventHandler(this.btRes_Click);
            // 
            // btBack
            // 
            this.btBack.Location = new System.Drawing.Point(424, 467);
            this.btBack.Name = "btBack";
            this.btBack.Size = new System.Drawing.Size(155, 49);
            this.btBack.TabIndex = 54;
            this.btBack.Text = "Back";
            this.btBack.UseVisualStyleBackColor = true;
            this.btBack.Click += new System.EventHandler(this.btBack_Click);
            // 
            // logoPic
            // 
            this.logoPic.Image = global::AliensGame.Properties.Resources.logo;
            this.logoPic.Location = new System.Drawing.Point(140, 73);
            this.logoPic.Name = "logoPic";
            this.logoPic.Size = new System.Drawing.Size(710, 202);
            this.logoPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logoPic.TabIndex = 55;
            this.logoPic.TabStop = false;
            // 
            // plrPic
            // 
            this.plrPic.Image = global::AliensGame.Properties.Resources.pl;
            this.plrPic.Location = new System.Drawing.Point(858, 6);
            this.plrPic.Name = "plrPic";
            this.plrPic.Size = new System.Drawing.Size(36, 20);
            this.plrPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.plrPic.TabIndex = 9;
            this.plrPic.TabStop = false;
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblTime.Location = new System.Drawing.Point(254, 9);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(33, 13);
            this.lblTime.TabIndex = 56;
            this.lblTime.Text = "Time:";
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MidnightBlue;
            this.ClientSize = new System.Drawing.Size(984, 662);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.logoPic);
            this.Controls.Add(this.btBack);
            this.Controls.Add(this.btRes);
            this.Controls.Add(this.sLblC);
            this.Controls.Add(this.sLblA);
            this.Controls.Add(this.sLbl8);
            this.Controls.Add(this.btMin8);
            this.Controls.Add(this.edt8);
            this.Controls.Add(this.btPlus8);
            this.Controls.Add(this.sLbl6);
            this.Controls.Add(this.btMin6);
            this.Controls.Add(this.edt6);
            this.Controls.Add(this.btPlus6);
            this.Controls.Add(this.sLbl3);
            this.Controls.Add(this.btMin3);
            this.Controls.Add(this.edt3);
            this.Controls.Add(this.btPlus3);
            this.Controls.Add(this.sLbl2);
            this.Controls.Add(this.btMin2);
            this.Controls.Add(this.edt2);
            this.Controls.Add(this.btPlus2);
            this.Controls.Add(this.sLbl1);
            this.Controls.Add(this.btMin1);
            this.Controls.Add(this.edt1);
            this.Controls.Add(this.btPlus1);
            this.Controls.Add(this.sLbl4);
            this.Controls.Add(this.btMin4);
            this.Controls.Add(this.edt4);
            this.Controls.Add(this.btPlus4);
            this.Controls.Add(this.sLbl0);
            this.Controls.Add(this.btMin0);
            this.Controls.Add(this.edt0);
            this.Controls.Add(this.btPlus0);
            this.Controls.Add(this.btQuit);
            this.Controls.Add(this.btSets);
            this.Controls.Add(this.btStart);
            this.Controls.Add(this.plrPic);
            this.Controls.Add(this.lblLives);
            this.Controls.Add(this.lblHealth);
            this.Controls.Add(this.lblLevel);
            this.Controls.Add(this.lblScore);
            this.MaximumSize = new System.Drawing.Size(1000, 700);
            this.MinimumSize = new System.Drawing.Size(1000, 700);
            this.Name = "mainForm";
            this.Text = "Aliens";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClose);
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.logoPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.plrPic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lblScore;
        public System.Windows.Forms.Label lblLevel;
        public System.Windows.Forms.Label lblHealth;
        public System.Windows.Forms.Label lblLives;
        private System.Windows.Forms.PictureBox plrPic;
        private System.Windows.Forms.Button btStart;
        private System.Windows.Forms.Button btSets;
        private System.Windows.Forms.Button btQuit;
        private System.Windows.Forms.Button btPlus0;
        private System.Windows.Forms.TextBox edt0;
        private System.Windows.Forms.Button btMin0;
        public System.Windows.Forms.Label sLbl0;
        public System.Windows.Forms.Label sLbl4;
        private System.Windows.Forms.Button btMin4;
        private System.Windows.Forms.TextBox edt4;
        private System.Windows.Forms.Button btPlus4;
        public System.Windows.Forms.Label sLbl3;
        private System.Windows.Forms.Button btMin3;
        private System.Windows.Forms.TextBox edt3;
        private System.Windows.Forms.Button btPlus3;
        public System.Windows.Forms.Label sLbl2;
        private System.Windows.Forms.Button btMin2;
        private System.Windows.Forms.TextBox edt2;
        private System.Windows.Forms.Button btPlus2;
        public System.Windows.Forms.Label sLbl1;
        private System.Windows.Forms.Button btMin1;
        private System.Windows.Forms.TextBox edt1;
        private System.Windows.Forms.Button btPlus1;
        public System.Windows.Forms.Label sLbl8;
        private System.Windows.Forms.Button btMin8;
        private System.Windows.Forms.TextBox edt8;
        private System.Windows.Forms.Button btPlus8;
        public System.Windows.Forms.Label sLbl6;
        private System.Windows.Forms.Button btMin6;
        private System.Windows.Forms.TextBox edt6;
        private System.Windows.Forms.Button btPlus6;
        public System.Windows.Forms.Label sLblA;
        public System.Windows.Forms.Label sLblC;
        private System.Windows.Forms.Button btRes;
        private System.Windows.Forms.Button btBack;
        public System.Windows.Forms.PictureBox logoPic;
        public System.Windows.Forms.Label lblTime;





    }
}

