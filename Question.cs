using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckListTool
{
    public enum QuestResult
    {
        Yes,
        No,
        Nothing
    }

    public partial class Question : UserControl
    {
        public Question()
        {
            InitializeComponent();
        }
        public void SetQuestion(string question)
        {
            label1.Text = question;
        }

        public bool IsRelevantQuestion()
        {
            return YesRadioButton.Checked;
        }

        public string GetQuestion()
        {
            return label1.Text;
        }

        public QuestResult GetQuestionResult()
        {
            if (YesRadioButton.Checked)
            {
                return QuestResult.Yes;
            }
            else if (NoRadioButton.Checked)
            {
                return QuestResult.No;
            }
            return QuestResult.Nothing;
        }
    }
}
