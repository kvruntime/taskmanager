using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using TaskManager.Shared;

namespace TaskManager.Server.Data
{
    public class DbStore : IDbStore
    {
        private readonly AppDbContext _context;

        public DbStore(AppDbContext context)
        {
            _context = context;
        }
        public async Task Add(TaskItem item)
        {
            await _context.TaskItems.AddAsync(item);
        }

        public void Delete(TaskItem item)
        {
            _context.Remove(item);
        }

        public async Task<TaskItem>? Get(string Id)
        {
            var item = await _context.TaskItems.FirstOrDefaultAsync(item => item.Id == Id);
            return item;
        }

        public async Task<IList<TaskItem>> GetAll()
        {
            return await _context.TaskItems.OrderBy(item => item.TaskName).ToListAsync();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async void Update(string Id, TaskItem item)
        {
            if (await _context.TaskItems.FirstOrDefaultAsync(item => item.Id == Id) is not null)
                _context.Update(item);
        }
    }
}