using System.ComponentModel.DataAnnotations;

namespace ShopperCart.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}