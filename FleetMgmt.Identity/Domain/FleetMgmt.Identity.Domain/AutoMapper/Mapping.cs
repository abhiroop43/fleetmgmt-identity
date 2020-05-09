using AutoMapper;
using FleetMgmt.Identity.Domain.Dto;
using FleetMgmt.Identity.Domain.Entities;

namespace FleetMgmt.Identity.Domain.AutoMapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<UserRegistrationRequestDto, IM_USERS>()
                // .ForMember(des => des.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(des => des.FIRSTNAME, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(des => des.LASTNAME, opt => opt.MapFrom(src => src.LastName))
                .ForMember(des => des.USERNAME, opt => opt.MapFrom(src => src.UserName))
                .ForMember(des => des.USEREMAIL, opt => opt.MapFrom(src => src.UserEmail))
                .ForMember(des => des.PASSWORD, opt => opt.MapFrom(src => src.Password))
                .ForMember(des => des.TELEPHONE, opt => opt.MapFrom(src => src.Telephone))
                .ForMember(des => des.ADDRESS, opt => opt.MapFrom(src => src.Address))
                .ForMember(des => des.ADDRESS1, opt => opt.MapFrom(src => src.Address1))
                .ForMember(des => des.ADDRESS2, opt => opt.MapFrom(src => src.Address2))
                .ForMember(des => des.REMARKS, opt => opt.MapFrom(src => src.Remarks))
                .ForMember(des => des.TERMS_ACCEPTED, opt => opt.MapFrom(src => src.TermsAccepted));
        }
    }
}