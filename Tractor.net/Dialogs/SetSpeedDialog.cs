using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kuaff.Tractor
{
    partial class SetSpeedDialog : Form
    {
        private MainForm mainForm;

        internal SetSpeedDialog(MainForm form)
        {
            mainForm = form;
            InitializeComponent();

            int a1 =  (int)(25 * Math.Log10(mainForm.gameConfig.FinishedOncePauseTime / 150.0));
            int a2 = (int)(25 * Math.Log10(mainForm.gameConfig.NoRankPauseTime / 500.0));
            int a3 = (int)(25 * Math.Log10(mainForm.gameConfig.Get8CardsTime / 100.0));
            int a4 = (int)(25 * Math.Log10(mainForm.gameConfig.SortCardsTime / 100.0));
            int a5 = (int)(25 * Math.Log10(mainForm.gameConfig.FinishedThisTime / 250.0));
            int a6 = (int)(25 * Math.Log10(mainForm.gameConfig.TimerDiDa / 10.0));

            if (a1 > 50)
            {
                a1 = 50;
            }
            if (a2 > 50)
            {
                a2 = 50;
            }
            if (a3 > 50)
            {
                a3 = 50;
            }
            if (a4 > 50)
            {
                a4 = 50;
            }
            if (a5 > 50)
            {
                a5 = 50;
            }
            if (a6 > 50)
            {
                a6 = 50;
            }


            if (a1 < 0)
            {
                a1 = 0;
            }
            if (a2 < 0)
            {
                a2 = 0;
            }
            if (a3 < 0)
            {
                a3 = 0;
            }
            if (a4 < 0)
            {
                a4 = 0;
            }
            if (a5 < 0)
            {
                a5 = 0;
            }
            if (a6 < 0)
            {
                a6 = 0;
            }

            trackBar1.Value = a1;
            trackBar2.Value = a2;
            trackBar3.Value = a3;
            trackBar4.Value = a4;
            trackBar5.Value = a5;
            trackBar6.Value = a6;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            trackBar1.Value = 25;
            trackBar2.Value = 25;
            trackBar3.Value = 25;
            trackBar4.Value = 25;
            trackBar5.Value = 25;
            trackBar6.Value = 25;
        }
    }
}