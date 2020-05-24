﻿using NUnit.Framework;
using Tests.Mock;
using TypingBook.Services;

namespace Tests.Services
{
    class BookPagesHandlerTest
    {
        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        //testing private method => ReplaceSelectedCharToEmpty()
        public void ReplaceSelectedCharToEmpty()
        {
            var bookContent = "Raz, dwa; trzy";

            var handler = new BookPagesHandler(bookContent);
            var result = handler.Execute();

            Assert.AreEqual("[\"Raz dwa trzy\"]", result);

        }

        [Test]
        //testing private method => RemoveWhitespacesTabsNewLinesDoublespaces
        public void RemoveDoubleSpaces()
        {
            var bookContent = "Raz  dwa    trzy";

            var handler = new BookPagesHandler(bookContent);
            var result = handler.Execute();

            Assert.AreEqual("[\"Raz dwa trzy\"]", result);
        }

        [Test]
        public void RemoveSpacesFromStartAndEndForEachPage()
        {
            var bookContent = " Raz dwa trzy   ";

            var handler = new BookPagesHandler(bookContent);
            var result = handler.Execute();

            Assert.AreEqual("[\"Raz dwa trzy\"]", result);
        }

        [Test]
        public void TestSpecialCharacters()
        {
            var bookContent = "Pięć Sześć";

            var handler = new BookPagesHandler(bookContent);
            var result = handler.Execute();

            Assert.AreEqual("[\"Pięć Sześć\"]", result);
        }
               
        [Test]
        [Ignore("Ignore a test - only for debbuging")]
        public void TestingPagesCount()
        {
            var bookContent = MockEntity.BookContentBeforeModifying();

            var handler = new BookPagesHandler(bookContent);
            var result = handler.Execute();

            Assert.AreEqual("[\"Pięć Sześć\"]", result);
        }
    }
}
