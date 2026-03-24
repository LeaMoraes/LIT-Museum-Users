using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lit_Museum_Users.Application.Dtos
{
    /// <summary>
    /// Informações para adicionar um novo Usuario
    /// </summary>
    public record UserCreateDto
    {
        /// <summary>
        /// Nome do Usuário
        /// </summary>
        [Required(ErrorMessage = "O Nome do usuário é obrigatório.")]
        [StringLength(100)]
        public string Name { get; init; }
        /// <summary>
        /// Email do Usuário
        /// </summary>
        [Required(ErrorMessage = "O email do usuário é obrigatório.")]
        [StringLength(100)]
        public string Email { get; init; }
        /// <summary>
        /// Senha do Usuario para Acesso
        /// 
        /// Requisitos:
        /// * Minimo `8` caracteres
        /// * Ter `1` letra minúscula
        /// * Ter `1` letra maiúscula
        /// * Ter `1` caracter especial
        /// </summary>
        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(100)]
        public string Password { get; init; }

    }
}
