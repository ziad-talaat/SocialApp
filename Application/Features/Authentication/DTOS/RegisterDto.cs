using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Application.Features.Authentication.DTOS
{
    public sealed class RegisterDto
    {

        public string? ImageUrl { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public  required  string  Email { get; set; }
        public  required  string  Password { get; set; }
        [Compare(nameof(Password),ErrorMessage ="password doesn't match")]
        public  required  string  ConfirmPassword { get; set; }
        public string?  Description { get; set; }
        public required string  ProfileName { get; set; }
        public required string  Country { get; set; }
        public Genders  Gender { get; set; }
    }
}
