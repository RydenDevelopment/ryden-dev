namespace ryden_dev.Website.Enums;

public enum LanguageCodeEnum
{
    Sv,
    En
}

public static class LanguageCodeEnumExtension
{
    public static string ToString(this LanguageCodeEnum languageCodeEnum)
    {
        return languageCodeEnum switch
        {
            LanguageCodeEnum.Sv => "sv",
            LanguageCodeEnum.En => "en",
            _ => throw new ArgumentOutOfRangeException(nameof(languageCodeEnum), languageCodeEnum, null)
        };
    }
}