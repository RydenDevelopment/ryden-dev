using ryden_dev.Website.Models.Interfaces;

namespace ryden_dev.Website.Services.Interfaces;

public interface ILocalizationService
{
    public IBaseViewModel SetLocalizationStringsForViewModel();
}