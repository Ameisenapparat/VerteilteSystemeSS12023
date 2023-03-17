using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VWW.Samples.Chat.Client.UI
{
    public partial class Form2 : Form
    {
        private Button selectButton;
        private OpenFileDialog openFileDialog1;
        private TextBox textBoxalfa;
        public IClient Client { get; set; }
        public Form2()
        {
            InitializeComponent();
            this.Shown += (s, e) =>
            {
                if (this.Client == null)
                    return;
                this.Text = this.Client.Username;
                updateList();
                this.Client.OnMessageReceived += (x,y) =>
                {
                    this.textBoxHistory.Text += Environment.NewLine + y.Data.Sender + ": " + y.Data.MessageText;
                };
                this.Client.OnUserLoggedIn += (x, y) =>
                {
                    this.textBoxHistory.Text += Environment.NewLine + y.Data + " entered";
                    updateList();
                };
                this.Client.OnUserLoggedOff += (x, y) =>
                {
                    this.textBoxHistory.Text += Environment.NewLine + y.Data + " left";
                    updateList();
                };
            };
        }

        private void updateList()
        {
            this.listBox1.Items.Clear();
            this.Client.User.ToList().ForEach(f => this.listBox1.Items.Add(f));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
                return;
            if (listBox1.SelectedItems.Count == 0)
            {
                this.Client.SendBroadcast(textBox1.Text);
            }else
            {
                this.Client.SendMessage(textBox1.Text, listBox1.SelectedItems.Cast<string>());
            }
            textBox1.Text = "";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.textBoxHistory.Text += Environment.NewLine + " uploading a new module";
        }

        private void button2_Click(object sender, EventArgs e)
        {

            this.textBoxHistory.Text += Environment.NewLine + " uploading a new module";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.textBoxHistory.Text += Environment.NewLine + " uploading a new module";
        }


        private void button4_Click(object sender, EventArgs e)
        {
            this.textBoxHistory.Text += Environment.NewLine + " uploading a new module";

          // var src = new StreamReader(textBox1.Text);
          // this.textBoxHistory.Text += Environment.NewLine + src.ReadToEnd();
          
            
            using (StreamReader reader = new StreamReader(textBox1.Text))
            {
                string content = reader.ReadToEnd();
                this.Client.UploadModule(content);
              //  File.WriteAllText(@"C:\Labor\mor.txt", content);
              
            }

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.label7.Text += this.Client.SystemStatusRequest();

        }
    }
}
