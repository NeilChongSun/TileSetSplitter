using System;
using System.Collections.Generic;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Forms;
using System.IO;

namespace TileSetSplitter
{
    public class Cropper
    {
        public int columns = 0;
        public int rows = 0;

        public int spriteWidth = 0;
        public int spriteHeight = 0;

        private int offsetX = 0;
        private int offsetY = 0;

        public List<CroppedBitmap> croppedBitmaps = new List<CroppedBitmap>();
        public List<ImageSource> selectedImages = new List<ImageSource>();

        public void Crop(TileSet tileSet)
        {
            croppedBitmaps.Clear();
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    CroppedBitmap croppedBitmap = new CroppedBitmap(tileSet.bitmap, new Int32Rect(x * spriteWidth + offsetX, y * spriteHeight + offsetY, spriteWidth, spriteHeight));
                    croppedBitmaps.Add(croppedBitmap);
                }
            }
        }

        public void DrawCropLine(ref Canvas canvas)
        {
            //Drwa horizontal lines
            for (int row = 0; row <= rows; row++)
            {
                Line verticalLine = new Line();
                verticalLine.StrokeThickness = 1;
                verticalLine.Stroke = Brushes.Red;

                verticalLine.X1 = 0 + offsetX;
                verticalLine.Y1 = row * spriteHeight + offsetY;
                verticalLine.X2 = columns * spriteWidth + offsetX;
                verticalLine.Y2 = row * spriteHeight + offsetY;

                canvas.Children.Add(verticalLine);
            }

            //Draw vertical lines
            for (int column = 0; column <= columns; column++)
            {
                Line horizontalLine = new Line();
                horizontalLine.StrokeThickness = 1;
                horizontalLine.Stroke = Brushes.Red;

                horizontalLine.X1 = column * spriteWidth + offsetX;
                horizontalLine.Y1 = 0 + offsetY;
                horizontalLine.X2 = column * spriteWidth + offsetX;
                horizontalLine.Y2 = rows * spriteHeight + offsetY;

                canvas.Children.Add(horizontalLine);
            }
        }

        public void ApplyValue(TileSet tileSet, string inputWidth, string inputHeight, string inputOffsetX, string inputOffsetY)
        {
            spriteWidth = SafetyParse(inputWidth);
            spriteHeight = SafetyParse(inputHeight);

            if (spriteHeight == 0 || spriteWidth == 0)
            {
                columns = 0;
                rows = 0;
                return;
            }

            offsetX = SafetyParse(inputOffsetX);
            offsetY = SafetyParse(inputOffsetY);

            columns = (int)((tileSet.width - offsetX) / spriteWidth);
            rows = (int)((tileSet.height - offsetY) / spriteHeight);
        }

        public void ExportSprites()
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "Image Files|*.png";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = fileDialog.FileName;
                if (System.IO.Path.HasExtension(fileName))
                {
                    string filePath = System.IO.Path.GetDirectoryName(fileName);
                    string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(fileName);
                    fileName = System.IO.Path.Combine(filePath, fileNameWithoutExtension);
                }

                for (int i = 0; i < selectedImages.Count; i++)
                {
                    FileStream stream = new FileStream(fileName + "_" + i.ToString() + ".png", FileMode.CreateNew);
                    PngBitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(croppedBitmaps[i]));
                    encoder.Save(stream);
                    stream.Close();
                }
                System.Windows.MessageBox.Show("Done!", "Export", MessageBoxButton.OK,MessageBoxImage.Information);
            }
        }

        //Parse input value to integer in a safe way in case the input is not a integer
        private int SafetyParse(string input)
        {
            int result = 0;
            if (Int32.TryParse(input, out result))
            {
                if (result >= 0)
                {
                    return result;
                }
            }
            System.Windows.MessageBox.Show("The input value must be a positive integer!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return 0;
        }
    }
}
