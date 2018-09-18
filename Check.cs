using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckListTool
{
    public enum CheckResult
    {
        NotRelevant,
        Helpful,
        AlreadyCovered,
        Nothing
    }

    public partial class Check : UserControl
    {
       // public int ExcelColumnNumber { get; set; }
        public Check()
        {
            InitializeComponent();
        }

        public void SetCheck(string check)
        {
            label1.Text = check;
        }

        public string GetCheck()
        {
            return label1.Text;
        }

        public CheckResult GetCheckResult()
        {
            if (Helpfulrb.Checked)
            {
                return CheckResult.Helpful;
            }
            else if (NotRelevantrb.Checked)
            {
                return CheckResult.NotRelevant;
            }
            else if (AlreadyCoveredrb.Checked)
            {
                return CheckResult.AlreadyCovered;
            }
            return CheckResult.Nothing;
        }

        private string checkFilePath;

        public void SetCheckFilePath(string path)
        {
            checkFilePath = path;
        }

        public string GetCheckFilePath()
        {
            return checkFilePath;
        }

        public void SetToolTip(string toolTipText)
        {
            toolTip1.AutoPopDelay = 12000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(label1,toolTipText);
        }
    }
}
