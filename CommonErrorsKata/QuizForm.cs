using CommonErrorsKata.Shared;
using System.IO;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommonErrorsKata
{
    public partial class CommonErrorsForm : Form
    {
        private const int MinAnswer = 15;
        private readonly AnswerQueue<TrueFalseAnswer> answerQueue;
        private readonly string[] files;
        private readonly SynchronizationContext synchronizationContext;
        private int currentPercent = 100;
        private string currentBaseName = null;
        private readonly string[] possibleAnswers = null;
        private readonly string[] fileNames = null;

        public CommonErrorsForm()
        {
            InitializeComponent();
            synchronizationContext = SynchronizationContext.Current;
            files = Directory.GetFiles(Environment.CurrentDirectory +  @"..\..\..\ErrorPics");
            possibleAnswers = new string[] { "Missing File", "null instance", "divide by zero" };
            fileNames = new string[] {"object_ref.png", "object_ref_not_set.png", "divide_by_zero.png" };
            lstAnswers.DataSource = possibleAnswers;
            answerQueue = new AnswerQueue<TrueFalseAnswer>(MinAnswer);
            Next();
            lstAnswers.Click += LstAnswers_Click;
            StartTimer();
        }

        private async void StartTimer()
        {
            await Task.Run(() =>
            {
                for (currentPercent = 100; currentPercent > 0; currentPercent--)
                {
                    UpdateProgress(currentPercent);
                    Thread.Sleep(500);
                }
                Message("Need to be quicker on your feet next time!  Try again...");
            });
        }

        private void LstAnswers_Click(object sender, EventArgs e)
        {
            currentPercent = 100;
            var currentImageName = currentBaseName.Split(' ');

            if (currentImageName.Length == 0)
            {
                return;
            }

            var imageName = currentImageName[0];
            var index = Array.IndexOf(fileNames, imageName);

            if (index < 0 || index >= possibleAnswers.Length)
            {
                return;
            }

            var correctAnswer = possibleAnswers[index];

            Console.WriteLine(lstAnswers.SelectedItem.ToString());

            var isCorrect = lstAnswers.SelectedItem.ToString() == correctAnswer;
            answerQueue.Enqueue(new TrueFalseAnswer(isCorrect));

            Next();

        }

        private void Next()
        {
            if (answerQueue.Count >= MinAnswer && answerQueue.Grade >= 98)
            {
                MessageBox.Show("Congratulations you've defeated me!");
                Application.Exit();
                return;
            }
            label1.Text = answerQueue.Grade.ToString() + "%";
            var file = files.GetRandom();
            currentBaseName = Path.GetFileName(file);
            pbImage.ImageLocation = file;
        }

        public void UpdateProgress(int value)
        {
            synchronizationContext.Post(new SendOrPostCallback(x => {
                progress.Value = value;
            }), value);
        }
        public void Message(string value)
        {
            synchronizationContext.Post(new SendOrPostCallback(x => {
                MessageBox.Show(value);
            }), value);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
