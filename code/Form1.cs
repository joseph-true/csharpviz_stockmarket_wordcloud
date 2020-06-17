// -------------------------------------------------
//Author:				Joseph True
//Copyright:			2008-2020
//
//Project ID:			Ch 8 - Stock Market Word Visualization
//CS Class:				CS 525D Fall 2008
//Programming Language:	C#
//
//Overall Design:		This program displays stock price performance
//						for different companies on one day using font size
//						and color to show price change.
//
//						Stock data was collected from Yahoo Finance format
//						http://finance.yahoo.com
//
//Additional Files:		Stock market data in CSV files.
//
// -------------------------------------------------
//
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Diagnostics;

namespace WindowsApplication2
{
	// .NET genertared code for form
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		//---------------------------------------
		// Application variables
		//---------------------------------------
	
		// String data values read from file
		ArrayList m_array1 = new ArrayList();
		ArrayList m_array2 = new ArrayList();
		ArrayList m_array3 = new ArrayList();
		ArrayList m_array4 = new ArrayList();
		ArrayList m_array5 = new ArrayList();
		ArrayList m_array6 = new ArrayList();
		ArrayList m_array7 = new ArrayList();
		ArrayList m_array8 = new ArrayList();

		ArrayList m_FileNames = new ArrayList();

        // Sub-directory for data files
        string m_contentDir = "data_files";

		// Symbol,Name,Last,Change,Change %,
		public struct StockInfo
		{
			public string Symbol;
			public string Name;
			public Double PriceLast;
			public Double PriceChange;
			public Double PriceChngPrcnt;
		}

		StockInfo[] m_StockData;

		// Chart size and boundary points

		static int m_ChartWidth = 850;	// chart width

		static int m_offsetX = 10;
		static int m_offsetY = 550;
	
		static int m_xAxisStart = m_offsetX;
		static int m_yAxisStart = m_offsetY;
		static int m_yAxisEnd =100;
		static int m_yAxisHeight = m_yAxisStart - m_yAxisEnd;	// height of chart
	
		string[] m_DimNames = new string[8];

		//imported data
		float[,] m_XYdata;
		//float m_yMin; // = new float;
		//float m_yMax; // = new float;
		private System.Windows.Forms.Label lblMsg;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btnDrawChart;
		private System.Windows.Forms.ComboBox cboStockName;
		private System.Windows.Forms.Button btnViewData;
        private ComboBox cboTextVal;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.lblMsg = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboTextVal = new System.Windows.Forms.ComboBox();
            this.btnViewData = new System.Windows.Forms.Button();
            this.cboStockName = new System.Windows.Forms.ComboBox();
            this.btnDrawChart = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblMsg
            // 
            this.lblMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.Location = new System.Drawing.Point(8, 8);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(848, 24);
            this.lblMsg.TabIndex = 0;
            this.lblMsg.Text = "label1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboTextVal);
            this.groupBox1.Controls.Add(this.btnViewData);
            this.groupBox1.Controls.Add(this.cboStockName);
            this.groupBox1.Controls.Add(this.btnDrawChart);
            this.groupBox1.Location = new System.Drawing.Point(8, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(856, 64);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Stock Market Index";
            // 
            // cboTextVal
            // 
            this.cboTextVal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTextVal.FormattingEnabled = true;
            this.cboTextVal.Location = new System.Drawing.Point(406, 24);
            this.cboTextVal.Name = "cboTextVal";
            this.cboTextVal.Size = new System.Drawing.Size(131, 21);
            this.cboTextVal.TabIndex = 6;
            // 
            // btnViewData
            // 
            this.btnViewData.Location = new System.Drawing.Point(674, 13);
            this.btnViewData.Name = "btnViewData";
            this.btnViewData.Size = new System.Drawing.Size(176, 40);
            this.btnViewData.TabIndex = 5;
            this.btnViewData.Text = "View Data in Notepad";
            this.btnViewData.Click += new System.EventHandler(this.btnViewData_Click);
            // 
            // cboStockName
            // 
            this.cboStockName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStockName.Location = new System.Drawing.Point(16, 24);
            this.cboStockName.Name = "cboStockName";
            this.cboStockName.Size = new System.Drawing.Size(368, 21);
            this.cboStockName.TabIndex = 3;
            // 
            // btnDrawChart
            // 
            this.btnDrawChart.Location = new System.Drawing.Point(556, 13);
            this.btnDrawChart.Name = "btnDrawChart";
            this.btnDrawChart.Size = new System.Drawing.Size(112, 40);
            this.btnDrawChart.TabIndex = 4;
            this.btnDrawChart.Text = "Update Display";
            this.btnDrawChart.Click += new System.EventHandler(this.btnDrawChart_Click);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(872, 566);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblMsg);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stock Market - Word Cloud Visulaization";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		// --- JTrue --------------------------------------------
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			getFileList();
			importData();

			string msgText;
			msgText = "This program displays stock price performance for different companies ";
			msgText = msgText + "on one day using font size and color to show price change.";
			lblMsg.Text =msgText;

            cboTextVal.Items.Add("Stock Name");
            cboTextVal.Items.Add("Stock Symbol");
            cboTextVal.Text = "Stock Name";
		}

		// C#
		protected override void OnPaint(PaintEventArgs e) 
		{			
			// Redraw screen when form re-paints
			drawChartCanvas();
			drawStockVis();
		}


		// Draw blank chart canvas
		private void drawChartCanvas()
		{
			// Draw white canvas
			using (Graphics g = this.CreateGraphics())
			{
				// White background
				Color myColor;
				myColor = Color.White;
				g.FillRectangle(new SolidBrush(myColor),m_xAxisStart,m_yAxisEnd,m_ChartWidth,m_yAxisHeight);
				// Black 1pt border
				Pen myPen = new Pen(Color.Black,1);
				g.DrawRectangle(myPen,m_xAxisStart,m_yAxisEnd,m_ChartWidth,m_yAxisHeight);
			}
		}

		// Read stock data from text CSV file.
		private void importData()
		{
			int fileIndex;
			string MyFileName = "";

			fileIndex = cboStockName.SelectedIndex;

            // Include sub-dir in path
            MyFileName = m_contentDir + "\\" + m_FileNames[fileIndex].ToString();

			FileStream aFile = new FileStream(MyFileName,FileMode.Open, FileAccess.Read);
			StreamReader sr = new StreamReader(aFile);		

			string strLine;

			// First line is stock name
			strLine = sr.ReadLine();	// read one line

			// Second line is column titles
			strLine = sr.ReadLine();	// read one line
			string[] strData;
			char[] chrDelimeter = new char[] {','};

			// Data starts on 3rd line
			strLine = sr.ReadLine();	// read one line

			// Read line from file and split into 8 dimensions
			while (strLine !=null)
			{
				strData = strLine.Split(chrDelimeter,10);

				m_array1.Add (strData[0]);
				m_array2.Add (strData[1]);
				m_array3.Add (strData[2]);
				m_array4.Add (strData[3]);
				m_array5.Add (strData[4]);
			
				strLine = sr.ReadLine();
			}
			sr.Close();			

			m_StockData = new StockInfo[m_array1.Count];
			m_XYdata = new float[7,m_array1.Count];

			// Convert data from string
			for(int i=0;i<m_array1.Count;i++)
			{
				m_StockData[i].Symbol=System.Convert.ToString ( m_array1[i]);	// Symbol
				m_StockData[i].Name=(System.Convert.ToString (m_array2[i]));	// Name
				m_StockData[i].PriceLast=System.Convert.ToDouble(m_array3[i]);	// Last
				m_StockData[i].PriceChange=(System.Convert.ToDouble (m_array4[i]));	// Change

				//remove % sign from Change Percent values
				char[] MyChar = {'%'};
				string MyString = m_array5[i].ToString();
				string NewString =MyString.TrimEnd(MyChar);
				m_StockData[i].PriceChngPrcnt=System.Convert.ToDouble(NewString);	// Change %
			}

			// Get min and max among stock price dimensions.
			// Date,Open,High,Low,Close,Volume,Adj Close
//			m_yMin= getMinVal(3);
//			m_yMax= getMaxVal(3);
		}

		// ===================================
		// Draw stock data
		private void drawStockVis()
		{
			Double  changeAmount;
			string stockName;
			float fontSize;

			int yLoc = m_yAxisEnd + 40;
			int xLoc = m_xAxisStart;
			int[] rgbVal = new int[3];

			Color myColor;

			// Starting point for first line of text
			PointF drawPoint = new PointF(m_offsetX + 10, m_yAxisEnd + 5);

			int texLocAdj = 0;

			for (int r=0;r<=m_StockData.GetUpperBound(0);r++)
			{

				// Get stock name
				// Get price change - green positive, red negative
				// Set font size of stock name based on value of change amount
				// ===========================================
				using (Graphics g = this.CreateGraphics())
				{
					changeAmount = System.Convert.ToDouble( m_StockData[r].PriceChngPrcnt);

                    if (cboTextVal.Text == "Stock Symbol")
                    {
                        stockName = m_StockData[r].Symbol;
                    }
                    else if (cboTextVal.Text == "Stock Name")
                    {
                        stockName = m_StockData[r].Name;
                    }
                    else
                    {
                        stockName = m_StockData[r].Name;
                    }
					

					// Set color based on positive or negative stock price move
					// ===========================================
					if (changeAmount < 0)
					{					
						// negative price change
						rgbVal = GetDownColor(Math.Abs(changeAmount)); 
						myColor = Color.FromArgb(rgbVal[0],rgbVal[1],rgbVal[2]);
					}
					else if (changeAmount > 0)
					{
						// positive price change
						rgbVal = GetUpColor(changeAmount);
						myColor = Color.FromArgb(rgbVal[0],rgbVal[1],rgbVal[2]);
					}
					else if (changeAmount == 0)
					{
						myColor = Color.Black;
					}
					else 
					{
						myColor = Color.Black;
					}

					// Set font size
					// Font size depends on % of change in stock price
					// ===========================================
					changeAmount = Math.Abs(changeAmount);

					if (changeAmount == 0)
					{
						fontSize = 7;
						texLocAdj = 16;
					}
					else if (changeAmount <= 1)
					{
						fontSize = 7;
						texLocAdj = 16;
					}
					else if (changeAmount <= 2)
					{
						fontSize = 9;
						texLocAdj = 15;
					}
					else if (changeAmount <= 4)
					{
						fontSize = 12;
						texLocAdj = 12;
					}
					else if (changeAmount <= 6)
					{
						fontSize = 14;
						texLocAdj = 10;
					}
					else if (changeAmount <= 8)
					{
						fontSize = 18;
						texLocAdj = 5;
					}
					else if (changeAmount <= 10)
					{
						fontSize = 20;
						texLocAdj = 2;
					}
					else
					{
						fontSize = 24;
					}

					Font myFnt = new Font("Verdana", fontSize, System.Drawing.FontStyle.Bold );

					SizeF textLen = new SizeF();

					textLen = g.MeasureString (stockName, myFnt);

					// Advance to next line if text length is too long
					if ((textLen.Width + drawPoint.X) > m_ChartWidth)
					{
						drawPoint.Y = drawPoint.Y + 45;
						drawPoint.X = m_offsetX+10;
						g.DrawString(stockName, myFnt, new SolidBrush(myColor),drawPoint.X,drawPoint.Y+ texLocAdj);
						drawPoint.X = drawPoint.X + textLen.Width + 10;
					}
					else
					{
						g.DrawString(stockName, myFnt, new SolidBrush(myColor),drawPoint.X,drawPoint.Y+ texLocAdj);
						drawPoint.X = drawPoint.X + textLen.Width + 10;
					}
				}
			}
		}

		// Get min value for a given dimension.
		private float getMinVal(int c)
		{
			float myVal=0;
			if (m_XYdata != null)
			{
				myVal = m_XYdata[c,0];
				for(int i=1; i<=m_XYdata.GetUpperBound(1);i++)
				{
					if (m_XYdata[c,i] < myVal)
					{
						myVal = m_XYdata[c,i];
					}
				}
			}
			return myVal;
		}

		// Get max value for a given dimension.
		private float getMaxVal(int c)
		{
			float myVal=0;
			if (m_XYdata != null)
			{
				myVal = m_XYdata[c,0];
				for(int i=1; i<=m_XYdata.GetUpperBound(1);i++)
				{
					if (m_XYdata[c,i] > myVal)
					{
						myVal = m_XYdata[c,i];
					}
				}			
			}
			return myVal;
		}

		// Draw chart for selected file
		private void btnDrawChart_Click(object sender, System.EventArgs e)
		{
			m_array1.Clear();
			m_array2.Clear();
			m_array3.Clear();
			m_array4.Clear();
			m_array5.Clear();
			m_array6.Clear();
			m_array7.Clear();

			drawChartCanvas();
			importData();
			drawStockVis();
			Form1.ActiveForm.Refresh();  
		}

		// Read list of CSV files in program directory.
		// ===========================================
		private void getFileList()
		{
			// Create a reference to the data sub-directory.
            string tmpDir = Environment.CurrentDirectory + "//" + m_contentDir;
            DirectoryInfo di = new DirectoryInfo(tmpDir);

			// Create an array representing the files in the current directory.
			FileInfo[] fi = di.GetFiles();
			
			// Print out the names of the files in the current directory.
			foreach (FileInfo fiTemp in fi)
			{
				int cmpVal =(fiTemp.Extension.CompareTo(".csv"));
				if (cmpVal==0)
				{
                    // Get local path to read title from first line
                    string csvFileName = fiTemp.Directory.Name + "\\" + fiTemp.Name;
                    FileStream aFile = new FileStream(csvFileName, FileMode.Open, FileAccess.Read);
					StreamReader sr = new StreamReader(aFile);		
		
					string strLine;
		
					// First line is stock name
					strLine = sr.ReadLine();	// read one line

					cboStockName.Items.Add(strLine);
					m_FileNames.Add(fiTemp.Name);
				}
			}
			cboStockName.SelectedIndex = 0;
		}

		// Get an RGB color value for a given x value.
		private int[] GetUpColor(Double Xval)
		{
			int R,G,B;
			int[] xColor = new int[3];

			// Color map ranges from light blue to dark blue
			//R 214 to 0
			//G 214 to 0
			//B 255 to 130

			R = 0; // - System.Convert.ToInt16((214)*(Xval/(100)));
			G = 214 - System.Convert.ToInt16((214)*(Xval/(100)));
			B = 0; //- System.Convert.ToInt16((125)*(Xval/(100)));

			xColor[0] = R;
			xColor[1] = G;
			xColor[2] = B;

			return xColor;
		}

		private int[] GetDownColor(Double Xval)
		{
			int R,G,B;
			int[] xColor = new int[3];

			// Color map ranges from light blue to dark blue
			//R 214 to 0
			//G 214 to 0
			//B 255 to 130

			R = 214 - System.Convert.ToInt16((214)*(Xval/(100)));
			G = 0; // 214 - System.Convert.ToInt16((214)*(Xval/(100)));
			B = 0; // 255 - System.Convert.ToInt16((125)*(Xval/(100)));

			xColor[0] = R;
			xColor[1] = G;
			xColor[2] = B;

			return xColor;
		}

		private void btnViewData_Click(object sender, System.EventArgs e)
		{
			int fileIndex;
			string MyFileName = "";
		
			fileIndex = cboStockName.SelectedIndex; 
			MyFileName = m_FileNames[fileIndex].ToString();

            MyFileName = (Application.StartupPath + "\\" + m_contentDir + "\\" + MyFileName);
				
			Process.Start ("Notepad.exe", MyFileName);
		}

	}
}
