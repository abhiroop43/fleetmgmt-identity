using System.Linq;
using FleetMgmt.Identity.Domain.Dto;
using FleetMgmt.Identity.Domain.Interfaces;
using FluentValidation;

namespace FleetMgmt.Identity.Domain.Validators
{
    public class UserRegistrationRequestValidator : BaseValidator<UserRegistrationRequestDto>
    {
        private readonly IUserRepository _userRepository;
        public UserRegistrationRequestValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            RuleFor(user => user.FirstName)
                .NotEmpty().WithMessage("FirstName required")
                .Matches(@"[\p{L} ]+$").WithErrorCode("InvalidFirstName").WithMessage("Please provide valid FirstName.")
                .MaximumLength(255).WithMessage("FirstName  should not be greater 255 characters..")
                .MinimumLength(2).WithMessage("FirstName at least 2 characters..");

            RuleFor(user => user.LastName)
                .Matches(@"[\p{L} ]+$").WithErrorCode("InvalidLastName").WithMessage("Please provide valid LastName.")
                .MaximumLength(255).WithMessage("LastName  should not be greater 255 characters..")
                .MinimumLength(2).WithMessage("LastName at least 2 characters..");

            RuleFor(user => user.UserName)
                .NotEmpty().WithMessage("UserName required")
                .Matches(@"[a-zA-Z0-9_.@]*").WithErrorCode("InvalidUserName").WithMessage("Please provide valid UserName.")
                .Must(x =>
                {
                    var valueExists = _userRepository.GetReadOnly(user => user.USERNAME == x.ToLowerInvariant());

                    return (valueExists == null);
                }).WithMessage("Username already exists, please choose a different one")
                .MaximumLength(65).WithMessage("UserName  should not be greater 65 characters..")
                .MinimumLength(7).WithMessage("UserName at least 7 characters..");

            RuleFor(user => user.UserEmail)
                .NotEmpty().WithMessage("Email required")
                .Matches(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$").WithErrorCode("InvalidEmail").WithMessage("Please provide valid email address register with your organization.")
                .EmailAddress().WithMessage("Please provide valid Email.")
                .Must(x =>
                {
                    var valueExists = _userRepository.GetReadOnly(user => user.USEREMAIL == x.ToLowerInvariant());

                    return (valueExists == null);
                }).WithMessage("User Email already exists, please provide a different one")
                .MaximumLength(65).WithMessage("Email  should not be greater 65 characters..")
                .MinimumLength(7).WithMessage("Email at least 7 characters..");

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("Password required")
                .Matches(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^\da-zA-Z])(.{8,15})$").WithErrorCode("InvalidPassword").WithMessage("Please provide valid Password with alphabets as uppercase, lowercase, number and special character.")
                .MaximumLength(15).WithMessage("Password  should not be greater 15 characters..")
                .MinimumLength(8).WithMessage("Password at least 8 characters..");

            RuleFor(user => user.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm Password required")
                .Equal(x => x.Password).WithErrorCode("InvalidConfirmPassword").WithMessage("Password and Confirm Password must match")
                .MaximumLength(15).WithMessage("Confirm Password  should not be greater 15 characters..")
                .MinimumLength(8).WithMessage("Confirm Password at least 8 characters..");

            RuleFor(user => user.Telephone)
               .Matches(@"^0[1-6]{1}[0-9]{7}$").WithErrorCode("InvalidTelephone").WithMessage("Please provide valid Telephone.");

            RuleFor(user => user.Mobile)
                .NotEmpty().WithMessage("Mobile is required")
                .Matches(@"^05[0-9]{8}$").WithErrorCode("InvalidMobile").WithMessage("Please provide valid Mobile.")
                .Must(x =>
                {
                    var valueExists = _userRepository.GetReadOnly(user => user.MOBILE == x);

                    return (valueExists == null);
                }).WithMessage("User Mobile already exists, please provide a different one");

            RuleFor(user => user.TermsAccepted)
                .Equal(true).WithMessage("Terms and Conditions must be accepted");

            RuleFor(group => group.Address)
                .Must(addr => !IsLengthValidation(addr, 255))
                .MaximumLength(255).WithMessage("Please provide valid Address.");
            
            RuleFor(group => group.Address1)
                .Must(addr => !IsLengthValidation(addr, 255))
                .MaximumLength(255).WithMessage("Please provide valid Address1.");
            
            RuleFor(group => group.Address2)
                .Must(addr => !IsLengthValidation(addr, 255))
                .MaximumLength(255).WithMessage("Please provide valid Address2.");
        }
    }
}
