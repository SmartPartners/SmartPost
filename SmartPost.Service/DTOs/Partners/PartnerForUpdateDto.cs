﻿namespace SmartPost.Service.DTOs.Partners;

public record PartnerForUpdateDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public decimal Debt { get; set; }
    public decimal Paid { get; set; }
}
