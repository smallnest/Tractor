using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Kuaff.Tractor
{
    internal partial class SetGameFinished : Form
    {
        MainForm mainForm;

        public SetGameFinished(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                mainForm.gameConfig.WhenFinished = int.Parse(textBox2.Text);
            }
            catch(Exception ex)
            {
            }
        }

        private void SaveGameConfig()
        {
            Stream stream = null;
            try
            {
                IFormatter formatter = new BinaryFormatter();
                stream = new FileStream("gameConfig", FileMode.Create, FileAccess.Write, FileShare.None);
                formatter.Serialize(stream, mainForm.gameConfig);
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
    }
}