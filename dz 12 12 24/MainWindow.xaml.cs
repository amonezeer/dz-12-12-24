using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using Microsoft.Win32;
using System.Windows.Media.Imaging;

namespace kdpk
{
    public partial class MainWindow : Window
    {
        private bool isDragging = false;
        private Point clickPosition;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDragging = true;
            clickPosition = e.GetPosition(this);
            image.CaptureMouse();
        }

        private void Image_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                var currentPosition = e.GetPosition(this);
                double offsetX = currentPosition.X - clickPosition.X;
                double offsetY = currentPosition.Y - clickPosition.Y;

                double newX = Canvas.GetLeft(image) + offsetX;
                double newY = Canvas.GetTop(image) + offsetY;

                Canvas.SetLeft(image, newX);
                Canvas.SetTop(image, newY);

                XTextBox.Text = newX.ToString();
                YTextBox.Text = newY.ToString();

                clickPosition = currentPosition;
            }
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            image.ReleaseMouseCapture();
        }

        private void XTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(XTextBox.Text, out double newX))
            {
                Canvas.SetLeft(image, newX);
            }
        }

        private void YTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(YTextBox.Text, out double newY))
            {
                Canvas.SetTop(image, newY);
            }
        }

        private void LoadImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

            if (openFileDialog.ShowDialog() == true)
            {
                BitmapImage bitmap = new BitmapImage(new Uri(openFileDialog.FileName));
                image.Source = bitmap;
            }
        }
    }
}
