using Nipper.DataManager.ApiClients;
using Nipper.DataManager.Models;
using Nipper.DataManager.Utilities;

namespace Nipper.Desktop
{
    public partial class NipperForm : Form
    {
        private readonly NipValidator validator = new();
        private readonly SettingsManager settingsManager = new();
        private Dictionary<string, string> nipResultDict = new();
        public NipperForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GenerateXlsxFile.Visible = false;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(userNips.Text))
            {
                return;
            }
            // GUI init
            checkNipsButton.Enabled = false;
            GenerateXlsxFile.Visible = false;
            nipCheckProgressBar.Visible = true;
            nipCheckProgressBar.Value = 0;
            outputBox.Text = "";

            nipResultDict.Clear();
            string[] nipsArray = userNips.Text.Split(["\n", "\r", " "], StringSplitOptions.RemoveEmptyEntries);
            int processedNips = 0;
            await foreach (var info in validator.CheckNipsAsync(nipsArray))
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
            GenerateXlsxFile.Visible = true;
        }

        private void wybierzFolderNaPlikiXlsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new())
            {
                folderDialog.Description = "Wybierz folder do zapisu plików xls z wynikami.";
                folderDialog.ShowNewFolderButton = true;
                string? currentPath = settingsManager.GetXlsSavePath();
                if (!string.IsNullOrEmpty(currentPath))
                {
                    folderDialog.InitialDirectory = currentPath;
                }

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    settingsManager.SetXlsSavePath(folderDialog.SelectedPath);
                }
            }
        }

        private void GenerateXlsxFile_Click(object sender, EventArgs e)
        {
            string? path = settingsManager.GetXlsSavePath();
            if (string.IsNullOrEmpty(path))
            {
                MessageBox.Show("Wybierz folder do zapisu wygenerowanych plików w ustawianiach");
                return;
            }
            XlsCreator.CreateXlsxFile(nipResultDict, path);
            MessageBox.Show("Wygenerowano plik");
            GenerateXlsxFile.Visible = false;
        }
    }
}
