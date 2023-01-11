using Newtonsoft.Json;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace Realism_Mod_Config_GUI
{
    public partial class Main_Form : Form
    {

        public static string ConfigFilePath = Path.Combine(Path.GetDirectoryName(Environment.ProcessPath), @"config\config.json");
        public static string ConfigJSON = File.ReadAllText(ConfigFilePath);
        public static ConfigTemplate Config = JsonConvert.DeserializeObject<ConfigTemplate>(ConfigJSON);

        public static string weapFilePath = Path.Combine(Path.GetDirectoryName(Environment.ProcessPath), @"db/templates/weapons/");
        public static DirectoryInfo weapDI = new DirectoryInfo(weapFilePath);
        public static DirectoryInfo[] weapPresetFilePath = weapDI.GetDirectories();

        public static string attFilePath = Path.Combine(Path.GetDirectoryName(Environment.ProcessPath), @"db/templates/attatchments/");
        public static DirectoryInfo attDI = new DirectoryInfo(attFilePath);
        public static DirectoryInfo[] attPresetFilePath = attDI.GetDirectories();




        public Main_Form()
        {
            InitializeComponent();


            setTitleBar();
            setNumericLimits();
            SetPresetComboBoxes(weapPresetFilePath, weapPresetCombo);
            SetPresetComboBoxes(attPresetFilePath, attachPresetCombo);

            try
            {
                warningTextBox.Hide();
                SetDisplayValues();
            }
            catch
            {
                warningTextBox.Show();
                warningTextBox.Text = $"config.json not found at file path: {Path.Combine(Path.GetDirectoryName(Environment.ProcessPath))}\\config\\ or specified preset wasn't found!";
            }
        }

        private void setTitleBar() 
        {
            string modVer = "v1.0.0";
            string sptVer = "v3.4.1";

            this.Text = "SPTRM Recoil & Attachment Ovherual Standalone " + modVer + " SPT " + sptVer;

        }

        private void setNumericLimits() 
        {
            decimal recoilMultiMin = 0.1m;
            decimal recoilMultiMax = 10.0m;
            decimal recoilMultiIncrement = 0.1m;
            int decimalPlaces = 2;

            procNumeric.Minimum = 0.1m;
            procNumeric.Maximum = 2.0m;
            procNumeric.Increment = 0.05m;
            procNumeric.DecimalPlaces= decimalPlaces;

            vertRecNumeric.Minimum = recoilMultiMin;
            vertRecNumeric.Maximum = recoilMultiMax;
            vertRecNumeric.Increment = recoilMultiIncrement;
            vertRecNumeric.DecimalPlaces = decimalPlaces;

            horzRecNumeric.Minimum = recoilMultiMin;
            horzRecNumeric.Maximum = recoilMultiMax;
            horzRecNumeric.Increment = recoilMultiIncrement;
            horzRecNumeric.DecimalPlaces = decimalPlaces;

            convNumeric.Minimum = recoilMultiMin;
            convNumeric.Maximum = recoilMultiMax;
            convNumeric.Increment = recoilMultiIncrement;
            convNumeric.DecimalPlaces = decimalPlaces;

            dispNumeric.Minimum = recoilMultiMin;
            dispNumeric.Maximum = recoilMultiMax;
            dispNumeric.Increment = recoilMultiIncrement;
            dispNumeric.DecimalPlaces = decimalPlaces;

            ergoNumeric.Minimum = recoilMultiMin;
            ergoNumeric.Maximum = recoilMultiMax;
            ergoNumeric.Increment = recoilMultiIncrement;
            ergoNumeric.DecimalPlaces = decimalPlaces;
        }

        private void SetPresetComboBoxes(DirectoryInfo[] dirInfoArr, ComboBox cb) 
        {
            foreach (DirectoryInfo dir in dirInfoArr)
            {
                cb.Items.Add(dir.Name);
            }
        }

        private void SetDefaultValues()
        {
      
            procNumeric.Value = 1.0m;
            vertRecNumeric.Value = 1.0m;
            horzRecNumeric.Value = 1.0m;
            convNumeric.Value = 1.0m;
            dispNumeric.Value = 1.0m;
            ergoNumeric.Value = 1.0m;

            crankCheck.Checked = true;

            weapPresetCombo.SelectedItem = "Default";
            attachPresetCombo.SelectedItem = "Default";

        }

        private void SetDisplayValues()
        {
            procNumeric.Value = (decimal)Config.procedural_intensity;
            vertRecNumeric.Value = (decimal)Config.vert_recoil_multi;
            horzRecNumeric.Value = (decimal)Config.horz_recoil_multi;
            convNumeric.Value = (decimal)Config.convergence_multi;
            dispNumeric.Value = (decimal)Config.dispersion_multi;
            ergoNumeric.Value = (decimal)Config.ergo_multi;

            crankCheck.Checked = Config.recoil_crank;

            weapPresetCombo.SelectedItem = Config.weap_preset;
            attachPresetCombo.SelectedItem = Config.att_preset;
        }

        public void Timer(Label label) 
        {
            var t = new Timer();
            t.Interval = 700;
            t.Tick += (s, e) =>
            {
                label.ForeColor = label.BackColor;
                t.Stop();
            };
            t.Start();
        }

        public class ConfigTemplate
        {
            public string weap_preset { get; set; }
            public string att_preset { get; set; }
            public bool recoil_crank { get; set; }
            public decimal procedural_intensity { get; set;}
            public decimal vert_recoil_multi { get; set; }
            public decimal horz_recoil_multi { get; set; }
            public decimal convergence_multi { get; set; }
            public decimal dispersion_multi { get; set; }
            public decimal ergo_multi { get; set; }

        }

        private void weapPresetCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Config.weap_preset = weapPresetCombo.SelectedItem.ToString();
        }

        private void attachPresetCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Config.att_preset = attachPresetCombo.SelectedItem.ToString();    
        }

        private void vertRecNumeric_ValueChanged(object sender, EventArgs e)
        {
            Config.vert_recoil_multi = vertRecNumeric.Value;
        }

        private void horzRecNumeric_ValueChanged(object sender, EventArgs e)
        {
            Config.horz_recoil_multi = horzRecNumeric.Value;
        }

        private void convNumeric_ValueChanged(object sender, EventArgs e)
        {
            Config.convergence_multi = convNumeric.Value;
        }

        private void dispNumeric_ValueChanged(object sender, EventArgs e)
        {
            Config.dispersion_multi = dispNumeric.Value;
        }

        private void ergoNumeric_ValueChanged(object sender, EventArgs e)
        {
            Config.ergo_multi = ergoNumeric.Value;
        }

        private void procNumeric_ValueChanged(object sender, EventArgs e)
        {
            Config.procedural_intensity = procNumeric.Value;
        }

        private void crankCheck_CheckedChanged(object sender, EventArgs e)
        {
            Config.recoil_crank = crankCheck.Checked == true ? true : false;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            File.WriteAllText(ConfigFilePath, JsonConvert.SerializeObject(Config));
            savedLabel.ForeColor = Color.GreenYellow;
            Timer(savedLabel);
        }
        private void revertButton_Click(object sender, EventArgs e)
        {
            SetDefaultValues();
            SetDisplayValues();
            File.WriteAllText(ConfigFilePath, JsonConvert.SerializeObject(Config));
            revertLabel.ForeColor = Color.DarkOrange;
            Timer(revertLabel);
        }
    }
}