using EstuSozluk.API.Models;
using EstuSozluk.API.Models.Dtos;
using System.Collections.Generic;

namespace EstuSozluk.API.Services.Abstracts
{
    public interface IEntryService
    {
        public Entry AddEntry(EntryDto entry);
        public Entry GetEntryById(int EntryId);
        public List<Entry> GetAllEntries();

        public List<Entry> GetEntryByUser(int userId);

        public object GetLikedEntryByUser(int userid);

        public object GetDislikedEntryByUser(int userid);

        public LikedEntries LikeEntry(LikedEntriesDto likedEntriesDto);

        public DislikedEntries DislikeEntry(DislikedEntriesDto dislikedEntriesDto);

        public Entry DeleteEntry(int entryid);

    }
}