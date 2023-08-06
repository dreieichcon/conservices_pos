using System.Globalization;
using System.Text;
using System.Text.Json.Serialization;
using Innkeep.Api.Pretix.Models.Base;
using Innkeep.Core.Utilities;

namespace Innkeep.Api.Pretix.Models.Objects;

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
    public string DefaultPriceString
    {
        get => DefaultPrice.ToString(CultureInfo.InvariantCulture);
        set
        {
            if (decimal.TryParse(value, out var price))
                DefaultPrice = price;
        }
    }

    [JsonIgnore]
    public decimal DefaultPrice { get; set; }

    [JsonPropertyName("original_price")]
    public string? OriginalPriceString
    {
        get => OriginalPrice.ToString(CultureInfo.InvariantCulture);
        set
        {
            if (decimal.TryParse(value, out var price))
                OriginalPrice = price;
        }
    }
    
    
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
    public string TaxRateString { 
        get => TaxRate.ToString(CultureInfo.InvariantCulture);
        set
        {
            if (decimal.TryParse(value, out var price))
                TaxRate = price;
        } 
    }
    
    [JsonIgnore]
    public decimal TaxRate { get; set; }
    
    [JsonPropertyName("tax_rule")]
    public int? TaxRule { get; set; }

    [JsonPropertyName("admission")]
    public bool Admission { get; set; }

    [JsonPropertyName("personalized")]
    public bool IsPersonalized { get; set; }
    
    [JsonPropertyName("issue_giftcard")]
    public bool IssueGiftCard { get; set; }
    
    // [JsonPropertyName("meta_data")]
    // public object meta_data { get; set; }
    
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
    
    // [JsonPropertyName("generate_tickets")]
    // public object generate_tickets { get; set; }
    
    [JsonPropertyName("allow_waitinglist")]
    public bool IsWaitingListAllowed { get; set; }
    
    // [JsonPropertyName("show_quota_left")]
    // public object show_quota_left { get; set; }
    //
    
    [JsonPropertyName("require_approval")]
    public bool IsApprovalRequired { get; set; }
    
    // [JsonPropertyName("require_bundling")]
    // public bool require_bundling { get; set; }
    //
    
    // [JsonPropertyName("require_membership")]
    // public bool require_membership { get; set; }
    //
    
    // [JsonPropertyName("require_membership_types")]
    // public IList<object> require_membership_types { get; set; }
    //
    // [JsonPropertyName("grant_membership_type")]
    // public object grant_membership_type { get; set; }
    //
    // [JsonPropertyName("grant_membership_duration_like_event")]
    // public bool grant_membership_duration_like_event { get; set; }
    //
    // [JsonPropertyName("grant_membership_duration_days")]
    // public int grant_membership_duration_days { get; set; }
    //
    // [JsonPropertyName("grant_membership_duration_months")]
    // public int grant_membership_duration_months { get; set; }
    //
    // [JsonPropertyName("validity_fixed_from")]
    // public object validity_fixed_from { get; set; }
    //
    // [JsonPropertyName("validity_fixed_until")]
    // public object validity_fixed_until { get; set; }
    //
    // [JsonPropertyName("validity_dynamic_duration_minutes")]
    // public object validity_dynamic_duration_minutes { get; set; }
    //
    // [JsonPropertyName("validity_dynamic_duration_hours")]
    // public object validity_dynamic_duration_hours { get; set; }
    //
    // [JsonPropertyName("validity_dynamic_duration_days")]
    // public object validity_dynamic_duration_days { get; set; }
    //
    // [JsonPropertyName("validity_dynamic_duration_months")]
    // public object validity_dynamic_duration_months { get; set; }
    //
    // [JsonPropertyName("validity_dynamic_start_choice")]
    // public bool validity_dynamic_start_choice { get; set; }
    //
    // [JsonPropertyName("validity_dynamic_start_choice_day_limit")]
    // public object validity_dynamic_start_choice_day_limit { get; set; }
    
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
