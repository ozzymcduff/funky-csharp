using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImpromptuInterface;
using ImpromptuInterface.Dynamic;
using System.Dynamic;
using Xunit;
namespace Funky
{
    public class ImpromptuInterface
    {
        public interface IMyInterface
        {

            string Prop1 { get; }

            long Prop2 { get; }

            Guid Prop3 { get; }

            bool Meth1(int x);
        }
        [Fact]
        public void can_get_an_anonymous_class_to_act_as_an_interface()
        {
            //Anonymous Class
            var anon = new
            {
                Prop1 = "Test",
                Prop2 = 42L,
                Prop3 = Guid.NewGuid(),
                Meth1 = Return<bool>.Arguments<int>(it => it > 5)
            };

            var myInterface = anon.ActLike<IMyInterface>();
            Assert.Equal("Test", myInterface.Prop1);
            Assert.Equal(true, myInterface.Meth1(10));
        }
        class MyClass
        {
            public string Prop1 { get; set; }

            public long Prop2 { get; set; }

            public Guid Prop3 { get; set; }

            public bool Meth1(int x) { return x > 5; }
        }

        [Fact]
        public void can_get_a_class_to_act_as_an_interface()
        {
            //Anonymous Class
            var anon = new MyClass
            {
                Prop1 = "Test",
                Prop2 = 42L,
                Prop3 = Guid.NewGuid()
            };

            var myInterface = anon.ActLike<IMyInterface>();
            Assert.Equal("Test", myInterface.Prop1);
            Assert.Equal(true, myInterface.Meth1(10));
        }
        [Fact]
        public void can_get_an_expando_object_to_act_as_an_interface()
        {
            dynamic expando = Build<ExpandoObject>.NewObject(
                Prop1: "Test",
                Prop2: 42L,
                Prop3: Guid.NewGuid(),
                Meth1: Return<bool>.Arguments<int>(it => it > 5)
            );

            IMyInterface myInterface = Impromptu.ActLike(expando);
            Assert.Equal("Test", myInterface.Prop1);
            Assert.Equal(true, myInterface.Meth1(10));
        }
    }
}
