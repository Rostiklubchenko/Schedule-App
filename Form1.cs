using System;
using System.Diagnostics;
using System.Globalization;
using System.Drawing;
using System.Windows.Forms;

namespace Schedule
{
    public partial class Form1 : Form
    {
        private readonly DateTime currentDate;
        DayOfWeek dayOfWeek;
        List<Pair> pair;
        readonly ProcessStartInfo psi;

        private NotifyIcon notifyIcon1;
        private ContextMenuStrip contextMenu1;
        private readonly ToolStripDropDownMenu Menu;

        public Form1()
        {
            InitializeComponent();

            ToolStripMenuItem menuItem1 = new ToolStripMenuItem("Open");
            menuItem1.Click += MenuItem1_Click;
            ToolStripMenuItem menuItem2 = new ToolStripMenuItem("Exit");
            menuItem2.Click += MenuItem2_Click;

            Menu = new ToolStripDropDownMenu();

            contextMenu1 = new ContextMenuStrip();
            contextMenu1.Items.AddRange(new ToolStripMenuItem[] { menuItem1, menuItem2 });

            notifyIcon1 = new NotifyIcon();
            notifyIcon1.ContextMenuStrip = contextMenu1;
            notifyIcon1.DoubleClick += NotifyIcon1_DoubleClick;
            notifyIcon1.Icon = new Icon("D:/Study/123/WinFormsApp1/icon.ico");
            notifyIcon1.Text = "Розклад";
            notifyIcon1.Visible = true;

            currentDate = DateTime.Now;
            dayOfWeek = currentDate.DayOfWeek;

            psi = new()
            {
                UseShellExecute = true
            };

            pair = new List<Pair>();

            Update();

            int weekNumber = GetIso8601WeekOfYear(currentDate);
            bool isOddWeek = weekNumber % 2 != 0;
            if (isOddWeek) infoLabel.Text += ", верхній тиждень";
            else infoLabel.Text += ", нижній тиждень";

            System.Timers.Timer timer = new()
            {
                Interval = 1000
            };
            timer.Elapsed += (sender, e) =>
            {
                DateTime currentTime = DateTime.Now;
                //foreach (Pair pr in pair)
                //{
                //    if (currentTime.Hour == pr.hourPair && currentTime.Minute == pr.minutePair &&
                //    pr.Started == false && autoCheck.Checked)
                //    {
                //        OpenURL(pr.URL);
                //        pr.Started = true;
                //    }
                //}
                if (DateTime.Now.DayOfWeek != dayOfWeek)
                {
                    dayOfWeek = DateTime.Now.DayOfWeek;
                    Update();
                }
                currentPair();
            };
            timer.Start();
        }

        public void currentPair()
        {
            DateTime current = DateTime.Now;
            int hour = current.Hour;
            int minute = current.Minute;

            if ((hour > pair[0].hourPair || (hour == pair[0].hourPair && minute >= pair[0].minutePair))
                && (hour < pair[1].hourPair || (hour == pair[1].hourPair && minute < pair[1].minutePair)))
            {
                panel1.BackColor = Color.Silver;
                panel2.BackColor = Color.White;
                panel3.BackColor = Color.White;
                panel4.BackColor = Color.White;
                panel5.BackColor = Color.White;

                if (pair[0].Started == false && autoCheck.Checked)
                {
                    OpenURL(pair[0].URL);
                    pair[0].Started = true;
                }
            }
            else if ((hour > pair[1].hourPair || (hour == pair[1].hourPair && minute >= pair[1].minutePair))
                && (hour < pair[2].hourPair || (hour == pair[2].hourPair && minute < pair[2].minutePair)))
            {
                panel1.BackColor = Color.White;
                panel2.BackColor = Color.Silver;
                panel3.BackColor = Color.White;
                panel4.BackColor = Color.White;
                panel5.BackColor = Color.White;

                if (pair[1].Started == false && autoCheck.Checked)
                {
                    OpenURL(pair[1].URL);
                    pair[1].Started = true;
                }
            }
            else if ((hour > pair[2].hourPair || (hour == pair[2].hourPair && minute >= pair[2].minutePair))
                && (hour < pair[3].hourPair || (hour == pair[3].hourPair && minute < pair[3].minutePair)))
            {
                panel1.BackColor = Color.White;
                panel2.BackColor = Color.White;
                panel3.BackColor = Color.Silver;
                panel4.BackColor = Color.White;
                panel5.BackColor = Color.White;

                if (pair[2].Started == false && autoCheck.Checked)
                {
                    OpenURL(pair[2].URL);
                    pair[2].Started = true;
                }
            }
            else if ((hour > pair[3].hourPair || (hour == pair[3].hourPair && minute >= pair[3].minutePair))
                && (hour < pair[4].hourPair || (hour == pair[4].hourPair && minute < pair[4].minutePair)))
            {
                panel1.BackColor = Color.White;
                panel2.BackColor = Color.White;
                panel3.BackColor = Color.White;
                panel4.BackColor = Color.Silver;
                panel5.BackColor = Color.White;

                if (pair[3].Started == false && autoCheck.Checked)
                {
                    OpenURL(pair[3].URL);
                    pair[3].Started = true;
                }
            }
            else if ((hour > pair[4].hourPair || (hour == pair[4].hourPair && minute >= pair[4].minutePair))
                && (hour < 16 || (hour == 16 && minute < 30)))
            {
                panel1.BackColor = Color.White;
                panel2.BackColor = Color.White;
                panel3.BackColor = Color.White;
                panel4.BackColor = Color.White;
                panel5.BackColor = Color.Silver;

                if (pair[4].Started == false && autoCheck.Checked)
                {
                    OpenURL(pair[4].URL);
                    pair[4].Started = true;
                }
            }
            else if (hour >= 16)
            {
                panel1.BackColor = Color.White;
                panel2.BackColor = Color.White;
                panel3.BackColor = Color.White;
                panel4.BackColor = Color.White;
                panel5.BackColor = Color.White;
            }

        }

        public void Update()
        {
            pair.Clear();
            if (dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday)
            {
                for (int i = 0; i < 5; i++) pair.Add(new Pair()
                {
                    dayOfWeekPair = dayOfWeek,
                    namePair = "",
                    teacherPair = "",
                    URL = "",
                    numPair = i + 1
                });
                greetingLabel.Text = "Добрий день, гарних вихідних";
            }
            else pair = Read();

            greetingLabel.Text = "Добрий день, ваш розклад на сьогодні";

            pair[0].hourPair = 9;
            pair[0].minutePair = 0;

            pair[1].hourPair = 10;
            pair[1].minutePair = 30;

            pair[2].hourPair = 12;
            pair[2].minutePair = 10;

            pair[3].hourPair = 13;
            pair[3].minutePair = 40;

            pair[4].hourPair = 15;
            pair[4].minutePair = 10;

            labelPair1.Text = pair[0].namePair;
            labelPair2.Text = pair[1].namePair;
            labelPair3.Text = pair[2].namePair;
            labelPair4.Text = pair[3].namePair;
            labelPair5.Text = pair[4].namePair;

            labelTeacher1.Text = pair[0].teacherPair;
            labelTeacher2.Text = pair[1].teacherPair;
            labelTeacher3.Text = pair[2].teacherPair;
            labelTeacher4.Text = pair[3].teacherPair;
            labelTeacher5.Text = pair[4].teacherPair;

            switch (dayOfWeek)
            {
                case DayOfWeek.Monday: infoLabel.Text = "пн, "; break;
                case DayOfWeek.Tuesday: infoLabel.Text = "вт, "; break;
                case DayOfWeek.Wednesday: infoLabel.Text = "ср, "; break;
                case DayOfWeek.Thursday: infoLabel.Text = "чт, "; break;
                case DayOfWeek.Friday: infoLabel.Text = "пт, "; break;
                case DayOfWeek.Saturday: infoLabel.Text = "сб, "; break;
                case DayOfWeek.Sunday: infoLabel.Text = "нд, "; break;
            }

            switch (currentDate.Month)
            {
                case 1: infoLabel.Text += $"Січень, {currentDate.Day}"; break;
                case 2: infoLabel.Text += $"Лютий, {currentDate.Day}"; break;
                case 3: infoLabel.Text += $"Березень, {currentDate.Day}"; break;
                case 4: infoLabel.Text += $"Квітень, {currentDate.Day}"; break;
                case 5: infoLabel.Text += $"Травень, {currentDate.Day}"; break;
                case 6: infoLabel.Text += $"Червень, {currentDate.Day}"; break;
                case 7: infoLabel.Text += $"Липень, {currentDate.Day}"; break;
                case 8: infoLabel.Text += $"Серпень, {currentDate.Day}"; break;
                case 9: infoLabel.Text += $"Вересень, {currentDate.Day}"; break;
                case 10: infoLabel.Text += $"Жовтень, {currentDate.Day}"; break;
                case 11: infoLabel.Text += $"Листопад, {currentDate.Day}"; break;
                case 12: infoLabel.Text += $"Грудень, {currentDate.Day}"; break;
            }
        }

        private void NotifyIcon1_DoubleClick(object? sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Normal;
            }
            else
            {
                if (Visible) Hide();
                else Show();
            }
        }

        private void MenuItem2_Click(object? sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        private void MenuItem1_Click(object? sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
                WindowState = FormWindowState.Normal;
            Show();
        }

        static int GetIso8601WeekOfYear(DateTime date)
        {
            var cal = DateTimeFormatInfo.CurrentInfo.Calendar;
            int week = cal.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            return week;
        }

        public List<Pair> Read()
        {
            DateTime currentDate = DateTime.Now;
            dayOfWeek = currentDate.DayOfWeek;
            List<Pair> pairs = new List<Pair>();
            string filePath;
            int weekNumber = GetIso8601WeekOfYear(currentDate);
            bool isOddWeek = weekNumber % 2 != 0;
            if (isOddWeek) filePath = "D:/Study/123/WinFormsApp1/scheduleUP.txt";
            else filePath = "D:/Study/123/WinFormsApp1/scheduleDOWN.txt";
            try
            {
                string[] lines = File.ReadAllLines(filePath);

                DayOfWeek day = DayOfWeek.Monday;
                int pair = 0;
                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];
                    if (pair == 5)
                    {
                        day++;
                        pair = 0;
                        continue;
                    }
                    if (day == dayOfWeek)
                    {
                        string[] parts = line.Split('|');
                        pairs.Add(new Pair
                        {
                            dayOfWeekPair = day,
                            namePair = parts[0],
                            teacherPair = parts[1],
                            URL = parts[2],
                            numPair = pair
                        });
                    }
                    pair++;
                }
            }
            catch (Exception) { }
            return pairs;
        }

        void OpenURL(string url = null)
        {

            if (url == null || url == "") return;
            psi.FileName = url;
            Process.Start(psi);
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            OpenURL(pair[0].URL);
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            OpenURL(pair[1].URL);
        }
        private void panel3_Click(object sender, EventArgs e)
        {
            OpenURL(pair[2].URL);
        }
        private void panel4_Click(object sender, EventArgs e)
        {
            OpenURL(pair[3].URL);
        }
        private void panel5_Click(object sender, EventArgs e)
        {
            OpenURL(pair[4].URL);
        }



        private void labelPair1_Click(object sender, EventArgs e)
        {
            OpenURL(pair[0].URL);
        }

        private void labelPair2_Click(object sender, EventArgs e)
        {
            OpenURL(pair[1].URL);
        }

        private void labelPair3_Click(object sender, EventArgs e)
        {
            OpenURL(pair[2].URL);
        }

        private void labelPair4_Click(object sender, EventArgs e)
        {
            OpenURL(pair[3].URL);
        }

        private void labelPair5_Click(object sender, EventArgs e)
        {
            OpenURL(pair[4].URL);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }
    }
}