using Nipper.DataManager.ApiClients;
using Nipper.DataManager.Models;
using Nipper.DataManager.Utilities;

namespace Nipper.Desktop
{
    public partial class NipperForm : Form
    {
        private readonly NipValidator validator = new();
        public NipperForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            // GUI init
            checkNipsButton.Enabled = false;
            nipCheckProgressBar.Visible = true;
            nipCheckProgressBar.Value = 0;
            outputBox.Text = "";

            Dictionary<string, string> nipResultDict = new();
            string[] nipsArray = userNips.Text.Split(["\n", "\r", " "], StringSplitOptions.RemoveEmptyEntries);
            int processedNips = 0;
            await foreach(var info in validator.CheckNipsAsync(nipsArray))
            {
                if (nipResultDict.ContainsKey(info.Nip))
                {
                    nipCheckProgressBar.Value = processedNips * nipCheckProgressBar.Maximum / (nipsArray.Length - 1);
                    processedNips++;
                    continue;
                }
                else if (string.IsNullOrEmpty(info.ErrorMesage) && !string.IsNullOrEmpty(info.CompanyName))
                {
                    nipResultDict.Add(info.Nip, info.CompanyName);
                }
                else if (!string.IsNullOrEmpty(info.ErrorMesage) && string.IsNullOrEmpty(info.CompanyName))
                {
                    nipResultDict.Add(info.Nip, info.ErrorMesage);
                }
                else
                {
                    nipResultDict.Add(info.Nip, "Wyst¹pi³ niezidentyfikowany problem");
                }
                outputBox.Text += $"{info.Nip} - {nipResultDict[info.Nip]}\r\n";
                if (nipsArray.Length > 1)
                    nipCheckProgressBar.Value = processedNips * nipCheckProgressBar.Maximum / (nipsArray.Length - 1);
                processedNips++;
            }
            

            checkNipsButton.Enabled = true;
            nipCheckProgressBar.Visible = false;
            MessageBox.Show("Sprawdzanie zakoñczone");
        }
    }
}
