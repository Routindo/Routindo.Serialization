using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Routindo.Plugins.Serialization.Tests.Mock
{
    public class FakeJsonObject
    {
        public string StringProperty { get; set; }

        public int IntegerProperty { get; set; }

        public FakeChildJsonObject ChildJsonObject { get; set; }
    }

    public class FakeChildJsonObject
    {
        public string ChildStringProperty { get; set; }

        public int ChildIntegerProperty { get; set; }
    }
}
