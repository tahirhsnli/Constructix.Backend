namespace Constructix.Domain.Common;
public static class Guard
{
    // 1. Generic Null Yoxlaması (Bütün obyekt tipləri üçün)
    public static void AgainstNull<T>(T value, string name) where T : class
    {
        if (value == null)
            throw new ArgumentNullException(name, $"{name} mütləq qeyd olunmalıdır.");
    }

    // 2. String Yoxlaması (Boşluq və null üçün)
    public static void AgainstNullOrEmpty(string value, string name)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException($"{name} boş və ya boşluqdan ibarət ola bilməz.");
    }

    // 3. Mənfi rəqəm yoxlaması (decimal)
    public static void AgainstNegative(decimal value, string name)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException(name, $"{name} mənfi ola bilməz.");
    }

    // 4. Sıfır və ya mənfi yoxlaması (int/double/decimal üçün istifadə oluna bilər)
    public static void AgainstZeroOrNegative(double value, string name)
    {
        if (value <= 0)
            throw new ArgumentOutOfRangeException(name, $"{name} sıfır və ya mənfi ola bilməz.");
    }

    // 5. Aralıq yoxlaması
    public static void AgainstOutOfRange(int value, int min, int max, string name)
    {
        if (value < min || value > max)
            throw new ArgumentOutOfRangeException(name, $"{name} dəyəri {min} və {max} aralığında olmalıdır.");
    }

    // 6. Tarix yoxlaması
    public static void AgainstPastDate(DateTime value, string name)
    {
        if (value.Date < DateTime.UtcNow.Date)
            throw new ArgumentException($"{name} keçmiş bir tarix ola bilməz.");
    }
}