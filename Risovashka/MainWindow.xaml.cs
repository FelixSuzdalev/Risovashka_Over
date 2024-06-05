using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Risovashka
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string TITLE = "Risovashka";
        public MainWindow()
        {
            InitializeComponent();
            this.Title = TITLE;
            this.WindowState = WindowState.Maximized;
            Closing += Window_Closing;
        }
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            this.field.Strokes.Clear();
            this.field.Background = new SolidColorBrush(Colors.White);
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            field.EditingMode = InkCanvasEditingMode.EraseByPoint;
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            field.EditingMode = InkCanvasEditingMode.Ink;
        }


        private void Red_Click(object sender, RoutedEventArgs e)
        {
            field.DefaultDrawingAttributes.Color = Colors.Red;
        }

        private void Orange_Click(object sender, RoutedEventArgs e)
        {
            field.DefaultDrawingAttributes.Color = Colors.Orange;
        }

        private void Yellow_Click(object sender, RoutedEventArgs e)
        {
            field.DefaultDrawingAttributes.Color = Colors.Yellow;
        }

        private void Green_Click(object sender, RoutedEventArgs e)
        {
            field.DefaultDrawingAttributes.Color = Colors.Green;
        }

        private void Cyan_Click(object sender, RoutedEventArgs e)
        {
            field.DefaultDrawingAttributes.Color = Colors.Cyan;
        }

        private void Blue_Click(object sender, RoutedEventArgs e)
        {
            field.DefaultDrawingAttributes.Color = Colors.Blue;
        }

        private void Purple_Click(object sender, RoutedEventArgs e)
        {
            field.DefaultDrawingAttributes.Color = Colors.Purple;
        }

        private void Black_Click(object sender, RoutedEventArgs e)
        {
            field.DefaultDrawingAttributes.Color = Colors.Black;
        }

        private void White_Click(object sender, RoutedEventArgs e)
        {
            field.DefaultDrawingAttributes.Color = Colors.White;
        }

        private void Filling_Click(object sender, RoutedEventArgs e)
        {
            Color c = ColorPicker.Color;
            if (field is null)
                return;
            field.Background = new SolidColorBrush(Color.FromRgb(c.R, c.G, c.B));

        }

        private void SizeBrush_(object sender, SelectionChangedEventArgs e)
        {
            if (!this.IsInitialized || field is null)
                return;

            DrawingAttributes inkAttributes = field.DefaultDrawingAttributes;
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
            inkAttributes.Width = Convert.ToDouble(selectedItem.Content);
            inkAttributes.Height = Convert.ToDouble(selectedItem.Content);
        }

       
       
        private void SaveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            RenderTargetBitmap renderBitmap = new RenderTargetBitmap((int)field.ActualWidth, (int)field.ActualHeight, 100d, 100d, PixelFormats.Default);

            renderBitmap.Render(field);

            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderBitmap));

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PNG Image|*.png|JPG Image|*.jpg|JPEG Image|*.jpeg|BMP Image|*.bmp";
            
            if (saveFileDialog.ShowDialog() == true)
            {
                using (FileStream fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                {
                    encoder.Save(fileStream);
                }
            }
        }
        private void OpenMenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string fileName = openFileDialog.FileName;
                FileInfo fileInfo = new FileInfo(openFileDialog.FileName);
                this.Title = TITLE + " - " + fileInfo.Name;
                BitmapImage bitmapImage = new BitmapImage(new Uri(fileName));
                Image image = new Image();
                image.Source = bitmapImage;
                field.Children.Clear();
                field.Children.Add(image);
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) 
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите выйти?", "Закрытие", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No) e.Cancel = true;
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ColorPicker_ColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Color c = (Color)e.NewValue;
            if (field is null)
            return;
            field.DefaultDrawingAttributes.Color = Color.FromRgb(c.R, c.G, c.B);
        }

        private void NewMenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.field.Strokes.Clear();
            this.field.Background = new SolidColorBrush(Colors.White);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double height = Convert.ToDouble(Height.Text);
            double width = Convert.ToDouble(Width.Text);
            field.Height = height;
            field.Width = width;
        }
    }
}


