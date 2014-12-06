using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Dynamitey;
using Dynamitey.DynamicObjects;
namespace Funky
{
    public class LateTypeTests
    {
        private static readonly dynamic LateConvert = new LateType(typeof(Convert));
        public static bool IsDBNull(object value)
        {
            return LateConvert.IsDBNull(value);
        }
        [Fact]
        public void can_use_is_db_null_and_it_gives_same_result_as_static_method()
        {
            Assert.True(IsDBNull(DBNull.Value));
            Assert.False(IsDBNull(null));
            Assert.True(Convert.IsDBNull(DBNull.Value));
            Assert.False(Convert.IsDBNull(null));
        }
    }
}
