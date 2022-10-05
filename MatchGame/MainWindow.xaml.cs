using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MatchGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tenthsOfSecondsElapsed;
        int matchFound;

        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(0.1);
            timer.Tick += Timer_Tick;
            SetUpGame();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthsOfSecondsElapsed++;
            timeTextBlock.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");
            if (tenthsOfSecondsElapsed / 10F == 10.0F)
            {
                timer.Stop();
                timeTextBlock.Text += "<Time out>- Play again?";
            }
            else if (matchFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text += "<You Win>- Play again?";
            }
        }

        private void SetUpGame()
        {
            List<string> animalEmoji = new List<string>()
            {
                "🐵", "🐵",
                "🐶", "🐶",
                "🐷", "🐷",
                "🐮", "🐮",
                "🐼", "🐼",
                "🐭", "🐭",
                "🐰", "🐰",
                "🐨", "🐨",
            };
            Random random = new Random();
            foreach(TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if(textBlock.Name != "timeTextBlock")
                {
                    textBlock.Visibility = Visibility.Visible;
                    int index = random.Next(animalEmoji.Count);
                    string nextEmoji = animalEmoji[index];
                    textBlock.Text = nextEmoji;
                    animalEmoji.RemoveAt(index);
                }
            }
            timer.Start();
            tenthsOfSecondsElapsed = 0;
            matchFound = 0;
        }

        TextBlock lastTextBlockClick;
        private bool findingMatch = false;

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if(findingMatch == false)
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClick = textBlock;
                findingMatch = true;
            }
            else if(lastTextBlockClick.Text == textBlock.Text)
            {
                matchFound++;
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
            }
            else
            {
                lastTextBlockClick.Visibility = Visibility.Visible;
                findingMatch = false;
            }
        }

        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(matchFound == 8 || (tenthsOfSecondsElapsed / 10F == 10.0F))
            {
                SetUpGame();
            }
        }
    }
}
