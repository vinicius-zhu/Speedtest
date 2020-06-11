using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using IWshRuntimeLibrary;
using Microsoft.Win32;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using File = System.IO.File;
using ThreadState = System.Threading.ThreadState;

namespace SpeedtestCore
{
    public partial class BBLTest : Form
    {
        private Settings ControlSettings { get; }
        private ChromeDriverService SeleniumChromeService { get; }
        private ChromeOptions SeleniumChromeOptions { get; }
        private ChromeDriver SeleniumChromeDriver { get; set; }
        private System.Windows.Forms.Timer TestTimer { get; }
        private Int32 TestTimerInterval { get; set; }
        private Thread TestTimerMonitor { get; set; }
        private Thread RunnerThread { get; set; }
        private Boolean Stopping { get; set; }
        private delegate void DelControlBoolean(Control c, Boolean b);

        public Image GetImageAssembly(String resource)
        {
            Assembly assembly = Assembly.GetEntryAssembly();
            Stream resStream = assembly.GetManifestResourceStream("SpeedtestCore." + resource);
            return Image.FromStream(resStream);
        }

        public byte[] GetTextAssembly(String resource)
        {
            Assembly assembly = Assembly.GetEntryAssembly();
            Stream resStream = assembly.GetManifestResourceStream("SpeedtestCore." + resource);
            List<Byte> array = new List<byte>();
            if (resStream != null)
            {
                int data = resStream.ReadByte();

                while (data != -1)
                {
                    array.Add((byte) data);
                    data = resStream.ReadByte();
                }
            }

            return array.ToArray();
        }

        public BBLTest()
        {
            InitializeComponent();
            Icon = Icon.FromHandle(((Bitmap)GetImageAssembly("speedometer.png")).GetHicon());
            notifyIcon.Icon = Icon;
            SeleniumChromeService = ChromeDriverService.CreateDefaultService();
            SeleniumChromeService.HideCommandPromptWindow = true;
            SeleniumChromeOptions = new ChromeOptions();
            SeleniumChromeOptions.AddArgument("headless");
            RunnerThread = new Thread(ThreadRun);
            TestTimerMonitor = new Thread(RestartTimer);
            TestTimer = new System.Windows.Forms.Timer();
            checkBoxAutoStart.Visible = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

                EventArgs ea = new EventArgs();
            if (File.Exists(Path.Combine(Application.StartupPath, "settings.xml")))
            {
                FileStream fs = new FileStream(Path.Combine(Application.StartupPath, "settings.xml"), FileMode.Open);
                BinaryFormatter binaryFmt = new BinaryFormatter();
                try
                {
                    ControlSettings = (Settings)binaryFmt.Deserialize(fs);
                    textBoxSavePath.Text = ControlSettings.SaveFolder;
                    numericUpDownInterval.Value = ControlSettings.Interval;
                    checkBoxAutoTest.Checked = ControlSettings.AutoTest;
                    checkBoxAutoStart.Checked = ControlSettings.AutoStart;
                }
                catch 
                {
                    
                    ControlSettings = new Settings();
                    textBoxSavePath_TextChanged(this, ea);
                    numericUpDownInterval_ValueChanged(this, ea);
                    checkBoxAutoTest_CheckedChanged(this, ea);
                    checkBoxAutoStart_CheckedChanged(this, ea);
                }

                fs.Close();
            }
            else
            {
                ControlSettings = new Settings();
                textBoxSavePath_TextChanged(this, ea);
                numericUpDownInterval_ValueChanged(this, ea);
                checkBoxAutoTest_CheckedChanged(this, ea);
                checkBoxAutoStart_CheckedChanged(this, ea);
            }

            if (String.IsNullOrEmpty(textBoxSavePath.Text))
            {
                textBoxSavePath.Text = Application.StartupPath;
            }

            if (checkBoxAutoTest.Checked)
            {
                buttonStartDaemon_Click(this, new EventArgs());
            }
        }

        private void RunSpeedtestCore()
        {
            Stopping = false;
            SeleniumChromeDriver = new ChromeDriver(SeleniumChromeService, SeleniumChromeOptions);
            for (int count = 0; count < 3; count++)
            {
                SeleniumChromeDriver.Url = "https://www.brasilbandalarga.com.br/bbl/";
                IWebElement button = SeleniumChromeDriver.FindElementById("btnIniciar");
                button.Click();
                try
                {
                    while (button.Text.ToUpper() != "INICIAR NOVO TESTE" && !Stopping)
                    {
                        Thread.Sleep(1000);
                    }
                }
                catch
                {
                    Stopping = true;
                }

                if (!Stopping)
                {
                    IWebElement medicao = SeleniumChromeDriver.FindElementById("medicao");
                    List<IWebElement> textos = new List<IWebElement>(medicao.FindElements(By.ClassName("textao")));
                    String download = textos[0].Text;
                    String upload = textos[1].Text;

                    IWebElement stats = medicao.FindElement(By.ClassName("text-left"));

                    List<IWebElement> rows = new List<IWebElement>(stats.FindElements(By.ClassName("row")));
                    Dictionary<String, String> testdata = new Dictionary<string, string>();
                    foreach (IWebElement element in rows)
                    {
                        if (!String.IsNullOrEmpty(element.Text))
                        {
                            try
                            {
                                List<String> separated = new List<string>(element.Text.Split(new[] {"\r\n"},
                                    StringSplitOptions.RemoveEmptyEntries));
                                if (separated.Count == 1)
                                {
                                    separated.Add("-");
                                }

                                if (separated.Count == 2)
                                {
                                    testdata.Add(separated[0].Trim(), separated[1].Trim());
                                }
                            }
                            catch
                            {
                            }
                        }
                    }

                    List<IWebElement> topstats =
                        new List<IWebElement>(medicao.FindElements(By.ClassName("text-center")));
                    String provider = topstats[0].Text.Trim();
                    String timestamp = topstats[1].Text.Trim();

                    Screenshot ss = SeleniumChromeDriver.GetScreenshot();

                    StreamWriter sw;
                    String logfilepath = Path.Combine(ControlSettings.SaveFolder, "log.csv");
                    String excelfilepath = Path.Combine(ControlSettings.SaveFolder, "Status.xlsm");

                    if (File.Exists(logfilepath))
                    {
                        sw = new StreamWriter(logfilepath, true, Encoding.UTF8);
                    }
                    else
                    {
                        sw = new StreamWriter(logfilepath, false, Encoding.UTF8);
                        sw.WriteLine(
                            "Timespan;Date;Time;Download;Upload;Latencia;Jitter;Perda;IP;Região Servidor;Região Teste;Operador");
                    }

                    if (!File.Exists(excelfilepath))
                    {
                        File.WriteAllBytes(excelfilepath, GetTextAssembly("Status.xlsm"));
                    }

                    DateTime today = DateTime.Parse(timestamp);
                    TimeSpan now = today.TimeOfDay;
                    String date = today.Year.ToString("D4") + "-" + today.Month.ToString("D2") + "-" +
                                  today.Day.ToString("D2");
                    String time = now.Hours.ToString("D2") + ":" + now.Minutes.ToString("D2");
                    sw.WriteLine(date + " " + time + ";" +
                                 date + ";" +
                                 time + ";" +
                                 download + ";" +
                                 upload + ";" +
                                 testdata["Latência"] + ";" +
                                 testdata["Jitter"] + ";" +
                                 testdata["Perda"] + ";" +
                                 testdata["IP"] + ";" +
                                 testdata["Região Servidor"] + ";" +
                                 testdata["Região Teste"] + ";" +
                                 provider
                    );
                    ss.SaveAsFile(Path.Combine(ControlSettings.SaveFolder,
                        date.Replace("-", "") + time.Replace(":", "") + ".jpg"));
                    sw.Close();
                    File.SetAttributes(logfilepath, File.GetAttributes(logfilepath) | FileAttributes.Hidden);
                }
            }

            SeleniumChromeDriver.Quit();
        }

        private void ThreadRun()
        {
            RunSpeedtestCore();
            ChangeControlStatus(buttonRunTest, true);
        }

        private void RestartTimer()
        {
            while (TestTimer.Enabled)
            {
                Thread.Sleep(10000);
            }
            ChangeControlStatus(buttonStartDaemon, false);
            TestTimer.Start();
        }

        private void ChangeControlStatus(Control control, Boolean status)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new DelControlBoolean(ChangeControlStatus), control, status);
            }
            else
            {
                control.Enabled = status;
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
            this.ShowInTaskbar = true;
        }

        private void buttonRunTest_Click(object sender, EventArgs e)
        {
            ChangeControlStatus(buttonRunTest, false);
            if (RunnerThread.ThreadState != ThreadState.Running)
            {
                RunnerThread = new Thread(ThreadRun);
                RunnerThread.Start();
            }
        }

        private void buttonStartDaemon_Click(object sender, EventArgs e)
        {
            try
            {
                if (TestTimer.Enabled)
                {
                    TestTimer.Stop();
                }

                TestTimer.Tick -= Timer_Tick;
            }
            catch
            {
                // ignored
            }

            TestTimerInterval = Convert.ToInt32(numericUpDownInterval.Value);
            TestTimer.Interval = TestTimerInterval * 60 * 1000;
            TestTimer.Tick += Timer_Tick;
            TestTimer.Start();
            buttonRunTest_Click(sender,e);
            ChangeControlStatus(buttonStartDaemon, false);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            buttonRunTest_Click(sender,e);
            if (TestTimerMonitor.ThreadState != ThreadState.Running)
            {
                TestTimerMonitor = new Thread(RestartTimer);
                TestTimerMonitor.Start();
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            Stopping = true;
            try
            {
                if (TestTimerMonitor.ThreadState == ThreadState.Running)
                {
                    TestTimerMonitor.Abort();
                }

                if (TestTimer.Enabled)
                {
                    TestTimer.Stop();
                }

                if (RunnerThread.ThreadState == ThreadState.Running)
                {
                    RunnerThread.Abort();
                }

                ChangeControlStatus(buttonRunTest, true);
                ChangeControlStatus(buttonStartDaemon, true);

                if (SeleniumChromeDriver != null)
                {
                    SeleniumChromeDriver.Quit();
                }
            }
            catch
            {
                // ignored
            }
        }

        private void BBLTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            buttonStop_Click(sender, e);
            //if (File.Exists(Path.Combine(Application.StartupPath, "settings.xml")))
            {
                FileStream fs = new FileStream(Path.Combine(Application.StartupPath, "settings.xml"), FileMode.OpenOrCreate);
                BinaryFormatter binaryFmt = new BinaryFormatter();
                try
                {
                    binaryFmt.Serialize(fs, ControlSettings);
                }
                catch
                {
                    // ignored
                }

                fs.Close();
            }
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog() { SelectedPath = String.IsNullOrEmpty(textBoxSavePath.Text) ? Application.StartupPath : textBoxSavePath.Text};
            DialogResult dr = dialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                textBoxSavePath.Text = dialog.SelectedPath;
            }
        }

        private void textBoxSavePath_TextChanged(object sender, EventArgs e)
        {
            ControlSettings.SaveFolder = textBoxSavePath.Text;
        }

        private void numericUpDownInterval_ValueChanged(object sender, EventArgs e)
        {
            ControlSettings.Interval = (Int32)numericUpDownInterval.Value;
        }

        private void checkBoxAutoTest_CheckedChanged(object sender, EventArgs e)
        {
            ControlSettings.AutoTest = checkBoxAutoTest.Checked;
        }

        private void checkBoxAutoStart_CheckedChanged(object sender, EventArgs e)
        {
            ControlSettings.AutoStart = checkBoxAutoStart.Checked;
            String AppName = Assembly.GetExecutingAssembly().GetName().Name;

            try
            {
                string startupDir = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
                string path = Path.Combine(startupDir,Assembly.GetExecutingAssembly().GetName().Name.Replace(" ", "") + ".lnk");
                if (ControlSettings.AutoStart)
                {
                    if (!File.Exists(path))
                    {
                        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                        {
                            WshShell shell = new WshShell();
                            var windowsApplicationShortcut = (IWshShortcut) shell.CreateShortcut(path);
                            windowsApplicationShortcut.Description = "SpeedtestCore from Brasil Banda Larga";
                            windowsApplicationShortcut.WorkingDirectory = Application.StartupPath;
                            windowsApplicationShortcut.TargetPath = Path.Combine(Application.StartupPath, Application.ProductName) + ".exe";
                            
                            windowsApplicationShortcut.Save();
                        }

                    }
                }
                else
                {
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    {
                        if (File.Exists(path))
                        {
                            File.Delete(path);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                try
                {
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    {
                        RegistryKey rk =
                            Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

                        if (ControlSettings.AutoStart)
                        {
                            rk.SetValue(AppName, Application.ExecutablePath);
                        }
                        else
                        {
                            rk.DeleteValue(AppName, false);
                        }
                    }
                }
                catch (Exception ex1)
                {
                    MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show(ex1.Message, ex1.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BBLTest_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon.Visible = true;
                this.ShowInTaskbar = false;
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
            this.ShowInTaskbar = true;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buttonStop_Click(sender, e);
            Close();
        }

        private void folderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(ControlSettings.SaveFolder))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    Arguments = ControlSettings.SaveFolder,
                    FileName = "explorer.exe"
                };

                Process.Start(startInfo);
            }
            else
            {
                MessageBox.Show(string.Format("Directory not configured!"));
            }
        }
    }
}
