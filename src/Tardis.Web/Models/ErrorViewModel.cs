// <copyright file="ErrorViewModel.cs" company="Tardis">
// Copyright © 2025 Tardis
// </copyright>
namespace Tardis.Web.Models;

public class ErrorViewModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(this.RequestId);
}
