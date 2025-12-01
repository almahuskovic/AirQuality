namespace Models.Dto
{
    public class CityCsv
    {
        public string city { get; set; }
        public string country { get; set; }
        public decimal? population { get; set; }
        public string? capital { get; set; }  // admin, primary, minor
        public double lat { get; set; }
        public double lng { get; set; }
        public string iso2 { get; set; }
    }
}
