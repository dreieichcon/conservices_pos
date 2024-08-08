using System.Text;
using System.Text.Json.Serialization;
using Innkeep.Api.Models.Pretix.Core;

namespace Innkeep.Api.Models.Pretix.Objects.Sales;

// ReSharper disable once ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
public class PretixSalesItem
{
    
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public required MultiString Name { get; set; }

    [JsonPropertyName("internal_name")]
    public string? InternalName { get; set; }

    [JsonPropertyName("sales_channels")]
    public required IEnumerable<string> SalesChannels { get; set; }

    [JsonPropertyName("default_price")]
    public decimal DefaultPrice { get; set; }
    
    [JsonPropertyName("original_price")]
    public decimal OriginalPrice { get; set; }

    [JsonPropertyName("category")]
    public int? Category { get; set; }

    [JsonPropertyName("active")]
    public bool IsActive { get; set; }

    [JsonPropertyName("description")]
    public MultiString? Description { get; set; }

    [JsonIgnore]
    public string Currency { get; set; } = "EUR";

    [JsonPropertyName("free_price")]
    public bool IsFreePrice { get; set; }
    
    [JsonPropertyName("tax_rate")]
    public decimal TaxRate { get; set; }
    
    [JsonPropertyName("tax_rule")]
    public int? TaxRule { get; set; }

    [JsonPropertyName("admission")]
    public bool Admission { get; set; }

    [JsonPropertyName("personalized")]
    public bool IsPersonalized { get; set; }
    
    [JsonPropertyName("issue_giftcard")]
    public bool IssueGiftCard { get; set; }
    
    [JsonPropertyName("position")]
    public int Position { get; set; }
    
    [JsonPropertyName("picture")]
    public string? Picture { get; set; }

    [JsonPropertyName("available_from")]
    public DateTime? AvailableStart { get; set; }
    
    [JsonPropertyName("available_until")]
    public DateTime? AvailableEnd { get; set; }
    
    [JsonIgnore]
    public object? IsHiddenIfAvailable { get; set; }
    
    [JsonPropertyName("require_voucher")]
    public bool IsVoucherRequired { get; set; }
    
    [JsonPropertyName("hide_without_voucher")]
    public bool IsHiddenWithoutVoucher { get; set; }
    
    [JsonPropertyName("allow_cancel")]
    public bool IsCancelAllowed { get; set; }
    
    [JsonPropertyName("min_per_order")]
    public int? MinimumOrderQuantity { get; set; }
    
    [JsonPropertyName("max_per_order")]
    public int? MaximumOrderQuantity { get; set; }
    
    [JsonPropertyName("checkin_attention")]
    public bool IsCheckinAttentionRequired { get; set; }
    
    [JsonPropertyName("has_variations")]
    public bool HasVariations { get; set; }
    
    [JsonPropertyName("allow_waitinglist")]
    public bool IsWaitingListAllowed { get; set; }
    
    [JsonPropertyName("require_approval")]
    public bool IsApprovalRequired { get; set; }
    
    public override string ToString()
    {
        var sb = new StringBuilder();
        
        sb.AppendLine(new('-', 50));
        sb.AppendLine($"Name: {Name}");
        sb.AppendLine($"Price: {DefaultPrice}");
        sb.AppendLine($"Currency: {Currency}");

        return sb.ToString();
    }
}
