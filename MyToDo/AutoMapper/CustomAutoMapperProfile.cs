using AutoMapper;
using MyToDo.API.Entity;
using MyToDo.Common.Models;


namespace MyToDo.AutoMapper
{
    public class CustomAutoMapperProfile:Profile
    {
        public CustomAutoMapperProfile()
        {
            CreateMap<ToDo, ToDoDto>();
            CreateMap<ToDoDto, ToDo>();
            CreateMap<Memo,MemoDto>();
            CreateMap<MemoDto,Memo>();
        }
    }
}
