﻿using CommonErrorsKata.Shared;
using System.IO;
using System.Linq;
using System;
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
        private string currentImageFileName = null;
        private readonly string[] fileNames = null;

        public CommonErrorsForm()
        {
            InitializeComponent();
            synchronizationContext = SynchronizationContext.Current;
            files = Directory.GetFiles(Environment.CurrentDirectory + @"..\..\..\ErrorPics");
            fileNames = files.Select(file => Path.GetFileName(file)).ToArray();
            lstAnswers.DataSource = fileNames.Select(element => element.Replace(".png", "")).ToList();
            answerQueue = new AnswerQueue<TrueFalseAnswer>(MinAnswer);
            AskNextQuestion();
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
                    Thread.Sleep(50);
                }
                Message("Need to be quicker on your feet next time!  Try again...");
            });
        }

        private void LstAnswers_Click(object sender, EventArgs e)
        {
            currentPercent = 100;

            var isCorrectAnswer = currentImageFileName.Replace(".png", "") == lstAnswers.SelectedItem.ToString();
            answerQueue.Enqueue(new TrueFalseAnswer(isCorrectAnswer));

            AskNextQuestion();

        }

        private void AskNextQuestion()
        {
            if (answerQueue.Count >= MinAnswer && answerQueue.Grade >= 98)
            {
                MessageBox.Show("Congratulations you've defeated me!");
                Application.Exit();
                return;
            }
            label1.Text = answerQueue.Grade + "%";
            var file = files.GetRandom();
            currentImageFileName = Path.GetFileName(file);
            pbImage.ImageLocation = file;
        }

        public void UpdateProgress(int value)
        {
            synchronizationContext.Post(x =>
            {
                progress.Value = value;
            }, value);
        }
        public void Message(string value)
        {
            synchronizationContext.Post(x =>
            {
                MessageBox.Show(value);
            }, value);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
