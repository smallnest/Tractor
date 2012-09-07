using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using ICSharpCode.SharpZipLib.BZip2;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using ICSharpCode.SharpZipLib.GZip;

namespace Kuaff.Tractor
{
    internal partial class SelectCardbackImage : Form
    {
        private MainForm form = null;
        internal string CardBackImageName = "";

        internal SelectCardbackImage(MainForm form)
        {
            InitializeComponent();

            InitComboBox();

            this.form = form;
        }

        private  void InitComboBox()
        {
            DirectoryInfo dir = new DirectoryInfo("cardbackimgs");
            FileInfo[] files = dir.GetFiles("*.png", SearchOption.TopDirectoryOnly);
            for (int i = 0; i < files.Length; i++)
            {
                CustomCardsImage.Items.Add(Path.GetFileNameWithoutExtension(files[i].Name));
            }
            if (CustomCardsImage.Items.Count>0)
            {
                CustomCardsImage.SelectedIndex = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //得到文件名
            
            if (CustomCardsImage.Text.Length == 0)
            {
                this.DialogResult = DialogResult.Cancel;
                return;
            }

            CardBackImageName = CustomCardsImage.Text;

            string name = CustomCardsImage.Text + ".png";
            name = Path.Combine("cardbackimgs", name);
            if (!File.Exists(name))
            {
                this.DialogResult = DialogResult.Cancel;
                return;
            }

             form.gameConfig.BackImage = new Bitmap(name);

             this.DialogResult = DialogResult.OK;
        }
    }
}