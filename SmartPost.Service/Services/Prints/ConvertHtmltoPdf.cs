using System;
using System.Collections.Generic;
using NReco.PdfGenerator;

namespace SmartPost.Service.Services.Prints
{
	internal class ConvertHtmltoPdf
	{
		public byte[] ConvertHtmlToPdf(string html, Dictionary<string, string> replacements)
		{
			foreach (var item in replacements)
			{
				html = html.Replace(item.Key, item.Value);
			}

			return ConvertHtmlToPdf(html);
		}

		public byte[] ConvertHtmlToPdf(string html)
		{
			HtmlToPdfConverter htmlToPdf = new HtmlToPdfConverter();
			return htmlToPdf.GeneratePdf(html);
		}
	}

	public class HtmlToPdfDto
	{
		public float PageWidth { get; set; }
		public float Zoom { get; set; }
		public string CustomWkHtmlArgs { get; set; }
		public PageMarginsDto Margins { get; set; }
		public PageSizeEnum PageSize { get; set; }
		public PageOrientationEnum PageOrientation { get; set; }
		public float PageHeight { get; set; }
		public string Html { get; set; }
	}

	public class PageMarginsDto
	{
		public float Top { get; set; }
		public float Left { get; set; }
		public float Right { get; set; }
		public float Bottom { get; set; }
	}

	public enum PageSizeEnum
	{
		A3 = 0,
		A4 = 1,
		A5 = 2,
		Letter = 3,
		Legal = 4,
	}

	public enum PageOrientationEnum
	{
		Portrait = 0,
		Landscape = 1,
	}
}
