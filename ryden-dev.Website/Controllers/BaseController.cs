using Microsoft.AspNetCore.Mvc;
using ryden_dev.Website.Enums;

namespace ryden_dev.Website.Controllers;

public abstract class BaseController : Controller
{
    public LanguageCodeEnum LanguageCode
    {
        get
        {
            var languageCodeString = ControllerContext.RouteData.Values["languageCode"] ?? "sv";

            return (string)languageCodeString switch
            {
                "sv" => LanguageCodeEnum.Sv,
                "en" => LanguageCodeEnum.En,
                _ => LanguageCodeEnum.Sv
            };
        }
    }
}