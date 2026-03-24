using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lit_Museum_Users.Application.Dtos
{
    public record UserRespondeDto(int Id, string Name, string Email, string Role);
}
