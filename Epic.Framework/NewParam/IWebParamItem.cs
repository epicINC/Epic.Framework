using System;
using System.Collections.Generic;

namespace Epic.NewParam
{
    public interface IWebParamItem
    {
        WebParamState State { get; }
        string Name { get; }
        string Original { get; }
        object Value { get; }

        bool HasError { get; }
        List<WebParamValidResult> Errors { get; }

    }
}
