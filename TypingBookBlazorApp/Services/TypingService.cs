using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using TypingBookBlazorApp.Data.Repositories;
using TypingBookBlazorApp.ViewModels;
using TypingBookLibrary.Typing;


namespace TypingBookBlazorApp.Services
{
    public class TypingService
    {
        readonly BookRepository _bookRepository;

        public TypingService(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<TypingViewModel> GetAsync(string userId, int? bookId = null)
        {
            if (string.IsNullOrEmpty(userId))
            {
                if (!bookId.HasValue)
                {
                    return await GetIntroductionModel();
                }
                else
                {
                    var bookEntity = await _bookRepository.GetPublicByIDAsync(bookId.Value);
                    return new TypingViewModel()
                    {
                        BookId = bookEntity.Id,
                        BookAuthors = bookEntity.Authors,
                        BookTitle = bookEntity.Title,
                        CurrentBookPage = 0,
                        BookPages = BookPageSerializer.GetBookPages(bookEntity.Content)
                    };
                }
            }
            
            else if (true)
            {
                throw new NotImplementedException();
            }
        }

        public async void SaveTypingProgress()
        {
            throw new NotImplementedException();
        }

        async Task<TypingViewModel> GetIntroductionModel()
        {
            var dataFromDB = "[\"In the summer of 1996 rains flooded the Amazon rendering it virtually impenetrable. Bridges were swept away and amid vast stretches of mud small holes appeared where cobras and armadillos had buried themselves.\",\"Rivers sank by thirty feet bogs became meadows islands turned into hills. Finally after months of waiting a team of Brazilian adventurers and scientists headed into the jungle determined to solve what has been described as “the greatest exploration mystery of the twentieth century.\",\"” The group was searching for signs of Colonel Percy Harrison Fawcett a British explorer who in 1925 had disappeared in the forest along with his son and another companion.The expedition expected to find little more than bones—yet even discovering those would have been a revelation.\",\"When he vanished Fawcett and his party had been trying to uncover a lost civilization hidden in the Amazon which Fawcett had named simply the City of Z.In the next seven decades scores of explorers had tried and failed to retrace Fawcett’s path.\"]";

            return await Task.FromResult(new TypingViewModel()
            {
                BookId = 0,
                BookAuthors = "TypingBookAuthor",
                BookTitle = "Introduction",
                CurrentBookPage = 0,
                BookPages = BookPageSerializer.GetBookPages(dataFromDB)
            });
        }
    }
}
