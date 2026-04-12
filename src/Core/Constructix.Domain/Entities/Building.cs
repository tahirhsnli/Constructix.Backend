namespace Constructix.Domain.Entities;
public sealed class Building : BaseEntity // <-- 1. Bünövrəni götürdü
{
    // Property-lər 'private set'dir - kənardan (məsələn, Controller-dən) dəyişdirilə bilməz
    public string Name { get; private set; } = null!;
    public string? Description { get; private set; }
    public Address Address { get; private set; } = null!; // <-- 2. Detalı götürdü
    public int TotalFloors { get; private set; }
    public decimal TotalArea { get; private set; }
    public BuildingStatus Status { get; private set; }

    // EF Core üçün gizli boş constructor
    private Building() { }

    // 3. Constructor: Binanı yaratmağın tək yolu budur
    public Building(string name, string? description, Address address, int totalFloors, decimal totalArea)
    {
        // 4. Guard: Yanlış məlumatla bina yaradılmasın
        Guard.AgainstNullOrEmpty(name, nameof(Name));
        Guard.AgainstZeroOrNegative(totalFloors, nameof(TotalFloors));
        Guard.AgainstZeroOrNegative(totalArea, nameof(TotalArea));

        Name = name;
        Description = description;
        Address = address;
        TotalFloors = totalFloors;
        TotalArea = totalArea;
        Status = BuildingStatus.Planning; // Yeni bina həmişə 'Planning' ilə başlayır
    }

    // 5. Biznes Metodları: Binanın vəziyyətini dəyişməyin tək yolu
    public void StartConstruction()
    {
        if (Status != BuildingStatus.Planning)
            throw new InvalidOperationException("Yalnız planlaşdırılan bina tikintiyə başlaya bilər.");

        Status = BuildingStatus.UnderConstruction;
    }

    public void CompleteBuilding() => Status = BuildingStatus.Completed;
}