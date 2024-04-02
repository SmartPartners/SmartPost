using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartPost.DataAccess.Interfaces.CancelOrders;
using SmartPost.Domain.Configurations;
using SmartPost.Domain.Entities.CancelOrders;
using SmartPost.Service.Commons.Exceptions;
using SmartPost.Service.Commons.Extensions;
using SmartPost.Service.DTOs.CancelOrders;
using SmartPost.Service.Interfaces.CancelOrders;

namespace SmartPost.Service.Services.CancelOrders;

/// <summary>
/// Service tugagach Controller yozish
/// </summary>
public class CancelOrderService : ICancelOrderService
{
    private readonly ICancelOrderRepository _cancelOrderRepository;
    private readonly IMapper _mapper;

    public CancelOrderService(ICancelOrderRepository cancelOrderRepository, IMapper mapper)
    {
        _mapper = mapper;
        _cancelOrderRepository = cancelOrderRepository;
    }

    public async Task<bool> ReamoveAsync(long id)
    {
        var order = await _cancelOrderRepository.SelectAll()
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();
        if (order is null)
            throw new CustomException(404, "Bekor qilingan buyurtma topilmadi.");

        await _cancelOrderRepository.DeleteAsync(id);

        return true;
    }

    public async Task<IEnumerable<CancelOrderForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var orders = await _cancelOrderRepository.SelectAll()
                .AsNoTracking()
                .ToPagedList(@params)
                .ToListAsync();

        return _mapper.Map<IEnumerable<CancelOrderForResultDto>>(orders);
    }

    public async Task<CancelOrderForResultDto> RetrieveByIdAsync(long id)
    {
        var order = await _cancelOrderRepository.SelectAll()
                .Where(c => c.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        if (order is null)
            throw new CustomException(404, "Bekor qilingan buyurtma topilmadi.");

        return _mapper.Map<CancelOrderForResultDto>(order);
    }
}
