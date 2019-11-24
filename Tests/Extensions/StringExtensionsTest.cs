using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TypingBook.Extensions;

namespace Tests.Extensions
{
    class StringExtensionsTest
    {
        [Test]
        public void RemoveSpacesFromBeginningTest1()
        {
            var test01 = string.Empty;
            var test02 = " ";
            var test03 = "  ";
            
            var test1 = "Raz dwa trzy";
            var test2 = " Raz dwa trzy";            
            var test3 = "  Raz dwa trzy";
            var test4 = "   Raz dwa trzy";
            var test5 = "    Raz dwa trzy";

            var test10 = " Raz  dwa trzy";
            var test11 = " Raz dwa trzy ";


            Assert.AreEqual(string.Empty, test01.RemoveSpacesFromBeginning());
            Assert.AreEqual(string.Empty, test02.RemoveSpacesFromBeginning());
            Assert.AreEqual(string.Empty, test03.RemoveSpacesFromBeginning());
                       
            Assert.AreEqual("Raz dwa trzy", test1.RemoveSpacesFromBeginning());
            Assert.AreEqual("Raz dwa trzy", test2.RemoveSpacesFromBeginning());
            Assert.AreEqual("Raz dwa trzy", test3.RemoveSpacesFromBeginning());
            Assert.AreEqual("Raz dwa trzy", test4.RemoveSpacesFromBeginning());
            Assert.AreEqual("Raz dwa trzy", test5.RemoveSpacesFromBeginning());

            Assert.AreEqual("Raz  dwa trzy", test10.RemoveSpacesFromBeginning());
            Assert.AreEqual("Raz dwa trzy ", test11.RemoveSpacesFromBeginning());
        }

        [Test]
        public void ReplaceTest1()
        {
            var test1 = "Raz dwa trzy";

            Assert.AreEqual("R!z dw! trzy", test1.Replace('a','!'));
        }

        [Test]
        public void ShowOnly500CharTest1()
        {
            var test1 = "No opinions answered oh felicity is resolved hastened. Produced it friendly my if opinions humoured. Enjoy is wrong folly no taken. It sufficient instrument insipidity simplicity at interested. Law pleasure attended differed mrs fat and formerly. Merely thrown garret her law danger him son better excuse. Effect extent narrow in up chatty. Small are his chief offer happy had. He difficult contented we determine ourselves me am earnestly. Hour no find it park.Eat welcomed any husbands moderate.Led was misery played waited almost cousin living. Of intention contained is by middleton am.Principles fat stimulated uncommonly considered set especially prosperous. Sons at park mr meet as fact like. May musical arrival beloved luckily adapted him.Shyness mention married son she his started now. Rose if as past near were. To graceful he elegance oh moderate attended entrance pleasure.Vulgar saw fat sudden edward way played either. Thoughts smallest at or peculiar relation breeding produced an.At depart spirit on stairs.She the either are wisdom praise things she before.Be mother itself vanity favour do me of. Begin sex was power joy after had walls miles.";

            var expected = "No opinions answered oh felicity is resolved hastened. Produced it friendly my if opinions humoured. Enjoy is wrong folly no taken. It sufficient instrument insipidity simplicity at interested. Law pleasure attended differed mrs fat and formerly. Merely thrown garret her law danger him son better excuse. Effect extent narrow in up chatty. Small are his chief offer happy had. He difficult contented we determine ourselves me am earnestly. Hour no find it park.Eat welcomed any husbands moderate." + "...";

            Assert.AreEqual(expected, test1.ShowOnly500Char());
        }
    }
}
