using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstuSozluk.API.Models;
using EstuSozluk.API.Models.Dtos;

namespace EstuSozluk.API.Services.Abstracts
{
    public interface IEntryService
    {
        public Entry AddEntry(EntryDto entry);
        public Entry GetEntryById(int EntryId);
        public List<Entry> GetAllEntries();

        public List<Entry> GetEntryByUser(int userId);

        public List<string> GetTitles();

        public object GetFirstEntryOfTitle();

        public List<Entry> GetEntryByTitleName(string title);




    }
}