using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Projet.Models
{


    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Adrresse Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Mémoriser ce navigateur ?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }

    }

    public class ForgotViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Adresse Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Adresse Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        [Display(Name = "Mémoriser le mot de passe ?")]
        public bool RememberMe { get; set; }
    }
   
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Adresse Email ")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La chaîne {0} doit comporter au moins {2} caractères.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmer le mot de passe ")]
        [Compare("Password", ErrorMessage = "Le mot de passe et le mot de passe de confirmation ne correspondent pas.")]
        public string ConfirmPassword { get; set; }     
   
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Nom D'utilisateur")]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Nom")]
        public string Name { get; set; }


        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Prenom")]
        public string FirstName { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]        
        [Display(Name = "Courrier électronique")]
        public string Email { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "La chaîne {0} doit comporter au moins {2} caractères.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        


        [DataType(DataType.Password)]
        [Display(Name = "Confirmer le mot de passe")]
        [Compare("Password", ErrorMessage = "Le nouveau mot de passe et le mot de passe de confirmation ne correspondent pas.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]        
        [Display(Name = "Adresse Email")]
        public string Email { get; set; }
    }
}
