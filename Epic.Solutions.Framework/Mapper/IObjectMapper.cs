using System;
namespace Epic.Mapper
{
    public interface IObjectMapper<Source, Dest>
    {
        Dest Convert(Source value);
    }
}
