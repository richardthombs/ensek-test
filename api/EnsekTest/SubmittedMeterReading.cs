namespace EnsekTest
{
	public class SubmittedMeterReading
	{
		public int LineNumber { get; set; }
		public string Content { get; set; }
		public bool Valid { get; set; }
		public MeterReading ParsedReading { get; set; }
		public bool Uploaded { get; set; }
	}
}
