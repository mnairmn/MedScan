using System;
namespace MedScan.Models
{
	public class BoxModel
	{
		public BoxModel(int xstart, int ystart, int height, int width)
		{
			XStart = xstart;
			YStart = ystart;
			Width = width;
			Height = height;
		}
		public int XStart { get; set; }
		public int YStart { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
	}
}

