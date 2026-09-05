using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Profile.Commands.CreateProfile;
using FluentValidation;

namespace Application.Features.Profile.Commands.UpdateProfileCommand
{
    internal class UpdateProfileValidator: CreateProfileValidator
    {
        public UpdateProfileValidator():base()
        {
            
        }
    }
}
