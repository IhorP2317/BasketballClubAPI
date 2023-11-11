using AutoMapper;
using BasketballClubAPI.Dto;
using BasketballClubAPI.Models;
using Microsoft.OpenApi.Extensions;
using System.Net.NetworkInformation;
using static System.Net.Mime.MediaTypeNames;

namespace BasketballClubAPI.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // CreateMap for Player to PlayerDto
            CreateMap<Player, PlayerDto>()
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position.GetStringValue()));

            // CreateMap for PlayerDto to Player
            CreateMap<PlayerDto, Player>()
                .ForMember(dest => dest.Position,
                    opt => opt.MapFrom(src => EnumExtension.ParseEnum<Position>(src.Position)));

            CreateMap<Coach, CoachDto>();
            CreateMap<CoachDto, Coach>();
            CreateMap<Team, TeamDto>();
            CreateMap<TeamDto, Team>();
            CreateMap<Match, MatchDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.GetStringValue()));

            // CreateMap for PlayerDto to Player
            CreateMap<MatchDto, Match>()
            .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => EnumExtension.ParseEnum<MatchStatus>(src.Status)));
            CreateMap<Statistic, StatisticDto>();
            CreateMap<StatisticDto, Statistic>();
        }
    }
}
