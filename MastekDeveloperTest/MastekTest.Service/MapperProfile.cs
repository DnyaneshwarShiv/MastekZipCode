using AutoMapper;
using MastekDeveloperTest.DTO;
using MastekTest.DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MastekDeveloperTest.Service
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<MastekAreaModel, MastekAreaDto>().IgnoreAllNonExisting().ReverseMap();
            
        }
        public static MapperConfiguration InitializeAutoMapper()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {

            });
            return config;
        }
    }
    public class NoMapAttribute : System.Attribute
    {
    }
    public static class IgnoreProperty
    {
        public static IMappingExpression<TSource, TDestination>
                IgnoreAllNonExisting<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
        {
            var sourceType = typeof(TSource);
            foreach (var property in sourceType.GetProperties())
            {
                PropertyDescriptor descriptor = TypeDescriptor.GetProperties(sourceType)[property.Name];
                NoMapAttribute attribute = (NoMapAttribute)descriptor.Attributes[typeof(NoMapAttribute)];
                if (attribute != null)
                    expression.ForMember(property.Name, opt => opt.Ignore());
            }
            return expression;
        }
    }
}
