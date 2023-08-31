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

namespace TinyMatchGame
{
    using System.Windows.Threading;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tenthsOfSecondsElapsed;
        int matchesFound;
        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_tick;
            SetUpGame();
        }

        private void Timer_tick(object sender, EventArgs e)
        {
            tenthsOfSecondsElapsed++;
            infoTextBlock.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");
            if (matchesFound == 8)
            {
                timer.Stop();
                infoTextBlock.Text = infoTextBlock.Text + " - Play again?";
            }
        }

        private void SetUpGame()
        {
            List<string> interestingEmoji = new List<string>
            {
                "😆","😆", // Grinning Squinting Face
                "😅","😅", // Grinning Face with Sweat
                "🤣","🤣", // Rolling on the Floor Laughing
                "😇","😇", // Smiling Face with Halo
                "🥰","🥰", // Smiling Face with Hearts
                "😋","😋", // Face Savoring Food
                "😤","😤", // Face with Steam From Nose
                "🥺","🥺", // Pleading Face
            };

            Random random = new Random();

            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Name != "infoTextBlock")
                {
                    textBlock.Visibility = Visibility.Visible;
                    int index = random.Next(interestingEmoji.Count);
                    string nextEmoji = interestingEmoji[index];
                    textBlock.Text = nextEmoji;
                    interestingEmoji.RemoveAt(index);
                }
            }

            // start the timer and reset the variables
            timer.Start();
            tenthsOfSecondsElapsed = 0;
            matchesFound = 0;
        }

        TextBlock lastTextBlockClicked;
        bool findingMatch = false;

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock senderText = sender as TextBlock;
            if (findingMatch == false)
            {
                senderText.Visibility = Visibility.Hidden;
                lastTextBlockClicked = senderText;
                findingMatch = true;
            }
            else if (senderText.Text == lastTextBlockClicked.Text)
            {
                matchesFound++;
                senderText.Visibility = Visibility.Hidden;
                findingMatch= false;
            }
            else
            {
                lastTextBlockClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }
        }

        private void InfoTextBlock_MouseBlock(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 8)
            {
                SetUpGame();
            }
        }
    }
}
