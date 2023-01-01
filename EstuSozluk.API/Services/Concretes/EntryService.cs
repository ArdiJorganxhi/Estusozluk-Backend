using EstuSozluk.API.Models;
using EstuSozluk.API.Models.Dtos;
using EstuSozluk.API.Models.Mappers;
using EstuSozluk.API.Repositories;
using EstuSozluk.API.Services.Abstracts;
using System.Collections.Generic;
using System.Linq;

namespace EstuSozluk.API.Services.Concretes
{
    public class EntryService : IEntryService
    {
        EstuSozlukContext _estuSozlukContext;
        ILoginService _loginService;

        public EntryService(EstuSozlukContext estuSozlukContext, ILoginService loginService)
        {
            _estuSozlukContext = estuSozlukContext;
            _loginService = loginService;
        }

        public Entry AddEntry(EntryDto entry)
        {
            Entry entryToSave = EntryMapper.GetEntryFromEntryDto(entry);
            _estuSozlukContext.Entries.Add(entryToSave);
            _estuSozlukContext.SaveChanges();
            return entryToSave;
        }

        public Entry GetEntryById(int EntryId)
        {

            return _estuSozlukContext.Entries.Where(e => e.entryid == EntryId).First();

        }

        public List<Entry> GetAllEntries()
        {
            return _estuSozlukContext.Entries.Select(e => e).ToList();
        }

        public List<Entry> GetEntryByUser(int userId)
        {
            return _estuSozlukContext.Entries.Where(e => e.userid == userId).ToList();
        }

        public object GetLikedEntryByUser(int userid)
        {
            return _estuSozlukContext.LikedEntries.Where(e => e.userid == userid)
                .Select(e => new { e.entry.titlename, e.entry.content, e.entry.writedate }).ToList();
        }

        public object GetDislikedEntryByUser(int userid)
        {
            return _estuSozlukContext.DislikedEntries.Where(e => e.userid == userid)
                .Select(e => new { e.entry.titlename, e.entry.content, e.entry.writedate }).ToList();

        }

        public LikedEntries LikeEntry(LikedEntriesDto likedEntriesDto)
        {
            LikedEntries likedEntry = LikedEntriesMapper.GetLikedEntriesFromDto(likedEntriesDto);

            var checkIfDislikeExists = _estuSozlukContext.DislikedEntries.Where(e =>
                e.dislikedentryid == likedEntry.likedentryid & e.userid == likedEntry.userid).FirstOrDefault();

            if (checkIfDislikeExists != null)
            {
                _estuSozlukContext.DislikedEntries.Remove(checkIfDislikeExists);
            }

            _estuSozlukContext.LikedEntries.Add(likedEntry);
            _estuSozlukContext.SaveChanges();

            return likedEntry;
        }

        public DislikedEntries DislikeEntry(DislikedEntriesDto dislikedEntriesDto)
        {
            DislikedEntries dislikedEntry = DislikedEntriesMapper.GetDislikedEntriesFromDto(dislikedEntriesDto);


            var checkIfLikeExists = _estuSozlukContext.LikedEntries.Where(e => e.likedentryid == dislikedEntry.dislikedentryid & e.userid == dislikedEntry.userid).FirstOrDefault();

            if (checkIfLikeExists != null)
            {
                _estuSozlukContext.LikedEntries.Remove(checkIfLikeExists);
            }
            _estuSozlukContext.DislikedEntries.Add(dislikedEntry);
            _estuSozlukContext.SaveChanges();

            return dislikedEntry;
        }

        public Entry DeleteEntry(int entryid)
        {
            var entry = _estuSozlukContext.Entries.Where(e => e.entryid == entryid).FirstOrDefault();
            if (entry != null)
            {
                _estuSozlukContext.Entries.Remove(entry);
                _estuSozlukContext.SaveChanges();
                return entry;
            }

            else
            {
                return null;

            }
            

            
        }
    }
}