using AutoMapper;
using payment_service.Dtos;
using payment_service.Models;

namespace payment_service.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateSubscriptionDto, Subscription>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId)) // Як мапити UserId
            .ForMember(dest => dest.PlanId, opt => opt.MapFrom(src => src.PlanId)) // Як мапити PlanId
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate)) // Як мапити StartDate
            .ForMember(dest => dest.ExpirationDate, opt => opt.MapFrom(src => src.ExpirationDate)); // Як мапити ExpirationDate
        
        CreateMap<CreateTransactionDto, Transaction>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
            .ForMember(dest => dest.TransactionType, opt => opt.MapFrom(src => src.TransactionType));
    }
}