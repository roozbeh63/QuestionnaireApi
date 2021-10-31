using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuestionnaireApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuestionnaireApi.Services
{
    public abstract class BaseService<TEntity> where TEntity : BaseModel
    {
        protected QAContext context;

        protected BaseService(QAContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<TEntity>> GetAsync()
        {
            /** Set<TEntity> provides you an access to entity DbSet */
            return await context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetAsync(long id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> SaveAsync(TEntity model)
        {
            if (model.ID == 0)
            {
                context.Set<TEntity>().Add(model);
            }
            else
            {
                TEntity entity = await this.GetAsync(model.ID);
                Mapper.Map(model, entity);
            }

            await context.SaveChangesAsync(false);

            return model;
        }

        public async Task<TEntity> DeleteAsync(long id)
        {
            TEntity entity = await this.GetAsync(id);
            context.Set<TEntity>().Remove(entity);
            await context.SaveChangesAsync();
            return entity;
        }
    }
}