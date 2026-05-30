namespace Constructix.Domain.Enums;

/// <summary>
/// İstifadəçinin daxil olduğu cihaz tipini müəyyən edən sadalanma.
/// </summary>
public enum DeviceType
{
    /// <summary>
    /// Naməlum və ya təyin olunmayan cihaz.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// Veb brauzer (Web) vasitəsilə daxil olma.
    /// </summary>
    Web = 1,

    /// <summary>
    /// Mobil tətbiq (Mobile) vasitəsilə daxil olma.
    /// </summary>
    Mobile = 2

}