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
        public Form1()
        {
            InitializeComponent();
        }

        private TabControl dynamicTabControl;
        Boolean isClickedBtn1 = false, isClickedBtn2 = false, isClickedBtn3 = false, isClickedBtn4 = false , isClickedBtn5 = false;
        //int indexBtn2 = 0;
        //TabPage tabPgm2;

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
            
         
            dynamicTabControl.Font = new Font("Georgia", 16);
            dynamicTabControl.Width = 500;
            dynamicTabControl.Height = 500;
            dynamicTabControl.Location = new Point(227,12);

            //Add the tabControl to the form
            this.Controls.Add(dynamicTabControl);

            AdjustFormSize();
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            
            if (isClickedBtn1)
            {
                btn1.Text = "Program 1";
                dynamicTabControl.TabPages[0].Dispose();
                isClickedBtn1 = false;

            }
            else
            {
                AddTabPage(btn1.Name);
                isClickedBtn1 = true;
                btn1.Text = "Close Program1";
               
                //Execute Calculator code
                Process p = Process.Start("calc.exe");
                Thread.Sleep(500);
                p.WaitForInputIdle();
                SetParent(p.MainWindowHandle, this.dynamicTabControl.TabPages[0].Handle);
            } 
            AdjustFormSize();

        }

        public void AddTabPage(string name)
        {
            TabPage tabPage1 = new TabPage();
            tabPage1.Name = name;
            tabPage1.Text = name;
            tabPage1.BackColor = Color.Green;
            tabPage1.ForeColor = Color.White;
            tabPage1.Font = new Font("Verdana", 12);
            tabPage1.Width = 500;
            tabPage1.Height = 500;
            this.dynamicTabControl.TabPages.Add(tabPage1);

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

        public void calculatorTab() { 
        
        }

        private void btn2_Click(object sender, EventArgs e)
        {
          /*  tabPgm2 = new TabPage();
           
            if (isClickedBtn2)
            {
               // btn2.Text = btn1.Name;
                dynamicTabControl.TabPages.Remove(tabPgm2);
                isClickedBtn2 = false;

            }
            else
            {
                //Create a new tab for program2
                tabPgm2.Name = btn2.Text;
                tabPgm2.Text = btn2.Text;

                dynamicTabControl.TabPages.Add(tabPgm2.Name);

                indexBtn2 = dynamicTabControl.TabIndex +1;
                isClickedBtn2 = true;
                btn1.Text = "Close Program2";

                //Execute Calculator code
                Process p = Process.Start("calc.exe");
                Thread.Sleep(500);
                p.WaitForInputIdle();
                SetParent(p.MainWindowHandle,tabPgm2.Handle);
            }
            AdjustFormSize(); */
        }
       
    }
}
