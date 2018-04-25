using System;
using Epic.NewParam;

namespace Epic.NewParam
{
    public interface IWebParamValidator
    {
        WebParamValidatorNodeType NodeType { get; }
        WebParamState State { get; }
        string Message { get; }
    }
}
