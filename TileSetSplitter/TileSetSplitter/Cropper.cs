using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
using System.Windows.Controls;

namespace TileSetSplitter
{
    class Cropper
    {        
        public int columns;
        public int rows;

        private List<Line> HorizontalLines = new List<Line>();
        private List<Line> VerticalLines = new List<Line>();


        void Crop(TileSet tileSet)
        {
           
        }



        public void Apply(TileSet tileSet,string inputWidth,string inputHeight,string inputOffsetX,string inputOffsetY)
        {
            int width = Int32.Parse(inputWidth);
            int height = Int32.Parse(inputHeight);

            int offsetX = Int32.Parse(inputOffsetX);
            int offsetY = Int32.Parse(inputOffsetY);

            columns =(int)(tileSet.width / width);
            rows =(int)(tileSet.height / width);
         
        }
    }
}
