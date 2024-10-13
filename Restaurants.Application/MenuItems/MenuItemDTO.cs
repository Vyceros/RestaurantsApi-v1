// <copyright file="MenuItemDTO.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Restaurants.Application.MenuItems
{
    public class MenuItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }

        public int RestaurantId { get; set; }
        public int? KiloCalories { get; set; }
    }
}
