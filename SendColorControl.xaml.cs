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

namespace Talaria
{
    /// <summary>
    /// Interaction logic for SendColorControl.xaml
    /// </summary>
    public partial class SendColorControl : UserControl
    {
        private string[] colors = { "R", "G", "B" };
        private int FirstCharIndex = 0;
        private int SecondCharIndex = 0;
        private int FirstNumberIndex = 1;
        private int SecondNumberIndex = 1;
        public SendColorControl()
        {
            InitializeComponent();

            FirstNumber.Text = "1";
            FirstChar.Text = "R";
            SecondNumber.Text = "8";
            SecondChar.Text = "G";
        }

        private void firstNumberPlus_Click(object sender, RoutedEventArgs e)
        {
            FirstNumberIndex++;
            if (FirstNumberIndex > 9)
            {
                FirstNumberIndex = 1;
            }
            FirstNumber.Text = FirstNumberIndex.ToString();
        }
        private void firstCharPlus_Click(object sender, RoutedEventArgs e)
        {
            FirstCharIndex = (FirstCharIndex + 1) % colors.Length;
            FirstChar.Text = colors[FirstCharIndex];
        }
        private void SecondNumberPlus_Click(object sender, RoutedEventArgs e)
        {
            SecondNumberIndex++;
            if (SecondNumberIndex > 9)
            {
                SecondNumberIndex = 1;
            }
            SecondNumber.Text = SecondNumberIndex.ToString();
        }
        private void SecondCharPlus_Click(object sender, RoutedEventArgs e)
        {
            SecondCharIndex = (SecondCharIndex + 1) % colors.Length;
            SecondChar.Text = colors[SecondCharIndex];
        }
        private void FirstNumberDecrease_Click(object sender, RoutedEventArgs e)
        {
            FirstNumberIndex--;
            if (FirstNumberIndex < 1)
            {
                FirstNumberIndex = 9;
            }
            FirstNumber.Text = FirstNumberIndex.ToString();
        }
        private void FirstCharDecrease_Click(object sender, RoutedEventArgs e)
        {
            FirstCharIndex = (FirstCharIndex - 1 + colors.Length) % colors.Length;
            FirstChar.Text = colors[FirstCharIndex];
        }
        private void SecondNumberDecrease_Click(object sender, RoutedEventArgs e)
        {
            SecondNumberIndex--;
            if (SecondNumberIndex < 1)
            {
                SecondNumberIndex = 9;
            }
            SecondNumber.Text = SecondNumberIndex.ToString();
        }
        private void SecondCharDecrease_Click(object sender, RoutedEventArgs e)
        {
            SecondCharIndex = (SecondCharIndex - 1 + colors.Length) % colors.Length;
            SecondChar.Text = colors[SecondCharIndex];
        }
        private void sendColorButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
