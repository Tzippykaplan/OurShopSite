using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    using System.ComponentModel.DataAnnotations;

    public record UserByIdDto(
        [ Required, EmailAddress] string email,
        [ Required, MinLength(1)] string firstName,
        [Required, MinLength(1)] string lastName);

    public record AddUserDto(
        [Required, EmailAddress] string email,
        [Required, MinLength(1)] string firstName,
        [Required, MinLength(1)] string lastName,
        [Required, MinLength(1)] string password);

    public record ReturnPostUserDto(    
        [ Required, EmailAddress] string email,
        [Required, MinLength(1)] string firstName,
        [Required, MinLength(1)] string lastName);

    public record ReturnLoginUserDto(
        [Required] int userId,
        [Required, EmailAddress] string email,
        [Required, MinLength(1)] string firstName,
        [Required, MinLength(1)] string lastName)
        ;
    public record LoginUserDto(
     [Required, EmailAddress] string email,
     [Required, MinLength(1)] string password);
}
