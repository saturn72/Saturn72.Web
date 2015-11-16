using Saturn72.Core;
using Saturn72.Web.Framework.Mvc;
using AutoMapper;

namespace Saturn72.Web.Framework.Extensions
{
    public static class AutoMapperExtensions
    {
        public static TDestination ToModel<TSource, TDestination>(this TSource entity)
            where TSource : BaseEntity
            where TDestination : BaseSaturn72EntityModel
        {
            return Mapper.Map<TSource, TDestination>(entity);
        }

        public static TDestination ToEntity<TSource, TDestination>(this TSource model)
            where TSource : BaseSaturn72EntityModel
            where TDestination : BaseEntity
        {
            return Mapper.Map<TSource, TDestination>(model);
        }
    }
}