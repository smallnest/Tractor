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
    internal partial class SelectCardsImage : Form
    {
        private MainForm form = null;
        internal string CardsName = "";

        internal SelectCardsImage(MainForm form)
        {
            InitializeComponent();

            InitComboBox();

            this.form = form;
        }

        private  void InitComboBox()
        {
            DirectoryInfo dir = new DirectoryInfo("cards");
            FileInfo[] files = dir.GetFiles("*.cds", SearchOption.TopDirectoryOnly);
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

            CardsName = CustomCardsImage.Text;

            string name = CustomCardsImage.Text + ".cds";
            name = Path.Combine("cards", name);
            if (!File.Exists(name))
            {
                this.DialogResult = DialogResult.Cancel;
                return;
            }

            //解压缩
            
            ZipInputStream s = new ZipInputStream(File.OpenRead(name));

            ZipEntry theEntry;
            while ((theEntry = s.GetNextEntry()) != null)
            {
                int number = int.Parse(Path.GetFileNameWithoutExtension(theEntry.Name));

                Stream stream = new MemoryStream();
                int size = 2048;
                byte[] data = new byte[2048];
                while (true)
                {
                    size = s.Read(data, 0, data.Length);
                    if (size > 0)
                    {
                        stream.Write(data, 0, size);
                    }
                    else
                    {
                        break;
                    }
                }
                
                Bitmap bmp = new Bitmap(stream);
                stream.Close();
                form.cardsImages[number] = bmp;
            }
            s.Close();


            this.DialogResult = DialogResult.OK;

        }
    }
}