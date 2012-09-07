using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;


using Kuaff.Tractor.Plugins;

namespace Kuaff.Tractor
{
    partial class SelectUserAlgorithm : Form
    {
        MainForm mainForm;
        Hashtable ht;

        internal SelectUserAlgorithm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            //初始化算法
            ht = new Hashtable();
            InitUserAlgorithm();

            comboBox1.Items.Add("内置的算法");
            comboBox2.Items.Add("内置的算法");
            comboBox3.Items.Add("内置的算法");
            comboBox4.Items.Add("内置的算法");

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;

            foreach (DictionaryEntry en in ht)
            {
                int i = comboBox1.Items.Add(en.Key);
                if (mainForm.UserAlgorithms[0] != null)
                {
                    IUserAlgorithm ua = (IUserAlgorithm)mainForm.UserAlgorithms[0];
                    if (ua.Name == en.Key.ToString())
                    {
                        comboBox1.SelectedIndex = i;
                    }
                }
                i = comboBox2.Items.Add(en.Key);
                if (mainForm.UserAlgorithms[1] != null)
                {
                    IUserAlgorithm ua = (IUserAlgorithm)mainForm.UserAlgorithms[1];
                    if (ua.Name == en.Key.ToString())
                    {
                        comboBox2.SelectedIndex = i;
                    }
                }
                i = comboBox3.Items.Add(en.Key);
                if (mainForm.UserAlgorithms[2] != null)
                {
                    IUserAlgorithm ua = (IUserAlgorithm)mainForm.UserAlgorithms[2];
                    if (ua.Name == en.Key.ToString())
                    {
                        comboBox3.SelectedIndex = i;
                    }
                }
                i = comboBox4.Items.Add(en.Key);
                if (mainForm.UserAlgorithms[3] != null)
                {
                    IUserAlgorithm ua = (IUserAlgorithm)mainForm.UserAlgorithms[3];
                    if (ua.Name == en.Key.ToString())
                    {
                        comboBox4.SelectedIndex = i;
                    }
                }
            }



        }

        private void InitUserAlgorithm()
        {
            if (!Directory.Exists("plugins"))
            {
                return;
            }

            string[] assemFiles = Directory.GetFiles("plugins", "*.dll");

            foreach (string assemFile in assemFiles)
            {
                Assembly assem = Assembly.LoadFile(Path.GetFullPath(assemFile));
                Type[] types = assem.GetTypes();
                foreach (Type type in types)
                {

                    if (type.IsClass && !type.IsAbstract) //如果继承接口
                    {
                        IUserAlgorithm ua = Activator.CreateInstance(type) as IUserAlgorithm;
                        if (ua != null)
                        {
                            ht.Add(ua.Name, type);
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex > 0)
            {
                Type type = (Type)ht[comboBox1.SelectedItem];
                IUserAlgorithm ua = (IUserAlgorithm)((Activator.CreateInstance(type)));
                mainForm.UserAlgorithms[0] = ua;
            }
            else
            {
                mainForm.UserAlgorithms[0] = null;
            }

            //北
            if (comboBox2.SelectedIndex > 0)
            {
                Type type = (Type)ht[comboBox2.SelectedItem];
                IUserAlgorithm ua = (IUserAlgorithm)((Activator.CreateInstance(type)));
                mainForm.UserAlgorithms[1] = ua;
            }
            else
            {
                mainForm.UserAlgorithms[1] = null;
            }

            //西
            if (comboBox3.SelectedIndex > 0)
            {
                Type type = (Type)ht[comboBox3.SelectedItem];
                IUserAlgorithm ua = (IUserAlgorithm)((Activator.CreateInstance(type)));
                mainForm.UserAlgorithms[2] = ua;
            }
            else
            {
                mainForm.UserAlgorithms[2] = null;
            }

            //东
            if (comboBox4.SelectedIndex > 0)
            {
                Type type = (Type)ht[comboBox4.SelectedItem];
                IUserAlgorithm ua = (IUserAlgorithm)((Activator.CreateInstance(type)));
                mainForm.UserAlgorithms[3] = ua;
            }
            else
            {
                mainForm.UserAlgorithms[3] = null;
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex > 0)
            {
                Type type = (Type)ht[comboBox1.SelectedItem];
                IUserAlgorithm ua = (IUserAlgorithm)((Activator.CreateInstance(type)));

                label12.Text = ua.Name + "";
                label11.Text = ua.Email + "";
                textBox3.Text = ua.Description + "";
            }
            else
            {
                label12.Text = "smallnest";
                label11.Text = "smallnest@gmail.com";
                textBox3.Text = "内置的出牌算法";
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex > 0)
            {
                Type type = (Type)ht[comboBox1.SelectedItem];
                IUserAlgorithm ua = (IUserAlgorithm)((Activator.CreateInstance(type)));

                label17.Text = ua.Name + "";
                label16.Text = ua.Email + "";
                textBox4.Text = ua.Description + "";
            }
            else
            {
                label17.Text = "smallnest";
                label16.Text = "smallnest@gmail.com";
                textBox4.Text = "内置的出牌算法";
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex > 0)
            {
                Type type = (Type)ht[comboBox1.SelectedItem];
                IUserAlgorithm ua = (IUserAlgorithm)((Activator.CreateInstance(type)));

                label7.Text = ua.Name + "";
                label6.Text = ua.Email + "";
                textBox2.Text = ua.Description + "";
            }
            else
            {
                label7.Text = "smallnest";
                label6.Text = "smallnest@gmail.com";
                textBox2.Text = "内置的出牌算法";
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex > 0)
            {
                Type type = (Type)ht[comboBox1.SelectedItem];
                IUserAlgorithm ua = (IUserAlgorithm)((Activator.CreateInstance(type)));

                label4.Text = ua.Name + "";
                label5.Text = ua.Email + "";
                textBox1.Text = ua.Description + "";
            }
            else
            {
                label4.Text = "smallnest";
                label5.Text = "smallnest@gmail.com";
                textBox1.Text = "内置的出牌算法";
            }
        }
    }


}