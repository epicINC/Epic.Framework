using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;

namespace Epic.Data.V2
{
    internal sealed class DynamicObjectParameterDictionary : DynamicObject
    {
        readonly Func<ObjectParameterDictionary> parameters;

        public DynamicObjectParameterDictionary()
        {
            this.parameters = () => new ObjectParameterDictionary();
        }

        public DynamicObjectParameterDictionary(Func<ObjectParameterDictionary> parameters)
        {
            this.parameters = parameters;
        }

        internal ObjectParameterDictionary Parameters
        {
            get
            {
                var result = this.parameters();
                if (result == null) Error.ArgumentNull("Func<ObjectParameterDictionary> parameters");
                return result;
            }
        }

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return this.Parameters.Keys;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = this.Parameters[binder.Name];
            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            this.Parameters[binder.Name] = new ObjectParameter(binder.Name, binder.Name, value);
            return true;
        }

    }
}
