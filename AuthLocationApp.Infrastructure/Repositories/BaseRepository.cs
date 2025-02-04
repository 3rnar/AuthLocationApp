using Microsoft.EntityFrameworkCore;
using AuthLocationApp.Application.Interfaces.Repositories;
using AuthLocationApp.Infrastructure.Data;
using AuthLocationApp.Infrastructure.DbModels;
using AuthLocationApp.Infrastructure.Mappers;
using Serilog;

namespace AuthLocationApp.Infrastructure.Repositories
{
   public abstract class BaseRepository<TDomain, TEntity> : IBaseRepository<TDomain>
       where TDomain : class
       where TEntity : BaseDbModel
   {
      protected readonly ApplicationDbContext _context;
      protected readonly IMapper<TEntity, TDomain> _mapper;
      protected readonly DbSet<TEntity> _dbSet;
      protected readonly ILogger _logger;

      public BaseRepository(ApplicationDbContext context, IMapper<TEntity, TDomain> mapper)
      {
         _context = context;
         _mapper = mapper;
         _dbSet = _context.Set<TEntity>();
         _logger = Log.ForContext(GetType());
      }

      public async Task<IReadOnlyCollection<TDomain>> GetAllAsync(bool asNoTracking = false, CancellationToken cancellationToken = default)
      {
         _logger.Information("Fetching all records from {EntityType}. AsNoTracking: {AsNoTracking}", typeof(TEntity).Name, asNoTracking);

         try
         {
            IQueryable<TEntity> query = _dbSet;

            if (asNoTracking)
            {
               query = query.AsNoTracking();
            }

            var dbModels = await query.ToListAsync(cancellationToken);
            var result = dbModels.Select(_mapper.ToDomain).ToList().AsReadOnly();

            _logger.Information("{Count} records retrieved from {EntityType}", result.Count, typeof(TEntity).Name);
            return result;
         }
         catch (Exception ex)
         {
            _logger.Error(ex, "Error fetching records from {EntityType}", typeof(TEntity).Name);
            throw;
         }
      }

      public async Task<TDomain?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
      {
         _logger.Information("Fetching record with ID {Id} from {EntityType}", id, typeof(TEntity).Name);

         try
         {
            var dbModel = await _dbSet.FindAsync([id], cancellationToken);

            if (dbModel is null)
            {
               _logger.Warning("Record with ID {Id} not found in {EntityType}", id, typeof(TEntity).Name);
               return null;
            }

            _logger.Information("Record with ID {Id} retrieved from {EntityType}", id, typeof(TEntity).Name);
            return _mapper.ToDomain(dbModel);
         }
         catch (Exception ex)
         {
            _logger.Error(ex, "Error fetching record with ID {Id} from {EntityType}", id, typeof(TEntity).Name);
            throw;
         }
      }

      public async Task AddAsync(TDomain domain, CancellationToken cancellationToken = default)
      {
         _logger.Information("Adding a new record to {EntityType}", typeof(TEntity).Name);

         try
         {
            var dbModel = _mapper.ToDbModel(domain);
            await _dbSet.AddAsync(dbModel, cancellationToken);
            _logger.Information("Record successfully added to {EntityType}", typeof(TEntity).Name);
         }
         catch (Exception ex)
         {
            _logger.Error(ex, "Error adding record to {EntityType}", typeof(TEntity).Name);
            throw;
         }
      }

      public void Update(TDomain domain)
      {
         _logger.Information("Updating record in {EntityType}", typeof(TEntity).Name);

         try
         {
            var dbModel = _mapper.ToDbModel(domain);
            _dbSet.Update(dbModel);
            _logger.Information("Record successfully updated in {EntityType}", typeof(TEntity).Name);
         }
         catch (Exception ex)
         {
            _logger.Error(ex, "Error updating record in {EntityType}", typeof(TEntity).Name);
            throw;
         }
      }

      public void Delete(TDomain domain)
      {
         _logger.Information("Deleting record from {EntityType}", typeof(TEntity).Name);

         try
         {
            var dbModel = _mapper.ToDbModel(domain);
            _dbSet.Remove(dbModel);
            _logger.Information("Record successfully deleted from {EntityType}", typeof(TEntity).Name);
         }
         catch (Exception ex)
         {
            _logger.Error(ex, "Error deleting record from {EntityType}", typeof(TEntity).Name);
            throw;
         }
      }

      public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
      {
         _logger.Information("Saving changes to database...");

         try
         {
            await _context.SaveChangesAsync(cancellationToken);
            _logger.Information("Database changes saved successfully.");
         }
         catch (DbUpdateException ex)
         {
            _logger.Error(ex, "Failed to save changes in the database.");
            throw;
         }
         catch (Exception ex)
         {
            _logger.Error(ex, "Unexpected error occurred while saving changes to the database.");
            throw;
         }
      }
   }
}
