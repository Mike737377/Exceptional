using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptional.Infrastructure
{
    public static class Mapper
    {
        static Mapper()
        {
        }

        public static MapTo<TSource> Map<TSource>(TSource source)
        {
            return new MapTo<TSource>(source);
        }

        public static MapCollectionTo<TSource> Map<TSource>(IEnumerable<TSource> source)
        {
            return new MapCollectionTo<TSource>(source);
        }
    }

    public class MapTo<TSource>
    {
        private readonly TSource source;

        public MapTo(TSource source)
        {
            this.source = source;
        }

        public TDestination To<TDestination>()
        {
            throw new NotImplementedException();
        }

        public TDestination To<TDestination>(TDestination destination)
        {
            throw new NotImplementedException();
        }
    }

    public class MapCollectionTo<TSource>
    {
        private readonly IEnumerable<TSource> source;

        public MapCollectionTo(IEnumerable<TSource> source)
        {
            this.source = source;
        }

        public IEnumerable<TDestination> To<TDestination>()
        {
            throw new NotImplementedException();
        }
    }
}