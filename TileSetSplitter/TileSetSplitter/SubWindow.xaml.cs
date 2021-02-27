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
        private Combiner combiner = new Combiner();
        private int columns;

        public SubWindow(Cropper c)
        {
            cropper = c;
            InitializeComponent();
            ApplyInputColumns();
            Show();
        }

        //Show cropped sprites on left side
        private void Show()
        {
            Sprites.Columns = cropper.columns;
            Sprites.Rows = cropper.rows;
            foreach (CroppedBitmap croppedBitmap in cropper.croppedBitmaps)
            {
                Image image = new Image();
                image.Source = croppedBitmap;
                image.Width = croppedBitmap.Width;
                image.Height = croppedBitmap.Height;
                image.Margin = new Thickness(0.5, 0.5, 0.5, 0.5);
                image.MouseDown += new MouseButtonEventHandler(SelectSpriteClick);
                Sprites.Children.Add(image);
            }
        }

        //Select sprites and show them on right size
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

        //Remove a selected sprite
        private void UnselectSpriteClick(object sender, MouseButtonEventArgs e)
        {
            SelectedSprites.Children.Remove(sender as Image);
        }

        private void ExportSpritesButtonClick(object sender, RoutedEventArgs e)
        {
            if (SelectedSprites.Children.Count==0)
            {
                MessageBox.Show("There is nothing to exporte!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            cropper.selectedImages.Clear();
            foreach(Image image in SelectedSprites.Children)
            {
                cropper.selectedImages.Add(image.Source);
            }
            cropper.ExportSprites();
        }

        private void ExportTileSetButtonClick(object sender, RoutedEventArgs e)
        {
            if (SelectedSprites.Children.Count == 0)
            {
                System.Windows.MessageBox.Show("There is nothing to exporte!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            combiner.MergeSprites(SelectedSprites,cropper);
        }

        private void ApplyButtonClick(object sender, RoutedEventArgs e)
        {
            ApplyInputColumns();

        }

        private void ApplyInputColumns()
        {
            columns = Int32.Parse(InputColumns.Text);
            SelectedSprites.Columns = columns;
        }

        private void SelectAllButtonClick(object sender, RoutedEventArgs e)
        {
            foreach (ImageSource source in cropper.croppedBitmaps)
            {
                Image image = new Image();
                image.Source = source;
                image.Width = source.Width;
                image.Height = source.Height;
                image.MouseDown += new MouseButtonEventHandler(UnselectSpriteClick);
                SelectedSprites.Children.Add(image);
            }
        }

        private void RemoveAllButtonClick(object sender, RoutedEventArgs e)
        {
            SelectedSprites.Children.Clear();
        }
    }
}
