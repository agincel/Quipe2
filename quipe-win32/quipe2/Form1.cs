using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net;
using System.IO;
using System.Runtime.Serialization;

namespace quipe2
{
    public partial class Form1 : Form
    {
        int focusDelay = 0;

        string typing = "";

        BindingList<string> leaderboard = new BindingList<string>();

        public Form1()
        {
            InitializeComponent();
            txtID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CheckEnterKeyPress);
            //quipe = Process.Start("quipe.exe");
            ProcessStartInfo sInfo = new ProcessStartInfo("http://adamgincel.com/quipegms");
            Process.Start(sInfo);
        }

        private void CheckEnterKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                //check if is valid swipe
                e.Handled = true;
                typing = txtID.Text;
                if ((typing.StartsWith("%") || typing.StartsWith(";")) && typing.EndsWith("?") && !typing.EndsWith("E?") && typing.Length >= 12)
                { //ID starts with ; or %, ends with a ?, but doesn't end with an E?, which is an error
                    if (tbName.Text == "")
                    {
                        try
                        {
                            WebRequest request = WebRequest.Create("http://localhost:5000/queue?id=" + typing.Substring(2, 8));
                            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                            wb.Parent.Enabled = true;
                            wb.Document.Focus();
                            wb.SelectNextControl(wb, true, true, true, true);
                            SendKeys.SendWait(typing);
                            SendKeys.SendWait("{ENTER}");

                            txtID.Text = "";

                            wb.Parent.Enabled = false;

                            focusDelay = 5;
                        } catch (Exception er)
                        {
                            MessageBox.Show(er.ToString());
                            txtID.Text = "";
                        }
                    } else
                    {
                        try
                        {
                            Console.WriteLine("Overwriting Name");
                            WebRequest request = WebRequest.Create("http://localhost:5000/name?id=" + typing.Substring(2, 8) + "&name=" + tbName.Text);
                            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                            tbName.Text = "";
                            txtID.Text = "";
                            lblTyping.Text = "Name updated in database.";
                            focusDelay = 1030;
                            txtID.Focus();
                        } catch (Exception er)
                        {
                            txtID.Focus();
                            MessageBox.Show(er.ToString());
                            txtID.Text = "";
                        }
                    }
                } else
                {
                    txtID.Text = "";
                    typing = "";
                    txtID.Focus();
                    lblTyping.Text = "There was an error swiping your card. Please try again.";
                    focusDelay = 1030;
                }
            }
        }

        private void tmrFocus_Tick(object sender, EventArgs e)
        {
            if (focusDelay == 1000)
            {
                focusDelay = 0;
                typing = "";
                txtID.Focus();
                lblTyping.Text = "Please Swipe your ID.";
            }
            else if (focusDelay > 1)
                focusDelay -= 1;
            else if (focusDelay == 1)
            {
                wb.SelectNextControl(wb, true, true, true, true);
                focusDelay = 0;
                txtID.Text = "";
                typing = "";
                txtID.Select();
                txtID.Select();
                txtID.Select();
                txtID.Select();
                txtID.Select();
                lblTyping.Text = "Please Swipe your ID.";
            } else if (focusDelay == 0)
            {
                //if (!txtID.Focused)
                  //  txtID.Select();
            }
        }

        private void updateLeaderboard()
        {
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:5000/leaders");
                httpWebRequest.Method = WebRequestMethods.Http.Get;
                httpWebRequest.Accept = "application/json";
                Console.WriteLine("1");
                HttpWebResponse res = (HttpWebResponse)httpWebRequest.GetResponse();
                Stream stream = res.GetResponseStream();
                System.Runtime.Serialization.Json.DataContractJsonSerializer dataContractJsonSerializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(List<User>));
                Console.WriteLine("2");
                List<User> objResponse = (List<User>)dataContractJsonSerializer.ReadObject(stream);
                leaderboard.Clear();
                Console.WriteLine("3");
                Console.WriteLine(objResponse);
                int i = 1;
                foreach(User u in objResponse)
                {

                    Console.WriteLine(u);
                    string temp = i.ToString() + " | ";
                    i += 1;
                    if (u.name != null)
                    {
                        Console.WriteLine("Adding name");
                        Console.WriteLine(u.name);
                        temp += u.name + ": ";
                    } else
                    {
                        Console.WriteLine(u.id);
                        temp += "****" + u.id.Substring(4) + ": ";
                    }
                    temp += u.score.ToString();
                    if (!u.hasSwiped)
                        temp += "*";
                    Console.WriteLine(temp);
                    leaderboard.Add(temp);
                }
                Console.WriteLine("4");
                lbLeaderboard.DataSource = leaderboard;

                httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:5000/number");
                res = (HttpWebResponse)httpWebRequest.GetResponse();
                stream = res.GetResponseStream();

                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    string value = reader.ReadToEnd();
                    lblSwipes.Text = "Swipes: " + value;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                ;
            }
        }

        [DataContract]
        public class User
        {
            [DataMember(Name = "id")]
            public string id { get; set; }

            [DataMember(Name = "name")]
            public string name { get; set; }

            [DataMember(Name = "score")]
            public int score { get; set; }

            [DataMember(Name = "hasSwiped")]
            public bool hasSwiped { get; set; }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            updateLeaderboard();
            txtID.Focus();
        }
    }
}
