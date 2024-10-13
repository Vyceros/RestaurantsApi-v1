// <copyright file="MenuItemsProfile.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using AutoMapper;
using Restaurants.Application.MenuItems.Commands.CreateMenuItem;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.MenuItems
{
    public class MenuItemsProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItemsProfile"/> class.
        /// </summary>
        public MenuItemsProfile()
        {
            CreateMap<CreateMenuItemCommand, MenuItem>();
            CreateMap<MenuItem, MenuItemDTO>();
        }
    }
}
