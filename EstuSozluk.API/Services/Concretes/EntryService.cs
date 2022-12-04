using System;
using System.Collections.Generic;
using System.Linq;
using EstuSozluk.API.Models;
using EstuSozluk.API.Models.Dtos;
using EstuSozluk.API.Models.Mappers;
using EstuSozluk.API.Repositories;
using EstuSozluk.API.Services.Abstracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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



      

        public object GetFirstEntryOfTitle()

        {

            return _estuSozlukContext.Entries
                .Include(e=> e.LikedEntries)

                .Include(e => e.User)

                .Select(e => e)
                .ToList()
                .GroupBy(q => q.titlename)
                .ToDictionary(e=> e.Key, e => 

                    e.Select(q => new { titleData = LandingMapper.MapFrom(q), likeCount = q.LikedEntries.Count})
                        .OrderByDescending(asd=>asd.likeCount)
                        .First()
                ).ToList();
        }

        public List<Entry> GetEntryByUser(int userId)
        {

            return _estuSozlukContext.Entries.Where(e => e.userid == userId).ToList();

               

        }
    }
}