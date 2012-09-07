using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Kuaff.Tractor
{
    internal partial class SelectMusic : Form
    {
        private string musicDirectory = "";

        internal SelectMusic()
        {
            InitializeComponent();

            DirectoryInfo dir = new DirectoryInfo(Path.Combine(Environment.CurrentDirectory,"music"));
            musicDirectory = dir.FullName;
            initMusic(dir);
        }

        private void initMusic(DirectoryInfo dir)
        {
            
            FileInfo[] files = dir.GetFiles();

            for (int i = 0; i < files.Length;i++ )
            {
                String name = files[i].FullName;
                if (name.EndsWith(".mid") || name.EndsWith(".mp3") || name.EndsWith(".wav"))
                {
                    name = name.Substring(musicDirectory.Length+1);
                    music.Items.Add(name);
                }
            }

            DirectoryInfo[] dis = dir.GetDirectories();
            for (int i=0;i<dis.Length;i++)
            {
                initMusic(dis[i]);
            }


            if (music.Items.Count>0)
            {
                Random random = new Random();
                music.SelectedIndex = random.Next(music.Items.Count);
            }
        }
    }
}