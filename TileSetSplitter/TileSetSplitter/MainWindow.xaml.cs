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


        public MainWindow()
        {
            InitializeComponent();
        }

        private void ImportButtonClick(object sender, RoutedEventArgs e)
        {
            tileSet.ImportTileSet();
            TileSetPicture.Source = tileSet.bitmap;
        }

        private void ExportButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void ApplyButtonClick(object sender, RoutedEventArgs e)
        {
            

            Cropper cropper = new Cropper();
            cropper.Apply(tileSet, SpriteWidth.Text, SpriteHeight.Text,OffsetX.Text,OffsetY.Text);
        }
    }
}
