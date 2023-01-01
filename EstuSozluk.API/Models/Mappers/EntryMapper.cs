using System;
using System.Collections.Generic;
using EstuSozluk.API.Models.Dtos;

namespace EstuSozluk.API.Models.Mappers
{
    public class EntryMapper
    {
        public static Entry GetEntryFromEntryDto(EntryDto entryDto)
        {
            Entry entry = new Entry();

            
            entry.userid = entryDto.userid;
            entry.titlename = entryDto.titlename.ToLower();
            entry.content = entryDto.content;
            entry.writedate = DateTime.Now;
            entry.editdate = DateTime.Now;

            
            

            return entry;
        }
    }
}