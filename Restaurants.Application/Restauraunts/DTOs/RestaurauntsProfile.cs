// <copyright file="RestaurauntsProfile.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Restaurants.Application.Restauraunts.DTOs
{
    using AutoMapper;
    using Restaurants.Application.Restauraunts.Commands.CreateRestauraunt;
    using Restaurants.Application.Restauraunts.Commands.UpdateRestauraunt;
    using Restaurants.Domain.Entities;

    public class RestaurauntsProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RestaurauntsProfile"/> class.
        /// </summary>
        public RestaurauntsProfile()
        {
            this.CreateMap<CreateRestaurauntCommand, Restaurant>()
                .ForMember(
                    a => a.Address,
                    opt =>
                        opt.MapFrom(src => new Address
                        {
                            City = src.City,
                            Street = src.Street,
                            PostalCode = src.PostalCode,
                        })
                );

            this.CreateMap<UpdateRestaurauntCommand, Restaurant>();

            this.CreateMap<Restaurant, RestaurauntDTO>()
                .ForMember(
                    d => d.City,
                    opt => opt.MapFrom(s => s.Address == null ? null : s.Address.City)
                )
                .ForMember(
                    d => d.Street,
                    opt => opt.MapFrom(s => s.Address == null ? null : s.Address.Street)
                )
                .ForMember(
                    d => d.PostalCode,
                    opt => opt.MapFrom(s => s.Address == null ? null : s.Address.PostalCode)
                )
                .ForMember(d => d.MenuItems, opt => opt.MapFrom(s => s.MenuItems));
        }
    }
}
