namespace CarVendor.mvc.Models
{
    public class BankTransferModel
    {
        public string BName { get; set; }
        public string BBranch { get; set; }
        public string PaymentDate { get; set; }
        public string TransferNo { get; set; }
        public string InputReferenceNo { get; set; }
        public string ACH { get; set; }
        public string Memo { get; set; }
    }
}