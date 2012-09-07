using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kuaff.Tractor
{
    internal partial class TotalScores : Form
    {
        private MainForm form = null;

        internal TotalScores(MainForm form)
        {
            InitializeComponent();

            this.form = form;

            label2.Text = "第" + (form.currentState.OurTotalRound + 1) + "轮第" + (form.currentState.OurCurrentRank + 1) + "局";
            label3.Text = "第" + (form.currentState.OpposedTotalRound + 1) + "轮第" + (form.currentState.OpposedCurrentRank + 1) + "局";

            label2.Select();
        }
    }
}