using System;
namespace MedScan.Models
{
	public class ImageModel
	{
		public string? FilePath { get; set; }
		public string? ReadData { get; set; } // this porperty will store any data picked up by the OCR in the image
		//note the question marks when declaring both properties - this signifies a nullable property, meaning even if the user does not provide a file path at first, the program will not crash
	}
}

