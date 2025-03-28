using ASPNetExapp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetExapp.Services;

public class NewspaperService
{
    private readonly DBContext _context;
    
    public NewspaperService(DBContext context)
    {
        _context = context;
    }

    public async Task<List<Newspaper>> GetAllNewspapers()
    {
        return await _context.Newspapers.ToListAsync();
    }
    public async Task<PaginatedResult<Newspaper>> GetNewspapers(string? query, int page, int pageSize)
    {
        var newspapers = _context.Newspapers.AsQueryable();

        if (!string.IsNullOrEmpty(query))
        {
            newspapers = newspapers.Where(n => n.Thema.Contains(query) || n.Text.Contains(query));
        }

        var totalRecords = await newspapers.CountAsync();
        var paginatedData = await newspapers
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedResult<Newspaper> { Data = paginatedData, TotalRecords = totalRecords };
    }

    public async Task<Newspaper?> GetNewsById(int id)
    {
        return await _context.Newspapers.FindAsync(id);
    }

    public async Task<Newspaper> CreateNewspaper(Newspaper newNewspaper)
    {
        if (newNewspaper.Date.Offset == TimeSpan.Zero) 
        {
            newNewspaper.Date = newNewspaper.Date.ToUniversalTime();   
        }
        _context.Newspapers.Add(newNewspaper);
        await _context.SaveChangesAsync();
        return newNewspaper;
    }

    public async Task<bool> UpdateNewspaper(int id, Newspaper updatedNewspaper)
    {
        var existingNewspaper = await _context.Newspapers.FindAsync(id);
        if (existingNewspaper == null) return false;

        existingNewspaper.Thema = updatedNewspaper.Thema;
        existingNewspaper.Text = updatedNewspaper.Text;
        existingNewspaper.Date = updatedNewspaper.Date;
        existingNewspaper.Names = updatedNewspaper.Names;
        existingNewspaper.Adreses = updatedNewspaper.Adreses;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteNewspaper(int id)
    {
        var newspaper = await _context.Newspapers.FindAsync(id);
        if (newspaper == null) return false;

        _context.Newspapers.Remove(newspaper);
        await _context.SaveChangesAsync();
        return true;
    }
}
