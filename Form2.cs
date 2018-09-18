using System;
using System.Linq;
using System.Windows.Forms;
using CommitCheckList;

namespace CheckListTool
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            Title.Text = @"The Checklist";
            MaximizeBox = false;
            InitUI();

        }

        private const int CP_NOCLOSE_BUTTON = 0x200;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        private void InitUI()
        {
            var checklist = XmlManagerController.GetChecks();
            checklist = checklist.GroupBy(c => c.CheckDescription).Select(c => c.First()).ToList();
            var rnd = new Random();
            checklist = checklist.OrderBy(item => rnd.Next()).ToList();
            foreach (var check in checklist)
            {
                var check1 = new Check()
                {
                    Parent = flowLayoutPanel1
                };
                check1.SetCheck(check.CheckDescription);
                check1.SetCheckFilePath(check.CheckFilePath);
                check1.SetToolTip(check.CheckToolTip);
                //check1.ExcelColumnNumber = check.ExcelColumnNumber;
                flowLayoutPanel1.Controls.Add(check1);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool filled = true;
            foreach (var check in flowLayoutPanel1.Controls)
            {
                if (check is Check)
                {
                    if ((check as Check).GetCheckResult() != CheckResult.Nothing)
                    {
                        switch ((check as Check).GetCheckResult())
                        {
                            case CheckResult.NotRelevant:
                                XmlManagerController.SetCheckResult(
                                    new CheckModel
                                    {
                                        CheckDescription = (check as Check).GetCheck(),
                                        CheckFilePath = (check as Check).GetCheckFilePath(),
                                        //ExcelColumnNumber = (check as Check).ExcelColumnNumber
                                    }, CheckResult.NotRelevant);
                                break;
                            case CheckResult.Helpful:
                                XmlManagerController.SetCheckResult(
                                    new CheckModel
                                    {
                                        CheckDescription = (check as Check).GetCheck(),
                                        CheckFilePath = (check as Check).GetCheckFilePath(),
                                        //ExcelColumnNumber = (check as Check).ExcelColumnNumber
                                    }, CheckResult.Helpful);
                                break;
                            case CheckResult.AlreadyCovered:
                                XmlManagerController.SetCheckResult(
                                    new CheckModel
                                    {
                                        CheckDescription = (check as Check).GetCheck(),
                                        CheckFilePath = (check as Check).GetCheckFilePath(),
                                        //ExcelColumnNumber = (check as Check).ExcelColumnNumber
                                    }, CheckResult.AlreadyCovered);
                                break;
                            default:
                                MessageBox.Show("Please Fill all Checklist!");
                                break;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please Fill all the Checklist!");
                        filled = false;
                        break;
                    }
                }
            }
            if (filled)
            {
                MessageBox.Show("Now the commit is safer!");
                Application.Exit();
            }
            XmlManagerController.CreateNode(Environment.UserName);
        }
    }
}

