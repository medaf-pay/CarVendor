namespace CarVendor.mvc.Models
{
    public class CustomerInfoModel
    {
        public int Individually { get; set; }
        public string OrgnizationName { get; set; }
        public string OrgnizationSite { get; set; }
        public string RegistrationNo { get; set; }
        public string FName { get; set; }
        public string MName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string MainAddress { get; set; }
        public string DeliveryAddress { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string NID { get; internal set; }
        public string Password { get; internal set; }
        public long Id { get; internal set; }
    }
}