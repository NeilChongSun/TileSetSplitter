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
using System.Windows.Shapes;

namespace TileSetSplitter
{
    /// <summary>
    /// Interaction logic for SubWindow.xaml
    /// </summary>
    public partial class SubWindow : Window
    {
        private Cropper cropper = new Cropper();
        public SubWindow(ref Cropper c)
        {
            cropper = c;
            InitializeComponent();
            Show();
        }

        private void Show()
        {
            foreach (CroppedBitmap croppedBitmap in cropper.croppedBitmaps)
            {
                Image image = new Image();
                image.Source = croppedBitmap;
                image.Width = croppedBitmap.Width;
                image.Height = croppedBitmap.Height;
                image.MouseDown += new MouseButtonEventHandler(SelectSpriteClick);
                Sprites.Children.Add(image);
            }
        }

        private void SelectSpriteClick(object sender, MouseButtonEventArgs e)
        {
            ImageSource source = ((Image)sender).Source;
            Image image = new Image();
            image.Source = source;
            image.Width = source.Width;
            image.Height = source.Height;
            image.MouseDown += new MouseButtonEventHandler(UnselectSpriteClick);
            SelectedSprites.Children.Add(image);
        }

        private void UnselectSpriteClick(object sender, MouseButtonEventArgs e)
        {

            SelectedSprites.Children.Remove(sender as Image);      
        }

        private void ExportSpritesButtonClick(object sender, RoutedEventArgs e)
        {
            cropper.selectedImages.Clear();
            foreach(Image image in SelectedSprites.Children)
            {
                cropper.selectedImages.Add(image.Source);
            }
            cropper.ExportSprites();
        }
    }
}
