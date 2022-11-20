using System.Collections.Generic;
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
        
        

    }
}