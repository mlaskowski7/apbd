namespace Tutorial9.Application.Mappers;

public interface IMapper<in TEntity, out TResponseDto>
{
    TResponseDto MapEntityToResponse(TEntity entity);
}