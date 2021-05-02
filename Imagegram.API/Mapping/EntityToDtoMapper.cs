using AutoMapper;
using Imagegram.Domain.AccountsEntity;
using Imagegram.Domain.CommentsEntity;
using Imagegram.Domain.PostsEntity;
using Imagegram.Models.DTOs;

namespace Imagegram.API.Mapping
{
    public class EntityToDtoMapper : Profile
    {
        public EntityToDtoMapper()
        {
            CreateMap<Accounts, AccountDto>();
            CreateMap<Accounts, CommentsDto>();
            CreateMap<Comments, CommentsDto>();
            CreateMap<Posts, PostDto>();
        }
    }
}
