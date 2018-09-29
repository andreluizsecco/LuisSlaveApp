using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Linq;
using System.Drawing;

namespace DesktopApp
{
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            var cmd = GetCommand(MessageTextBox.Text);
            switch (cmd.Name.Value)
            {
                case "conversation.about":
                    MessageBox.Show("Olá! Estou aqui para fazer o que me pede. Deseja algo?");
                    break;
                case "command.openCalc":
                    Open("calc");
                    break;
                case "command.openNotepad":
                    Open("notepad");
                    break;
                case "command.backgroundColor":
                    SetBackgroundColor(cmd.Parameter.Value);
                    break;
                case "command.allOff":
                    ChangeLights(LightCommand.AllOff);
                    break;
                case "command.allOn":
                    ChangeLights(LightCommand.AllOn);
                    break;
                case "command.allOnGreen":
                    ChangeLights(LightCommand.AllOnGreen);
                    break;
                case "command.leftOn":
                    ChangeLights(LightCommand.LeftOn);
                    break;
                case "command.rightOn":
                    ChangeLights(LightCommand.RightOn);
                    break;
                default:
                    MessageBox.Show("Não entendi!");
                    break;
            }
        }

        private dynamic GetCommand(string message)
        {
            var client = new RestClient($"https://luisslaveapp.azurewebsites.net/api/Messages/GetCommand?Message={MessageTextBox.Text}");
            var request = new RestRequest(Method.POST);
            IRestResponse response = client.Execute(request);
            return (dynamic)JObject.Parse(response.Content);
        }

        private void Open(string program)
        {
            Process.Start(program);
        }

        private void Close(string program)
        {
            Process.GetProcessesByName(program).ToList().ForEach(process =>
            {
                process.Close();
            });
        }

        private void SetBackgroundColor(string color)
        {
            try
            {
                this.BackColor = System.Drawing.ColorTranslator.FromHtml(color);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não entendi de qual cor está falando!");
            }
        }

        private void ChangeLights(LightCommand cmd)
        {
            Bitmap img = null;
            switch(cmd)
            {
                case LightCommand.AllOff:
                    img = Properties.Resources.all_off;
                    break;
                case LightCommand.AllOn:
                    img = Properties.Resources.all_on;
                    break;
                case LightCommand.AllOnGreen:
                    img = Properties.Resources.all_on_green;
                    break;
                case LightCommand.RightOn:
                    img = Properties.Resources.right_on;
                    break;
                case LightCommand.LeftOn:
                    img = Properties.Resources.left_on;
                    break;
            }
            pictureBox.Image = img;
        }

        enum LightCommand
        {
            AllOff,
            AllOn,
            AllOnGreen,
            LeftOn,
            RightOn
        }
    }
}
