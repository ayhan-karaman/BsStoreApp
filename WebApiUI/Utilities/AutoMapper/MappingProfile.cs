using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace WebApiUI.Utilities.AutoMapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<BookForUpdateDto, Book>().ReverseMap();
            CreateMap<Book, BookDto>();
            CreateMap<BookForInsertionDto, Book>();
        }
    }
}