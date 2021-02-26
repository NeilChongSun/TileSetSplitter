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
using System.Windows.Forms;


namespace TileSetSplitter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TileSet tileSet = new TileSet();
        private Image preViewImage = new Image();
        private Cropper cropper = new Cropper();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ImportButtonClick(object sender, RoutedEventArgs e)
        {
            tileSet.ImportTileSet();

            double width = tileSet.width;
            double height = tileSet.height;

            Canvas.Width = tileSet.width;
            TileSetWidthInfo.Text = "Image Width: " + (int)width;
            OffsetXSlider.Maximum = (int)width;
            Canvas.Height = height;
            TileSetHeightInfo.Text = "Image Height: " + (int)width;
            OffsetYSlider.Maximum = (int)height;
            preViewImage.Source = tileSet.bitmap;
            Canvas.Children.Add(preViewImage);
        }

        private void ApplyButtonClick(object sender, RoutedEventArgs e)
        {
            ApplyValue();
        }

        private void OffsetValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ApplyValue();
        }

        private void SplitButtonClick(object sender, RoutedEventArgs e)
        {
            cropper.Crop(tileSet);
            SubWindow subWindow = new SubWindow(cropper);
            subWindow.Show();
        }

        private void NewButtonClick(object sender, RoutedEventArgs e)
        {

        }

        //Reset all data
        private void Reset()
        {

        }

        //Apply all input values: sprite height, sprite width, offsetX, offsetY
        private void ApplyValue()
        {
            cropper.ApplyValue(tileSet, SpriteWidth.Text, SpriteHeight.Text, OffsetX.Text, OffsetY.Text);
            RowInfo.Text = "Rows: " + cropper.rows;
            ColumnInfo.Text = "Columns: " + cropper.columns;
            SpritesCountInfo.Text = "Count: " + (cropper.rows * cropper.columns);
            Canvas.Children.Clear();
            Canvas.Children.Add(preViewImage);
            cropper.DrawCropLine(ref Canvas);
        }
    }

}
