using System.ComponentModel.DataAnnotations;

namespace SchollApi;

public class LoginModel
{
    [Required(ErrorMessage = "Email é obrigatório")]
    [EmailAddress(ErrorMessage = "Formato de e-mail inválido")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Senha é obrigatória")]
    [StringLength(20, ErrorMessage = "A senha deve ter no mínimo {2} e no máximo {1} caracteres.", MinimumLength = 10)]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
