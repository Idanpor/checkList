using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CommitCheckList;

namespace CheckListTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Title.Text = @"Commit Checklist - Filtered Questions";
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
            var questList = XmlManagerController.GetQuestions();
            foreach (var quest in questList)
            {
                var quest1 = new Question()
                {
                    Parent = flowLayoutPanel1
                };
                quest1.SetQuestion(quest);
                flowLayoutPanel1.Controls.Add(quest1);
            }

            var excelQuestList = XmlManagerController.GetExcelQuestions();
            foreach (var excelQuest in excelQuestList)
            {
                var quest1 = new QuestionForExcel()
                {
                    Parent = ExcelQuestfLPanel,
                    ExcelColumnNumber = excelQuest.Key
                };
                quest1.SetQuestion(excelQuest.Value);
                ExcelQuestfLPanel.Controls.Add(quest1);       
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dictionary<string,bool> dic = new Dictionary<string, bool>();
            bool isFilled = true;
            foreach (var quest in flowLayoutPanel1.Controls)
            {
                if (((Question) quest).GetQuestionResult() == QuestResult.Nothing)
                {
                    MessageBox.Show("Please fill all questions");
                    isFilled = false;
                    break;
                }

                dic.Add((quest as Question)?.GetQuestion() ?? throw new InvalidOperationException(), (quest as Question).IsRelevantQuestion());
            }

            foreach (var quest in ExcelQuestfLPanel.Controls)
            {
                if (((QuestionForExcel) quest).GetQuestionResult() == QuestResult.Nothing)
                {
                    MessageBox.Show("Please fill all questions");
                    isFilled = false;
                    break;
                }

            }

            if (tbTaskId.Text == String.Empty)
            {
                MessageBox.Show("Please fill the workItem Id");
                isFilled = false;
            }

            if (isFilled && !ExcelManagerController.IsFileLocked())
            {
                int lastColum = 0;
                ExcelManagerController.OpenExcel();
                ExcelManagerController.OpenAndSet(1, DateTime.Now.ToShortDateString());
                ExcelManagerController.OpenAndSet(2, XmlManagerController.GetDeveloperName());
                ExcelManagerController.OpenAndSet(3, tbTaskId.Text);
                foreach (Control quest in ExcelQuestfLPanel.Controls)
                {
                    ExcelManagerController.OpenAndSet(((QuestionForExcel)quest).ExcelColumnNumber, (((QuestionForExcel)quest).GetQuestionResult() == QuestResult.Yes) ? "Y" : "N");
                    lastColum = ((QuestionForExcel)quest).ExcelColumnNumber;
                }
                ExcelManagerController.Close();
                XmlManagerController.dictionaryChecks = dic;
                Form2 f2 = new Form2(); // Instantiate a Form3 object.
                f2.Show(); // Show Form3 and
                this.Hide();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
