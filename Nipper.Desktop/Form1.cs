using Nipper.DataManager.Clients;
using Nipper.DataManager.Models;

namespace Nipper.Desktop
{
    public partial class NipperForm : Form
    {
        private static readonly WlApiClient client = new WlApiClient();
        public NipperForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            checkNipsButton.Enabled = false;
            nipCheckProgressBar.Visible = true;
            nipCheckProgressBar.Value = 0;
            outputBox.Text = "";
            Dictionary<string, string> nipResultDict = new();
            string[] nipsArray = userNips.Text.Split(["\n", "\r", " "], StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < nipsArray.Length; i++)
            {
                if (i % 20 == 0 && i != 0)
                    await Task.Delay(3000);
                var response = await client.CheckNip(nipsArray[i]);
                nipCheckProgressBar.Value = i * nipCheckProgressBar.Maximum / (nipsArray.Length - 1);

                switch (response)
                {
                    case EntityResponse entityResponse:
                        if (entityResponse.Result.Subject == null)
                        {
                            nipResultDict[nipsArray[i]] = "Firma o podanym nipie nie istnieje";
                            break;
                        }
                        nipResultDict[nipsArray[i]] = entityResponse.Result.Subject.Name;
                        break;
                    case ExceptionResponse exceptionResponse:
                        nipResultDict[nipsArray[i]] =
                            $"Wyst¹pi³ b³¹d o kodzie {exceptionResponse.Code}: {exceptionResponse.Message}";                     
                        break;
                    default:
                        nipResultDict[nipsArray[i]] = "Niezidentyfikowana akcja";
                        break;
                }
                outputBox.Text += $"{nipsArray[i]} - {nipResultDict[nipsArray[i]]}\r\n";
            }

            checkNipsButton.Enabled = true;
            nipCheckProgressBar.Visible = false;
            MessageBox.Show("Sprawdzanie zakoñczone");
        }
    }
}
