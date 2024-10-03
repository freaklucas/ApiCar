using ApiCar.Data;
using ApiCar.Models;
using ApiCar.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace ApiCar.Repositories.Impl;

public class TestDriveRepository(Context context) :ITestDriveRepository
{
    private readonly Context _context = context;
    
    public async Task<IEnumerable<TestDrive>> GetAll()
    {
        var testDrives = await _context
            .TestDrives
            .Include(c => c.Car)
            .Include(c => c.User)
            .Include(c => c.Dealership)
            .AsTracking()
            .ToListAsync() ?? throw new InvalidOperationException("Nenhum test drive." );

        return testDrives;
    }

    public async Task<TestDrive> GetById(int id)
    {
        var testDrive = await _context
            .TestDrives
            .Include(c => c.Car)
            .Include(c => c.User)
            .Include(c => c.Dealership)
            .AsTracking()
            .FirstOrDefaultAsync(p => p.Id == id) ?? throw new KeyNotFoundException("Nenhum test drive.");

        return testDrive;
    }

    public async Task Create(TestDrive testDrive)
    {
       if(testDrive == null) throw new ArgumentNullException(nameof(testDrive), "Erro ao criar.");

       _context.TestDrives.Add(testDrive);

       await _context.SaveChangesAsync();
    }

    public async Task Update(TestDrive testDrive, int id)
    {
        if(testDrive == null) throw new ArgumentNullException(nameof(testDrive), "Erro ao atualizar.");

        var existingTestDrive = await _context
            .TestDrives
            .FirstOrDefaultAsync(p => p.Id == id) 
                    ?? throw new ArgumentNullException(nameof(testDrive), "Teste drive nulo.");
        
        existingTestDrive.Notes = testDrive.Notes;
        existingTestDrive.ScheduledDate = testDrive.ScheduledDate;
        
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var testDrive = await _context
            .TestDrives
            .FindAsync(id);
        
        if(testDrive == null) throw new ArgumentNullException(nameof(testDrive), "Teste drive nulo.");

        _context.TestDrives.Remove(testDrive);
        await _context.SaveChangesAsync();
    }
}