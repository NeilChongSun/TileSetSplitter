﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Forms;
using System.IO;

namespace TileSetSplitter
{
    class Cropper
    {
        public int columns;
        public int rows;

        public int spriteWidth;
        public int spriteHeight;

        public List<CroppedBitmap> croppedBitmaps=new List<CroppedBitmap>();

        public void Crop(TileSet tileSet)
        {
            croppedBitmaps.Clear();
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    CroppedBitmap croppedBitmap = new CroppedBitmap(tileSet.bitmap, new Int32Rect(x*spriteWidth, y*spriteHeight, spriteWidth, spriteHeight));
                    croppedBitmaps.Add(croppedBitmap);
                }
            }
            System.Windows.MessageBox.Show("Done");
        }

        public void DrawCropLine(ref Canvas canvas)
        {
            //Drwa vertical lines
            for (int row = 0; row <= rows; row++)
            {
                Line verticalLine = new Line();
                verticalLine.StrokeThickness = 1;
                verticalLine.Stroke = Brushes.Red;

                verticalLine.X1 = 0;
                verticalLine.Y1 = row * spriteHeight;
                verticalLine.X2 = columns * spriteWidth;
                verticalLine.Y2 = row * spriteHeight;

                canvas.Children.Add(verticalLine);
            }

            //Draw horizontal lines
            for (int column = 0; column <= columns; column++)
            {
                Line horizontalLine = new Line();
                horizontalLine.StrokeThickness = 1;
                horizontalLine.Stroke = Brushes.Red;

                horizontalLine.X1 = column * spriteWidth;
                horizontalLine.Y1 = 0;
                horizontalLine.X2 = column * spriteWidth;
                horizontalLine.Y2 = rows * spriteWidth;

                canvas.Children.Add(horizontalLine);
            }
        }

        public void ApplyValue(TileSet tileSet, string inputWidth, string inputHeight, string inputOffsetX, string inputOffsetY)
        {
            spriteWidth = Int32.Parse(inputWidth);
            spriteHeight = Int32.Parse(inputHeight);

            int offsetX = Int32.Parse(inputOffsetX);
            int offsetY = Int32.Parse(inputOffsetY);

            columns = (int)(tileSet.width / spriteWidth);
            rows = (int)(tileSet.height / spriteWidth);
        }

        public void ExportSprites()
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();



            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "Image Files|*.png";
            if (fileDialog.ShowDialog()==DialogResult.OK)
            {
                for (int i = 0; i < croppedBitmaps.Count; i++)
                {
                    FileStream stream = new FileStream(fileDialog.FileName+"_"+i.ToString()+".png", FileMode.CreateNew);
                    PngBitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(croppedBitmaps[i]));
                    encoder.Save(stream);
                    stream.Close();
                }            
            }
            System.Windows.MessageBox.Show("Done");
        }


    }//end class Cropper
}
