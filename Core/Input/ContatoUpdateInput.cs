using System.ComponentModel.DataAnnotations;

namespace Core.Input
{
    public class ContatoUpdateInput
    {
        [Range(1, int.MaxValue, ErrorMessage = "Id deve ser maior que zero")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo nome é obrigatório")]
        [StringLength(100, ErrorMessage = "Campo nome deve ter no máximo 100 caracteres")]
        public required string Nome { get; set; }

        [Required(ErrorMessage = "Campo DDD é obrigatório")]
        [Range(11, 99, ErrorMessage = "Campo DDD deve estar entre 11 e 99")]
        public int DDD { get; set; }

        [Required(ErrorMessage = "Campo telefone é obrigatório")]
        [StringLength(9, ErrorMessage = "Campo telefone deve ter no máximo 9 caracteres")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Campo e-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "Campo e-mail inválido")]
        public string Email { get; set; }
    }
}
