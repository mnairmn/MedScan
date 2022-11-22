using System;
using MedScan.Models;

namespace MedScan.Services.Images
{
	public interface IImageHandling //extra I stands for inerface
	{
		string GetFilePath(ImageModel image); //both these methods work by passing in a new instance of an ImageModel object
		string Read(ImageModel image);
	}
}

