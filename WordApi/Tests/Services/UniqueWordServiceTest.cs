using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Moq;
using UniqueWord.Api.Models;
using UniqueWord.Api.Repositories.Interfaces;
using UniqueWord.Api.Services;
using Xunit;

namespace Tests.Services
{
    public class UniqueWordServiceTest
    {
        private readonly Mock<ILogger<UniqueWordService>> _iLoggerMock;
        private readonly Mock<IWatchlistRepository> _iWatchRepositoryMock;
        private readonly Mock<IUniqueWordEntryRepository> _iUniqueWordEntryRepositoryMock;
        private readonly Mock<ITextEntryRepository> _iTextEntryRepositoryMock;
        private readonly UniqueWordService _service;

        public UniqueWordServiceTest()
        {
            _iLoggerMock = new Mock<ILogger<UniqueWordService>>();
            _iWatchRepositoryMock = new Mock<IWatchlistRepository>();
            _iUniqueWordEntryRepositoryMock = new Mock<IUniqueWordEntryRepository>();
            _iTextEntryRepositoryMock = new Mock<ITextEntryRepository>();

            _service = new UniqueWordService(
                _iLoggerMock.Object,
                _iWatchRepositoryMock.Object,
                _iTextEntryRepositoryMock.Object,
                _iUniqueWordEntryRepositoryMock.Object);

        }

        [Fact]
        public void TestDistinctWords()
        {
            //Arrange
            const string text = "a horse and a dog";

            //Act
            var words = _service.GetDistinctWords(text);

            //Assert
            Assert.NotNull(words);
            Assert.Equal(4, words.Count);
        }

        [Fact]
        public void TestEmptyDistinctWords()
        {
            //Arrange
            const string text = "";

            //Act
            var words = _service.GetDistinctWords(text);

            //Assert
            Assert.NotNull(words);
            Assert.Equal(0, words.Count);
        }

        [Fact]
        public void TestGetWatchedWords()
        {
            //Arrange
            var distinctWords = new List<string>
            {
                "one",
                "man",
                "had",
                "a",
                "hat"
            };

            _iWatchRepositoryMock.Setup(r => r.GetWords()).Returns(() => new List<string>
            {
                "one",
                "two",
                "three"
            });

            //Act
            var watchedWords = _service.GetWatchedWords(distinctWords);

            //Assert
            Assert.NotNull(watchedWords);
            Assert.Contains("one", watchedWords);
        }

        [Fact]
        public void TestSaveResults()
        {
            //Arrange
            var distinctWords = new List<string>
            {
                "one",
                "man",
                "had",
                "a",
                "hat"
            };
            var textEntry = new TextEntry();

            _iTextEntryRepositoryMock.Setup(r => r.SaveTextEntry(It.IsAny<int>())).Returns(textEntry);
            _iUniqueWordEntryRepositoryMock.Setup(r => r.BatchSave(It.IsAny<TextEntry>(), It.IsAny<List<string>>())).Returns(true);

            //Act
            _service.SaveResults(distinctWords);


            //Assert
            _iTextEntryRepositoryMock.Verify(m => m.SaveTextEntry(distinctWords.Count), Times.Once);
            _iUniqueWordEntryRepositoryMock.Verify(m => m.BatchSave(textEntry, distinctWords), Times.Once);
        }

        [Fact]
        public void TestSaveResultsEmpty()
        {
            //Arrange
            var distinctWords = new List<string>();
            var textEntry = new TextEntry();

            _iTextEntryRepositoryMock.Setup(r => r.SaveTextEntry(It.IsAny<int>())).Returns(textEntry);
            _iUniqueWordEntryRepositoryMock.Setup(r => r.BatchSave(It.IsAny<TextEntry>(), It.IsAny<List<string>>())).Returns(true);

            //Act
            _service.SaveResults(distinctWords);


            //Assert
            _iTextEntryRepositoryMock.Verify(m => m.SaveTextEntry(distinctWords.Count), Times.Once);
            _iUniqueWordEntryRepositoryMock.Verify(m => m.BatchSave(textEntry, distinctWords), Times.Once);
        }
    }
}
