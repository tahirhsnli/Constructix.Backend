namespace Constructix.Domain.Enums;
/// <summary>
/// Sistemdəki istifadəçi tiplərini və rollarını təyin edən sadalanma.
/// </summary>
public enum UserType
{
    /// <summary> 
    /// Sistemin sahibi və yaradıcısı (Tahir). 
    /// Bütün şirkətləri, bütün binaları və limitləri idarə edir.
    /// </summary>
    SystemAdmin = 1,

    /// <summary> 
    /// Şirkət və Layihə rəhbərliyi (Məs: Kristal Abşeron rəhbərliyi). 
    /// Maliyyə hesabatlarına və binanın ümumi işinə nəzarət edir.
    /// </summary>
    Management = 2,

    /// <summary> 
    /// Sahədə çalışan professional heyət (Usta, Mühafizə, Mühasib, Reseption). 
    /// Gündəlik texniki və əməliyyat işlərini icra edir.
    /// </summary>
    Staff = 3,

    /// <summary> 
    /// Mülkiyyət və yaşayış hüququ olan şəxslər (Sakin, Ev sahibi, Obyekt sahibi). 
    /// Xidmətdən yararlanır və ödəniş edir.
    /// </summary>
    Resident = 4,

    /// <summary> 
    /// Kənar tərəfdaşlar və xidmət təminatçıları. 
    /// Məsələn: Lift servis şirkəti, Zibil daşıyan podratçı şirkət və ya Dövlət Müfəttişliyi.
    /// Yalnız onlara aid olan hissəyə məhdud baxış icazələri olur.
    /// </summary>
    Partner = 5
}