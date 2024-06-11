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

namespace Projekt_Logik_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int[] secretCode;
        List<Brush> colors;
        Brush selectedColor = Brushes.Gray;
        int round = 0;
        Button checkButton;
        public MainWindow()
        {
            InitializeComponent();
            InitializeColors();
            CreateSecretCode();
            PripravPole();

        }
        public void InitializeColors()
        {
            colors = new List<Brush> { Brushes.Red, Brushes.Green, Brushes.Blue, Brushes.Yellow, Brushes.Black, Brushes.White };

        }

        public void CreateSecretCode()
        {
            Random random = new Random();
            secretCode = new int[4];
            for (int i = 0; i < 4; i++)
            {
                secretCode[i] = random.Next(6);
            }
        }

        public void PripravPole()
        {
            for (int column = 0; column < 12; column++)
                for (int row = 0; row < 4; row++)
                    AddColorPin(row, column);

            checkButton = new Button
            {
                Height = 80,
                Width = 80,
                Content = "Check"
            };
            checkButton.Click += Check;
            checkButton.SetValue(Grid.RowProperty, 4);
            checkButton.SetValue(Grid.ColumnProperty, round);
            MainGrid.Children.Add(checkButton);
        }

        public void AddColorPin(int row, int column)
        {
            Ellipse e = new Ellipse
            {
                Height = 80,
                Width = 80,
                Stroke = Brushes.Black,
                Fill = Brushes.Gray,
            };
            e.MouseDown += FillColor;
            e.SetValue(Grid.RowProperty, row);
            e.SetValue(Grid.ColumnProperty, column);
            MainGrid.Children.Add(e);

        }

        public void AddEvaluationPins()
        {
             //Need two rows and columns
            Grid g = new Grid();
            g.RowDefinitions.Add(new RowDefinition());
            g.RowDefinitions.Add(new RowDefinition());
            g.ColumnDefinitions.Add(new ColumnDefinition());
            g.ColumnDefinitions.Add(new ColumnDefinition());
            for (int i = 0; i < 4; i++)
            {
                Ellipse e = new Ellipse
                {
                    Height = 40,
                    Width = 40,
                    Stroke = Brushes.Black,
               
                };
                e.Fill = pins[i] == 2 ? Brushes.Black : pins[i] == 1 ? Brushes.Red : Brushes.Gray;
                e.MouseDown += SelectColor;
                e.SetValue(Grid.RowProperty, i / 2);
                e.SetValue(Grid.ColumnProperty, i % 2);
                g.Children.Add(e);
            }
            g.SetValue(Grid.RowProperty, 4);
            g.SetValue(Grid.ColumnProperty, round);
            MainGrid.Children.Add(g);
        }

        private void SelectColor(object sender, MouseButtonEventArgs e)
        {
            selectedColor = ((Ellipse)sender).Fill;
            CursorColorIndicator.Fill = selectedColor;
            CursorColorIndicator.Visibility = Visibility.Visible;
        }

        private void FillColor(object sender, MouseButtonEventArgs e)
        {
            Ellipse pin = (Ellipse)sender;
            if ((int)pin.GetValue(Grid.ColumnProperty) == round)
                pin.Fill = selectedColor;
        }

        private void Check(object sender, RoutedEventArgs e)
        {

            int[] pins = new int[4];
            for (int i = 0; i < 4; i++)
            {
                Ellipse pin = (Ellipse)MainGrid.Children.Cast<UIElement>().First(x => Grid.GetRow(x) == i && Grid.GetColumn(x) == round && x is not Canvas);
                pins[i] = colors.IndexOf(pin.Fill);
                if (pins[i] == -1)
                {
                    MessageBox.Show("You need to fill all pins!");
                    return;
                }
            }

            int[] evaluation = EvaluatePins(pins);

            if (pins.SequenceEqual(secretCode))
            {
                FinalWindow f = new FinalWindow(true);
                f.Show();
                Close();
            }
            else
            {
                if (round == 12)
                {
                    FinalWindow f = new FinalWindow();
                    f.Show();
                    Close();
                }
                else
                    MessageBox.Show($"You lost! ({String.Join(',', secretCode)})");
            }
            AddEvaluationPins();
            checkButton.SetValue(Grid.ColumnProperty, ++round);

        }

        private int[] EvaluatePins(int[] pins)
        {
            int[] evaluation = new int[4];
            bool[] codeUsed = new bool[4];
            bool[] pinsUsed = new bool[4];

            // Evaluate for correct color and position (black pins)
            for (int i = 0; i < 4; i++)
            {
                if (pins[i] == secretCode[i])
                {
                    evaluation[i] = 2; // Black pin
                    codeUsed[i] = true;
                    pinsUsed[i] = true;

                }
            }

            // Evaluate for correct color but wrong position (red pins)
            for (int i = 0; i < 4; i++)
            {
                if (!pinsUsed[i])
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (!codeUsed[j] && pins[i] == secretCode[j])
                        {
                            evaluation[i] = 1; // Red pin
                            codeUsed[j] = true;
                            break;
                        }
                    }
                }
            }

            return evaluation.OrderByDescending(x => x).ToArray();
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            var position = e.GetPosition(MainGrid);
            CursorColorIndicator.Margin = new Thickness(position.X + 10, position.Y + 10, 0, 0);
        }
    }
}
