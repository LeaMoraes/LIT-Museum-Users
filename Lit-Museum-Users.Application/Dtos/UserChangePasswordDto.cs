using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lit_Museum_Users.Application.Dtos
{
    public class UserChangePasswordDto
    {

        /// <summary>
        /// Senha Atual do Usuario
        /// </summary>
        [Required(ErrorMessage = "A senha atual é obrigatória.")]
        [StringLength(100)]
        public string Password { get; set; }

        /// <summary>
        /// Senha Nova do Usuario
        /// 
        /// Requisitos:
        /// * Minimo `8` caracteres
        /// * Ter `1` letra minúscula
        /// * Ter `1` letra maiúscula
        /// * Ter `1` caracter especial
        /// </summary>
        [Required(ErrorMessage = "A nova senha é obrigatória.")]
        [StringLength(100)]
        public string NewPassword { get; set; }

    }
}
