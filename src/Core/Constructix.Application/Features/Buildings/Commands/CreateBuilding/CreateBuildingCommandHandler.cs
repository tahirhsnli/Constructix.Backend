using Constructix.Application.Common.Interfaces.Repositories.Building;

namespace Constructix.Application.Features.Buildings.Commands.CreateBuilding;

public record CreateBuildingCommand(
    string Name,
    string Description,
    string City,
    string District,
    string Street,
    int TotalFloor,
    decimal TotalArea) : IRequest<Guid>;
public class CreateBuildingCommandHandler : IRequestHandler<CreateBuildingCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBuildingReadRepository _buildingReadRepository;

    public CreateBuildingCommandHandler(IUnitOfWork unitOfWork, IBuildingReadRepository buildingReadRepository)
    {
        _unitOfWork = unitOfWork;
        _buildingReadRepository = buildingReadRepository;
    }

    public async Task<Guid> Handle(CreateBuildingCommand request, CancellationToken cancellationToken)
    {
        // 1. Biznes yoxlaması: Eyni adda bina varmı? 
        // QEYD: Əgər IsNameUnique həm Read, həm Write-da varsa, fərqi yoxdur.
        // Amma adətən belə yoxlamalar WriteRepository-də olur.
        var isUnique = await _buildingReadRepository.IsNameUniqueAsync(request.Name, cancellationToken);

        if (!isUnique)
        {
            // Burda sabah özəl Exception-ımızı atacağıq
            throw new Exception($"'{request.Name}' adlı bina artıq mövcuddur.");
        }

        // 2. Domain obyektini (Entity) yaradırıq
        // Sənin Domain qatındakı constructor-a uyğun olaraq:
        var address = new Address(request.City, request.District, request.Street);

        var building = new Building(
            request.Name,
            request.Description,
            address,
            request.TotalFloor,
            request.TotalArea);

        // 3. WriteRepository vasitəsilə əlavə edirik
        await _unitOfWork.Buildings.AddAsync(building, cancellationToken);

        // 4. Dəyişiklikləri Unit of Work ilə bazaya tətbiq edirik (Commit)
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // 5. Yeni yaranan binanın Id-sini qaytarırıq
        return building.Id;
    }
}