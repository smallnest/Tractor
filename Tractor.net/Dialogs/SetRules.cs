using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Kuaff.Tractor
{
     partial class SetRules : Form
    {
        private MainForm form = null;
         internal SetRules(MainForm form)
        {
            this.form = form;
            InitializeComponent();

            string mustRank = form.gameConfig.MustRank;
            if (mustRank.IndexOf(",3,") >= 0)
            {
                checkBox1.CheckState = CheckState.Checked;
            }
            else
            {
                checkBox1.CheckState = CheckState.Unchecked;
            }
            if (mustRank.IndexOf(",8,") >= 0)
            {
                checkBox2.CheckState = CheckState.Checked;
            }
            else
            {
                checkBox2.CheckState = CheckState.Unchecked;
            }
            if (mustRank.IndexOf(",9,") >= 0)
            {
                checkBox6.CheckState = CheckState.Checked;
            }
            else
            {
                checkBox6.CheckState = CheckState.Unchecked;
            }
            if (mustRank.IndexOf(",10,") >= 0)
            {
                checkBox7.CheckState = CheckState.Checked;
            }
            else
            {
                checkBox7.CheckState = CheckState.Unchecked;
            }
            if (mustRank.IndexOf(",11,") >= 0)
            {
                checkBox3.CheckState = CheckState.Checked;
            }
            else
            {
                checkBox3.CheckState = CheckState.Unchecked;
            }
            if (mustRank.IndexOf(",12,") >= 0)
            {
                checkBox4.CheckState = CheckState.Checked;
            }
            else
            {
                checkBox4.CheckState = CheckState.Unchecked;
            }
            if (mustRank.IndexOf(",13,") >= 0)
            {
                checkBox5.CheckState = CheckState.Checked;
            }
            else
            {
                checkBox5.CheckState = CheckState.Unchecked;
            }

             //
            if (form.gameConfig.JToBottom)
            {
                checkBox8.CheckState = CheckState.Checked;
            }
            else
            {
                checkBox8.CheckState = CheckState.Unchecked;
            }

            if (form.gameConfig.QToHalf)
            {
                checkBox9.CheckState = CheckState.Checked;
            }
            else
            {
                checkBox9.CheckState = CheckState.Unchecked;
            }

            if (form.gameConfig.AToJ)
            {
                checkBox13.CheckState = CheckState.Checked;
            }
            else
            {
                checkBox13.CheckState = CheckState.Unchecked;
            }


            if (form.gameConfig.IsPass)
            {
                radioButton1.Checked = true;
            }
            else
            {
                radioButton2.Checked = true;
            }

            if (form.gameConfig.BottomAlgorithm == 1)
            {
                radioButton3.Checked = true;
            }
            else if (form.gameConfig.BottomAlgorithm == 2)
            {
                radioButton4.Checked = true;
            }
            else if (form.gameConfig.BottomAlgorithm == 3)
            {
                radioButton5.Checked = true;
            }
            

             //
            if (form.gameConfig.CanRankJack)
            {
                checkBox10.CheckState = CheckState.Checked;
            }
            else
            {
                checkBox10.CheckState = CheckState.Unchecked;
            }

            if (form.gameConfig.CanMyRankAgain)
            {
                checkBox11.CheckState = CheckState.Checked;
            }
            else
            {
                checkBox11.CheckState = CheckState.Unchecked;
            }

            if (form.gameConfig.CanMyStrengthen)
            {
                checkBox12.CheckState = CheckState.Checked;
            }
            else
            {
                checkBox12.CheckState = CheckState.Unchecked;
            }

           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string mustRank = ",";
            if (checkBox1.Checked)
            {
                mustRank += "3,";
            }
            if (checkBox2.Checked)
            {
                mustRank += "8,";
            }
            if (checkBox3.Checked)
            {
                mustRank += "11,";
            }
            if (checkBox4.Checked)
            {
                mustRank += "12,";
            }
            if (checkBox5.Checked)
            {
                mustRank += "13,";
            }
            if (checkBox6.Checked)
            {
                mustRank += "9,";
            }
            if (checkBox7.Checked)
            {
                mustRank += "10,";
            }

            
            
            form.gameConfig.MustRank = mustRank;
            //保存到文件
            SaveGameConfig();
        }

         private void button2_Click(object sender, EventArgs e)
         {
             if (checkBox8.Checked)
             {
                 form.gameConfig.JToBottom = true;
             }
             else
             {
                 form.gameConfig.JToBottom = false;
             }
             if (checkBox9.Checked)
             {
                 form.gameConfig.QToHalf = true;
             }
             else
             {
                 form.gameConfig.QToHalf = false;
             }

             if (checkBox13.Checked)
             {
                 form.gameConfig.AToJ = true;
             }
             else
             {
                 form.gameConfig.AToJ = false;
             }

            
             //保存到文件
             SaveGameConfig();
         }

         private void SaveGameConfig()
         {
             Stream stream = null;
             try
             {
                 IFormatter formatter = new BinaryFormatter();
                 stream = new FileStream("gameConfig", FileMode.Create, FileAccess.Write, FileShare.None);
                 formatter.Serialize(stream, form.gameConfig);
             }
             catch (Exception ex)
             {

             }
             finally
             {
                 if (stream != null)
                 {
                     stream.Close();
                 }
             }
         }

         private void button3_Click(object sender, EventArgs e)
         {
             if (radioButton1.Checked)
             {
                 form.gameConfig.IsPass = true;
             }
             else
             {
                 form.gameConfig.IsPass = false;
             }

             //保存到文件
             SaveGameConfig();
         }

         private void button4_Click(object sender, EventArgs e)
         {
             if (radioButton3.Checked)
             {
                 form.gameConfig.BottomAlgorithm = 1;
             }
             else if (radioButton4.Checked)
             {
                 form.gameConfig.BottomAlgorithm = 2;
             }
             else if (radioButton5.Checked)
             {
                 form.gameConfig.BottomAlgorithm = 3;
             }

             //保存到文件
             SaveGameConfig();
         }

         private void button5_Click(object sender, EventArgs e)
         {
             if (checkBox10.Checked)
             {
                 form.gameConfig.CanRankJack = true;
             }
             else
             {
                 form.gameConfig.CanRankJack = false;
             }
             if (checkBox11.Checked)
             {
                 form.gameConfig.CanMyRankAgain = true;
             }
             else
             {
                 form.gameConfig.CanMyRankAgain = false;
             }

             if (checkBox12.Checked)
             {
                 form.gameConfig.CanMyStrengthen = true;
             }
             else
             {
                 form.gameConfig.CanMyStrengthen = false;
             }

             //保存到文件
             SaveGameConfig();
         }
    }
}