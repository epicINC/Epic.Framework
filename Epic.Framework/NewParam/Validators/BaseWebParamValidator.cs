using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.NewParam
{



    public abstract class BaseWebParamValidator<T, K> : IWebParamValidator
    {
        protected BaseWebParamValidator(WebParamValidatorNodeType nodeType, string message)
        {
            this.NodeType = nodeType;
            this.Message = message;
        }

        public WebParamValidatorNodeType NodeType
        {
            get;
            protected set;
        }

        public WebParamState State
        {
            get;
            internal set;
        }

        public string Message
        {
            get;
            protected set;
        }

    }
}
