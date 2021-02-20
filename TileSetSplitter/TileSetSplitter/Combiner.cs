using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Forms;
using System.IO;

namespace TileSetSplitter
{
    class Combiner
    {
        private int rows;
        private int columns;

        //Merge selected sprites by input columns
        public void MergeSprites(UniformGrid uniformGrid, Cropper cropper)
        {
            int spritedsCount = uniformGrid.Children.Count;

            columns = uniformGrid.Columns;
            rows = (int)Math.Ceiling((double)spritedsCount / (double)columns);

            int width = cropper.spriteWidth;
            int heigth = cropper.spriteHeight;

            DrawingVisual drawingVisual = new DrawingVisual();
            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                for (int y = 0; y < rows; y++)
                {
                    for (int x = 0; x < columns; x++)
                    {
                        int index = GetIndex(x, y);

                        ImageSource source = null;

                        if (index < spritedsCount)
                            source = ((Image)uniformGrid.Children[index]).Source;

                        Rect rect = new Rect(x * width, y * heigth, width, heigth);

                        drawingContext.DrawImage(source, rect);
                    }
                }
            }
            RenderTargetBitmap bitmap = new RenderTargetBitmap(columns * width, rows * heigth, 96, 96, PixelFormats.Pbgra32);
            bitmap.Render(drawingVisual);
            ExportTileSet(bitmap);
        }


        private void ExportTileSet(RenderTargetBitmap bitmap)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "Image Files|*.png";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                FileStream stream = new FileStream(fileDialog.FileName, FileMode.Create);
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmap));
                encoder.Save(stream);
                stream.Close();
            }
            System.Windows.MessageBox.Show("Done");
        }

        private int GetIndex(int x, int y)
        {
            return x + (y * columns);
        }
    }
}
