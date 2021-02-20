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
            Canvas.Width = tileSet.width;
            Canvas.Height = tileSet.height;                     
            preViewImage.Source = tileSet.bitmap;
            Canvas.Children.Add(preViewImage);
        }

        private void ApplyButtonClick(object sender, RoutedEventArgs e)
        {            
            cropper.ApplyValue(tileSet,SpriteWidth.Text, SpriteHeight.Text,OffsetX.Text,OffsetY.Text);
            Canvas.Children.Clear();
            Canvas.Children.Add(preViewImage);
            cropper.DrawCropLine(ref Canvas);
        }

        private void SplitButtonClick(object sender, RoutedEventArgs e)
        {
            cropper.Crop(tileSet);
            SubWindow subWindow = new SubWindow(cropper);
            subWindow.Show();
        }

    }

}
