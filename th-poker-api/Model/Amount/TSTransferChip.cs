using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class TSTransferChip
{
    [Key, Column(TypeName = "nvarchar(100)")]
    public string TransferID { get; set; }
    public float Amount { get; set; }
    public string Sender { get; set; }
    public string Receiver { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
}
