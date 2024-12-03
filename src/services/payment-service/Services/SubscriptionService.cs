using AutoMapper;
using payment_service.Dtos;
using payment_service.Interfaces;
using payment_service.Models;
using payment_service.Repositories;

namespace payment_service.Services;

public class SubscriptionService : ISubscriptionService
{
    private readonly ISubscriptionRepository _repository;
    private readonly IMapper _mapper;

    public SubscriptionService(ISubscriptionRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<SubscriptionDto> CreateSubscriptionAsync(CreateSubscriptionDto dto)
    {
        var subscription = _mapper.Map<Subscription>(dto);
        await _repository.AddAsync(subscription);
        return _mapper.Map<SubscriptionDto>(subscription);
    }

    public async Task<SubscriptionDto> GetSubscriptionByIdAsync(Guid id)
    {
        var subscription = await _repository.GetByIdAsync(id);
        return subscription != null ? _mapper.Map<SubscriptionDto>(subscription) : null;
    }

    public async Task<bool> UpdateSubscriptionAsync(Guid id, UpdateSubscriptionDto dto)
    {
        var subscription = await _repository.GetByIdAsync(id);
        if (subscription == null)
            return false;

        _mapper.Map(dto, subscription);
        await _repository.UpdateAsync(subscription);
        return true;
    }

    public async Task<bool> DeleteSubscriptionAsync(Guid id)
    {
        var subscription = await _repository.GetByIdAsync(id);
        if (subscription == null)
            return false;

        await _repository.DeleteAsync(subscription);
        return true;
    }
}