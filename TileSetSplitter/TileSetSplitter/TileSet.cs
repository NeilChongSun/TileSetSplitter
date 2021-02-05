using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Forms;

namespace TileSetSplitter
{
    class TileSet
    {
        private string path;
        public double height;
        public double width;
        
        public BitmapImage bitmap = new BitmapImage();

        public void ImportTileSet()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files|*.png";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                path = fileDialog.FileName;
                LoadTileSet();
            }
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
