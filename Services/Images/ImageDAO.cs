using System;
using MedScan.Models;
using SkiaSharp;
using IronOcr;

namespace MedScan.Services.Images
{
    public class ImageDAO : IImageHandling
    {
        public string GetFilePath(ImageModel image)
        {
            return image.FilePath;
        }

        public string Read(ImageModel image)
        {
            List<BoxModel> listOfBoxes = new List<BoxModel>();
            //instantiate new list to store boxes in
            BoxModel box1 = new BoxModel(5, 4, 40, 70);
            //nums in brackets are assigned to properties of a BoxModel in order: xstart, ystart, width, height.
            BoxModel box2 = new BoxModel(6, 5, 20, 40);
            BoxModel box3 = new BoxModel(30, 5, 30, 70);
            //hard coded data boxes being instantiated with a wide range of data to test the system's robustness
            listOfBoxes.Add(box1);
            listOfBoxes.Add(box2);
            listOfBoxes.Add(box3);
            //add all boxes to the list of boxes
            string readData = "";
            //empty string variable to store data in later

            var ocr = new IronTesseract();
            //new IronTesseract object instantiated - this is the key engine for the OCR

            foreach (BoxModel box in listOfBoxes) //loops until every box has had the following done to it
            {              
                using (var Input = new OcrInput())
                {
                    var ContentArea = new CropRectangle(x: box.XStart, y: box.YStart, height: box.Height, width: box.Width);
                    // creates an area in the shape of a rectangle that we will later use to narrow our search in the image
                    Input.AddImage($"{image.FilePath}", ContentArea);
                    //$ represents interpolated strings - the FilePath property is passed in, essentially referencing the image we want to be read
                    // ContentArea is passed into the Input as well - this is where we are looking for characters with the OCR
                    var result = ocr.Read(Input);

                    foreach (var value in result.Text)
                    {
                        readData += Convert.ToString(value);
                        //value is of result.Text type, so before adding to a string, value must be converted to a string as well
                    }
                }
            }

            return readData;
        }
    }
}

