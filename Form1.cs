using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ARGL
{
    public partial class Form1 : Form
    { 
        private TabControl dynamicTabControl;
        int isClickedBtn1 = 0, isClickedBtn2 = 0;
        private TabPage tabPage;
        int click_number = 0;
        string previous_tab;

        public Form1()
        {
            InitializeComponent();   
        }

        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hwc, IntPtr hwp);

        public void Form1_Load(object sender, EventArgs e)
        {
            btnexit.Text = "Exit";
            btn1.Text = "Program1";
            //Create a TabControl Tab pages will be added by clicking on buttons.

            dynamicTabControl = new TabControl();
            dynamicTabControl.Name = "DynamicTabControl";
            dynamicTabControl.ForeColor = Color.Transparent;
            dynamicTabControl.BackColor = Color.Transparent;
            
            dynamicTabControl.Font = new Font("Georgia", 12);
            dynamicTabControl.Width = 500;
            dynamicTabControl.Height = 500;
            dynamicTabControl.Location = new Point(227,12);
            dynamicTabControl.MouseDown += new System.Windows.Forms.MouseEventHandler(tab_OnMouseDown);

            //Add the tabControl to the form
            this.Controls.Add(dynamicTabControl);

            AdjustFormSize();
        }

        private void tab_OnMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            click_number++;
            previous_tab = sender.ToString();
            if (click_number > 1)
            { 
                for (int i = 0; i < this.dynamicTabControl.TabPages.Count; i++)
                {
                    Rectangle r = dynamicTabControl.GetTabRect(i);
                    // Rectangle closeButton = new Rectangle(r.Right - 5, r.Top -2, 9, 10);
                    Rectangle closeButton = new Rectangle(r.Location.X, r.Location.Y, r.Size.Width, r.Size.Height);
                    if (closeButton.Contains(e.Location))
                    {
                        if (MessageBox.Show("Would you like to Close this Tab", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            this.dynamicTabControl.TabPages.RemoveAt(i);
                            break;
                        }
                    }
                }
                click_number = 0;
            }

            
            if (this.dynamicTabControl.TabPages.Count == 0)
            {
                AdjustFormSize();
            }
            
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn1_Click(object sender, EventArgs e)
        {
           if (isClickedBtn1 == 0)
           {
                btn1.Text = "Program 1";
  
                AddTabPage(btn1);
                isClickedBtn1 ++;
               
                //Execute Calculator code
                Process p = Process.Start("calc.exe");
                //Process p = Process.Start("C:\\Program Files\\Ocean Optics\\OceanView\\oceanview\\bin\\OceanViewWindowsLauncher.exe");
               //Process p = Process.Start("C:\\Program Files (x86)\\Fiso Technologies\\FISOCommander 2 Standard Edition\\FCSv2.exe");
                Thread.Sleep(500);
                p.WaitForInputIdle();
                SetParent(p.MainWindowHandle, dynamicTabControl.TabPages[btn1.Name].Handle);
          } 
            AdjustFormSize();
        }

 
        private void btn2_Click(object sender, EventArgs e)
        {   
            if (isClickedBtn2 == 0)
            {
                AddTabPage(btn2);
                isClickedBtn2 ++;
                btn2.Text = "Program2";

                //Execute Calculator code
                Process p = Process.Start("calc.exe");
                Thread.Sleep(500);
                p.WaitForInputIdle();
                SetParent(p.MainWindowHandle, dynamicTabControl.TabPages[btn2.Name].Handle);
            }
            AdjustFormSize(); 
        }

        public void AddTabPage(Button btn)
        {
            tabPage = new TabPage("");
            tabPage.Name = btn.Name;
            tabPage.Text = btn.Text;
            //tabPage.BackColor = Color.Green;
            tabPage.ForeColor = Color.White;
            tabPage.Font = new Font("Verdana", 12);
            tabPage.Width = 500;
            tabPage.Height = 500;
            this.dynamicTabControl.TabPages.Add(tabPage);

        }

        public void AdjustFormSize()
        {
            if (dynamicTabControl.TabCount > 0)
            {
                this.Size = new Size(800, 500);
            }
            else
            {
                this.Size = groupBox1.Size;
            }
        }

       
    }
}
