using AutoMapper;
using PRN222.Kahoot.Repository.Models;
using PRN222.Kahoot.Service.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Kahoot.Service.Mappers
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<QuizSessionModel, QuizSession>().ReverseMap();
            CreateMap<ResponseModel, Response>().ReverseMap();
            CreateMap<UserModel, User>().ReverseMap();
            CreateMap<QuestionModel, Question>().ReverseMap();
            CreateMap<QuestionSessionModel, QuestionSession>().ReverseMap();
            CreateMap<QuizModel, Quiz>().ReverseMap();
            CreateMap<ParticipantModel, Participant>().ReverseMap();
        }
    }
}
