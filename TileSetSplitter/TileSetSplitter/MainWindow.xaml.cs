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
            Reset();
        }

        private void ImportButtonClick(object sender, RoutedEventArgs e)
        {
            Reset();
            if (tileSet.ImportTileSet())
            {
                double width = tileSet.width;
                double height = tileSet.height;

                Canvas.Width = width;
                Canvas.Height = height;
                OffsetXSlider.Maximum = (int)width;
                OffsetYSlider.Maximum = (int)height;
                TileSetWidthInfo.Text = "Image Width: " + (int)width;
                TileSetHeightInfo.Text = "Image Height: " + (int)height;
                preViewImage.Source = tileSet.bitmap;
                Canvas.Children.Add(preViewImage);
                ElementsIsEnable(true);
            }
        }

        private void ApplyButtonClick(object sender, RoutedEventArgs e)
        {
            ApplyValue();
        }

        private void OffsetValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            OffsetX.Text = OffsetXSlider.Value.ToString();
            OffsetY.Text = OffsetYSlider.Value.ToString();
            ApplyValue();
        }

        private void SplitButtonClick(object sender, RoutedEventArgs e)
        {
            cropper.Crop(tileSet);
            SubWindow subWindow = new SubWindow(cropper);
            subWindow.Show();
        }

        //Reset all datas
        private void Reset()
        {
            tileSet = new TileSet();
            preViewImage = new Image();
            cropper = new Cropper();
            SpriteHeight.Text = "0";
            SpriteWidth.Text = "0";
            OffsetXSlider.Value = 0;
            OffsetYSlider.Value = 0;
            Canvas.Width = 0;
            Canvas.Height = 0;
            RowInfo.Text = "Rows: 0";
            ColumnInfo.Text = "Columns: 0";
            SpritesCountInfo.Text = "Count: 0";
            Canvas.Children.Clear();

            ElementsIsEnable(false);
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

        //Set all buttons and textbox is enable
        void ElementsIsEnable(bool isEnable)
        {
            SpriteWidth.IsEnabled = isEnable;
            SpriteHeight.IsEnabled = isEnable;
            ApplyButton.IsEnabled = isEnable;
            OffsetX.IsEnabled = isEnable;
            OffsetY.IsEnabled = isEnable;
            OffsetXSlider.IsEnabled = isEnable;
            OffsetYSlider.IsEnabled = isEnable;
            SplitButton.IsEnabled = isEnable;
        }

    }

}
