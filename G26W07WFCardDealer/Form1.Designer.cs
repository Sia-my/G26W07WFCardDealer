namespace G26W07WFCardDealer
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Card1 = new PictureBox();
            btnDeal = new Button();
            Card2 = new PictureBox();
            Card3 = new PictureBox();
            Card4 = new PictureBox();
            Card5 = new PictureBox();
            btnProbabilities = new Button();
            lvProbabilities = new ListView();
            colRank = new ColumnHeader();
            colCount = new ColumnHeader();
            colProbability = new ColumnHeader();
            colCurrent = new ColumnHeader();
            lblResult = new Label();
            lblInfo = new Label();
            ((System.ComponentModel.ISupportInitialize)Card1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Card2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Card3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Card4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Card5).BeginInit();
            SuspendLayout();
            // 
            // Card1
            // 
            Card1.Image = Properties.Resources.red_joker;
            Card1.Location = new Point(12, 12);
            Card1.Name = "Card1";
            Card1.Size = new Size(231, 452);
            Card1.SizeMode = PictureBoxSizeMode.Zoom;
            Card1.TabIndex = 0;
            Card1.TabStop = false;
            Card1.Click += OnDeal;
            // 
            // btnDeal
            // 
            btnDeal.Location = new Point(12, 470);
            btnDeal.Name = "btnDeal";
            btnDeal.Size = new Size(586, 100);
            btnDeal.TabIndex = 1;
            btnDeal.Text = "카드 분배";
            btnDeal.UseVisualStyleBackColor = true;
            btnDeal.Click += OnDeal;
            // 
            // Card2
            // 
            Card2.Image = Properties.Resources.red_joker;
            Card2.Location = new Point(249, 12);
            Card2.Name = "Card2";
            Card2.Size = new Size(231, 452);
            Card2.SizeMode = PictureBoxSizeMode.Zoom;
            Card2.TabIndex = 2;
            Card2.TabStop = false;
            Card2.Click += OnDeal;
            // 
            // Card3
            // 
            Card3.Image = Properties.Resources.red_joker;
            Card3.Location = new Point(486, 12);
            Card3.Name = "Card3";
            Card3.Size = new Size(231, 452);
            Card3.SizeMode = PictureBoxSizeMode.Zoom;
            Card3.TabIndex = 3;
            Card3.TabStop = false;
            Card3.Click += OnDeal;
            // 
            // Card4
            // 
            Card4.Image = Properties.Resources.red_joker;
            Card4.Location = new Point(723, 12);
            Card4.Name = "Card4";
            Card4.Size = new Size(231, 452);
            Card4.SizeMode = PictureBoxSizeMode.Zoom;
            Card4.TabIndex = 4;
            Card4.TabStop = false;
            Card4.Click += OnDeal;
            // 
            // Card5
            // 
            Card5.Image = Properties.Resources.red_joker;
            Card5.Location = new Point(961, 12);
            Card5.Name = "Card5";
            Card5.Size = new Size(231, 452);
            Card5.SizeMode = PictureBoxSizeMode.Zoom;
            Card5.TabIndex = 5;
            Card5.TabStop = false;
            Card5.Click += OnDeal;
            // 
            // btnProbabilities
            // 
            btnProbabilities.Location = new Point(606, 470);
            btnProbabilities.Name = "btnProbabilities";
            btnProbabilities.Size = new Size(586, 100);
            btnProbabilities.TabIndex = 7;
            btnProbabilities.Text = "전체 족보 확률 보기";
            btnProbabilities.UseVisualStyleBackColor = true;
            btnProbabilities.Click += OnShowProbabilities;
            // 
            // lblResult
            // 
            lblResult.Font = new Font("맑은 고딕", 16F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblResult.Location = new Point(12, 585);
            lblResult.Name = "lblResult";
            lblResult.Size = new Size(1180, 60);
            lblResult.TabIndex = 8;
            lblResult.Text = "[카드 분배] 버튼을 눌러 주세요";
            lblResult.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblInfo
            // 
            lblInfo.Location = new Point(12, 650);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(1180, 45);
            lblInfo.TabIndex = 9;
            lblInfo.Text = "카드 : -     |     족보 : -";
            lblInfo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lvProbabilities
            // 
            lvProbabilities.Columns.AddRange(new ColumnHeader[] { colRank, colCount, colProbability, colCurrent });
            lvProbabilities.FullRowSelect = true;
            lvProbabilities.GridLines = true;
            lvProbabilities.Location = new Point(12, 700);
            lvProbabilities.Name = "lvProbabilities";
            lvProbabilities.Size = new Size(1180, 420);
            lvProbabilities.TabIndex = 10;
            lvProbabilities.UseCompatibleStateImageBehavior = false;
            lvProbabilities.View = View.Details;
            // 
            // colRank
            // 
            colRank.Text = "족보";
            colRank.Width = 380;
            // 
            // colCount
            // 
            colCount.Text = "경우의 수";
            colCount.TextAlign = HorizontalAlignment.Right;
            colCount.Width = 260;
            // 
            // colProbability
            // 
            colProbability.Text = "확률";
            colProbability.TextAlign = HorizontalAlignment.Right;
            colProbability.Width = 260;
            // 
            // colCurrent
            // 
            colCurrent.Text = "현재 패";
            colCurrent.Width = 230;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1204, 1132);
            Controls.Add(lvProbabilities);
            Controls.Add(lblInfo);
            Controls.Add(lblResult);
            Controls.Add(btnProbabilities);
            Controls.Add(Card5);
            Controls.Add(Card4);
            Controls.Add(Card3);
            Controls.Add(Card2);
            Controls.Add(btnDeal);
            Controls.Add(Card1);
            Name = "Form1";
            Text = "카드딜러 - 포커 족보 판별";
            ((System.ComponentModel.ISupportInitialize)Card1).EndInit();
            ((System.ComponentModel.ISupportInitialize)Card2).EndInit();
            ((System.ComponentModel.ISupportInitialize)Card3).EndInit();
            ((System.ComponentModel.ISupportInitialize)Card4).EndInit();
            ((System.ComponentModel.ISupportInitialize)Card5).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox Card1;
        private Button btnDeal;
        private PictureBox Card2;
        private PictureBox Card3;
        private PictureBox Card4;
        private PictureBox Card5;
        private Button btnProbabilities;
        private ListView lvProbabilities;
        private ColumnHeader colRank;
        private ColumnHeader colCount;
        private ColumnHeader colProbability;
        private ColumnHeader colCurrent;
        private Label lblResult;
        private Label lblInfo;
    }
}
