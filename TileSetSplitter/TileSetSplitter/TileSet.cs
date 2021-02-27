using System;
using System.Windows.Media.Imaging;
using System.Windows.Forms;

namespace TileSetSplitter
{
    public class TileSet
    {
        private string path;
        public double height;
        public double width;
        
        public BitmapImage bitmap = new BitmapImage();

        public bool ImportTileSet()
        {
            bitmap = new BitmapImage();
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files|*.png";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                path = fileDialog.FileName;
                LoadTileSet();
                return true;
            }
            else
                return false;
        }

        private void LoadTileSet()
        {
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(path, UriKind.Absolute);
            bitmap.EndInit();

            height = bitmap.Height;
            width = bitmap.Width;
        }
    }
}
