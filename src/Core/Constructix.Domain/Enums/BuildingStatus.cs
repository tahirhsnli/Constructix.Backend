namespace Constructix.Domain.Enums;

public enum BuildingStatus
{
    /// <summary>
    /// Bina hələ planlaşdırma və layihələndirmə mərhələsindədir.
    /// </summary>
    Planning = 1,

    /// <summary>
    /// Tikinti rəsmi olaraq başlayıb və davam edir.
    /// </summary>
    UnderConstruction = 2,

    /// <summary>
    /// Tikinti işləri tam başa çatıb, bina təhvil verilməyə hazırdır.
    /// </summary>
    Completed = 3,

    /// <summary>
    /// Bina artıq istifadədədir və texniki xidmət mərhələsindədir.
    /// </summary>
    Maintenance = 4,

    /// <summary>
    /// Tikinti hər hansı bir səbəbdən (maliyyə, hüquqi və s.) dayandırılıb.
    /// </summary>
    Suspended = 5
}