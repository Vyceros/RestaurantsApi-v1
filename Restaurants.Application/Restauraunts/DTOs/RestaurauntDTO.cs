// <copyright file="RestaurauntDTO.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Restaurants.Application.MenuItems;

namespace Restaurants.Application.Restauraunts.DTOs
{
    public class RestaurauntDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool HasDelivery { get; set; }
        public bool IsOpen { get; set; }

        public string? ContactNumber { get; set; }
        public string? Socials { get; set; }

        public string? City { get; set; }
        public string? Street { get; set; }
        public string? PostalCode { get; set; }

        public List<MenuItemDTO>? MenuItems { get; set; } = [];
    }
}
