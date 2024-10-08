﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Fundipedia.TechnicalInterview.Model.Supplier;

public class Phone
{
    /// <summary>
    /// Gets or sets the phone id
    /// </summary>
    [Key]
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the phone number
    /// </summary>
    [Phone]
    [MaxLength(10, ErrorMessage = "Phone number can't be more than 10 digit")]
    public string PhoneNumber { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the email is the preferred one or not
    /// </summary>
    public bool IsPreferred { get; set; }
}